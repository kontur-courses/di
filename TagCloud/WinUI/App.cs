using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using TagsCloudVisualisation;
using TagsCloudVisualisation.Extensions;
using TagsCloudVisualisation.Layouting;
using TagsCloudVisualisation.Output;
using TagsCloudVisualisation.Text;
using TagsCloudVisualisation.Text.Formatting;
using TagsCloudVisualisation.Text.Preprocessing;
using TagsCloudVisualisation.Visualisation;
using WinUI.InputModels;

namespace WinUI
{
    public class App
    {
        private readonly IGui gui;
        private readonly TagCloudGenerator cloudGenerator;
        private readonly UserInputSelector<IFileWordsReader> readers;
        private readonly UserInputSelector<IWordFilter> filters;
        private readonly UserInputSelector<IWordNormalizer> normalizers;
        private readonly UserInputSelector<IFileResultWriter> writers;
        private readonly UserInputSelector<ITagCloudLayouter> layouters;
        private readonly UserInputSelector<IColorSource> colorSources;
        private readonly UserInputSelector<IFontSource> fontSources;
        private readonly UserInputField pathSource;
        private readonly CloudVisualiser visualiser;

        public App(IGui gui,
            TagCloudGenerator cloudGenerator,
            CloudVisualiser visualiser,
            IEnumerable<IFileWordsReader> readers,
            IEnumerable<IWordFilter> filters,
            IEnumerable<IWordNormalizer> normalizers,
            IEnumerable<ITagCloudLayouter> layouters,
            IEnumerable<IFontSource> fontSources,
            IEnumerable<IColorSource> colorSources,
            IEnumerable<IFileResultWriter> writers)
        {
            this.gui = gui;
            this.cloudGenerator = cloudGenerator;
            this.visualiser = visualiser;

            this.readers = CreateInputFrom(ToDictionaryByName(readers), "Choose words file reader");
            this.filters = CreateInputFrom(ToDictionaryByName(filters), "Choose words filtering method");
            this.normalizers = CreateInputFrom(ToDictionaryByName(normalizers), "Choose words normalization method");
            this.writers = CreateInputFrom(ToDictionaryByName(writers), "Chose result writing method");
            this.layouters = CreateInputFrom(ToDictionaryByName(layouters), "Choose layouting algorithm");
            this.colorSources = CreateInputFrom(ToDictionaryByName(colorSources), "Choose color source");
            this.fontSources = CreateInputFrom(ToDictionaryByName(fontSources), "Choose font source");

            pathSource = new UserInputField("Enter source file path");
        }

        public void Subscribe()
        {
            gui.ExecutionRequested += ExecutionRequested;
            colorSources.SelectedChanged += x => gui.SetImageBackground(x.Value.BackgroundColor);

            gui.AddUserInput(pathSource);
            gui.AddUserInput(readers);
            gui.AddUserInput(filters);
            gui.AddUserInput(normalizers);
            gui.AddUserInput(writers);
            gui.AddUserInput(layouters);
            gui.AddUserInput(colorSources);
            gui.AddUserInput(fontSources);
        }

        private async void ExecutionRequested()
        {
            using (var lockingContext = gui.StartLockingOperation())
            {
                var words = await ReadWordsAsync(pathSource.Value, lockingContext.CancellationToken);
                if (lockingContext.CancellationToken.IsCancellationRequested)
                    return;

                using (var pbContext = lockingContext.GetProgressBarContext(0, words.Length))
                {
                    var image = await CreateImageAsync(words, lockingContext.CancellationToken, pbContext.Increment);
                    gui.SetImage(image);
                    writers.Selected.Value.Save(
                        image.FillBackground(colorSources.Selected.Value.BackgroundColor),
                        pathSource.Value + ".png");
                }
            }
        }

        private async Task<Image> CreateImageAsync(WordWithFrequency[] words, CancellationToken cancellationToken,
            Action? callback = null)
        {
            if (callback != null)
                cloudGenerator.AfterWordDrawn += callback;

            var selectedLayouter = layouters.Selected.Value;
            var resultImage = await cloudGenerator.DrawWordsAsync(
                fontSources.Selected.Value,
                colorSources.Selected.Value,
                selectedLayouter,
                visualiser,
                words,
                cancellationToken
            );

            selectedLayouter.Reset();
            if (callback != null)
                cloudGenerator.AfterWordDrawn -= callback;
            return resultImage;
        }

        private async Task<WordWithFrequency[]> ReadWordsAsync(string sourcePath, CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                var words = readers.Selected.Value.EnumerateWordsFrom(sourcePath)
                    .Where(filters.Selected.Value.IsValidWord);

                var normalizedWords = normalizers.Selected.Value.Normalize(words);

                if (cancellationToken.IsCancellationRequested)
                    return new WordWithFrequency[0];

                var dictionary = new Dictionary<string, int>();
                foreach (var word in normalizedWords)
                {
                    if (dictionary.ContainsKey(word))
                        dictionary[word] += 1;
                    else dictionary[word] = 0;

                    if (cancellationToken.IsCancellationRequested)
                        break;
                }

                return dictionary.Select(x => new WordWithFrequency(x.Key, x.Value))
                    .OrderBy(x => x.Frequency)
                    .ToArray();
            }, cancellationToken);
        }

        private static Dictionary<string, TService> ToDictionaryByName<TService>(IEnumerable<TService> source) =>
            source.Where(x => x != null)
                .ToDictionary(x => x.GetType().GetCustomAttribute<VisibleNameAttribute>()?.Name ?? x.GetType().Name);

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