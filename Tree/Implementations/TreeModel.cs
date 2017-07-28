﻿using System;
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
