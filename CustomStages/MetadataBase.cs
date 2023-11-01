using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace diorama
{
    internal class MetadataBase
    {
        public string Title { get; set; } = "N/A";
        public string Version { get; set; } = "N/A";
        public string Author { get; set; } = "N/A";
        public string Description { get; set; } = "N/A";
        public string Color { get; set; } = "1;1;1";
        public string StageLocation { get; set; } = "N/A";
        public string AssetBundle { get; set; } = "N/A";
        public int GamebananaID { get; set; } = -1;
        public string Location { get; set; }

        public static T Load<T>(string path) where T : MetadataBase
        {
            T t;

            try
            {
                t = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
                t.Location = Path.GetDirectoryName(path);
            }
            catch (JsonReaderException exception)
            {
                t = default(T);
                t.Title = Path.GetFileName(Path.GetDirectoryName(path));
            }

            return t;
        }

        public static MetadataBase Load(string path)
        {
            return MetadataBase.Load<MetadataBase>(path);
        }
    }
}
