namespace Tree.Interfaces
{
    public interface IOrder : ITreeNode, IOrderLine
    {
        IOrderLine Order { get; }
    }
}
