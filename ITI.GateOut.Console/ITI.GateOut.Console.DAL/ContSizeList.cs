using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.GateOut.Console.DAL
{

    [Serializable]
    public class ContSizeList : ArrayList
    {
        private static ContSizeList _contSizeList = null;

        public static ContSizeList Value
        {
            get
            {
                if (_contSizeList == null)
                {
                    _contSizeList = new ContSizeList();
                }
                return _contSizeList;
            }
        }

        public ContSizeList() : base()
        {
            Add("20");
            Add("40");
            Add("45");
        }
    }
}
