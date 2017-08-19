using Tree.Interfaces;

namespace ProfitController
{
    public class TreeNodeWrapper
    {
        public TreeNodeWrapper(ITreeNode node)
        {
            Source = node;
        }

        public ITreeNode Source { get; private set; }

        public override string ToString()
        {
            var margin = string.Empty;
            for (var n = Source.Parent; n.Parent != null; n = n.Parent)
                margin = margin + "   ";
            var expander = Source.ChildNodes.Count > 0 ? Source.IsExpanded ? "-" : "+" : " ";
            return string.Format("{0}{1}{2}", margin, expander, Source.NodeName);
        }
    }
}
