using MystemHandler;
using TagCloudContainer.Interfaces;

namespace TagCloudContainer.Parsers
{
    public class FileLinesParser : IFileParser
    {
        public IEnumerable<string> Parse(string text, bool filterBoring)
        {
            if (!filterBoring)
                return text.Split(Environment.NewLine);

            MystemMultiThread mystem = new(1, @"mystem.exe");
            var words = mystem.StemWords(text)!
                .Where(l => !l.IsSlug)
                .Select(l => l.Lemma);
            return words;
        }
    }
}
