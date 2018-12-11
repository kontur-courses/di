using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace TagsCloudContainer
{
    public class TextSourceFile : ISource
    {
        private readonly string filename;

        public TextSourceFile(string filename)
        {
            this.filename = filename;
        }

        public string[] GetWords()
        {
            if (filename == null)
                return new string[0];
            var text = File.ReadAllText(filename);
            var matches = Regex.Matches(text, @"\b[\w']+\b");

            var words = from m in matches.Cast<Match>()
                        select m.Value;

            return words.ToArray();
        }
    }
}
