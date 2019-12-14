using System.IO;
using TagsCloudContainer.Infrastructure.Common;

namespace TagsCloudContainer
{
    public class WordReaderFromFile : IWordReader
    {
        private readonly string text;

        public WordReaderFromFile(string path)
        {
            using (var fileStream = File.Open(path, FileMode.Open))
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