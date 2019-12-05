using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TagsCloudForm.CircularCloudLayouter;
using TagsCloudForm.Common;
using TagsCloudForm.UiActions;

namespace TagsCloudForm.Actions
{
    class CircularCloudLayouterWithWordsAction : IUiAction
    {
        private readonly IImageHolder imageHolder;
        private readonly Palette palette;
        private readonly Func<Point, CircularCloudLayouter.CircularCloudLayouter> circularCloudLayouterFactory;
        private readonly IWordsFrequencyParser parser;
        private readonly SpellCheckerFilter spellFilter;

        private readonly Func<IImageHolder,
            CircularCloudLayouterWithWordsSettings, Palette, ICircularCloudLayouter
            , Dictionary<string, int>, CloudWithWordsPainter> painterFactory;
        public CircularCloudLayouterWithWordsAction(IImageHolder imageHolder,
             Palette palette, Func<Point, CircularCloudLayouter.CircularCloudLayouter> circularCloudLayouterFactory, IWordsFrequencyParser parser,
             Func<IImageHolder,
             CircularCloudLayouterWithWordsSettings, Palette, ICircularCloudLayouter, Dictionary<string, int>,
             CloudWithWordsPainter> painterFactory, SpellCheckerFilter spellFilter)
        {
            this.painterFactory = painterFactory;
            this.imageHolder = imageHolder;
            this.palette = palette;
            this.circularCloudLayouterFactory = circularCloudLayouterFactory;
            this.parser = parser;
            this.spellFilter = spellFilter;
        }
        public string Category => "CircularCloud";
        public string Name => "LayouterWithWords";
        public string Description => "Создание облака";

        public void Perform()
        {
            var settings = new CircularCloudLayouterWithWordsSettings();
            SettingsForm.For(settings).ShowDialog();
            var layouter = circularCloudLayouterFactory.Invoke(new Point(settings.CenterX, settings.CenterY));

            IEnumerable<string> lines;
            try
            {
                lines = File.ReadLines(settings.WordsSource);
            }
            catch (Exception e)
            {
                lines = new List<string>();
                MessageBox.Show(e.Message, "Не удалось загрузить файл");
            }

            var filteredWords = spellFilter.Filter(lines, settings.Language);//здесь еще надо boring words filter
            var wordsWithFrequency = parser.GetWordsFrequency(filteredWords, settings.Language);

            painterFactory.Invoke(imageHolder, settings, palette, layouter, wordsWithFrequency).Paint();
        }
    }
}
