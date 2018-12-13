using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloud.TagsCloudVisualization;
using TagsCloud.WordPrework;

namespace TagsCloud
{
    public class Application
    {
        private readonly IWordAnalyzer wordAnalyzer;
        private readonly ITagCloudLayouter tagCloudLayouter;
        private readonly ITagsCloudVisualizer tagsCloudVisualizer;

        public Application(IWordAnalyzer wordAnalyzer, ITagCloudLayouter tagCloudLayouter, ITagsCloudVisualizer tagsCloudVisualizer)
        {
            this.wordAnalyzer = wordAnalyzer;
            this.tagCloudLayouter = tagCloudLayouter;
            this.tagsCloudVisualizer = tagsCloudVisualizer;
        }

        public void Run(Options options)
        {
            var tags = GetTags(options);
            DrawTagCloudAndSave(options, tags);
        }

        public List<Tag> GetTags(Options options)
        {
            var frequency = options.PartsToUse.Any()
                ? wordAnalyzer.GetSpecificWordFrequency(options.PartsToUse)
                : wordAnalyzer.GetWordFrequency(new HashSet<PartOfSpeech>(options.BoringParts));
            return tagCloudLayouter.GetTags(frequency);
        }

        public void DrawTagCloudAndSave(Options options, List<Tag> tags)
        {
            var bitmap = tagsCloudVisualizer.GetCloudVisualization(tags);
            var name = Path.GetFileName(options.File);
            var newName = Path.ChangeExtension(name, "jpg");
            bitmap.Save(newName, ImageFormat.Jpeg);
        }
    }
}
