using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.GateOut.Console.DAL
{
    [Serializable()]
    public class ConditionList : ArrayList
    {
        private static ConditionList _conditionList = null;

        public static ConditionList Value
        {
            get
            {
                if (_conditionList == null)
                {
                    _conditionList = new ConditionList();
                }
                return _conditionList;
            }
        }

        public ConditionList() : base()
        {
            Add("AV");
            Add("DM");
            Add("WS");
        }
    }
}
