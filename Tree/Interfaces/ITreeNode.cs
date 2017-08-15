using System.Collections.Generic;
using System.Xml.Linq;

namespace Tree.Interfaces
{
    // !!! ATTENTION !!! //
    // Each class, that implements this interface must have public default constructor
    // !!! ATTENTION !!! //
    public interface ITreeNode
    {

        string NodeName { get; }
        ICollection<IOrderLine> Orders { get; }
        IDictionary<string, object> Summary { get; }
        ICollection<ITreeNode> ChildNodes { get; }
        ITreeNode Parent { get; set; }
        bool CanHasChildren { get; }
        bool IsExpanded { get; set; }

        ICollection<ITreeNode> AllChildren { get; }
        XElement ToXElement();
        bool FromXElement(XElement elem);
        ITreeNode CreateNewChild();
        bool AddChild(ITreeNode child = null);
        bool RemoveThis();
        bool AddOrder();
        bool RemoveOrder(IOrderLine orderLine);
    }
}
