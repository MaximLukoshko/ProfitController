using DAOLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Tree.Implementations.TreeNode;
using Tree.Implementations.TreeNode.StaticNodes;
using Tree.Interfaces;

namespace DAOLayer.Implementations
{
    public class DAO : IDAO
    {
        private const string CLASS_TYPE = @"ClassType";
        private const string CHILDREN = @"Children";
        public bool SaveModelToFile(ITreeModel model, string filename)
        {
            XDocument doc = new XDocument(NodeToXElement(model.Root));
            doc.Save(filename);
            return true;
        }

        public bool LoadModelFromFile(ITreeModel model, string filename)
        {
            var doc = XDocument.Load(filename);
            var root = NodeFromXElement(doc.Root);
            model.InitModel(root);
            return root != null;
        }


        public bool SaveNodeToFile(ITreeNode node, string filename)
        {
            var root = new UndefinedTreeNode();
            root.AddChild(node);
            XDocument doc = new XDocument(NodeToXElement(root));
            doc.Save(filename);
            return true;
        }

        private XElement NodeToXElement(ITreeNode node)
        {
            var ret = node.ToXElement();
            ret.Add(new XElement(CLASS_TYPE, TreeNodeFactory.GetTypeFromElement(node)));
            if (node.AllChildren.Count > 0)
            {
                var children = new XElement(CHILDREN);
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
            var ret = TreeNodeFactory.CreateTreeNode(element.Elements(CLASS_TYPE).FirstOrDefault().Value);
            ret.FromXElement(element);
            var childElements = element.Elements(CHILDREN);
            foreach(var ch in childElements.Elements())
                ret.AddChild(NodeFromXElement(ch));
            
            return ret;
        }
    }
}
