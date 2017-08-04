﻿using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Tree.BaseEnums;
using Tree.Interfaces;
using System.Linq;

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
        public override ICollection<IOrderLine> Orders
        {
            get
            {
                var ret = new List<IOrderLine>();
                foreach (var child in AllChildren)
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
            return new Order(true) {Year = Year, Month = Month};
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
            int year = -1;
            int.TryParse(elem.Elements(YEAR).FirstOrDefault().Value, out year);
            Year = year;
            
            MonthEn month = 0;
            Enum.TryParse<MonthEn>(elem.Elements(MONTH).FirstOrDefault().Value, out month);
            Month = month;

            int day = -1;
            int.TryParse(elem.Elements(DAY).FirstOrDefault().Value, out day);
            Day = day;

            DeviceName = elem.Elements(DEVICE_NAME).FirstOrDefault().Value;
            Address = elem.Elements(ADDRESS).FirstOrDefault().Value;
            Phone = elem.Elements(PHONE).FirstOrDefault().Value;
            JobType = elem.Elements(JOBTYPE).FirstOrDefault().Value;
            InstalledDetails = elem.Elements(INSTALLED_DETAILS).FirstOrDefault().Value;

            int income = -1;
            int.TryParse(elem.Elements(INCOME).FirstOrDefault().Value, out income);
            Income = income;

            int outgo = -1;
            int.TryParse(elem.Elements(OUTGO).FirstOrDefault().Value, out outgo);
            Outgo = outgo;

            bool canhavechildren = false;
            bool.TryParse(elem.Elements(CAN_HAVE_CHILDREN).FirstOrDefault().Value, out canhavechildren);
            _canHasChildren = canhavechildren;

            return true;
        }
        public override bool AddOrder()
        {
            return AddChild(new Order());
        }
    }
}
