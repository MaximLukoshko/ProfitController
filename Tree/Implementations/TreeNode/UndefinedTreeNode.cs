using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tree.Interfaces;

namespace Tree.Implementations.TreeNode
{
    public class UndefinedTreeNode : TreeNodeBase
    {
        public override string NodeName
        {
            get 
            { 
                return "<...>"; 
            }
        }

        public override ITreeNode CreateNewChild()
        {
            return new Year(2016);
        }
    }
}
