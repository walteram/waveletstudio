using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using DiagramNet;
using WaveletStudio.Designer.Resources;

namespace WaveletStudio.Designer.Documents
{
    public class DocumentSerializer
    {
        public void Save(DocumentModel document, string filename)
        {
            using (var stream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                document.ModifiedAt = DateTime.Now;
                document.ModifiedBy = new CurrentUserDiscoverer().Discover();

                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, document);
                stream.Close();
                document.Saved = true;
            }
        }

        public DocumentModel Load(string filename)
        {
            var fileInfo = new FileInfo(filename);
            DocumentModel documentModel;
            try
            {
                documentModel = Load<DocumentModel>(filename);
            }
            catch (Exception)
            {             
                //Deal with old document format
                var document = Load<Document>(filename);
                documentModel = new DocumentModel
                {
                    Document = document,
                    CreatedAt = fileInfo.CreationTime,
                    Author = DesignerResources.Unknown,
                    ModifiedAt = fileInfo.LastWriteTime,
                    ModifiedBy = DesignerResources.Unknown,
                };
            }
            documentModel.FileSize = fileInfo.Length;
            documentModel.Saved = true;
            return documentModel;
        }        

        private static T Load<T>(string filename)
        {
            using (Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                IFormatter formatter = new BinaryFormatter();
                var document = (T)formatter.Deserialize(stream);
                stream.Close();
                return document;
            }
        }
    }
}
