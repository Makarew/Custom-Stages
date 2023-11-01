using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace diorama
{
    internal class MetadataMusic
    {
        public string SoundBankID { get; set; } = "N/A";
        public int SongCount { get; set; } = 0;
        public string SongOneTitle { get; set; } = "N/A";
        public string SongOneStateName { get; set; } = "N/A";
        public string SongOneColor { get; set; } = "1;1;1";
        public string SongTwoTitle { get; set; } = "N/A";
        public string SongTwoStateName { get; set; } = "N/A";
        public string SongTwoColor { get; set; } = "1;1;1";
        public string SongThreeTitle { get; set; } = "N/A";
        public string SongThreeStateName { get; set; } = "N/A";
        public string SongThreeColor { get; set; } = "1;1;1";
        public string SongFourTitle { get; set; } = "N/A";
        public string SongFourStateName { get; set; } = "N/A";
        public string SongFourColor { get; set; } = "1;1;1";
        public string Location { get; set; }

        public static T Load<T>(string path) where T : MetadataMusic
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
                //t.Title = Path.GetFileName(Path.GetDirectoryName(path));
            }

            return t;
        }

        public static MetadataMusic Load(string path)
        {
            return MetadataMusic.Load<MetadataMusic>(path);
        }
    }
}
