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
        private readonly IRenderOptions renderOptions;
        private readonly IEnumParser enumParser;

        public FileWordsProvider(
            IWordsParser wordParser,
            IFileTextLoaderResolver loaderResolver,
            IRenderOptions renderOptions,
            IEnumParser enumParser)
        {
            this.wordParser = wordParser;
            this.loaderResolver = loaderResolver;
            this.renderOptions = renderOptions;
            this.enumParser = enumParser;
        }

        public IEnumerable<string> GetWords()
        {
            var filename = renderOptions.InputPath;
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