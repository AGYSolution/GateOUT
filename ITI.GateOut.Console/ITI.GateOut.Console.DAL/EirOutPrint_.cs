using ReportPrinting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.GateOut.Console.DAL
{
    public class EirOutPrint_: IReportMaker
    {
        private ContInOut mObj;
        private Customer mCust;
        private InOutRevenue mOr;

        public EirOutPrint_(ContInOut obj)
        {
            this.mObj = obj;
            this.mCust = new Customer();
            CustomerDAL customerDAL = new CustomerDAL();
            this.mCust = customerDAL.FillByCustomerCode(this.mObj.CustomerCode);
            this.mOr = new InOutRevenue();
            InOutRevenueDAL inOutRevenueDAL = new InOutRevenueDAL();
            this.mOr = inOutRevenueDAL.FillByNoSeri(obj.NoSeriOrOut);
        }

        public void MakeDocument(ReportDocument reportDocument)
        {
            if (this.mObj != null)
            {
                TextStyle.ResetStyles();
                DateTime serverDtm = GlobalWebServiceDAL.GetServerDtm();
                float num = 1f;
                float num2 = 10f;
                float num3 = 10f;
                float num4 = 420f;
                float num5 = 480f;
                float num6 = 150f;
                float num7 = 100f;
                try
                {
                    num = Convert.ToSingle(ConfigurationSettings.AppSettings["eiroutprint.pen.width"]);
                }
                catch (Exception)
                {
                }
                try
                {
                    num2 = Convert.ToSingle(ConfigurationSettings.AppSettings["eiroutprint.box.margin"]);
                }
                catch (Exception)
                {
                }
                try
                {
                    num3 = Convert.ToSingle(ConfigurationSettings.AppSettings["eiroutprint.box.padding"]);
                }
                catch (Exception)
                {
                }
                try
                {
                    num4 = Convert.ToSingle(ConfigurationSettings.AppSettings["eiroutprint.box.width"]);
                }
                catch (Exception)
                {
                }
                try
                {
                    num5 = Convert.ToSingle(ConfigurationSettings.AppSettings["eiroutprint.box.height"]);
                }
                catch (Exception)
                {
                }
                try
                {
                    num6 = Convert.ToSingle(ConfigurationSettings.AppSettings["eiroutprint.col1.width"]);
                }
                catch (Exception)
                {
                }
                try
                {
                    num7 = Convert.ToSingle(ConfigurationSettings.AppSettings["eiroutprint.col2.width"]);
                }
                catch (Exception)
                {
                }
                Pen borders = new Pen(Color.White, num / 100f);
                Pen pen = new Pen(Color.Black, num / 100f);
                ReportBuilder builder = new ReportBuilder(reportDocument);
                builder.StartBox(num2 / 100f, borders, num3 / 100f, Brushes.White, num4 / 100f, num5 / 100f);
                builder.StartLinearLayout(Direction.Vertical);
                builder.DefaultTablePen = borders;
                string name = "Times New Roman";
                int num8 = 12;
                string text = "PT. MULTICON INDRAJAYA TERMINAL";
                string str3 = "EQUIPMENT INTERCHANGE RECEIPT (OUT)";
                string str4 = "Lucida Console";
                int num9 = 10;
                string headerText = "D/O No.";
                string str6 = "Shipper";
                string str7 = "VESSEL/VOY No.";
                string str8 = "Destination";
                string str9 = "Quantity";
                string str10 = "Principal";
                string str11 = "Delivered";
                string str12 = "Vehicle No.";
                string str13 = "Lucida Console";
                int num10 = 10;
                string str14 = "CONTAINER PREFIX + NUMBER";
                string str15 = "SIZE";
                string str16 = "TYPE";
                string str17 = "CONDITION";
                string str18 = "SEAL NUMBER";
                string format = "REMARKS : {0} {1} {2}";
                string str20 = "Arial";
                int num11 = 10;
                string str21 = "Printed and Authorized, {0} {1}";
                string str22 = "PT. MULTICON INDRAJAYA TERMINAL";
                try
                {
                    num8 = Convert.ToInt32(ConfigurationSettings.AppSettings["eiroutprint.header.font.size"]);
                }
                catch (Exception)
                {
                }
                try
                {
                    num9 = Convert.ToInt32(ConfigurationSettings.AppSettings["eiroutprint.main.font.size"]);
                }
                catch (Exception)
                {
                }
                try
                {
                    num10 = Convert.ToInt32(ConfigurationSettings.AppSettings["eiroutprint.line.font.size"]);
                }
                catch (Exception)
                {
                }
                try
                {
                    num11 = Convert.ToInt32(ConfigurationSettings.AppSettings["eiroutprint.footer.font.size"]);
                }
                catch (Exception)
                {
                }
                try
                {
                    name = ConfigurationSettings.AppSettings["eiroutprint.header.font.name"];
                }
                catch (Exception)
                {
                }
                try
                {
                    text = ConfigurationSettings.AppSettings["eiroutprint.header.1"];
                }
                catch (Exception)
                {
                }
                try
                {
                    str3 = ConfigurationSettings.AppSettings["eiroutprint.header.2"];
                }
                catch (Exception)
                {
                }
                try
                {
                    str4 = ConfigurationSettings.AppSettings["eiroutprint.main.font.name"];
                }
                catch (Exception)
                {
                }
                try
                {
                    headerText = ConfigurationSettings.AppSettings["eiroutprint.main.1"];
                }
                catch (Exception)
                {
                }
                try
                {
                    str6 = ConfigurationSettings.AppSettings["eiroutprint.main.2"];
                }
                catch (Exception)
                {
                }
                try
                {
                    str7 = ConfigurationSettings.AppSettings["eiroutprint.main.3"];
                }
                catch (Exception)
                {
                }
                try
                {
                    str8 = ConfigurationSettings.AppSettings["eiroutprint.main.4"];
                }
                catch (Exception)
                {
                }
                try
                {
                    str9 = ConfigurationSettings.AppSettings["eiroutprint.main.5"];
                }
                catch (Exception)
                {
                }
                try
                {
                    str10 = ConfigurationSettings.AppSettings["eiroutprint.main.6"];
                }
                catch (Exception)
                {
                }
                try
                {
                    str11 = ConfigurationSettings.AppSettings["eiroutprint.main.7"];
                }
                catch (Exception)
                {
                }
                try
                {
                    str12 = ConfigurationSettings.AppSettings["eiroutprint.main.8"];
                }
                catch (Exception)
                {
                }
                try
                {
                    str13 = ConfigurationSettings.AppSettings["eiroutprint.line.font.name"];
                }
                catch (Exception)
                {
                }
                try
                {
                    str14 = ConfigurationSettings.AppSettings["eiroutprint.line.1"];
                }
                catch (Exception)
                {
                }
                try
                {
                    str15 = ConfigurationSettings.AppSettings["eiroutprint.line.2"];
                }
                catch (Exception)
                {
                }
                try
                {
                    str16 = ConfigurationSettings.AppSettings["eiroutprint.line.3"];
                }
                catch (Exception)
                {
                }
                try
                {
                    str17 = ConfigurationSettings.AppSettings["eiroutprint.line.4"];
                }
                catch (Exception)
                {
                }
                try
                {
                    str18 = ConfigurationSettings.AppSettings["eiroutprint.line.5"];
                }
                catch (Exception)
                {
                }
                try
                {
                    format = ConfigurationSettings.AppSettings["eiroutprint.line.6"];
                }
                catch (Exception)
                {
                }
                try
                {
                    str20 = ConfigurationSettings.AppSettings["eiroutprint.footer.font.name"];
                }
                catch (Exception)
                {
                }
                try
                {
                    str21 = ConfigurationSettings.AppSettings["eiroutprint.footer.1"];
                }
                catch (Exception)
                {
                }
                try
                {
                    str22 = ConfigurationSettings.AppSettings["eiroutprint.footer.2"];
                }
                catch (Exception)
                {
                }
                TextStyle textStyle = new TextStyle(TextStyle.BoldStyle);
                textStyle.FontFamily = new FontFamily(name);
                textStyle.Size = num8;
                textStyle.StringAlignment = StringAlignment.Center;
                TextStyle style2 = new TextStyle(TextStyle.Normal);
                style2.FontFamily = new FontFamily(str4);
                style2.Size = num9;
                TextStyle style3 = new TextStyle(TextStyle.Normal);
                style3.FontFamily = new FontFamily(str13);
                style3.Size = num10;
                TextStyle style4 = new TextStyle(TextStyle.Normal);
                style4.FontFamily = new FontFamily(str20);
                style4.Size = num11;
                builder.AddText(text, textStyle);
                builder.AddText(str3, textStyle);
                builder.AddHorizontalLine(pen);
                DataTable table = new DataTable("main");
                table.Columns.Add("column1", typeof(string));
                table.Columns.Add("column2", typeof(string));
                table.Rows.Add(new object[] { str6, this.mObj.Shipper });
                table.Rows.Add(new object[] { str7, this.mObj.VesselVoyageName });
                table.Rows.Add(new object[] { str8, this.mObj.DestinationName });
                table.Rows.Add(new object[] { str9, "?" });
                table.Rows.Add(new object[] { str10, this.mCust.Name });
                table.Rows.Add(new object[] { str11, this.mObj.AngkutanOut });
                table.Rows.Add(new object[] { str12, this.mObj.NoMobilOut });
                SectionTable table2 = builder.AddTable(table.DefaultView, true);
                table2.InnerPenHeaderBottom.Color = Color.White;
                table2.InnerPenRow.Color = Color.White;
                table2.HeaderTextStyle.SetFromFont(style2.GetFont());
                table2.HorizontalAlignment = HorizontalAlignment.Center;
                ReportDataColumn column = builder.AddColumn("column1", headerText, num6 / 100f, false, false);
                column.HeaderTextStyle = style2;
                column.DetailRowTextStyle = style2;
                column.AlternatingRowTextStyle = style2;
                ReportDataColumn column2 = builder.AddColumn("column2", this.mObj.DoNumber, num7 / 100f, false, false);
                column2.HeaderTextStyle = style2;
                column2.DetailRowTextStyle = style2;
                column2.AlternatingRowTextStyle = style2;
                builder.AddHorizontalLine(pen);
                DataTable table3 = new DataTable("line");
                table3.Columns.Add("column1", typeof(string));
                table3.Columns.Add("column2", typeof(string));
                table3.Rows.Add(new object[] { str15, this.mObj.Size });
                table3.Rows.Add(new object[] { str16, this.mObj.Type });
                table3.Rows.Add(new object[] { str17, this.mObj.Condition });
                table3.Rows.Add(new object[] { str18, this.mObj.Seal });
                SectionTable table4 = builder.AddTable(table3.DefaultView, true);
                table4.InnerPenHeaderBottom = borders;
                table4.InnerPenRow = borders;
                table4.OuterPens = borders;
                table4.HeaderTextStyle.SetFromFont(style3.GetFont());
                table4.HorizontalAlignment = HorizontalAlignment.Center;
                ReportDataColumn column3 = builder.AddColumn("column1", str14.Replace('+', '&'), num6 / 100f, false, false);
                column3.HeaderTextStyle = style3;
                column3.DetailRowTextStyle = style3;
                column3.AlternatingRowTextStyle = style3;
                ReportDataColumn column4 = builder.AddColumn("column2", this.mObj.Cont, num7 / 100f, false, false);
                column4.HeaderTextStyle = style3;
                column4.DetailRowTextStyle = style3;
                column4.AlternatingRowTextStyle = style3;
                builder.AddText(" ", style3);
                builder.AddText(string.Format(format, this.mObj.Remarks), style3);
                builder.AddHorizontalLine(pen);
                builder.AddText(" ", style4);
                string introduced93 = serverDtm.ToLongDateString();
                builder.AddText(string.Format(str21, introduced93, serverDtm.ToLongTimeString()), style4);
                builder.AddText(str22, style4);
                builder.AddText(" \r\n ", style4);
                builder.FinishLinearLayout();
                builder.FinishBox();
            }
        }
    }
}
