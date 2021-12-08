using System.IO;

namespace TagsCloudContainer.WordsLoading
{
    public class TxtFileTextLoader : TextFileLoader
    {
        protected override string LoadTextFromExistingFile(string filename) =>
            File.ReadAllText(filename);
    }
}