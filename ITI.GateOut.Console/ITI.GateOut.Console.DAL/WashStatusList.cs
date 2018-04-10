using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.GateOut.Console.DAL
{

    [Serializable]
    public class WashStatusList : ArrayList
    {
        private static WashStatusList _washStatusList = null;

        public static WashStatusList Value
        {
            get
            {
                if (_washStatusList == null)
                {
                    _washStatusList = new WashStatusList();
                }
                return _washStatusList;
            }
        }

        public WashStatusList() : base()
        {
            Add("W");
            Add("D");
            Add("C");
            Add("X");
            Add("N");
        }
    }
}
