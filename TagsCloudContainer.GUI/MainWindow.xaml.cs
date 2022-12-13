using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Infrastructure.Settings;

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

        public static readonly DependencyProperty OptionsProperty = DependencyProperty.Register(nameof(Options), typeof(Options), typeof(MainWindow));
        public Options Options
        {
            get => (Options)GetValue(OptionsProperty);
            private set => SetValue(OptionsProperty, value);
        }

        public MainWindow(Options options,
                          ISettingsProvider settingsProvider,
                          IWordReader wordReader,
                          IWordPreparer wordPreparer,
                          ITagsCloudGenerator tagsCloudGenerator,
                          WordPlateVisualizer wordPlateVisualizer)
        {
            Options = options;

            this.settingsProvider = settingsProvider;
            this.wordReader = wordReader;
            this.wordPreparer = wordPreparer;
            this.tagsCloudGenerator = tagsCloudGenerator;
            this.wordPlateVisualizer = wordPlateVisualizer;

            InitializeComponent();
            DataContext = this;
        }
    }
}