using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Infrastructure.Settings;
using TagsCloudContainer.Infrastructure.WordPreparers;
using TagsCloudContainer.Infrastructure.WordReaders;

namespace TagsCloudContainer.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ISettingsProvider settingsProvider;
        private readonly IWordReader wordReader;
        private readonly IWordPreparer wordPreparer;
        private readonly ITagsCloudGenerator tagsCloudGenerator;
        private readonly WordPlateVisualizer wordPlateVisualizer;

        public static readonly DependencyProperty OptionsProperty = DependencyProperty.Register(nameof(OptionsViewModel), typeof(OptionsViewModel), typeof(MainWindow));
        public OptionsViewModel OptionsViewModel
        {
            get => (OptionsViewModel)GetValue(OptionsProperty);
            private set => SetValue(OptionsProperty, value);
        }

        public MainWindow(Options options,
                          ISettingsProvider settingsProvider,
                          IWordReader wordReader,
                          IWordPreparer wordPreparer,
                          ITagsCloudGenerator tagsCloudGenerator,
                          WordPlateVisualizer wordPlateVisualizer)
        {
            this.settingsProvider = settingsProvider;
            this.wordReader = wordReader;
            this.wordPreparer = wordPreparer;
            this.tagsCloudGenerator = tagsCloudGenerator;
            this.wordPlateVisualizer = wordPlateVisualizer;

            InitializeComponent();
            DataContext = this;

            OptionsViewModel = new OptionsViewModel();
            OptionsViewModel.CurrentOptions = options;
        }

        private void GenerateTagsCloudButton_Click(object sender, RoutedEventArgs e)
        {
            Result result;
            if (!(result = wordReader.TryReadWords(settingsProvider.GetTextReaderSettings().Filename, out var words)).Success)
            {
                MessageBox.Show("Can't read words from input file: " + result.Message, "File error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var wordFrequencies = GetWordFrequencies(wordPreparer.Prepare(words));

            var wordFontSettings = settingsProvider.GetWordFontSettings();
            wordFontSettings.FontSizeSettings.WordFrequencies = wordFrequencies;

            var wordColorSettings = settingsProvider.GetWordColorSettings();
            wordColorSettings.WordFrequencies = wordFrequencies;

            words = wordFrequencies.Keys.ToArray();
            var outputImageSettings = settingsProvider.GetOutputImageSettings();
            var pictureSize = new System.Drawing.Size(outputImageSettings.Width, outputImageSettings.Height);

            var wordPlates = tagsCloudGenerator.GeneratePlates(words, new PointF(pictureSize.Width / 2.0F, pictureSize.Height / 2.0F), wordFontSettings);
            var bitmap = wordPlateVisualizer.DrawPlates(wordPlates, pictureSize, wordColorSettings);
            TagsCloudImage.Source = Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }

        private static Dictionary<string, int> GetWordFrequencies(IEnumerable<string> words)
        {
            var frequencies = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (!frequencies.ContainsKey(word))
                    frequencies.Add(word, 0);
                frequencies[word]++;
            }
            return frequencies;
        }

        private void SaveTagsCloudButton_Click(object sender, RoutedEventArgs e)
        {
            var jpgEncoder = new JpegBitmapEncoder();
            jpgEncoder.Frames.Add(BitmapFrame.Create((BitmapSource)TagsCloudImage.Source));
            using var stream = File.Create(settingsProvider.GetOutputImageSettings().Filename);
            jpgEncoder.Save(stream);
            MessageBox.Show("Saved");
        }
    }
}