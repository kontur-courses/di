using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CircularCloudLayouter;
using TagsCloudForm.Actions;
using TagsCloudForm.CircularCloudLayouterSettings;
using TagsCloudForm.Common;
using TagsCloudForm.WordFilters;

namespace TagsCloudForm.CloudPainters
{
    public class CloudWithWordsPainterFactory : IPainterFactory
    {
        private readonly IImageHolder imageHolder;
        private readonly IPalette palette;
        private readonly Func<Point, ICircularCloudLayouter> circularCloudLayouterFactory;
        private readonly ICircularCloudLayouterWithWordsSettings settings;
        private readonly IWordsFilter[] filters;
        private readonly IWordsFrequencyParser parser;
        public CloudWithWordsPainterFactory(IImageHolder imageHolder,
            IPalette palette,
            ICircularCloudLayouterWithWordsSettings settings,
            Func<Point, ICircularCloudLayouter> circularCloudLayouterFactory, IWordsFilter[] filters, IWordsFrequencyParser parser)
        {
            this.imageHolder = imageHolder;
            this.palette = palette;
            this.circularCloudLayouterFactory = circularCloudLayouterFactory;
            this.settings = settings;
            this.filters = filters;
            this.parser = parser;
        }

        public ICloudPainter Create()
        {
            IEnumerable<string> lines;
            try
            {
                lines = File.ReadLines(settings.WordsSource);
            }
            catch (Exception e)
            {
                lines = new List<string>();
                MessageBox.Show(e.Message, "Не удалось загрузить файл с словами");
            }

            if (settings.UpperCase)
                lines = lines.Select(x => x.ToUpper());
            else
                lines = lines.Select(x => x.ToLower());
            foreach (var filter in filters)
            {
                IEnumerable<string> filtered = lines;
                try
                {
                    filtered = filter.Filter(settings, lines);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    filtered = lines;
                }
                lines = filtered;
            }
            var wordsWithFrequency = parser.GetWordsFrequency(lines, settings.Language);
            var layouter = circularCloudLayouterFactory.Invoke(new Point(settings.CenterX, settings.CenterY));
            return new CloudWithWordsPainter(imageHolder, settings, palette, layouter, wordsWithFrequency);
        }
    }
}
