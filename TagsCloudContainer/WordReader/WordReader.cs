using System.IO;

namespace TagsCloudContainer
{
    public class WordReader : IWordReader
    {
        private readonly string text;

        public WordReader(Setting setting)
        {
            using (var fileStream = File.Open(setting.Path, FileMode.Open))
            {
                var array = new byte[fileStream.Length];
                fileStream.Read(array, 0, array.Length);
                fileStream.Close();
                text = System.Text.Encoding.Default.GetString(array);
            }
        }

        public string[] ReadWords()
        {
            return ParsText();
        }

        private string[] ParsText() => text.Split('\n');
    }
}