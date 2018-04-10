using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Data;

namespace ITI.GateOut.Console.DAL
{
    public class CustomerDAL
    {
        public Customer FillByCustomerCode(string code)
        {
            Customer customer = new Customer();
            try
            {
                using (NpgsqlConnection npgsqlConnection = AppConfig.GetConnection())
                {
                    if (npgsqlConnection.State == ConnectionState.Closed)
                    {
                        npgsqlConnection.Open();
                    }
                    string query = "SELECT customerid,customercode,name,altname,address1,address2,attn,telpfax,textflag,ratepermanhour,currencycode,currencyname,boxadmin,flag_other,flag_isotank FROM customer WHERE customercode=@customercode ";
                    using (NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, npgsqlConnection))
                    {
                        npgsqlCommand.Parameters.AddWithValue("@customercode", code);
                        using (NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader())
                        {
                            if (npgsqlDataReader.Read())
                            {
                                MappingDataReaderToCustomer(npgsqlDataReader, customer);
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
            return customer;
        }

        private void MappingDataReaderToCustomer(NpgsqlDataReader npgsqlDataReader, Customer customer)
        {
            customer.CustomerID = npgsqlDataReader.GetInt32(npgsqlDataReader.GetOrdinal("customerid"));

            customer.CustomerCode = npgsqlDataReader.GetString(npgsqlDataReader.GetOrdinal("CustomerCode"));

            customer.Name = npgsqlDataReader.GetString(npgsqlDataReader.GetOrdinal("Name"));
        }
    }
}
