using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TagsCloudForm.CircularCloudLayouter;
using TagsCloudForm.Common;
using TagsCloudForm.UiActions;
using TagsCloudForm.WordFilters;
using CircularCloudLayouter;

namespace TagsCloudForm.Actions
{
    class CircularCloudLayouterWithWordsAction : IUiAction
    {
        private readonly IWordsFrequencyParser parser;
        private readonly IWordsFilter[] filters;
        private readonly CloudWithWordsPainterFactory factory;

        public CircularCloudLayouterWithWordsAction(IWordsFrequencyParser parser, IWordsFilter[] filters, CloudWithWordsPainterFactory factory)
        {
            this.parser = parser;
            this.filters = filters;
            this.factory = factory;
        }
        public string Category => "CircularCloud";
        public string Name => "LayouterWithWords";
        public string Description => "Создание облака";

        public void Perform()
        {
            var settings = new CircularCloudLayouterWithWordsSettings();
            SettingsForm.For(settings).ShowDialog();
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
                lines = filter.Filter(settings, lines).OnFail(x => MessageBox.Show(x)).Value;
            }
            var wordsWithFrequency = parser.GetWordsFrequency(lines, settings.Language);
            factory.Create(settings, wordsWithFrequency).Paint();
        }
    }
}
