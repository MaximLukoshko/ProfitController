using System;
using Tree.Implementations.TreeNode.StaticNodes;
using Tree.Interfaces;

namespace DAOLayer.Implementations
{
    public class TreeNodeFactory
    {
        public static ITreeNode CreateTreeNode(string typeStr)
        {
            var type = Type.GetType(typeStr);
            ITreeNode ret;
            try
            {
                if (type == null)
                    throw new NullReferenceException(string.Format("Type '{0}' was not found", typeStr));

                ret = Activator.CreateInstance(type) as ITreeNode;
                if (ret == null)
                    throw new NullReferenceException(string.Format("Object of type '{0}' can not be created", typeStr));
            }
            catch (Exception)
            {
                ret = new UndefinedTreeNode();
            }

            return ret;
        }

        public static string GetTypeFromElement(ITreeNode elem)
        {
            return elem != null ? elem.GetType().AssemblyQualifiedName : string.Empty;
        }
    }
}
