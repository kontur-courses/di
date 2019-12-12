using System;
using System.Drawing;
using CommandLine;
using TagsCloudContainer.Savers;

namespace TagsCloudContainer.Clients.CLI
{
    public class ConsoleClient : BaseClient
    {
        private readonly string[] args;

        public ConsoleClient(
            string[] args,
            Func<TagsCloudSettings> settingsFactory,
            Func<TagsCloud> cloudFactory,
            IImageSaver saver) :
            base(settingsFactory, cloudFactory, saver)
        {
            this.args = args;
        }

        public override void Run()
        {
            Parser.Default.ParseArguments<Options>(args).WithParsed(Execute);
        }

        private void Execute(Options options)
        {
            var settings = Configure(options);
            var image = CreateTagsCloud(settings);
            SaveTagsCloud(options.ImagePath, image);
        }

        private TagsCloudSettings Configure(Options options)
        {
            var settings = SettingsFactory();
            settings.Distance = options.Distance;
            settings.Delta = options.Delta;
            settings.WordsPath = options.WordsPath;
            settings.BoringWordsPath = options.BoringWordsPath;
            settings.FontFamily = new FontFamily(options.Font);
            settings.SizeFactor = options.SizeFactor;
            settings.TextColor = Color.FromName(options.TextColor);
            settings.FillColor = Color.FromName(options.FillColor);
            settings.BorderColor = Color.FromName(options.BorderColor);
            settings.BackgroundColor = Color.FromName(options.BackgroundColor);
            return settings;
        }
    }
}