using System.Collections.Generic;
using System.Xml.Linq;
using Tree.Interfaces;
using System;
using System.Linq;


namespace Tree.Implementations.TreeNode
{

    public class Year : TreeNodeBase
    {
        private const string YEAR = @"Year";
        #region Properties
        private int Value { get; set; }
        public override string NodeName
        {
            get
            {
                return Value.ToString();
            }
        }
        #endregion Properties

        #region Methods

        public Year(int val = 0)
        {
            Value = val;
            AddChild();
            AddChild();
            AddChild();
            AddChild();
        }

        public Year()
        {
        }

        public override ITreeNode CreateNewChild()
        {
            return ChildNodes.Count < 12 ? new Month(ChildNodes.Count) : null;
        }
        #endregion Methods

        public override XElement ToXElement()
        {
            return new XElement("Item", new XElement(YEAR, Value));
        }

        public override bool FromXElement(XElement elem)
        {
            int val = -1;
            int.TryParse(elem.Elements(YEAR).FirstOrDefault().Value, out val);
            Value = val;
            return true;
        }
    }
}
