using System.Collections.Generic;
using System.Linq;
using TagsCloudApp.Parsers;
using TagsCloudApp.RenderCommand;

namespace TagsCloudApp.WordsLoading
{
    public class FileWordsProvider : IWordsProvider
    {
        private readonly IWordsParser wordParser;
        private readonly IFileTextLoaderResolver loaderResolver;
        private readonly IRenderArgs renderArgs;
        private readonly IEnumParser enumParser;

        public FileWordsProvider(
            IWordsParser wordParser,
            IFileTextLoaderResolver loaderResolver,
            IRenderArgs renderArgs,
            IEnumParser enumParser)
        {
            this.wordParser = wordParser;
            this.loaderResolver = loaderResolver;
            this.renderArgs = renderArgs;
            this.enumParser = enumParser;
        }

        public IEnumerable<string> GetWords()
        {
            var filename = renderArgs.InputPath;
            var fileType = GetFileType(filename);
            var fileTextLoader = loaderResolver.GetFileTextLoader(fileType);
            var text = fileTextLoader.LoadText(filename);
            return wordParser.Parse(text);
        }

        private FileType GetFileType(string filename)
        {
            var fileExtension = filename.Split('.').Last();
            return enumParser.Parse<FileType>(fileExtension);
        }
    }
}