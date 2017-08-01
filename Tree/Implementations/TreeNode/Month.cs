using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Tree.BaseEnums;
using Tree.Interfaces;
using System.Linq;

namespace Tree.Implementations.TreeNode
{
    public class Month : TreeNodeBase
    {
        private const string MONTH = @"Month";
        #region Properties
        public MonthEn Value { get; set; }

        public override string NodeName
        {
            get
            {
                return Value.ToString();
            }
        }

        public override ICollection<IOrderLine> Orders
        {
            get
            {
                var ret = new List<IOrderLine>();
                foreach (var child in _childNodes)
                {
                    var ord = child as IOrder;
                    if (ord != null)
                        ret.Add(ord.Order);
                }
                return ret;
            }
        }

        #endregion Properties

        #region Methods

        public Month(string month)
        {
            MonthEn val;
            Enum.TryParse(month, out val);
            Value = val;
        }

        public Month(int month = 0)
        {
            Value = (MonthEn) month;
            AddChild();
            AddChild();
            AddChild();
        }

        public Month()
        {

        }

        public override ITreeNode CreateNewChild()
        {
            return  new Order(true) {
                    Year = 2017, 
                    Month = (MonthEn)1,
                    Day= 25, 
                    DeviceName ="DeviceName",
                    Address ="Address",
                    Phone ="Phone",
                    JobType ="JobType",
                    InstalledDetails ="InstalledDetails",
                    Income =2000,
                    Outgo =500
                };
        }

        #endregion Methods

        public override XElement ToXElement()
        {
            return new XElement("Item", new XElement(MONTH, Value));
        }

        public override bool FromXElement(XElement elem)
        {
            MonthEn val = 0;
            Enum.TryParse<MonthEn>(elem.Elements(MONTH).FirstOrDefault().Value, out val);
            Value = val;
            return true;
        }

        public override bool AddOrder()
        {
            return AddChild(new Order());
        }
    }
}
