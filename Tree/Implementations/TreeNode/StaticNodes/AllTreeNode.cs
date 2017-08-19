using System;
using System.Xml.Linq;
using Tree.Interfaces;

namespace Tree.Implementations.TreeNode.StaticNodes
{
    public class AllTreeNode : TreeNodeBase
    {
        #region TreeNodeBase

        public override string NodeName
        {
            get { return "Все"; }
        }

        public override ITreeNode CreateNewChild()
        {
            return new Year(DateTime.Now.Year);
        }

        public override bool RemoveThis()
        {
            return false;
        }

        public override XElement ToXElement()
        {
            return new XElement("Item");
        }

        public override bool FromXElement(XElement elem)
        {
            return true;
        }

        #endregion TreeNodeBase
    }
}
