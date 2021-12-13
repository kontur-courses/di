using TagsCloudContainer;
using TagsCloudContainer.BitmapSaver;
using TagsCloudContainer.FileReader;

namespace TagsCloud.Console
{
    public class ConsoleUI : IConsoleUI
    {
        private readonly IResolver<string, IFileReader> fileReadersResolver;
        private readonly IBitmapSaver saver;
        private readonly ITagCloud tagCloud;

        public ConsoleUI(IResolver<string, IFileReader> fileReadersResolver, IBitmapSaver saver, ITagCloud tagCloud)
        {
            this.fileReadersResolver = fileReadersResolver;
            this.saver = saver;
            this.tagCloud = tagCloud;
        }

        public void Run(IAppSettings appSettings, ITagCloudSettings tagCloudSettings)
        {
            var content = fileReadersResolver.Get(appSettings.InputPath).ReadWords(appSettings.InputPath);
            using var visualization = tagCloud.LayDown(content);
            saver.Save(visualization, appSettings.OutputPath);
        }
    }
}