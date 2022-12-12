using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.App.Layouter;

namespace TagsCloudContainer.App.Actions
{
    public class TagsLayouterAction : IUiAction
    {
        private TagsLayouter tagsLayouter;
        private ITagsPainter[] tagsPainter;
        private readonly IImageDirectoryProvider imageDirectoryProvider;
        private readonly ITextReader textReader;
        private readonly ImageSettings imageSettings;

        public TagsLayouterAction(TagsLayouter tagsLayouter, ITagsPainter[] tagsPainter,
            IImageDirectoryProvider imageDirectoryProvider, ITextReader textReader, 
            ImageSettings imageSettings)
        {
            this.tagsLayouter = tagsLayouter;
            this.tagsPainter = tagsPainter;
            this.imageDirectoryProvider = imageDirectoryProvider;
            this.textReader = textReader;
            this.imageSettings = imageSettings;
        }

        public string Category => "Облако тегов";
        public string Name => "Облако тегов";
        public string Description => "Облако тегов";

        public void Perform()
        {
            var dialog = new OpenFileDialog()
            {
                CheckFileExists = false,
                InitialDirectory = Path.GetFullPath(imageDirectoryProvider.ImagesDirectory),
                Filter = textReader.Filter
            };
            var res = dialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                var allText = textReader.ReadText(dialog.FileName);
                var tags = tagsLayouter.PutAllTags(allText);
                tagsPainter
                    .FirstOrDefault(t => t.CanPaint(imageSettings.PainterType))?
                    .Paint(tags);
            }
        }
    }
}