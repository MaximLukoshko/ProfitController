using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tree.Interfaces;

namespace Tree.Implementations.TreeNode
{
    public abstract class TreeNodeBase : ITreeNode
    {
        #region Properties
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
                throw new NotImplementedException();
            } 
        }
        public virtual ICollection<ITreeNode> ChildNodes 
        { 
            get
            {
                return _childNodes;
            } 
        }

        public bool CanHasChildren { get; set; }
        #endregion Properties

        protected ICollection<ITreeNode> _childNodes = new List<ITreeNode>();
        public virtual ITreeNode Parent { get; set; }
        #region Methods
        public virtual ITreeNode CreateNewChild()
        {
            return null;
        }
     
        public bool RemoveThis()
        {
            var parent = this.Parent;
            return parent != null ? parent.ChildNodes.Remove(this) : false;
        }
        #endregion Methods


        public virtual bool AddNewChild()
        {
            var child = CreateNewChild();

            if (child != null)
            {
                child.Parent = this;
                _childNodes.Add(child);
            }
            
            return child != null;
        }
    }
}
