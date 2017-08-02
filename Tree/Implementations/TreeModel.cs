﻿using System.Collections.Generic;
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
        }

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
    }
}
