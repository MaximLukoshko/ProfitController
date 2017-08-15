using System.Xml.Linq;

namespace Tree.Implementations.TreeNode.StaticNodes
{
    public class UndefinedTreeNode : TreeNodeBase
    {
        public override string NodeName
        {
            get { return "<...>"; }
        }

        public override XElement ToXElement()
        {
            return new XElement("Item");
        }

        public override bool FromXElement(XElement elem)
        {
            return true;
        }
    }
}
