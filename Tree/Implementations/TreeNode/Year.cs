using System.Xml.Linq;
using Tree.Interfaces;
using System.Linq;


namespace Tree.Implementations.TreeNode
{
    public class Year : TreeNodeBase
    {
        private int Value { get; set; }

        public override string NodeName
        {
            get { return Value.ToString(); }
        }

        public Year(int val = 0)
        {
            Value = val;
        }

        public Year()
        {
        }

        public override ITreeNode CreateNewChild()
        {
            return ChildNodes.Count < 12 ? new Month(Value, ChildNodes.Count) : null;
        }

        public override XElement ToXElement()
        {
            return new XElement("Item", new XElement(StringConstants.Year, Value));
        }

        public override bool FromXElement(XElement elem)
        {
            var year = elem.Elements(StringConstants.Year).FirstOrDefault();
            if (year != null)
            {
                int val;
                int.TryParse(year.Value, out val);
                Value = val;
            }
            return true;
        }
    }
}
