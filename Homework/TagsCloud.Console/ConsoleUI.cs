﻿using System.IO;
using TagsCloudContainer;
using TagsCloudContainer.BitmapSaver;
using TagsCloudContainer.FileReader;
using TagsCloudContainer.Visualizer;
using TagsCloudContainer.WordsFilter;
using TagsCloudContainer.WordsFrequencyAnalyzers;

namespace TagsCloud.Console
{
    public class ConsoleUI : IConsoleUI
    {
        private ITagCloud tagCloud;
        private readonly IResolver<string, IFileReader> fileReadersResolver;
        private readonly IBitmapSaver saver;

        public ConsoleUI(IResolver<string, IFileReader> fileReadersResolver, IBitmapSaver saver, ITagCloud tagCloud)
        {
            this.fileReadersResolver = fileReadersResolver;
            this.saver = saver;
            this.tagCloud = tagCloud;
        }

        public void Run(IAppSettings appSettings, ITagCloudSettings tagCloudSettings)
        {
            var content = fileReadersResolver.Get(appSettings.InputPath).ReadWords(appSettings.InputPath);
            using var visualization = tagCloud.LayDown(content, tagCloudSettings);
            saver.Save(visualization, appSettings.OutputPath);
        }
    }
}