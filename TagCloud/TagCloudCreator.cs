using System.Collections.Generic;
using System.Drawing;
using TagCloud.Interfaces;

namespace TagCloud
{
    public class TagCloudCreator : ITagCloudCreator
    {
        private readonly IWordsForCloudGenerator wordsForCloudGenerator;
        private readonly IWordsReader wordsReader;
        private readonly IWordsNormalizer wordsNormalizer;
        private readonly ICloudDrawer cloudDrawer;

        public TagCloudCreator(IWordsForCloudGenerator wordsForCloudGenerator, IWordsReader wordsReader, IWordsNormalizer wordsNormalizer, ICloudDrawer cloudDrawer)
        {
            this.wordsNormalizer = wordsNormalizer;
            this.cloudDrawer = cloudDrawer;
            this.wordsReader = wordsReader;
            this.wordsForCloudGenerator = wordsForCloudGenerator;
        }

        public Bitmap GetCloud()
        {
            var words = wordsReader.Get();
            var normalizedWords = wordsNormalizer.NormalizeWords(words);
            var wordsForCloud = wordsForCloudGenerator.Generate(normalizedWords);
            return cloudDrawer.DrawCloud(wordsForCloud);
        }
    }
}