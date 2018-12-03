using System.IO;

namespace TagsCloudContainer.SourceTextReaders
{
    public class TxtSourceTextReader : ISourceTextReader
    {
        public string[] ReadText(string filePath)
        {
//            var encoding = Encoding.GetEncoding(Encoding.Default);
//            using (var sr = new StreamReader(filePath, Encoding.UTF8))
//            {
//                return sr.ReadToEnd().Split('\n');
//            }
                return File.ReadAllLines(filePath);
        }

    }
}
