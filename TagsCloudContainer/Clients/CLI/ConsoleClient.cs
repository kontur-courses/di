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
            TagsCloudSettings cloudSettings,
            Func<TagsCloudGenerator> cloudFactory,
            FileImageSaver saver) :
            base(cloudSettings, cloudFactory, saver)
        {
            this.args = args;
        }

        public override void Run()
        {
            Parser.Default.ParseArguments<Options>(args).WithParsed(Execute);
        }

        private void Execute(Options options)
        {
            ConfigureCloud(options);
            var image = CreateTagsCloud(CloudSettings);
            SaveTagsCloud(options.ImagePath, image);
        }

        private void ConfigureCloud(Options options)
        {
            CloudSettings.Distance = options.Distance;
            CloudSettings.Delta = options.Delta;
            CloudSettings.WordsPath = options.WordsPath;
            CloudSettings.BoringWordsPath = options.BoringWordsPath;
            CloudSettings.AffFile = options.AffPath;
            CloudSettings.DicFile = options.DicPath;
            CloudSettings.FontFamily = new FontFamily(options.Font);
            CloudSettings.SizeFactor = options.SizeFactor;
            CloudSettings.Painter = options.Painter;
            CloudSettings.Layouter = options.Layouter;
            CloudSettings.TextColor = Color.FromName(options.TextColor);
            CloudSettings.FillColor = Color.FromName(options.FillColor);
            CloudSettings.BorderColor = Color.FromName(options.BorderColor);
            CloudSettings.PrimaryColor = Color.FromName(options.PrimaryColor);
            CloudSettings.MajorityColor = Color.FromName(options.MajorityColor);
            CloudSettings.MinorityColor = Color.FromName(options.MinorityColor);
            CloudSettings.BackgroundColor = Color.FromName(options.BackgroundColor);
            if (options.Width.HasValue && options.Height.HasValue)
                CloudSettings.ImageSize = new Size(options.Width.Value, options.Height.Value);
        }
    }
}