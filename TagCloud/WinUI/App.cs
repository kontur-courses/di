using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using TagsCloudVisualisation;
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
        private readonly IUi ui;
        private readonly TagCloudGenerator cloudGenerator;
        private readonly UserInputOneOptionChoice<IFileWordsReader> readerPicker;
        private readonly UserInputMultipleOptionsChoice<IWordFilter> filterPicker;
        private readonly UserInputOneOptionChoice<IWordNormalizer> normalizerPicker;
        private readonly UserInputOneOptionChoice<IFileResultWriter> writerPicker;
        private readonly UserInputOneOptionChoice<ILayouterFactory> layouterPicker;
        private readonly UserInputOneOptionChoice<IColorSource> brushSourcePicker;
        private readonly UserInputOneOptionChoice<IFontSizeResolver> fontSizeResolverPicker;
        private readonly UserInputOneOptionChoice<FontFamily> fontFamilyPicker;
        private readonly UserInputField filePathInput;
        private readonly UserInputSizeField centerOffsetPicker;
        private readonly UserInputSizeField betweenWordsDistancePicker;

        public App(IUi ui,
            TagCloudGenerator cloudGenerator,
            IEnumerable<IFileWordsReader> readers,
            IEnumerable<IWordFilter> filters,
            IEnumerable<IWordNormalizer> normalizers,
            IEnumerable<ILayouterFactory> layouters,
            IEnumerable<IFontSizeResolver> fontSources,
            IEnumerable<IColorSource> colorSources,
            IEnumerable<IFileResultWriter> writers)
        {
            this.ui = ui;
            this.cloudGenerator = cloudGenerator;

            readerPicker = UserInput.SingleChoice(ToDictionaryByName(readers), "Words file reader");
            filterPicker = UserInput.MultipleChoice(ToDictionaryByName(filters), "Words filtering method");
            writerPicker = UserInput.SingleChoice(ToDictionaryByName(writers), "Result writing method");
            layouterPicker = UserInput.SingleChoice(ToDictionaryByName(layouters), "Layouting algorithm");
            normalizerPicker = UserInput.SingleChoice(ToDictionaryByName(normalizers), "Words normalization method");
            fontSizeResolverPicker = UserInput.SingleChoice(ToDictionaryByName(fontSources), "Font size source");
            brushSourcePicker = UserInput.SingleChoice(ToDictionaryByName(colorSources), "Color source");

            filePathInput = UserInput.Field("Enter source file path");
            fontFamilyPicker = UserInput.SingleChoice(FontFamily.Families.ToDictionary(x => x.Name), "Font family");
            centerOffsetPicker = UserInput.Size("Cloud center offset");
            betweenWordsDistancePicker = UserInput.Size("Minimal distance between rectangles");
        }

        public void Subscribe()
        {
            ui.ExecutionRequested += ExecutionRequested;

            ui.AddUserInput(filePathInput);
            ui.AddUserInput(readerPicker);
            ui.AddUserInput(filterPicker);
            ui.AddUserInput(normalizerPicker);
            ui.AddUserInput(writerPicker);
            ui.AddUserInput(layouterPicker);
            ui.AddUserInput(brushSourcePicker);
            ui.AddUserInput(fontSizeResolverPicker);
            ui.AddUserInput(fontFamilyPicker);
            ui.AddUserInput(centerOffsetPicker);
            ui.AddUserInput(betweenWordsDistancePicker);
        }

        private async void ExecutionRequested()
        {
            using (var lockingContext = ui.StartLockingOperation())
            {
                var words = await ReadWordsAsync(filePathInput.Value, lockingContext.CancellationToken);
                if (lockingContext.CancellationToken.IsCancellationRequested)
                    return;

                var brushSource = brushSourcePicker.Selected.Value;
                var image = await CreateImageAsync(brushSource, words, lockingContext.CancellationToken);

                ui.OnAfterWordDrawn(image, brushSource.Background);
                FillBackgroundAndSave(image, brushSource.Background);
            }
        }

        private async Task<Image> CreateImageAsync(IColorSource colorSource,
            WordWithFrequency[] words, CancellationToken cancellationToken)
        {
            var selectedFactory = layouterPicker.Selected.Value;
            var selectedLayouter = selectedFactory.Get(
                centerOffsetPicker.PointFromCurrent(),
                centerOffsetPicker.SizeFromCurrent());

            var fontSizeSource = fontSizeResolverPicker.Selected.Value;
            var fontFamily = fontFamilyPicker.Selected.Value;

            var resultImage = await cloudGenerator.DrawWordsAsync(
                fontSizeSource,
                colorSource,
                selectedLayouter,
                words,
                cancellationToken,
                fontFamily);

            return resultImage;
        }

        private async Task<WordWithFrequency[]> ReadWordsAsync(string sourcePath, CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                var words = readerPicker.Selected.Value.GetWordsFrom(sourcePath)
                    .Where(w => filterPicker.Selected.All(f => f.Value.IsValidWord(w)));

                var normalizedWords = normalizerPicker.Selected.Value.Normalize(words);

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

        private void FillBackgroundAndSave(Image image, Color backgroundColor)
        {
            using var newImage = new Bitmap(image.Size.Width, image.Size.Height);
            using (var g = Graphics.FromImage(newImage))
            using (var brush = new SolidBrush(backgroundColor))
            {
                g.FillRectangle(brush, new Rectangle(Point.Empty, image.Size));
                g.DrawImage(image, Point.Empty);
            }

            writerPicker.Selected.Value.Save(newImage, ImageFormat.Png, filePathInput.Value + ".png");
        }

        private static Dictionary<string, TService> ToDictionaryByName<TService>(IEnumerable<TService> source) =>
            source.Where(x => x != null)
                .ToDictionary(x => x.GetType().GetCustomAttribute<VisibleNameAttribute>()?.Name ?? x.GetType().Name);
    }
}