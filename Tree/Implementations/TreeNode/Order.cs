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
        public bool CanHasSubOrders { get; set; }
        #endregion IOrderLine
        private IList<IOrder> _childs = new List<IOrder>();
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
                return new List<ITreeNode>(_childs);
            }
        }
        #endregion Properties

        #region Methods
        public Order(bool canHasSubOrders = false)
        {
            CanHasSubOrders = canHasSubOrders;
        }

        public override bool AddNewCild()
        {
            _childs.Add(new Order(true));
            return true;
        }
        #endregion Methods
    }
}
