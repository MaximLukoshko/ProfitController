using DAOLayer.Interfaces;
using System.Linq;
using System.Xml.Linq;
using Tree;
using Tree.Implementations.TreeNode.StaticNodes;
using Tree.Interfaces;

namespace DAOLayer.Implementations
{
    public class Dao : IDao
    {
        public bool SaveModelToFile(ITreeModel model, string filename)
        {
            if (string.IsNullOrEmpty(filename))
                return false;
            var doc = new XDocument(NodeToXElement(model.Root));
            doc.Save(filename);
            return true;
        }

        public bool LoadModelFromFile(ITreeModel model, string filename)
        {
            if (string.IsNullOrEmpty(filename))
                return false;
            var doc = XDocument.Load(filename);
            var root = NodeFromXElement(doc.Root);
            model.InitModel(root);
            return root != null;
        }


        public bool SaveNodeToFile(ITreeNode node, string filename)
        {
            if (string.IsNullOrEmpty(filename))
                return false;
            var root = new UndefinedTreeNode();
            root.AddChild(node);
            XDocument doc = new XDocument(NodeToXElement(root));
            doc.Save(filename);
            return true;
        }

        private XElement NodeToXElement(ITreeNode node)
        {
            var ret = node.ToXElement();
            ret.Add(new XElement(StringConstants.ClassType, TreeNodeFactory.GetTypeFromElement(node)));
            if (node.AllChildren.Count > 0)
            {
                var children = new XElement(StringConstants.Children);
                foreach (var child in node.AllChildren)
                {
                    children.Add(NodeToXElement(child));
                }
                ret.Add(children);
            }

            return ret;
        }

        private ITreeNode NodeFromXElement(XElement element)
        {
            // ReSharper disable once PossibleNullReferenceException
            var ret = TreeNodeFactory.CreateTreeNode(element.Elements(StringConstants.ClassType).FirstOrDefault().Value);
            ret.FromXElement(element);
            var childElements = element.Elements(StringConstants.Children);
            foreach (var ch in childElements.Elements())
                ret.AddChild(NodeFromXElement(ch));

            return ret;
        }
    }
}
