using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tree.Implementations.TreeNode;
using Tree.Interfaces;

namespace Tree.Implementations
{
    public class TreeModel : ITreeModel
    {
        public ICollection<ITreeNode> Nodes
        {
            get 
            {
                return Root.ChildNodes;
            }
        }

        private ITreeNode Root { get; set; }

        public TreeModel()
        {
            Root = new UndefinedTreeNode();
            Root.AddChild();
            Root.AddChild();
        }

        public bool RemoveOrderFromNode(ITreeNode node, IOrderLine orderLine)
        {
            if(node != null)
                node.AddChild();
            return true;
        }

        public bool AddOrderToNode(ITreeNode node)
        {
            if (node != null)
                node.RemoveThis();
            return true;
        }

        public bool RemoveNode(ITreeNode node)
        {
            return true;
        }

        public bool AddChildToNode(ITreeNode node, ITreeNode childNode)
        {
            if (node == null)
                return false;
            childNode = childNode ?? node.CreateNewChild();
            return childNode != null ? node.AddChild(childNode) : false;
        }

        public void InitModel(ITreeNode root)
        {
            if(root !=null)
            {
                Root = null;
                Root = root;
            }
        }
    }
}
