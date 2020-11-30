using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TagsCloudVisualisation.Output;
using TagsCloudVisualisation.Text;
using TagsCloudVisualisation.Text.Preprocessing;
using TagsCloudVisualisation.Visualisation;

namespace WinUI
{
    public partial class MainForm : Form
    {
        public MainForm(IWordsReader reader, IWordFilter filter, IWordNormalizer normalizer, WordsCloudDrawer drawer,
            IResultWriter writer)
        {
            InitializeComponent();
            var normalizedWords = reader.EnumerateWords()
                .Where(filter.IsValidWord)
                .Select(normalizer.Normalize);

            var dictionary = new Dictionary<string, int>();
            foreach (var word in normalizedWords)
            {
                if (dictionary.ContainsKey(word))
                    dictionary[word] += 1;
                else dictionary[word] = 0;
            }

            var words = dictionary.Select(x => new WordWithFrequency(x.Key, x.Value))
                .OrderBy(x => x.Frequency)
                .Take(500)
                .ToArray();
            var resultImage = drawer.DrawWords(words);
            writer.Save(resultImage);
        }
    }
}