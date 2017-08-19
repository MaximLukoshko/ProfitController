using Tree.Interfaces;

namespace DAOLayer.Interfaces
{
    public interface IDao
    {
        bool SaveModelToFile(ITreeModel model, string filename);
        bool LoadModelFromFile(ITreeModel model, string filename);
        bool SaveNodeToFile(ITreeNode node, string filename);
    }
}