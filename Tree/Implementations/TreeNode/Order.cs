using System;
using System.Collections.Generic;
using Tree.BaseEnums;
using Tree.Interfaces;
namespace Tree.Implementations.TreeNode
{
    public class Order : TreeNodeBase, IOrder
    {
        #region Properties
        
        #region IOrderLine
        public int Year { get; set; }
        public MonthEn Month { get; set; }
        public int Day { get; set; }
        public string DeviceName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string JobType { get; set; }
        public string InstalledDetails { get; set; }
        public double Income { get; set; }
        public double Outgo { get; set; }
        #endregion IOrderLine
        public override string NodeName
        {
            get
            {
                return string.Format("{0}: {1}", Day, DeviceName);
            }
        }

        public override ICollection<ITreeNode> ChildNodes
        {
            get
            {
                var ret = new List<ITreeNode>();
                foreach (var child in _childNodes)
                    if (child.CanHasChildren)
                        ret.Add(child);
                return ret;
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
                    else
                        ret.AddRange(ord.Orders);
                }
                return ret;
            }
        }
        IOrderLine IOrder.Order
        {
            get
            { 
                return this;
            }
        }
        public override bool CanHasChildren 
        { 
            get
            {
                // Must be false after creating GroupOrder class
                return _childNodes.Count > 0;
            }
        }
        #endregion Properties
       
        #region Methods
        public Order(bool canHasSubOrders = false)
        {
            if (canHasSubOrders)
                AddNewChild();
        }

        public override ITreeNode CreateNewChild()
        {
            return new Order();
        }


        #endregion Methods
    }
}
