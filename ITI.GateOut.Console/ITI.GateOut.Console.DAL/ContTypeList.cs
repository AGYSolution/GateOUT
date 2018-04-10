using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.GateOut.Console.DAL
{
    [Serializable()]
    public class ContTypeList : ArrayList
    {
        private static ContTypeList _contTypeList = null;

        public static ContTypeList Value
        {
            get
            {
                if (_contTypeList == null)
                {
                    _contTypeList = new ContTypeList();
                }
                return _contTypeList;
            }
        }

        public ContTypeList() : base()
        {
            Add("GP");
            Add("HU");
            Add("RF");
            Add("RH");
            Add("TK");
            Add("HR");
            Add("FR");
            Add("OT");
            Add("VT");
            Add("OS");
            Add("AL");
            Add("SR");
        }
    }
}
