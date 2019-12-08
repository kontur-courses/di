using System.Collections.Generic;
using System.Drawing;
using TagCloud.Infrastructure;
using TagCloud.TextReading;
using TagCloud.WordsPreparation;

namespace TagCloud
{
    public class TagCloudGenerator : ITagCloudGenerator
    {
        private PictureConfiguration pictureConfiguration;
        private readonly IWordsProvider wordsProvider;

        public TagCloudGenerator(PictureConfiguration pictureConfiguration, IWordsProvider wordsProvider)
        {
            this.pictureConfiguration = pictureConfiguration;
            this.wordsProvider = wordsProvider;
        }

        public Bitmap GetTagCloudBitmap()
        {
            var words = wordsProvider.GetWords();
            throw new System.NotImplementedException();
        }

    }
}
