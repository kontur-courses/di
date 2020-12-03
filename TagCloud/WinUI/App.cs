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
        private readonly UserInputSingleChoice<IFileWordsReader> readers;
        private readonly UserInputMultipleChoice<IWordFilter> filters;
        private readonly UserInputSingleChoice<IWordNormalizer> normalizers;
        private readonly UserInputSingleChoice<IFileResultWriter> writers;
        private readonly UserInputSingleChoice<ITagCloudLayouter> layouters;
        private readonly UserInputSingleChoice<IBrushSource> colorSources;
        private readonly UserInputSingleChoice<IFontSource> fontSources;
        private readonly UserInputField pathSource;
        private readonly UserInputBoolean showVisualisationFrames;
        private readonly CloudVisualiser visualiser;

        public App(IGui gui,
            TagCloudGenerator cloudGenerator,
            CloudVisualiser visualiser,
            IEnumerable<IFileWordsReader> readers,
            IEnumerable<IWordFilter> filters,
            IEnumerable<IWordNormalizer> normalizers,
            IEnumerable<ITagCloudLayouter> layouters,
            IEnumerable<IFontSource> fontSources,
            IEnumerable<IBrushSource> colorSources,
            IEnumerable<IFileResultWriter> writers)
        {
            this.gui = gui;
            this.cloudGenerator = cloudGenerator;
            this.visualiser = visualiser;

            this.readers = UserInput.SingleChoice(ToDictionaryByName(readers), "Words file reader");
            this.filters = UserInput.MultipleChoice(ToDictionaryByName(filters), "Words filtering method");
            this.writers = UserInput.SingleChoice(ToDictionaryByName(writers), "Result writing method");
            this.layouters = UserInput.SingleChoice(ToDictionaryByName(layouters), "Layouting algorithm");
            this.normalizers = UserInput.SingleChoice(ToDictionaryByName(normalizers), "Words normalization method");
            this.fontSources = UserInput.SingleChoice(ToDictionaryByName(fontSources), "Font source");
            this.colorSources = UserInput.SingleChoice(ToDictionaryByName(colorSources), "Color source");

            showVisualisationFrames = UserInput.Boolean("Show every visualisation frame", false);
            pathSource = UserInput.Field("Enter source file path");
        }

        public void Subscribe()
        {
            gui.ExecutionRequested += ExecutionRequested;
            colorSources.AfterSelectionChanged += x => gui.SetImageBackground(x.Value.BackgroundColor);

            gui.AddUserInput(pathSource);
            gui.AddUserInput(showVisualisationFrames);
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
                    var image = await CreateImageAsync(words, lockingContext.CancellationToken, dc =>
                    {
                        pbContext.Increment();
                        if (showVisualisationFrames.Value && dc.Image != null)
                            gui.SetImage(dc.Image);
                    });

                    gui.SetImage(image);
                    writers.Selected.Value.Save(
                        image.FillBackground(colorSources.Selected.Value.BackgroundColor),
                        pathSource.Value + ".png");
                }
            }
        }

        private async Task<Image> CreateImageAsync(WordWithFrequency[] words, CancellationToken cancellationToken,
            Action<DrawingContext>? callback = null)
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
                    .Where(w => filters.Selected.All(f => f.Value.IsValidWord(w)));

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
                    .OrderByDescending(x => x.Frequency)
                    .ToArray();
            }, cancellationToken);
        }

        private static Dictionary<string, TService> ToDictionaryByName<TService>(IEnumerable<TService> source) =>
            source.Where(x => x != null)
                .ToDictionary(x => x.GetType().GetCustomAttribute<VisibleNameAttribute>()?.Name ?? x.GetType().Name);
    }
}