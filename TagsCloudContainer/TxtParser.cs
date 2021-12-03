using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer 
{
    public class TxtParser : IParser
    {
        public IEnumerable<string> Parse(string path)
        {
            using (var reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                    yield return reader.ReadLine();
            }
        }
    }
}
