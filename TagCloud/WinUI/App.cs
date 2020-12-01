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
        private readonly UserInputSelector<IWordFormatter> formatters;
        private readonly Func<IWordFormatter, ITagCloudLayouter, TagCloudGenerator> cloudGeneratorFactory;
        private readonly IFileResultWriter writer;

        public App(MainForm mainForm,
            IFileWordsReader reader,
            IWordFilter filter,
            IWordNormalizer normalizer,
            IEnumerable<ITagCloudLayouter> layouters,
            IEnumerable<IWordFormatter> formatters,
            Func<IWordFormatter, ITagCloudLayouter, TagCloudGenerator> cloudGeneratorFactory,
            IFileResultWriter writer)
        {
            this.mainForm = mainForm;
            this.reader = reader;
            this.filter = filter;
            this.normalizer = normalizer;
            this.cloudGeneratorFactory = cloudGeneratorFactory;
            this.writer = writer;
            this.layouters = CreateInputFrom(ToDictionaryByName(layouters), "Choose layouting algorithm");
            this.formatters = CreateInputFrom(ToDictionaryByName(formatters), "Choose formatting");
        }

        public void Run()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(mainForm);
        }

        private async void ExecuteButtonClicked()
        {
            var sourcePath = RequestInputForm.RequestInput("source path");
            var words = ReadWords(sourcePath);
            var tokenSource = new CancellationTokenSource();
            var image = await CreateImageAsync(words, tokenSource.Token);
            mainForm.PictureBox.Image = image;
            writer.Save(image, sourcePath + ".png");
        }

        private async Task<Image> CreateImageAsync(WordWithFrequency[] words, CancellationToken cancellationToken)
        {
            var cloudGenerator = cloudGeneratorFactory.Invoke(formatters.Selected.Value, layouters.Selected.Value);
            var resultImage = await cloudGenerator.DrawWordsAsync(words, cancellationToken);
            layouters.Selected.Value.Reset();
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
}