using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree.Interfaces
{
    public interface ITreeModel
    {
        ICollection<ITreeNode> Nodes { get; }


        #region Methods
        bool AddChildToNode(ITreeNode node, ITreeNode childNode);
        bool RemoveNode(ITreeNode node);
        bool AddOrderToNode(ITreeNode node);
        bool RemoveOrderFromNode(ITreeNode node, IOrderLine orderLine);
        #endregion Methods
        void InitModel(ITreeNode root);
    }
}
