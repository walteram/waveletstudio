using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using DiagramNet;

namespace WaveletStudio.Designer.Documents
{
    [Serializable]
    public class DocumentModel
    {   
        public DateTime CreatedAt { get; set; }

        public string Author { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public string ModifiedBy { get; set; }

        public Document Document { get; set; }

        public bool Saved
        {
            get { return _saved; }
            set
            {
                _saved = value;
                if (OnSaveChanged != null)
                {
                    OnSaveChanged();
                }
            }
        }

        public string Notes { get; set; }

        public long FileSize { get; set; }

        public delegate void OnSaveChangedEventHandler();

        [NonSerialized]
        public OnSaveChangedEventHandler OnSaveChanged;

        private bool _saved;

        public int BlockCount
        {
            get { return Document.ElementCount(); }
        }

        public int ConnectionCount
        {
            get { return Document.LinkCount(); }
        }

        public string FileSizeText()
        {
            if (FileSize == 0 || !Saved)
            {
                using (var stream = new MemoryStream())
                {
                    IFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, this);
                    FileSize = stream.Length;
                    stream.Close();                 
                }   
            }
            if (FileSize > 1024 * 1024)
            {
                return (FileSize / 1024f / 1024f).ToString("0.##") + " MB";
            }
            if (FileSize > 1024)
            {
                return (FileSize / 1024f).ToString("0.##") + " KB";
            }
            return FileSize + " bytes";
        }

        public string CanvasSize
        {
            get
            {
                var area = Document.GetArea();
                return string.Format("↔ {0}px | ↕ {1}px", area.Width, area.Height);
            }
        }

        public void Touch()
        {
            Saved = false;            
        }
    }
}
