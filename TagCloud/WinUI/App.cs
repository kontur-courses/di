using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TagsCloudVisualisation;
using TagsCloudVisualisation.Extensions;
using TagsCloudVisualisation.Layouting;
using TagsCloudVisualisation.Output;
using TagsCloudVisualisation.Text;
using TagsCloudVisualisation.Text.Formatting;
using TagsCloudVisualisation.Text.Preprocessing;

namespace WinUI
{
    public class App
    {
        private readonly MainForm mainForm;
        private readonly IFileWordsReader reader;
        private readonly IWordFilter filter;
        private readonly IWordNormalizer normalizer;
        private readonly UserInputSelector<ITagCloudLayouter> layouters;
        private readonly Func<ITagCloudLayouter, IFontSource, IColorSource, TagCloudGenerator> cloudGeneratorFactory;
        private readonly IFileResultWriter writer;
        private readonly UserInputSelector<IColorSource> colorSources;
        private UserInputSelector<IFontSource> fontSources;

        public App(MainForm mainForm,
            IFileWordsReader reader,
            IWordFilter filter,
            IWordNormalizer normalizer,
            IEnumerable<ITagCloudLayouter> layouters,
            IEnumerable<IFontSource> fontSources,
            IEnumerable<IColorSource> colorSources,
            Func<ITagCloudLayouter, IFontSource, IColorSource, TagCloudGenerator> cloudGeneratorFactory,
            IFileResultWriter writer)
        {
            this.mainForm = mainForm;
            this.reader = reader;
            this.filter = filter;
            this.normalizer = normalizer;
            this.cloudGeneratorFactory = cloudGeneratorFactory;
            this.writer = writer;
            this.layouters = CreateInputFrom(ToDictionaryByName(layouters), "Choose layouting algorithm");
            this.colorSources = CreateInputFrom(ToDictionaryByName(colorSources), "Choose color source");
            this.fontSources = CreateInputFrom(ToDictionaryByName(fontSources), "Choose font source");

            ConfigureForm();
        }

        public void Run()
        {
            Application.Run(mainForm);
        }

        private void ConfigureForm()
        {
            mainForm.ExecuteButtonClicked += ExecuteButtonClicked;
        }

        private async void ExecuteButtonClicked()
        {
            var sourcePath = RequestInputForm.RequestInput("source path");
            using (var lockingContext = mainForm.StartLockingOperation())
            {
                var words = ReadWords(sourcePath);
                using (var pbContext = lockingContext.GetProgressBarContext(0, words.Length))
                {
                    var image = await CreateImageAsync(words, lockingContext.CancellationToken, pbContext.Increment);
                    mainForm.SetImage(new Bitmap(image, mainForm.PictureBoxSize));
                    writer.Save(image, sourcePath + ".png");
                }
            }
        }

        private async Task<Image> CreateImageAsync(WordWithFrequency[] words, CancellationToken cancellationToken,
            Action callback = null)
        {
            var cloudGenerator = cloudGeneratorFactory.Invoke(
                layouters.Selected.Value,
                fontSources.Selected.Value,
                colorSources.Selected.Value);
            if (callback != null)
                cloudGenerator.AfterWordDrawn += callback;

            var resultImage = await cloudGenerator.DrawWordsAsync(words, cancellationToken);

            layouters.Selected.Value.Reset();
            if (callback != null)
                cloudGenerator.AfterWordDrawn -= callback;
            return resultImage.FillBackground(Color.Black);
        }

        private WordWithFrequency[] ReadWords(string sourcePath)
        {
            var normalizedWords = reader.EnumerateWordsFrom(sourcePath)
                .Where(filter.IsValidWord)
                .Select(normalizer.Normalize);

            var dictionary = new Dictionary<string, int>();
            foreach (var word in normalizedWords)
            {
                if (dictionary.ContainsKey(word))
                    dictionary[word] += 1;
                else dictionary[word] = 0;
            }

            return dictionary.Select(x => new WordWithFrequency(x.Key, x.Value))
                .OrderBy(x => x.Frequency)
                .ToArray();
        }

        private static Dictionary<string, TService> ToDictionaryByName<TService>(IEnumerable<TService> source) =>
            source.ToDictionary(x => x.GetType().GetCustomAttribute<VisibleNameAttribute>()?.Name ?? x.GetType().Name);

        private static UserInputSelector<TService> CreateInputFrom<TService>(IDictionary<string, TService> source,
            string description)
        {
            var result = new UserInputSelector<TService>
            {
                Description = description,
                Available = source.Select(x => new UserInputSelectorItem<TService> {Name = x.Key, Value = x.Value})
                    .OrderBy(x => x.Name)
                    .ToArray(),
            };

            result.Selected = result.Available.FirstOrDefault();
            return result;
        }
    }

    public class ActionDisposable : IDisposable
    {
        private readonly Action onDispose;

        public ActionDisposable(Action onDispose)
        {
            this.onDispose = onDispose;
        }

        public void Dispose()
        {
            onDispose.Invoke();
        }
    }
}