using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagsCloudContainer.Drawing;
using TagsCloudContainer.FileReaders;
using TagsCloudContainer.ImageSaver;
using TagsCloudContainer.Settings;
using TagsCloudContainer.WordFilters;
using TagsCloudContainer.WordsConverter;

namespace TagsCloudContainer
{
    public class TagCloudCreator
    {
        private readonly IImageSaver saver;
        private readonly IWordsFilter wordsFilter;
        private readonly IEnumerable<IFileReader> fileReaders;
        private readonly IDrawer cloudDrawer;
        private readonly IAppSettings appSettings;
        private readonly IWordConverter wordConverter;

        public TagCloudCreator(IImageSaver saver, IWordsFilter wordsFilter, IEnumerable<IFileReader> fileReaders,
            IDrawer cloudDrawer, IAppSettings appSettings, IWordConverter wordConverter)
        {
            this.saver = saver;
            this.wordsFilter = wordsFilter;
            this.fileReaders = fileReaders;
            this.cloudDrawer = cloudDrawer;
            this.appSettings = appSettings;
            this.wordConverter = wordConverter;
        }

        public void CreateTagCloudImage()
        {
            var words = GetProperFileReader()
                .ReadWordsFromFile(appSettings.InputPath)
                .Select(word => word.ToLower());
            var fileredWords = wordsFilter.Filter(words).ToList();
            var tags = wordConverter.ConvertWords(fileredWords);
            var image = cloudDrawer.DrawImage(tags);
            saver.Save(image, appSettings.ImagePath);
        }

        private IFileReader GetProperFileReader()
        {
            var inputFileFormat = Path.GetExtension(appSettings.InputPath);
            
            foreach (var reader in fileReaders)
            {
                if (reader.SupportedFormats.Contains(inputFileFormat))
                {
                    return reader;
                }
            }

            throw new ArgumentException($"{inputFileFormat} format is not supported");
        }
    }
}