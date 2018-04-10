using AGY.Solution.Helper.Common;
using ITI.GateOut.Console.DAL;
using ReportPrinting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ITI.GateOut.Console.UI
{
    class Program
    {
        public static string _SecureGateLocName = "DefaultSGate";
        public static string _CaptureImageExeFile = "";
        public static string _LastProcessedInput = "";
        public static string _CaptureFile = "";
        public static int posnumber = 0;
        public static int _WarningPaperOut = 0;
        public static string _WarningPaperOutMailTo = "test1@intercon.co.id";
        public static string _WarningPaperOutMailCC = "test2@intercon.co.id";
        public static string _SecureGateTelnetAddress = string.Empty;
        public static string _SecureGateTelnetPort = string.Empty;
        private static void DoPrintEirOut(ContInOut cont)
        {
            EirOutPrint_ t_ = new EirOutPrint_(cont);
            ReportDocument document = new ReportDocument();
            string printerName = document.PrinterSettings.PrinterName;

            document.PrinterSettings.PrinterName = ConfigurationSettings.AppSettings["eiroutprint.printer1.name"];

            if (!document.PrinterSettings.IsValid)
            {
                document.PrinterSettings.PrinterName = printerName;
            }
            document.ReportMaker = t_;
            int num = 0;
            int num2 = 0;
            try
            {
                num = Convert.ToInt32(ConfigurationSettings.AppSettings["eiroutprint.left"]);
            }
            catch (Exception)
            {
            }
            try
            {
                num2 = Convert.ToInt32(ConfigurationSettings.AppSettings["eiroutprint.top"]);
            }
            catch (Exception)
            {
            }
            document.PrintController = new StandardPrintController();
            document.DefaultPageSettings.Margins.Bottom = 0;
            document.DefaultPageSettings.Margins.Left = num;
            document.DefaultPageSettings.Margins.Top = num2;
            document.DefaultPageSettings.Margins.Right = 0;
            try
            {
                System.Console.WriteLine("Printing starting....");
                document.Print();
                System.Console.WriteLine("Printing complete.");
            }
            catch (Exception exception)
            {
                System.Console.WriteLine("Printing error: " + exception.Message);
            }
        }


        static void Main(string[] args)
        {

            SecureGateLog log = new SecureGateLog();
            _SecureGateTelnetAddress = ConfigurationSettings.AppSettings["TelnetAddress"];
            _SecureGateTelnetPort = ConfigurationSettings.AppSettings["TelnetPort"];
            bool flag;
            bool flag2;
            Exception exception;
            bool flag3;
            _SecureGateLocName = ConfigurationSettings.AppSettings["sgatelocname"];
            _WarningPaperOut = Convert.ToInt32(ConfigurationSettings.AppSettings["warningpaperout"]);
            _WarningPaperOutMailTo = ConfigurationSettings.AppSettings["warningpaperoutmailto"];
            _WarningPaperOutMailCC = ConfigurationSettings.AppSettings["warningpaperoutmailcc"];
            _CaptureImageExeFile = ConfigurationSettings.AppSettings["sgatecaptureexefile"];
            _CaptureFile = ConfigurationSettings.AppSettings["capturefile"];
            if ((_SecureGateLocName == null) || (_SecureGateLocName == string.Empty))
            {
                _SecureGateLocName = "sgateout1";
            }

            System.Console.WriteLine("welcome to Secure Gate Out System " + Assembly.GetExecutingAssembly().GetName().Version.ToString() + ", the SCGOUT interactive terminal." + System.Console.Out.NewLine + System.Console.Out.NewLine + @"type: \c for copyright info" + System.Console.Out.NewLine + @"    : \h for help with SCGOUT command" + System.Console.Out.NewLine + @"    : \q to quit" + System.Console.Out.NewLine + System.Console.Out.NewLine + "warning: make sure barcode scanner & impact printer are connected and " + System.Console.Out.NewLine + "working properly. See SCGOUT manual page \"Installation and Setup\" for " + System.Console.Out.NewLine + "instruction.");
            string str = ConfigurationSettings.AppSettings["loginserver"];
            if (string.IsNullOrEmpty(str))
            {
                str = "localhost";
            }
            System.Console.WriteLine(System.Console.Out.NewLine + System.Console.Out.NewLine + "info: SCGOUT location: " + _SecureGateLocName + System.Console.Out.NewLine + "info: logging in..." + System.Console.Out.NewLine + "server: '" + str + "'");
            Random random = new Random();
            goto Label_04A2;
            Label_048B:
            if (flag)
            {
                goto Label_04A2;
            }
            Label_0498:
            flag3 = true;
            System.Console.Write("SCGOUT> ");
            string input = System.Console.ReadLine();
            flag = false;
            string str3 = input.ToLower();
            if (str3 != null)
            {
                if (str3 != @"\c")
                {
                    if (str3 == @"\q")
                    {
                        return;
                    }
                    if ((str3 == @"\h") || (str3 == @"\?"))
                    {
                    }
                }
                else
                {
                    flag = true;
                    goto Label_048B;
                }
            }
            System.Console.WriteLine(System.Console.Out.NewLine + @"type: \c to connect" + System.Console.Out.NewLine + @"    : \h for help with SCGOUT command" + System.Console.Out.NewLine + @"    : \q to quit" + System.Console.Out.NewLine);
            goto Label_048B;
            Label_04A2:
            flag3 = true;
            try
            {
                //AppPrincipal.LoginForService();
                System.Console.Write("info: logged in" + System.Console.Out.NewLine + "info: waiting for input..." + System.Console.Out.NewLine);
                input = string.Empty;
                flag2 = false;
                goto Label_07BE;
            }
            catch (Exception)
            {
                System.Console.WriteLine("error: unable to login");
                if (random.Next(2) > 0)
                {
                    System.Console.WriteLine("info: make sure the database server is online and configured correctly. " + System.Console.Out.NewLine + "See SCGOUT manual page \"Installation and Setup\" for details.");
                }
                else
                {
                    System.Console.WriteLine("info: make sure the Secure Gate Out is configured properly. " + System.Console.Out.NewLine + "See SCGOUT manual page \"Installation and Setup\" for instruction.");
                }
                System.Console.WriteLine(System.Console.Out.NewLine + @"type: \c to connect" + System.Console.Out.NewLine + @"    : \h for help with SCGOUT command" + System.Console.Out.NewLine + @"    : \q to quit" + System.Console.Out.NewLine);
            }
            input = string.Empty;
            flag = false;
            goto Label_0498;
            Label_07B0:
            if (flag2)
            {
                System.Console.WriteLine("info: logging out...");
                //AppPrincipal.LogOut();
                System.Console.WriteLine("info: logged out");
                return;
            }
            Label_07BE:
            flag3 = true;
            System.Console.Write("SCGOUT> ");
            input = System.Console.ReadLine();
            flag2 = false;
            switch (input.ToLower())
            {
                case @"\c":
                    System.Console.WriteLine(System.Console.Out.NewLine + "Secure Gate Out terminal" + System.Console.Out.NewLine + System.Console.Out.NewLine + "Copyright (c) 2018, Intercon International Terminal. All rights reserved." + System.Console.Out.NewLine + System.Console.Out.NewLine + "Portions Copyright (c) 2018, AGY Solutions." + System.Console.Out.NewLine);
                    goto Label_07B0;

                case @"\q":
                case "quit":
                case "exit":
                    flag2 = true;
                    goto Label_07B0;

                case @"\o":
                case "openup":
                case "open":
                    try
                    {
                        OpenGate("openup command", log, new ContCard());
                    }
                    catch (Exception exception2)
                    {
                        exception = exception2;
                        System.Console.WriteLine("error: " + exception.Message);
                        _LastProcessedInput = string.Empty;
                    }
                    goto Label_07B0;

                case @"\h":
                case @"\?":
                case "help":
                case "?":
                    System.Console.WriteLine(System.Console.Out.NewLine + "type: {container card number}[terminal code] scan container card barcode or " + System.Console.Out.NewLine + "enter the number manually" + System.Console.Out.NewLine + @"    : \o to open the gate" + System.Console.Out.NewLine + @"    : \c for copyright info" + System.Console.Out.NewLine + @"    : \h for help with SCGOUT command" + System.Console.Out.NewLine + @"    : \q to quit" + System.Console.Out.NewLine);
                    goto Label_07B0;

                default:
                    try
                    {
                        ProcessInput(input, log);
                    }
                    catch (Exception exception3)
                    {
                        exception = exception3;
                        System.Console.WriteLine("error: " + exception.Message);
                        _LastProcessedInput = string.Empty;
                    }
                    goto Label_07B0;
            }
            //System.Console.WriteLine("--- Welcome to gate app (GATE OUT)---");
            //InputDataGate(service);
        }

        private static bool PrintEir(ContCard card)
        {
            System.Console.Write("Step 2. Checking INOUTREV at " + DateTime.Now.ToString("hh:mm:ss") + "...");
            InOutRevenue revenue = new InOutRevenue();
            InOutRevenueDAL revenueDal = new InOutRevenueDAL();
            revenue = revenueDal.FillInOutRevenueByInOutRevenueId(card.RefID);
            System.Console.WriteLine("OK");
            System.Console.Write("Step 3. Checking CUSTDO at " + DateTime.Now.ToString("hh:mm:ss") + "...");
            CustDo custDo = new CustDo();
            CustDoDAL custDoDAL = new CustDoDAL();
            custDo = custDoDAL.FillCustDoByCustDoId(revenue.RefId);

            System.Console.WriteLine("OK");
            System.Console.Write("Step 4. Checking CONTINOUT at " + DateTime.Now.ToString("hh:mm:ss") + "...");
            ContInOut cont = new ContInOut();
            ContInOutDAL contInOutDAL = new ContInOutDAL();
            cont = contInOutDAL.FillContInOutById(card.ContInOutID);
            if (cont.ContInOutId == 0)
            {
                System.Console.WriteLine("Error: Container is Invalid.");
                return false;
            }
            if (cont.DtmOut.Length > 0)
            {
                System.Console.WriteLine("Error: Container is Gated Out: " + cont.Cont + " " + cont.DtmOut);
                return false;
            }
            System.Console.WriteLine("OK");
            cont.VesselVoyage = custDo.VesselVoyage;
            cont.VesselVoyageName = custDo.VesselVoyageName;
            cont.Destination = custDo.Destination;
            cont.DestinationName = custDo.DestinationName;
            cont.DoNumber = custDo.DoNumber;
            cont.NoSeriOrOut = revenue.NoSeri;
            cont.ActOut = custDo.ActOut;
            cont.Shipper = (custDo.ExBatalRealShipper.Length == 0) ? custDo.Shipper : custDo.ExBatalRealShipper;
            if (custDo.ExBatalRealShipper.Length > 0)
            {
                cont.Remarks = "EBO";
            }
            cont.DtmOutDepoIn = DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss");
            cont.NoMobilOut = card.NoMobilOut;
            if (cont.AngkutanOut.Length == 0)
            {
                cont.AngkutanOut = card.AngkutanOut;
            }
            cont.Seal = card.Seal1;
            if (cont.DtmOut.Length == 0)
            {
                cont.DtmOut = GlobalWebServiceDAL.GetServerDtm().AddMinutes(1.0).ToString("yyyy-MM-dd HH:mm:ss");
            }
            if (cont.DtmOut.Length == 0)
            {
                System.Console.WriteLine("Error: DtmOut Empty");
                return false;
            }
            TruckInDepo depo = new TruckInDepo();
            TruckInDepoDAL depoDAL = new TruckInDepoDAL();
            depo = depoDAL.FillByNomobilNotOut(cont.NoMobilOut);
            if (depo.TruckInDepoId == 0)
            {
                depo.DtmOut = GlobalWebServiceDAL.GetServerDtm();
            }
            cont.EirOut = CtsCounter.NextValCtsCounter("EIRCTR").ToString();
            cont.Location = "";

            try
            {
                int contCount;
                ContInOut out2;
                if (revenue.TakeDef.Contains(cont.Cont))
                {
                    goto Label_04FD;
                }
                List<ContInOut> list = new List<ContInOut>();
                list = contInOutDAL.FillByNoSeriOrOut(revenue.NoSeri, cont.Size, cont.Type, revenue.TakeDef);
                int count = list.Count;
                System.Console.WriteLine("Checking over release of " + count.ToString() + " parties at " + DateTime.Now.ToString("hh:mm:ss") + "...");
                System.Console.Write("Please wait....");
                ContSpec spec = new ContSpec();

                string size = cont.Size;
                if (size != null)
                {
                    if (size != "20")
                    {
                        if (size == "40")
                        {
                            goto Label_042E;
                        }
                        if (size == "45")
                        {
                            goto Label_0438;
                        }
                    }
                    else
                    {
                        spec[0] = revenue.Take20;
                    }
                }
                goto Label_0442;
                Label_042E:
                spec[0] = revenue.Take40;
                goto Label_0442;
                Label_0438:
                spec[0] = revenue.Take45;
                Label_0442:
                contCount = 0;
                foreach (ContSpec item in spec)
                {
                    if (item.ToString() == cont.Type)
                    {
                        contCount = item.ContTotalCount;
                    }
                }
                System.Console.WriteLine("completed at " + DateTime.Now.ToString("hh:mm:ss"));
                if (contCount <= count)
                {
                    System.Console.WriteLine("Error: Too Many Container Released");
                    return false;
                }
                Label_04FD:
                out2 = new ContInOut();
                ContInOutDAL contInOutDAL2 = new ContInOutDAL();
                out2 = contInOutDAL2.FillContInOutById(cont.ContInOutId);
                if (out2.DtmOut.Length > 0)
                {
                    System.Console.WriteLine("Error: Container " + out2.Cont + " Already Gate Out At " + out2.DtmOut + " !!!");
                    return false;
                }
                contInOutDAL.UpdateContInOut(cont);
                if (depo.TruckInDepoId == 0)
                {
                    System.Console.Write("Step 5. Updating truckindepo at " + DateTime.Now.ToString("hh:mm:ss") + "...");
                    //depo.Update(null, tr);

                    System.Console.WriteLine("OK");
                }
                System.Console.Write("Step 6. Committing changes at " + DateTime.Now.ToString("hh:mm:ss") + "...");

                System.Console.WriteLine("OK");
                ContInOut out3 = new ContInOut();
                ContInOutDAL contInOutDAL3 = new ContInOutDAL();
                out3 = contInOutDAL3.FillContInOutById(cont.ContInOutId);
                if (out3.DtmOut.Length == 0)
                {
                    System.Console.WriteLine("Re-Checking: continout dtmout is empty");
                    return false;
                }
                System.Console.WriteLine("Re-Checking: continout dtmout is OK at " + out3.DtmOut);
                DoPrintEirOut(cont);
                return true;
            }
            catch (Exception exception)
            {
                System.Console.WriteLine("Error printing : " + exception.Message);
                return false;
            }
        }
        private static void OpenGate(string openedby, SecureGateLog log, ContCard contCard)
        {
            try
            {
                if (_CaptureImageExeFile.Length > 0)
                {
                    System.Console.WriteLine("info: capturing container picture...");
                    Process.Start(_CaptureImageExeFile);
                    System.Console.WriteLine("info: container picture captured");
                    Thread.Sleep(1000);
                }
                else
                {
                    System.Console.WriteLine("warning: container picture not captured");
                }
            }
            catch (Exception exception)
            {
                System.Console.WriteLine("error: " + exception.Message);
            }
            try
            {
                var terminal = new Terminal(_SecureGateTelnetAddress, Convert.ToInt32(_SecureGateTelnetPort), 10, 80, 40); // hostname, port, timeout [s], width, height
                PushCommand.PushOK(terminal);

                Thread.Sleep(1000);


            }
            catch (Exception exception2)
            {
                System.Console.WriteLine("error: " + exception2.Message);
            }
            try
            {
                SecureGateLogDAL logDal = new SecureGateLogDAL();
                log.Loc1 = _SecureGateLocName;
                log.LogCat = "GATE OPEN EVENT";
                log.LogRemark = openedby;
                log.RefID = contCard.ContCardID;
                if (log.SecureGateLogID <= 0)
                {
                    logDal.DeleteSecureGateLog(log);
                    logDal.InsertSecureGateLog(log);
                }
                else
                    logDal.UpdateSecureGateLog(log);
            }
            catch (Exception exception3)
            {
                System.Console.WriteLine("error: " + exception3.Message);
            }
        }
        private static void ProcessInput(string input, SecureGateLog log)
        {
            System.Console.WriteLine("Proses " + input);
            if (input.Length > 0)
            {
                string str = string.Empty;
                string[] strArray = new string[] { ConfigurationSettings.AppSettings["eiroutprint.printer1.code"], ConfigurationSettings.AppSettings["eiroutprint.printer2.code"] };
                if ((strArray[0] == null) || (strArray[0] == string.Empty))
                {
                    strArray[0] = "#1";
                }
                if ((strArray[1] == null) || (strArray[1] == string.Empty))
                {
                    strArray[1] = "#2";
                }
                if (input.ToUpper().EndsWith(strArray[0].ToUpper()))
                {
                    input = input.Substring(0, input.Length - strArray[0].Length);
                    posnumber = 1;
                    str = strArray[0];
                }
                else if (input.ToUpper().EndsWith(strArray[1].ToUpper()))
                {
                    input = input.Substring(0, input.Length - strArray[1].Length);
                    posnumber = 2;
                    str = strArray[1];
                }
                else
                {
                    input = input.Substring(0, input.Length - strArray[1].Length);
                    posnumber = 1;
                    str = strArray[0];
                }
                if (str != string.Empty)
                {
                    System.Console.WriteLine("container card: " + input + " terminal: " + str);
                    _LastProcessedInput = input + str;
                }
                else
                {
                    System.Console.WriteLine("container card: " + input + " terminal: default");
                    _LastProcessedInput = input;
                }
                ContCard card = new ContCard();
                ContCardDal contCardDal = new ContCardDal();
                card = contCardDal.CheckKendaraan(Convert.ToInt64(input));
                System.Console.WriteLine(input);
                if (card.ContCardID == 0)
                {
                    System.Console.WriteLine("Card #" + card.ContCardID + " is NOT recognized !!!");
                }
                else if (card.Dtm2.Length == 0)
                {
                    System.Console.WriteLine("Step 1. Start proceeding MODE " + card.CardMode + " at " + DateTime.Now.ToString("hh:mm:ss"));
                    if ((card.CardMode == "OUT") && (card.ContInOutID > 0L))
                    {
                        if (!PrintEir(card))
                        {
                            System.Console.WriteLine("INVALID EIROUT CONDITION DETECTED !!!");
                            return;
                        }
                        System.Console.Write("Step 7. Updating counter at " + DateTime.Now.ToString("hh:mm:ss") + "...");
                        //string str2 = "";
                        //int num = 0;
                        //string str4 = ConfigurationSettings.AppSettings["loginserver"];
                        //if (string.IsNullOrEmpty(str4))
                        //    str4 = "localhost";
                        //NpgsqlConnection connection = new NpgsqlConnection("Server=" + str4 + ";Port=5432;User=edimsl;Password=medus;Database=mitcts;");
                        //connection.Open();
                        //NpgsqlCommand command = new NpgsqlCommand("select * from counter", connection);
                        //if ((posnumber == 0) || (posnumber == 1))
                        //{
                        //    str2 = "1";
                        //}
                        //else if (posnumber == 2)
                        //{
                        //    str2 = "2";
                        //}
                        //num = Convert.ToInt16(str2) - 1;
                    }
                    else if ((card.CardMode == "IN") && ((card.Dtm3.Length == 0) || (card.UserID3.Length == 0)))
                    {
                        System.Console.WriteLine("INVALID UNLOAD CONDITION DETECTED !!! please scan barcode on unload device before gate out.");
                        return;
                    }
                    card.Dtm2 = GlobalWebServiceDAL.GetServerDtm().ToString("yyyy-MM-dd HH:mm:ss");
                    card.Loc2 = _SecureGateLocName;
                    contCardDal.UpdateContCardGateOut(card.ContCardID, card.Loc2);
                    System.Console.WriteLine("DTM2 USED");
                    if (card.CardMode == "OUT")
                    {
                        bool flag2 = false;
                        System.Console.Write("Checking DTM2 at " + DateTime.Now.ToString("hh:mm:ss") + "...");
                        //new ContInOut().FillByID(card.ContInOutID);
                        flag2 = card.Dtm2.Length > 0;
                        if (flag2)
                        {
                            System.Console.WriteLine("OK");
                        }
                        if (!flag2)
                        {
                            System.Console.WriteLine("Concard dtmout empty, Please contact Customer Service");
                        }
                        else
                        {
                            OpenGate(input,log,card);
                        }
                    }
                    else
                    {
                        OpenGate(input, log, card);
                    }
                    if ((_CaptureFile.Length > 0) && File.Exists(_CaptureFile))
                    {
                        FileStream stream = File.OpenRead(_CaptureFile);
                        BinaryReader reader = new BinaryReader(stream);
                        ContCardPic pic = new ContCardPic();
                        ContCardPICDal contCardPICDal = new ContCardPICDal();
                        pic.ContCardID = card.ContCardID;
                        pic.PicName = "OUT";
                        pic.PicData = reader.ReadBytes((int)stream.Length);
                        reader.Close();
                        stream.Close();
                        contCardPICDal.InsertContCardPIC(pic);
                        File.Move(_CaptureFile, string.Concat(new object[] { _CaptureFile, ".", CtsCounter.NextValCtsCounter("CONTCARDPICOUT_SEQ"), ".jpg" }));
                        System.Console.WriteLine("Picture Captured To Database.");
                    }
                }
                else
                {
                    System.Console.WriteLine("DTM 2 (STACKING) IS INVALID !!!");
                }
            }
        }
        //static void InputDataGate(GateService.GateServicesSoapClient service)
        //{
        //    var terminal = new Terminal("192.168.15.161", 8023, 10, 80, 40); // hostname, port, timeout [s], width, height
        //    //var terminal = new Terminal("192.168.43.99", 8023, 10, 80, 40); // hostname, port, timeout [s], width, height

        //    long contCardID;
        //    string location;

        //    System.Console.Write("Please input container card ID: ");
        //    contCardID = Convert.ToInt64(System.Console.ReadLine());

        //    //var vehicle = DAL.GateINDal.CheckKendaraan(contCardID);
        //    var vehicle = service.CheckKendaraan(contCardID);
        //    if (vehicle.Length == 0 || vehicle.Contains("Error"))
        //    {
        //        PushCommand.PushER(terminal);
        //        System.Console.WriteLine("Data Kendaraan tidak ditemukan!");
        //    }
        //    else
        //    {
        //        var dataSetContCard = Converter.ConvertXmlToDataSet(vehicle);
        //        var data = dataSetContCard.Tables[0].ToList<ContCard>();
        //        System.Console.WriteLine("Data Kendaraan:");
        //        System.Console.WriteLine(data.ToStringTable(
        //            new[] { "ID ContCard", "Card Mode", "Ref Mode", "Cont Count", "Cont Size", "Cont Type" },
        //            a => a.ContCardID, a => a.CardMode, a => a.RefID, a => a.Cont, a => a.Size, a => a.Type));
        //        System.Console.WriteLine("---");
        //        System.Console.Write("Please input location: ");
        //        location = System.Console.ReadLine();
        //        if (service.UpdateContCardGateOut(contCardID, location))
        //        {
        //            if (service.UpdateContInOutGateOut(contCardID, location))
        //            {
        //                System.Console.WriteLine("Data Kendaraan berhasil diupdate!");
        //                System.Console.WriteLine("Press enter to continue..(OPEN GATE)");
        //                System.Console.ReadLine();
        //                PushCommand.PushOK(terminal);
        //                System.Console.WriteLine("---");
        //            }
        //        }
        //    }


        //    InputDataGate(service);
        //}
    }
}
