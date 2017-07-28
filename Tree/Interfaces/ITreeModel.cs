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
        void InitModel(ITreeNode root);
    }
}
