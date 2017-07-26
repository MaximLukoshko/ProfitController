using System;
using System.Collections.Generic;
using Tree.BaseEnums;
using Tree.Interfaces;

namespace Tree.Implementations.TreeNode
{
    public class Month : TreeNodeBase
    {
       
        #region Properties
        private IList<Order> _orders = new List<Order>();
        public override ICollection<IOrderLine> Orders
        {
            get
            {
                var ret = new List<IOrderLine>();
                foreach (var ord in _orders)
                    ret.Add(ord);

                return ret;
            }
        }
        public MonthEn Value { get; set; }

        public override string NodeName
        {
            get
            {
                return Value.ToString();
            }
        }
        public override ICollection<ITreeNode> ChildNodes
        {
            get
            {
                ICollection<ITreeNode> ret = new List<ITreeNode>();
                foreach (var ord in _orders)
                    if (ord.CanHasSubOrders)
                        ret.Add(ord);
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

        public Month(int month)
        {
            Value = (MonthEn) month;
            _orders = new List<Order>
            {
                new Order(true) {
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
                },
                new Order(true) {
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
                },
                new Order(true) {
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
                },
            };
        }

        public override ITreeNode CreateNewChild()
        {
            return new Order();
        }

        #endregion Methods
    }
}
