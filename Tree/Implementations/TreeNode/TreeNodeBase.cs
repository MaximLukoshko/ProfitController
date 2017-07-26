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
        public abstract ICollection<ITreeNode> ChildNodes { get; }
        #endregion Properties

        #region Methods
        public abstract bool AddNewCild();
     
        #endregion Methods
    }
}
