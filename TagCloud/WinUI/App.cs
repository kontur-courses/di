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
using WinUI.InputModels;

namespace WinUI
{
    public class App
    {
        private readonly MainForm mainForm;
        private readonly UserInputSelector<IFileWordsReader> readers;
        private readonly UserInputSelector<IWordFilter> filters;
        private readonly UserInputSelector<IWordNormalizer> normalizers;
        private readonly Func<ITagCloudLayouter, IFontSource, IColorSource, TagCloudGenerator> cloudGeneratorFactory;
        private readonly UserInputSelector<IFileResultWriter> writers;
        private readonly UserInputSelector<ITagCloudLayouter> layouters;
        private readonly UserInputSelector<IColorSource> colorSources;
        private readonly UserInputSelector<IFontSource> fontSources;
        private readonly UserInputField pathSource;

        public App(MainForm mainForm,
            Func<ITagCloudLayouter, IFontSource, IColorSource, TagCloudGenerator> cloudGeneratorFactory,
            IEnumerable<IFileWordsReader> readers,
            IEnumerable<IWordFilter> filters,
            IEnumerable<IWordNormalizer> normalizers,
            IEnumerable<ITagCloudLayouter> layouters,
            IEnumerable<IFontSource> fontSources,
            IEnumerable<IColorSource> colorSources,
            IEnumerable<IFileResultWriter> writers)
        {
            this.mainForm = mainForm;
            this.cloudGeneratorFactory = cloudGeneratorFactory;
            this.readers = CreateInputFrom(ToDictionaryByName(readers), "Choose words file reader");
            this.filters = CreateInputFrom(ToDictionaryByName(filters), "Choose words filtering method");
            this.normalizers = CreateInputFrom(ToDictionaryByName(normalizers), "Choose words normalization method");
            this.writers = CreateInputFrom(ToDictionaryByName(writers), "Chose result writing method");
            this.layouters = CreateInputFrom(ToDictionaryByName(layouters), "Choose layouting algorithm");
            this.colorSources = CreateInputFrom(ToDictionaryByName(colorSources), "Choose color source");
            this.fontSources = CreateInputFrom(ToDictionaryByName(fontSources), "Choose font source");

            pathSource = new UserInputField("Enter source file path");

            ConfigureForm();
        }

        public void Run()
        {
            Application.Run(mainForm);
        }

        private void ConfigureForm()
        {
            mainForm.ExecuteButtonClicked += ExecuteButtonClicked;
            mainForm.AddUserInput(pathSource);

            mainForm.AddUserInput(readers);
            mainForm.AddUserInput(filters);
            mainForm.AddUserInput(normalizers);
            mainForm.AddUserInput(writers);
            mainForm.AddUserInput(layouters);
            mainForm.AddUserInput(colorSources);
            mainForm.AddUserInput(fontSources);
        }

        private async void ExecuteButtonClicked()
        {
            using (var lockingContext = mainForm.StartLockingOperation())
            {
                var words = await ReadWordsAsync(pathSource.Value, lockingContext.CancellationToken);
                if (lockingContext.CancellationToken.IsCancellationRequested)
                    return;

                using (var pbContext = lockingContext.GetProgressBarContext(0, words.Length))
                {
                    var image = await CreateImageAsync(words, lockingContext.CancellationToken, pbContext.Increment);
                    mainForm.SetImage(new Bitmap(image, mainForm.PictureBoxSize));
                    writers.Selected.Value.Save(image, pathSource.Value + ".png");
                }
            }
        }

        private async Task<Image> CreateImageAsync(WordWithFrequency[] words, CancellationToken cancellationToken,
            Action? callback = null)
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

        private async Task<WordWithFrequency[]> ReadWordsAsync(string sourcePath, CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                var words = readers.Selected.Value.EnumerateWordsFrom(sourcePath)
                    .Where(filters.Selected.Value.IsValidWord);
                var normalizedWords = normalizers.Selected.Value.Normalize(words);

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
            }, cancellationToken);
        }

        private static Dictionary<string, TService> ToDictionaryByName<TService>(IEnumerable<TService> source) =>
            source.ToDictionary(x => x.GetType().GetCustomAttribute<VisibleNameAttribute>()?.Name ?? x.GetType().Name);

        private static UserInputSelector<TService> CreateInputFrom<TService>(IDictionary<string, TService> source,
            string description)
        {
            var availableOptions = source.Select(x => new UserInputSelectorItem<TService>(x.Key, x.Value))
                .OrderBy(x => x.Name)
                .ToArray();

            return new UserInputSelector<TService>(description, availableOptions);
        }
    }
}