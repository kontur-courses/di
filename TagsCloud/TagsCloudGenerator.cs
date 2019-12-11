using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using TagsCloud.Renderers;
using TagsCloud.FileParsers;
using TagsCloud.ImageSavers;
using TagsCloud.Layouters;
using TagsCloud.WordsFiltering;

namespace TagsCloud
{
    public class TagsCloudGenerator
    {
        private readonly IFileParser[] parsers;
        public List<string> Words { get; private set; }

        private readonly IFilter[] filters;
        public List<string> FilteredWords { get; private set; }

        public List<(string Tag, int Rate)> Tags { get; private set; }

        private List<LayoutItem> layoutItems;
        private readonly ITagsCloudLayouter layouter;
        private readonly ITagsCloudRenderer renderer;
        private readonly IImageSaver[] imageSavers;

        public TagsCloudGenerator(IFileParser[] parsers, IFilter[] filters, ITagsCloudLayouter layouter,
            ITagsCloudRenderer renderer, IImageSaver[] imageSavers)
        {
            this.parsers = parsers;
            this.filters = filters;
            this.layouter = layouter;
            this.renderer = renderer;
            this.imageSavers = imageSavers;
        }

        public TagsCloudGenerator GenerateCloud(string filename)
        {
            LoadWords(filename);
            FilterLoadedWords();
            DetermineTags();
            PrepareLayout();
            Render();
            return this;
        }

        private void LoadWords(string filename)
        {
            var fileExtension = Path.GetExtension(filename);
            var fileParser = parsers.FirstOrDefault(p =>
                p.FileExtensions.Any(ext => ext == fileExtension));
            if (fileParser == null)
                throw new ArgumentException($"Can't select file parser for this file format ({filename})");
            Words = fileParser.Parse(filename);
        }

        private void FilterLoadedWords()
        {
            FilteredWords = new List<string>(Words);
            foreach (var filter in filters)
                FilteredWords = filter.FilterFunc(FilteredWords);
        }

        private void DetermineTags()
        {
            Tags = new List<(string Tag, int Rate)>();
            foreach (var group in FilteredWords.GroupBy(word => word))
                Tags.Add((group.Key, group.Count()));
        }

        private void PrepareLayout()
        {
            layoutItems = Tags.ConvertAll(t => new LayoutItem(new Rectangle(Point.Empty, Size.Empty), t.Tag, t.Rate));
            renderer.CalcTagsRectanglesSizes(layoutItems);
            layouter.ReallocItems(layoutItems);
        }

        private void Render()
        {
            TagCloudImage = renderer.Render(layoutItems);
        }

        public Image TagCloudImage { get; private set; }

        public void SaveTo(string filename)
        {
            var fileExtension = Path.GetExtension(filename);
            var imageSaver = imageSavers.FirstOrDefault(p =>
                p.FileExtensions.Any(ext => ext == fileExtension));
            if (imageSaver == null)
                throw new ArgumentException($"Can't select file saver for this file format ({filename})");
            imageSaver.Save(TagCloudImage, filename);
        }
    }
}
