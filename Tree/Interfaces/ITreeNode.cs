using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree.Interfaces
{
    public interface ITreeNode
    {
        #region Properties

        string NodeName { get; }
        ICollection<IOrderLine> Orders { get; }
        IDictionary<string, object> Summary { get; }
        ICollection<ITreeNode> ChildNodes { get; }
        ITreeNode Parent { get; set; }
        bool CanHasChildren { get; }
        #endregion Properties

        #region Methods
        bool AddNewChild();
        bool RemoveThis();
        #endregion Methods
    }
}
