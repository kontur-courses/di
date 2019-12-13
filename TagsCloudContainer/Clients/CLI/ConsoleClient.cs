using System;
using System.Drawing;
using CommandLine;
using TagsCloudContainer.Cloud;
using TagsCloudContainer.Savers;

namespace TagsCloudContainer.Clients.CLI
{
    public class ConsoleClient : BaseClient
    {
        private readonly string[] args;

        public ConsoleClient(
            string[] args,
            TagsCloudSettings settings,
            Func<TagsCloud> cloudFactory,
            IImageSaver saver) :
            base(settings, cloudFactory, saver)
        {
            this.args = args;
        }

        public override void Run()
        {
            Parser.Default.ParseArguments<Options>(args).WithParsed(Execute);
        }

        private void Execute(Options options)
        {
            Configure(options);
            var image = CreateTagsCloud(Settings);
            SaveTagsCloud(options.ImagePath, image);
        }

        private void Configure(Options options)
        {
            Settings.Distance = options.Distance;
            Settings.Delta = options.Delta;
            Settings.WordsPath = options.WordsPath;
            Settings.BoringWordsPath = options.BoringWordsPath;
            Settings.FontFamily = new FontFamily(options.Font);
            Settings.SizeFactor = options.SizeFactor;
            Settings.TextColor = Color.FromName(options.TextColor);
            Settings.FillColor = Color.FromName(options.FillColor);
            Settings.BorderColor = Color.FromName(options.BorderColor);
            Settings.BackgroundColor = Color.FromName(options.BackgroundColor);
        }
    }
}