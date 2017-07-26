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
            Root.ChildNodes.Add(new Year(2016));
            Root.ChildNodes.Add(new Year(2017));
        }
    }
}
