using System.Drawing;
using TagCloud.CloudLayouter;
using TagCloud.WordsPreprocessing;

namespace TagCloud.CloudVisualizer
{
    class CloudVisualizer
    {
        private WordsAnalyzer wordsAnalyzer;
        private ICloudLayouter cloudLayouter;
        private CloudViewConfiguration.CloudViewConfiguration cloudViewConfiguration; 

        public CloudVisualizer(WordsAnalyzer wordsAnalyzer,
            ICloudLayouter cloudLayouter,
            CloudViewConfiguration.CloudViewConfiguration cloudViewConfiguration)
        {
            this.wordsAnalyzer = wordsAnalyzer;
            this.cloudViewConfiguration = cloudViewConfiguration;
            this.cloudLayouter = cloudLayouter;
        }

        public Bitmap GetCloud(Word[] words, int count)
        {
            return null;
        }
    }
}
