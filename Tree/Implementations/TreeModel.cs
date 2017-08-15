using System.Collections.Generic;
using Tree.Implementations.TreeNode.StaticNodes;
using Tree.Interfaces;

namespace Tree.Implementations
{
    public class TreeModel : ITreeModel
    {
        #region Constructors

        public TreeModel()
        {
            Root = new UndefinedTreeNode();
            Root.AddChild(new AllTreeNode());
        }

        #endregion Constructors

        #region ITreeModel

        public ICollection<ITreeNode> Nodes
        {
            get { return Root.ChildNodes; }
        }

        public ITreeNode Root { get; set; }

        public bool RemoveOrderFromNode(ITreeNode node, IOrderLine orderLine)
        {
            return node != null && node.RemoveOrder(orderLine);
        }

        public bool AddOrderToNode(ITreeNode node)
        {
            return node != null && node.AddOrder();
        }

        public bool RemoveNode(ITreeNode node)
        {
            return node != null && node.RemoveThis();
        }

        public bool AddChildToNode(ITreeNode node, ITreeNode childNode = null)
        {
            if (node == null)
                return false;
            return node.AddChild(childNode);
        }

        public void InitModel(ITreeNode root)
        {
            Root = root ?? Root;
        }

        #endregion ITreeModel

        #region Object

#pragma warning disable 659
        public override bool Equals(object obj)
#pragma warning restore 659
        {
            var cmpObj = obj as TreeModel;
            return cmpObj != null && Root.Equals(cmpObj.Root);
        }

        #endregion Object
    }
}
