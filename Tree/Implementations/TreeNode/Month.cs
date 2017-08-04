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
        private const string YEAR = @"Year";
        private const string MONTH = @"Month";
        public MonthEn Value { get; set; }
        public int Year { get; private set; }

        public override string NodeName
        {
            get { return Value.ToString(); }
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
                }
                return ret;
            }
        }

        public Month(string month)
        {
            MonthEn val;
            Enum.TryParse(month, out val);
            Value = val;
        }

        public Month(int year, int month = 0)
        {
            Year = year;
            Value = (MonthEn) month;
        }

        public Month()
        {

        }

        public override ITreeNode CreateNewChild()
        {
            return new Order(true) {Year = Year, Month = Value};
        }

        public override XElement ToXElement()
        {
            return new XElement("Item",
                new XElement(YEAR, Year),
                new XElement(MONTH, Value));
        }

        public override bool FromXElement(XElement elem)
        {
            MonthEn val;
            var firstOrDefault = elem.Elements(MONTH).FirstOrDefault();
            if (firstOrDefault != null)
            {
                Enum.TryParse(firstOrDefault.Value, out val);
                Value = val;
            }

            var year = elem.Elements(YEAR).FirstOrDefault();
            if (year != null)
            {
                int y;
                int.TryParse(year.Value, out y);
                Year = y;
            }

            return true;
        }

        public override bool AddOrder()
        {
            return AddChild(new Order(false) {Year = Year, Month = Value});
        }
    }
}