using System.Collections.Generic;

namespace Tree.Interfaces
{
    public interface ITreeModel
    {
        ICollection<ITreeNode> Nodes { get; }
        ITreeNode Root { get;}

        bool AddChildToNode(ITreeNode node, ITreeNode childNode = null);
        bool RemoveNode(ITreeNode node);
        bool AddOrderToNode(ITreeNode node);
        bool RemoveOrderFromNode(ITreeNode node, IOrderLine orderLine);
        void InitModel(ITreeNode root);
    }
}
