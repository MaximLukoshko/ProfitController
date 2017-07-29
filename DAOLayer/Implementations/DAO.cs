using DAOLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Tree.Interfaces;

namespace DAOLayer.Implementations
{
    public class DAO : IDAO
    {
        public bool SaveModelToFile(ITreeModel model, string filename)
        {
            XDocument doc = new XDocument(new XElement("body",
                                           new XElement("level1",
                                               new List<XElement>{ new XElement("level2", "text"),
                                               new XElement("level2", "other text")})));
            doc.Save( @"Test.xml" );
            
            return true;
        }

        public bool LoadModelFromFile(ITreeModel model, string filename)
        {
            var doc = new XmlDocument();
            doc.Load(@"Test.xml");

            return true;
        }


        public bool SaveNodeToFile(ITreeNode node, string filename)
        {
            throw new NotImplementedException();
        }
    }
}
