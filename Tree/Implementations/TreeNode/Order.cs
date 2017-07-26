using System;
using System.Collections.Generic;
using Tree.BaseEnums;
using Tree.Interfaces;
namespace Tree.Implementations.TreeNode
{
    public class Order : TreeNodeBase, IOrderLine
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
        public bool CanHasSubOrders { get; set; }
        #endregion IOrderLine
        public override string NodeName
        {
            get
            {
                return string.Format("{0}: {1}", Day, DeviceName);
            }
        }
        #endregion Properties

        #region Methods
        public Order(bool canHasSubOrders = false)
        {
            CanHasSubOrders = canHasSubOrders;
            if (CanHasSubOrders)
                AddNewChild();
        }

        public override ITreeNode CreateNewChild()
        {
            return new Order();
        }
        #endregion Methods
    }
}
