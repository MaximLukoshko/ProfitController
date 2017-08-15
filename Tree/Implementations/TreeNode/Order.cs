using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;
using Tree.Interfaces;
using System.Linq;

namespace Tree.Implementations.TreeNode
{
    public class Order : TreeNodeBase, IOrder
    {
        #region IOrderLine

        public int Year { get; set; }
        public MonthEn Month { get; set; }
        public int Day { get; set; }
        public string DeviceName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string JobType { get; set; }
        public string InstalledDetails { get; set; }
        public double Outgo { get; set; }
        public double Income { get; set; }

        public double Profit
        {
            get { return Income - Outgo; }
        }

        #endregion IOrderLine

        public override string NodeName
        {
            get { return string.Format("{0}: {1}", Day, DeviceName); }
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
                        ret.AddRange(child.Orders);
                }
                return ret;
            }
        }

        IOrderLine IOrder.Order
        {
            get { return this; }
        }

        public override bool CanHasChildren
        {
            get { return _canHasChildren; }
        }

        private bool _canHasChildren;

        #region Methods

        public Order(bool canHasSubOrders = false)
        {
            _canHasChildren = canHasSubOrders;
            Day = DateTime.Now.Day;
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
                new XElement(StringConstants.Year, Year),
                new XElement(StringConstants.Month, (int) Month),
                new XElement(StringConstants.Day, Day),
                new XElement(StringConstants.DeviceName, DeviceName),
                new XElement(StringConstants.Address, Address),
                new XElement(StringConstants.Phone, Phone),
                new XElement(StringConstants.Jobtype, JobType),
                new XElement(StringConstants.InstalledDetails, InstalledDetails),
                new XElement(StringConstants.Income, Income),
                new XElement(StringConstants.Outgo, Outgo),
                new XElement(StringConstants.CanHaveChildren, _canHasChildren)
                );
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public override bool FromXElement(XElement elem)
        {
            int year;
            int.TryParse(elem.Elements(StringConstants.Year).FirstOrDefault().Value, out year);
            Year = year;

            MonthEn month;
            Enum.TryParse(elem.Elements(StringConstants.Month).FirstOrDefault().Value, out month);
            Month = month;

            int day;
            int.TryParse(elem.Elements(StringConstants.Day).FirstOrDefault().Value, out day);
            Day = day;

            DeviceName = elem.Elements(StringConstants.DeviceName).FirstOrDefault().Value;
            Address = elem.Elements(StringConstants.Address).FirstOrDefault().Value;
            Phone = elem.Elements(StringConstants.Phone).FirstOrDefault().Value;
            JobType = elem.Elements(StringConstants.Jobtype).FirstOrDefault().Value;
            InstalledDetails = elem.Elements(StringConstants.InstalledDetails).FirstOrDefault().Value;

            int income;
            int.TryParse(elem.Elements(StringConstants.Income).FirstOrDefault().Value, out income);
            Income = income;

            int outgo;
            int.TryParse(elem.Elements(StringConstants.Outgo).FirstOrDefault().Value, out outgo);
            Outgo = outgo;

            bool canhavechildren;
            bool.TryParse(elem.Elements(StringConstants.CanHaveChildren).FirstOrDefault().Value, out canhavechildren);
            _canHasChildren = canhavechildren;

            return true;
        }

        public override bool AddOrder()
        {
            return AddChild(new Order());
        }

#pragma warning disable 659
        public override bool Equals(object obj)
#pragma warning restore 659
        {
            var cmpObj = obj as Order;
            if (cmpObj == null)
                return false;

            var ret = cmpObj.Year == Year;
            ret = ret && cmpObj.Month == Month;
            ret = ret && cmpObj.Day == Day;
            ret = ret && cmpObj.DeviceName == DeviceName;
            ret = ret && cmpObj.Address == Address;
            ret = ret && cmpObj.Phone == Phone;
            ret = ret && cmpObj.JobType == JobType;
            ret = ret && cmpObj.InstalledDetails == InstalledDetails;
            ret = ret && (cmpObj.Income.Equals(Income));
            ret = ret && (cmpObj.Outgo.Equals(Outgo));
            ret = ret && (cmpObj.Profit.Equals(Profit));

            return ret && base.Equals(obj);
        }
    }
}
