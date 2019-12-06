using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using TagsCloud.Visualization;
using TagsCloud.Visualization.Tag;
using TagsCloud.WordPreprocessing;

namespace TagsCloud
{
    public class Application
    {
        private readonly IWordAnalyzer wordAnalyzer;
        private readonly ILayouter layouter;
        private readonly IVisualizer visualizer;
        private readonly Options options;

        public Application(IWordAnalyzer wordAnalyzer, ILayouter layouter, IVisualizer visualizer, Options options)
        {
            this.wordAnalyzer = wordAnalyzer;
            this.layouter = layouter;
            this.visualizer = visualizer;
            this.options = options;
        }

        public void Run()
        {
            var tags = GetTags();
            CreateImageAndSave(tags);
        }

        private void CreateImageAndSave(IEnumerable<Tag> tags)
        {
            var bitmap = visualizer.GetCloudVisualization(tags);
            var name = Path.GetFileName(options.File);
            var jpgName = Path.ChangeExtension(name, "jpg");
            Console.WriteLine($"Your image located at:  {new FileInfo(jpgName).FullName}");
            bitmap.Save(jpgName, ImageFormat.Jpeg);
        }

        private IEnumerable<Tag> GetTags()
        {
            var statistics = wordAnalyzer.GetWordsStatistics();
            return layouter.GetTags(statistics);
        }
        
    }
}