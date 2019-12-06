using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Annotations;
using System.Windows.Forms;
using TagsCloudForm.CircularCloudLayouter;
using TagsCloudForm.Common;
using TagsCloudForm.UiActions;
using TagsCloudForm.WordFilters;

namespace TagsCloudForm.Actions
{
    class CircularCloudLayouterWithWordsAction : IUiAction
    {
        private readonly IImageHolder imageHolder;
        private readonly Palette palette;
        private readonly Func<Point, CircularCloudLayouter.CircularCloudLayouter> circularCloudLayouterFactory;
        private readonly IWordsFrequencyParser parser;
        private readonly SpellCheckerFilter spellFilter;
        private readonly BoringWordsFilter boringWordsFilter;
        private readonly AppSettings appSettings;
        private readonly Func<IImageHolder,
            CircularCloudLayouterWithWordsSettings, Palette, ICircularCloudLayouter
            , Dictionary<string, int>, CloudWithWordsPainter> painterFactory;

        public CircularCloudLayouterWithWordsAction(IImageHolder imageHolder,
             Palette palette, Func<Point, CircularCloudLayouter.CircularCloudLayouter> circularCloudLayouterFactory, IWordsFrequencyParser parser,
             Func<IImageHolder,
             CircularCloudLayouterWithWordsSettings, Palette, ICircularCloudLayouter, Dictionary<string, int>,
             CloudWithWordsPainter> painterFactory, SpellCheckerFilter spellFilter, BoringWordsFilter boringWordsFilter)
        {
            this.painterFactory = painterFactory;
            this.imageHolder = imageHolder;
            this.palette = palette;
            this.circularCloudLayouterFactory = circularCloudLayouterFactory;
            this.parser = parser;
            this.spellFilter = spellFilter;
            this.boringWordsFilter = boringWordsFilter;
            this.appSettings = appSettings;
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
                MessageBox.Show(e.Message, "Не удалось загрузить файл с словами");
            }
            HashSet<string> boringWords;
            try
            {
                var settingsFilename = settings.BoringWordsFile;
                boringWords = File.ReadAllLines(settingsFilename).ToHashSet(StringComparer.OrdinalIgnoreCase);
            }
            catch (Exception e)
            {
                boringWords = new HashSet<string>();
                MessageBox.Show(e.Message, "Не удалось загрузить файл с boring words");
            }

            if (settings.UpperCase)
                lines = lines.Select(x => x.ToUpper());
            else
                lines = lines.Select(x => x.ToLower());
            var filteredWords = spellFilter.Filter(lines, settings.Language);
            filteredWords = boringWordsFilter.Filter(boringWords, filteredWords);
            var wordsWithFrequency = parser.GetWordsFrequency(filteredWords, settings.Language);

            painterFactory.Invoke(imageHolder, settings, palette, layouter, wordsWithFrequency).Paint();
        }
    }
}
