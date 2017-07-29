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

        public ITreeNode Root { get; set; }

        public TreeModel()
        {
            Root = new UndefinedTreeNode();
            Root.AddChild();
            Root.AddChild();
        }

        public bool RemoveOrderFromNode(ITreeNode node, IOrderLine orderLine)
        {
            return node != null ? node.RemoveOrder(orderLine) : false;
        }

        public bool AddOrderToNode(ITreeNode node)
        {
            return node != null ? node.AddOrder() : false;
        }

        public bool RemoveNode(ITreeNode node)
        {
            return node != null ? node.RemoveThis() : false; ;
        }

        public bool AddChildToNode(ITreeNode node, ITreeNode childNode = null)
        {
            if (node == null)
                return false;
            return node.AddChild(childNode);
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
