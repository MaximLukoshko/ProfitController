using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
        ITreeNode CreateNewChild();
        bool AddChild(ITreeNode child = null);
        bool RemoveThis();
        bool AddOrder();
        bool RemoveOrder(IOrderLine orderLine);
        XDocument ToXDocument();
        bool FromXDocument(XDocument doc);
        #endregion Methods
    }
}
