using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.GateOut.Console.DAL
{
    public static class ContainerMapping
    {
        #region Fields
        private static Dictionary<string, string> _containerDictionary;
        #endregion

        #region Properties
        public static Dictionary<string, string> ContainerDictionary
        {
            get
            {
                if (_containerDictionary == null)
                {
                    _containerDictionary = new Dictionary<string, string>();
                    // Key: Customer Type; Value: MIT Type
                    _containerDictionary.Add("RQ", "RH");
                }
                return _containerDictionary;
            }
        }
        #endregion
    }
}
