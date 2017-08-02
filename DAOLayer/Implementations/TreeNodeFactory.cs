using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tree.BaseEnums;
using Tree.Implementations.TreeNode;
using Tree.Implementations.TreeNode.StaticNodes;
using Tree.Interfaces;

namespace DAOLayer.Implementations
{
    public class TreeNodeFactory
    {
        public static ITreeNode CreateTreeNode(string typeStr)
        {
            Type type = Type.GetType(typeStr);
            ITreeNode ret = null;
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
