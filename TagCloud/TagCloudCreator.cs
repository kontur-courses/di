using System.Collections.Generic;
using System.Drawing;

namespace TagCloud
{
    public class TagCloudCreator : ITagCloudCreator
    {
        private readonly IWordsForCloudGenerator wordsForCloudGenerator;
        private readonly IFileReader fileReader;
        private readonly IWordsNormalizer wordsNormalizer;
        private readonly ICloudDrawer cloudDrawer;

        public TagCloudCreator(IWordsForCloudGenerator wordsForCloudGenerator, IFileReader fileReader, IWordsNormalizer wordsNormalizer, ICloudDrawer cloudDrawer)
        {
            this.wordsNormalizer = wordsNormalizer;
            this.cloudDrawer = cloudDrawer;
            this.fileReader = fileReader;
            this.wordsForCloudGenerator = wordsForCloudGenerator;
        }

        public Bitmap GetCloud()
        {
            var words = fileReader.Get();
            var normalizedWords = wordsNormalizer.NormalizeWords(words);
            var wordsForCloud = wordsForCloudGenerator.Generate(normalizedWords);
            return cloudDrawer.DrawCloud(wordsForCloud);
        }
    }
}