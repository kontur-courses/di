using TagsCloudContainer.Interfaces;
using Application = Microsoft.Office.Interop.Word.Application;

namespace TagsCloudContainer 
{
    public class DocParser : IParser
    {
        private readonly char[] separators = { ' ', '\t', '\r', '\n' };

        public IEnumerable<string> Parse(string path)
        {
            if (!File.Exists(path))
                throw new ArgumentException("Passed file doesn't exist!");

            var application = new Application { Visible = false };
            var document = application.Documents.Open(path);

            foreach (var word in document.Range().Text
                .Split(separators, StringSplitOptions.RemoveEmptyEntries))
                yield return word;

            document.Close();
            application.Quit();
        }
    }
}
