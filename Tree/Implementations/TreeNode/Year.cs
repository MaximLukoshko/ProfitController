﻿using System.Xml.Linq;
using Tree.Interfaces;
using System.Linq;


namespace Tree.Implementations.TreeNode
{
    public class Year : TreeNodeBase
    {
        #region Constructors

        public Year(int val = 0)
        {
            Value = val;
        }

        public Year()
        {
        }

        #endregion Constructors

        #region TreeNodeBase

        public override string NodeName
        {
            get { return Value.ToString(); }
        }

        public override ITreeNode CreateNewChild()
        {
            return ChildNodes.Count < 12 ? new Month(Value, ChildNodes.Count) : null;
        }

        public override XElement ToXElement()
        {
            return new XElement("Item", new XElement(StringConstants.Year, Value));
        }

        public override bool FromXElement(XElement elem)
        {
            var year = elem.Elements(StringConstants.Year).FirstOrDefault();
            if (year != null)
            {
                int val;
                int.TryParse(year.Value, out val);
                Value = val;
            }
            return true;
        }

        #endregion TreeNodeBase

        #region PrivateMembers

        private int Value { get; set; }

        #endregion PrivateMembers
    }
}
