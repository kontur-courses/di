using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TagCloud.CloudVisualizer.CloudViewConfiguration;
using TagCloud.Interfaces.GUI.UIActions;
using TagCloud.WordsPreprocessing.DocumentParsers;
using TagCloud.WordsPreprocessing.TextAnalyzers;

namespace TagCloud.Interfaces.GUI
{
    public class MainForm : Form
    {
        private CloudVisualizer.CloudVisualizer visualizer;
        private CloudViewConfiguration cloudConfiguration;
        private IDocumentParser[] parsers;
        private ITextAnalyzer textAnalyzer;
        private ApplicationSettings appSettings;
        private PictureBox pictureBox;

        public Image Image { get; private set; }

        public MainForm(CloudVisualizer.CloudVisualizer visualizer, CloudViewConfiguration cloudConfiguration,
            IDocumentParser[] parsers, ITextAnalyzer textAnalyzer, IUIAction[] actions, ApplicationSettings settings, PictureBox pictureBox)
        {
            appSettings = settings;
            this.visualizer = visualizer;
            this.cloudConfiguration = cloudConfiguration;
            this.parsers = parsers;
            this.textAnalyzer = textAnalyzer;
            var mainMenu = new MenuStrip();
            mainMenu.Items.AddRange(actions.ToMenuItems());
            Controls.Add(mainMenu);
            ClientSize = new Size(600, 400);
            Resize += OnResizeWindow;
            this.pictureBox = pictureBox;
            pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
        }

        public void RedrawImage()
        {
            var format = $".{appSettings.FilePath.Split('.').Last()}";
            var parser = parsers.First(p => p.AllowedTypes.Contains(format));
            var words = textAnalyzer.GetWords(parser.GetWords(appSettings), cloudConfiguration.WordsCount);
            Image = visualizer.GetCloud(words);
            Image.Save("test.bmp");
            pictureBox.Image = Image;
            Controls.Add(pictureBox);
            ClientSize = Image.Size;
            Refresh();
        }

        public void OnResizeWindow(object sender, System.EventArgs e)
        {
            var control = (Control) sender;
            appSettings.WindowSize = control.Size;
        }
    }
}