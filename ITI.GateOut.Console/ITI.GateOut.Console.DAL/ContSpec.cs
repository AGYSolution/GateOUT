using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.GateOut.Console.DAL
{
    public class ContSpec : ArrayList
    {
        public event EventHandler ContChanged;

        public ContSpec()
        {
        }

        public ContSpec(string input)
        {
            this.FromString(input);
        }

        public ContSpecItem FindByType(string type)
        {
            foreach (ContSpecItem item in this)
            {
                if (item.ContType == type)
                {
                    return item;
                }
            }
            return new ContSpecItem();
        }

        public ContSpecItem FindByType_NullIfNF(string type)
        {
            foreach (ContSpecItem item in this)
            {
                if (item.ContType == type)
                {
                    return item;
                }
            }
            return null;
        }

        public void FromString(string input)
        {
            this.Clear();
            foreach (string str in input.Split(",".ToCharArray()))
            {
                ContSpecItem item = new ContSpecItem(str);
                if (!item.IsError)
                {
                    ContSpecItem item2 = this.FindByType_NullIfNF(item.ContType);
                    if (item2 == null)
                    {
                        this.Add(item);
                    }
                    else
                    {
                        item2.mContCount += item.mContCount;
                    }
                }
            }
            this.OnContChanged();
        }

        protected void OnContChanged()
        {
            if (this.ContChanged != null)
            {
                this.ContChanged(this, new EventArgs());
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder("");
            for (int i = 0; i < this.Count; i++)
            {
                builder.Append((this[i] as ContSpecItem).ToString());
                if (i != (this.Count - 1))
                {
                    builder.Append(",");
                }
            }
            return builder.ToString();
        }

        public int ContTotalCount
        {
            get
            {
                int num = 0;
                foreach (ContSpecItem item in this)
                {
                    num += item.ContCount;
                }
                return num;
            }
        }

        [Serializable]
        public class ContSpecItem
        {
            internal int mContCount;
            internal string mContType;
            public bool IsError;

            public ContSpecItem()
            {
                this.mContType = "";
                this.IsError = true;
            }

            public ContSpecItem(string input)
            {
                this.mContType = "";
                this.IsError = true;
                input = input.Trim();
                input = input.Replace(" ", "");
                if (input.Length >= 3)
                {
                    string str = input.Substring(0, input.Length - 2);
                    string str2 = input.Substring(input.Length - 2, 2);
                    try
                    {
                        this.mContCount = Convert.ToInt32(str);
                    }
                    catch (Exception)
                    {
                        return;
                    }
                    this.mContType = str2;
                    if (ContTypeList.Value.Contains(this.mContType))
                    {
                        this.IsError = false;
                    }
                }
            }

            public override string ToString()
            {
                return (this.mContCount.ToString() + this.mContType);
            }

            public string ContType
            {
                get
                {
                    return this.mContType;
                }
            }

            public int ContCount
            {
                get
                {
                    return this.mContCount;
                }
            }
        }
    }
}
