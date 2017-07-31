using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Tree.BaseEnums;
using Tree.Interfaces;
namespace Tree.Implementations.TreeNode
{
    public class Order : TreeNodeBase, IOrder
    {
        private const string YEAR = @"Year";
        private const string MONTH = @"Month";
        private const string DAY = @"Day";
        private const string DEVICE_NAME = @"DeviceName";
        private const string ADDRESS = @"Address";
        private const string PHONE = @"Phone";
        private const string JOBTYPE = @"JobType";
        private const string INSTALLED_DETAILS = @"InstalledDetails";
        private const string INCOME = @"Income";
        private const string OUTGO = @"OutGo";
        private const string CAN_HAVE_CHILDREN = @"CanHaveChildren";
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
                return _canHasChildren;
            }
        }
        #endregion Properties
        private bool _canHasChildren = false;

        #region Methods
        public Order(bool canHasSubOrders = false)
        {
            _canHasChildren = canHasSubOrders;
        }

        public Order()
        {

        }

        public override ITreeNode CreateNewChild()
        {
            return new Order(true);
        }


        #endregion Methods

        public override XElement ToXElement()
        {
            return new XElement("Item", 
                new XElement(YEAR, Year),
                new XElement(MONTH, Month),
                new XElement(DAY, Day),
                new XElement(DEVICE_NAME, DeviceName),
                new XElement(ADDRESS, Address),
                new XElement(PHONE, Phone),
                new XElement(JOBTYPE, JobType),
                new XElement(INSTALLED_DETAILS, InstalledDetails),
                new XElement(INCOME, Income),
                new XElement(OUTGO, Outgo),
                new XElement(CAN_HAVE_CHILDREN, _canHasChildren)
                );
        }

        public override bool FromXElement(XElement elem)
        {
            throw new NotImplementedException();
        }
    }
}
