using DAOLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOLayer.Implementations
{
    public class DAO : IDAO
    {
        public bool SaveModelToFile(Tree.Interfaces.ITreeModel model, string filename)
        {
            throw new NotImplementedException();
        }

        public bool LoadModelFromFile(Tree.Interfaces.ITreeModel model, string filename)
        {
            throw new NotImplementedException();
        }
    }
}
