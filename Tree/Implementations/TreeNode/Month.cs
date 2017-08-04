﻿using System;
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

        public Month(int month = 0)
        {
            Value = (MonthEn) month;
        }

        public Month()
        {

        }

        public override ITreeNode CreateNewChild()
        {
            return new Order(true);
        }

        public override XElement ToXElement()
        {
            return new XElement("Item", new XElement(MONTH, Value));
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
            return true;
        }

        public override bool AddOrder()
        {
            return AddChild(new Order());
        }
    }
}
