using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.GateOut.Console.DAL
{

    public class TruckInDepoDAL
    {
        public const string DEFAULT_COLUMN = "truckindepoid, nomobil, dtmin, dtmout, remark, refno, muatan, tipe, angkutan, note";
        public const string DEFAULT_TABLE = "truckindepo";

        /// <summary>
        /// Insert Truck In Depo
        /// </summary>
        /// <param name="truckInDepo"></param>
        /// <returns></returns>
        public int InsertTruckInDepo(TruckInDepo truckInDepo)
        {
            int affectedRow = 0;
            try
            {
                using (NpgsqlConnection npgsqlConnection = AppConfig.GetUserConnection())
                {
                    if (npgsqlConnection.State == ConnectionState.Closed)
                    {
                        npgsqlConnection.Open();
                    }
                    string query = string.Format("INSERT INTO {1}({0}) " +
                                                " VALUES(nextval('truckindepo_truckindepoid_seq'), @NoMobil, @DtmIn, @DtmOut, @Remark, @RefNo, " +
                                                "       @Muatan, @Tipe, @Angkutan, @Note)",
                                                DEFAULT_COLUMN, DEFAULT_TABLE);
                    using (NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, npgsqlConnection))
                    {
                        npgsqlCommand.Parameters.AddWithValue("@NoMobil", truckInDepo.NoMobil);
                        npgsqlCommand.Parameters.AddWithValue("@DtmIn", truckInDepo.DtmIn);
                        if (truckInDepo.DtmOut.HasValue)
                        {
                            npgsqlCommand.Parameters.AddWithValue("@DtmOut", truckInDepo.DtmOut);
                        }
                        else
                        {
                            npgsqlCommand.Parameters.AddWithValue("@DtmOut", DBNull.Value);
                        }
                        npgsqlCommand.Parameters.AddWithValue("@Remark", truckInDepo.Remark);
                        npgsqlCommand.Parameters.AddWithValue("@RefNo", truckInDepo.RefNo);
                        npgsqlCommand.Parameters.AddWithValue("@Muatan", truckInDepo.Muatan);
                        npgsqlCommand.Parameters.AddWithValue("@Tipe", truckInDepo.Tipe);
                        npgsqlCommand.Parameters.AddWithValue("@Angkutan", truckInDepo.Angkutan);
                        npgsqlCommand.Parameters.AddWithValue("@Note", truckInDepo.Note);
                        affectedRow = npgsqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return affectedRow;
        }
        public TruckInDepo FillByNomobilNotOut(string nomobil)
        {
            TruckInDepo truckInDepo = new TruckInDepo();
            try
            {
                using (NpgsqlConnection npgsqlConnection = AppConfig.GetConnection())
                {
                    if (npgsqlConnection.State == ConnectionState.Closed)
                    {
                        npgsqlConnection.Open();
                    }
                    string query = "SELECT truckindepoid,nomobil,dtmin,dtmout,remark, CURRENT_TIMESTAMP AS serverdtm, refno, muatan, tipe, angkutan, note FROM truckindepo WHERE dtmout IS NULL AND (nomobil=@nomobil1 OR nomobil=@nomobil2) ORDER BY truckindepoid ";
                    using (NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, npgsqlConnection))
                    {
                        npgsqlCommand.Parameters.AddWithValue("@nomobil1", nomobil);

                        npgsqlCommand.Parameters.AddWithValue("@nomobil2", nomobil);
                        using (NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader())
                        {
                            if (npgsqlDataReader.Read())
                            {
                                MappingDataReaderToTruckInDepo(npgsqlDataReader, truckInDepo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return truckInDepo;


        }

        private void MappingDataReaderToTruckInDepo(NpgsqlDataReader npgsqlDataReader, TruckInDepo truckInDepo)
        {
            truckInDepo.TruckInDepoId = npgsqlDataReader.GetInt64(0);
            truckInDepo.NoMobil = npgsqlDataReader.GetString(1);
            truckInDepo.DtmIn = npgsqlDataReader.GetDateTime(2);
            truckInDepo.DtmOut = npgsqlDataReader.GetDateTime(3);
            truckInDepo.Remark = npgsqlDataReader.GetString(4);
            truckInDepo.ServerDTM = npgsqlDataReader.GetDateTime(5);
            if (truckInDepo.DtmOut.ToString().Length == 0)
            {
                truckInDepo.DurasiMenit = (int)truckInDepo.ServerDTM.Value.Minute;
            }
            else
            {
                truckInDepo.DurasiMenit = (int)truckInDepo.DtmOut.Value.Minute;
            }
            truckInDepo.RefNo = npgsqlDataReader.GetString(6);
            truckInDepo.Muatan = npgsqlDataReader.GetString(7);
            truckInDepo.Tipe = npgsqlDataReader.GetString(8);
            truckInDepo.Angkutan = npgsqlDataReader.GetString(9);
            truckInDepo.Note = npgsqlDataReader.GetString(10);
        }
    }
}
