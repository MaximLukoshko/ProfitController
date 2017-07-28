using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree.Interfaces
{
    public interface IOrder : ITreeNode, IOrderLine
    {
        IOrderLine Order { get; }
        //void InitFromOrderVine(IOrderLine orderLine);
    }
}
