using System.Collections.Generic;
using Tree.Interfaces;

namespace Tree.Implementations.TreeNode
{

    public class Year : TreeNodeBase
    {
        #region Properties
        private int Value { get; set; }
        public override string NodeName
        {
            get
            {
                return Value.ToString();
            }
        }
        #endregion Properties

        #region Methods

        public Year(int val = 0)
        {
            Value = val;
            AddNewChild();
            AddNewChild();
            AddNewChild();
            AddNewChild();
        }

        public override ITreeNode CreateNewChild()
        {
            return ChildNodes.Count < 12 ? new Month(ChildNodes.Count) : null;
        }
        #endregion Methods
    }
}
