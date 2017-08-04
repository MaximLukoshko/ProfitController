using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Tree.Interfaces;

namespace Tree.Implementations.TreeNode
{
    public abstract class TreeNodeBase : ITreeNode
    {
        protected TreeNodeBase()
        {
            AllChildren = new List<ITreeNode>();
        }

        public abstract string NodeName { get; }

        public virtual ICollection<IOrderLine> Orders
        {
            get
            {
                var ret = new List<IOrderLine>();
                foreach (var ord in ChildNodes)
                    ret.AddRange(ord.Orders);
                return ret;
            }
        }

        public virtual IDictionary<string, object> Summary
        {
            get
            {
                var ret = new Dictionary<string, object>();
                ret.Add("Чистая прибыль", Orders.Sum(ordLine => ordLine.Income - ordLine.Outgo));
                return ret;
            }
        }

        public virtual ICollection<ITreeNode> ChildNodes
        {
            get { return AllChildren.Where(child => child.CanHasChildren).ToList(); }
        }

        public virtual bool CanHasChildren
        {
            get { return true; }
        }

        public ICollection<ITreeNode> AllChildren { get; private set; }
        public virtual ITreeNode Parent { get; set; }

        public virtual ITreeNode CreateNewChild()
        {
            return null;
        }

        public virtual bool RemoveThis()
        {
            var parent = Parent;
            return parent != null && parent.AllChildren.Remove(this);
        }

        public bool AddChild(ITreeNode child = null)
        {
            child = child ?? CreateNewChild();

            if (child != null)
            {
                child.Parent = this;
                AllChildren.Add(child);
            }

            return child != null;
        }

        public virtual bool AddOrder()
        {
            return false;
        }

        public virtual bool RemoveOrder(IOrderLine orderLine)
        {
            var order = orderLine as IOrder;
            if (order != null)
            {
                if (AllChildren.Contains(order))
                    return AllChildren.Remove(order);

                return AllChildren.Any(ch => ch.RemoveOrder(orderLine));
            }
            return false;
        }

        public abstract XElement ToXElement();
        public abstract bool FromXElement(XElement elem);
    }
}
