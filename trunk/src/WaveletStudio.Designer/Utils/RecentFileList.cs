using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace WaveletStudio.Designer.Utils
{
    [Serializable]
    internal class RecentFileList : List<RecentFile>
    {
        public static RecentFileList ReadFromFile()
        {
            var filename = GetFileName();
            if (!File.Exists(filename))
            {
                return new RecentFileList();
            }
            RecentFileList list;
            using (var stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var formatter = new BinaryFormatter();
                list = (RecentFileList)formatter.Deserialize(stream);
                stream.Close();
            }
            for (var i = list.Count-1; i >= 0; i--)
            {
                if (!File.Exists(list[i].FilePath))
                {
                    list.RemoveAt(i);
                }
            }
            return list;
        }

        public void SaveToFile()
        {
            using (var stream = new FileStream(GetFileName(), FileMode.Create, FileAccess.Write, FileShare.None))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, this);                
                stream.Close();
            }
        }

        private static string GetFileName()
        {
            return Path.Combine(WaveletStudio.Utils.AssemblyDirectory, "Recents.dat");
        }
    }

    [Serializable]
    internal class RecentFile
    {
        public string FilePath { get; set; }

        public DateTime DateAdded { get; set; }

        public Image Thumbnail { get; set; }
    }
}
