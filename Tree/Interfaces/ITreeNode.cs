using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tree.BaseEnums;

namespace Tree.Interfaces
{
    // !!! ATTENTION !!! //
    // Each class, that implements this interface must have public default constructor
    // !!! ATTENTION !!! //
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
        ICollection<ITreeNode> AllChildren { get; }
        XElement ToXElement();
        bool FromXElement(XElement elem);
        ITreeNode CreateNewChild();
        bool AddChild(ITreeNode child = null);
        bool RemoveThis();
        bool AddOrder();
        bool RemoveOrder(IOrderLine orderLine);
        #endregion Methods
    }
}
