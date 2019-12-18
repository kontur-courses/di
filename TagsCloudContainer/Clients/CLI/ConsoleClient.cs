using System;
using System.Collections.Generic;
using System.Drawing;
using CommandLine;
using TagsCloudContainer.Cloud;
using TagsCloudContainer.Savers;
using TagsCloudContainer.Visualization.Painters;

namespace TagsCloudContainer.Clients.CLI
{
    public class ConsoleClient : BaseClient
    {
        private static readonly IDictionary<string, Type> Services;

        private readonly string[] args;

        static ConsoleClient()
        {
            Services = new Dictionary<string, Type>()
            {
                ["constant"] = typeof(ConstantColorsPainter),
                ["stepped"] = typeof(SteppedColorPainter)
            };
        }

        public ConsoleClient(
            string[] args,
            TagsCloudSettings cloudSettings,
            ServiceSettings serviceSettings,
            Func<TagsCloudGenerator> cloudFactory,
            FileImageSaver saver) :
            base(cloudSettings, serviceSettings, cloudFactory, saver)
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
            ConfigureService(options);
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
            CloudSettings.TextColor = Color.FromName(options.TextColor);
            CloudSettings.FillColor = Color.FromName(options.FillColor);
            CloudSettings.BorderColor = Color.FromName(options.BorderColor);
            CloudSettings.PrimaryColor = Color.FromName(options.PrimaryColor);
            CloudSettings.MajorityColor = Color.FromName(options.MajorityColor);
            CloudSettings.MinorityColor = Color.FromName(options.MinorityColor);
            CloudSettings.BackgroundColor = Color.FromName(options.BackgroundColor);
        }

        private void ConfigureService(Options options)
        {
            ServiceSettings.SetService<IPainter>(Services[options.Painter]);
        }
    }
}