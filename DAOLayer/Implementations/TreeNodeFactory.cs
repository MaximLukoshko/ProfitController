using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tree.BaseEnums;
using Tree.Implementations.TreeNode;
using Tree.Interfaces;

namespace DAOLayer.Implementations
{
    public class TreeNodeFactory
    {
        public static ITreeNode CreateTreeNode(string typeStr)
        {
            Type type = Type.GetType(typeStr);
            var ret = Activator.CreateInstance(type) as ITreeNode;
            return ret ?? new UndefinedTreeNode();
        }

        public static string GetTypeFromElement(ITreeNode elem)
        {
            return elem != null ? elem.GetType().AssemblyQualifiedName : string.Empty;
        }
    }
}
