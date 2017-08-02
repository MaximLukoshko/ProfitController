using System.Xml.Linq;
using Tree.Interfaces;
using System.Linq;


namespace Tree.Implementations.TreeNode
{

    public class Year : TreeNodeBase
    {
        private const string YEAR = @"Year";
        private int Value { get; set; }
        public override string NodeName
        {
            get
            {
                return Value.ToString();
            }
        }

        public Year(int val = 0)
        {
            Value = val;
            AddChild();
            AddChild();
            AddChild();
            AddChild();
        }

        public Year()
        {
        }

        public override ITreeNode CreateNewChild()
        {
            return ChildNodes.Count < 12 ? new Month(ChildNodes.Count) : null;
        }

        public override XElement ToXElement()
        {
            return new XElement("Item", new XElement(YEAR, Value));
        }

        public override bool FromXElement(XElement elem)
        {
            var year = elem.Elements(YEAR).FirstOrDefault();
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
