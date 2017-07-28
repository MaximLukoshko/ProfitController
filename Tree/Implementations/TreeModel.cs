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
            Root.AddNewChild();
            Root.AddNewChild();
        }

        public bool RemoveOrderFromNode(ITreeNode node, IOrderLine orderLine)
        {
            if(node != null)
                node.AddNewChild();
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

        public bool AddChildToNode(ITreeNode node)
        {
            return true;
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
