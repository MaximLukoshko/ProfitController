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
        #endregion Properties

        #region Methods
        bool AddNewCild();
//        bool RemoveThis();
        #endregion Methods
    }
}
