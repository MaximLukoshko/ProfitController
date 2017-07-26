using System.Collections.Generic;
using Tree.Interfaces;

namespace Tree.Implementations.TreeNode
{

    public class Year : TreeNodeBase
    {
        #region Properties

        private IList<Month> Monthes { get; set; }

        private int Value { get; set; }
        public override string NodeName
        {
            get
            {
                return Value.ToString();
            }
        }
        public override ICollection<ITreeNode> ChildNodes
        {
            get 
            {
                return new List<ITreeNode>(Monthes);
            }
        }
        #endregion Properties

        #region Methods

        public Year(int val = 0)
        {
            Value = val;
            Monthes = new List<Month> {new Month(0), new Month(1), new Month(2)};
        }

        public override ITreeNode CreateNewChild()
        {
            return Monthes.Count < 12 ? new Month(Monthes.Count) : null;
        }
        #endregion Methods
    }
}
