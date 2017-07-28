using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tree.Interfaces;

namespace DAOLayer.Interfaces
{
    public interface IDAO
    {
        bool SaveModelToFile(ITreeModel model, string filename);
        bool LoadModelFromFile(ITreeModel model, string filename);
    }
}
