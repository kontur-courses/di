using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer 
{
    public class TxtParser : IParser
    {
        public IEnumerable<string> Parse(string path)
        {
            if (!File.Exists(path))
                throw new ArgumentException();
            using (var reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                    yield return reader.ReadLine();
            }
        }
    }
}
