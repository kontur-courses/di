using System.Collections.Generic;
using System.IO;

namespace TagsCloudApp.WordsLoading
{
    public class TxtFileTextLoader : TextFileLoader
    {
        public override IEnumerable<FileType> SupportedTypes => new[] {FileType.Txt};

        protected override string LoadTextFromExistingFile(string filename) =>
            File.ReadAllText(filename);
    }
}