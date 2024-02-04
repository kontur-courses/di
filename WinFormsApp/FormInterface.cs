using Microsoft.Extensions.DependencyInjection;
using TagsCloudContainer;
using TagsCloudContainer.Drawer;
using TagsCloudContainer.FrequencyAnalyzers;
using TagsCloudContainer.SettingsClasses;
using TagsCloudContainer.TextTools;
using TagsCloudVisualization;
using WinFormsApp.SettingsForms;

namespace WinFormsApp
{
    internal class FormInterface : Form
    {
        private MenuStrip menuStrip;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripMenuItem colorToolStripMenuItem;
        private ToolStripMenuItem propertiesToolStripMenu;

        private Graphics gr;
        private Image image;

        private TagsCloudLayouter layouter;
        private IEnumerable<(string, int)> text;
        private AppSettings appSettings;
        private ServiceProvider serviceProvider;
        private List<IPointsProvider> providers;

        public FormInterface()
        {
            menuStrip = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem("File");
            openToolStripMenuItem = new ToolStripMenuItem("Open..");
            saveToolStripMenuItem = new ToolStripMenuItem("Save..");
            exitToolStripMenuItem = new ToolStripMenuItem("Exit");
            settingsToolStripMenuItem = new ToolStripMenuItem("Settings");
            colorToolStripMenuItem = new ToolStripMenuItem("Colors");
            propertiesToolStripMenu = new ToolStripMenuItem("Properties");

            menuStrip.Items.Add(fileToolStripMenuItem);
            menuStrip.Items.Add(settingsToolStripMenuItem);
            fileToolStripMenuItem.DropDownItems.Add(openToolStripMenuItem);
            fileToolStripMenuItem.DropDownItems.Add(saveToolStripMenuItem);
            fileToolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());
            fileToolStripMenuItem.DropDownItems.Add(exitToolStripMenuItem);

            settingsToolStripMenuItem.DropDownItems.Add(colorToolStripMenuItem);
            settingsToolStripMenuItem.DropDownItems.Add(propertiesToolStripMenu);

            Controls.Add(menuStrip);

            openToolStripMenuItem.Click += new EventHandler(openToolStripMenuItem_Click);
            saveToolStripMenuItem.Click += new EventHandler(saveToolStripMenuItem_Click);
            colorToolStripMenuItem.Click += new EventHandler(colorToolStripMenuItem_Click);
            exitToolStripMenuItem.Click += new EventHandler(exitToolStripMenuItem_Click);
            propertiesToolStripMenu.Click += new EventHandler(propertiesToolStripMenuItem_Click);

            gr = this.CreateGraphics();

            appSettings = SettingsManager.SettingsManager.LoadSettings();

            var services = DependencyInjectionConfig.AddCustomServices(new ServiceCollection());
            serviceProvider = services.BuildServiceProvider();
            layouter = serviceProvider.GetService<TagsCloudLayouter>();

            providers = serviceProvider.GetServices<IPointsProvider>().ToList();

            Size = appSettings.DrawingSettings.Size;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var openDialog = new OpenFileDialog();
            openDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                appSettings.TextFile = openDialog.FileName;

                var reader = serviceProvider.GetService<ITextReader>();
                var analyzer = serviceProvider.GetService<IAnalyzer>();

                analyzer.Analyze(reader.ReadText(appSettings.TextFile), appSettings.FilterFile);

                text = analyzer.GetAnalyzedText();

                RedrawImage();
            }
        }

        private void RedrawImage()
        {
            layouter = new TagsCloudLayouter();
            layouter.Initialize(appSettings.DrawingSettings, text);
            layouter.GetTextImages();

            Size = new Size(appSettings.DrawingSettings.Size.Width + 20, appSettings.DrawingSettings.Size.Height + 50);
            gr = CreateGraphics();
            if (text != null)
            {
                image = Visualizer.Draw(appSettings.DrawingSettings.Size,
                                        layouter.GetTextImages(),
                                        appSettings.DrawingSettings.BgColor);

                gr.DrawImage(image, new Point(0, 0));
                SettingsManager.SettingsManager.SaveSettings(appSettings);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var saveDialog = new SaveFileDialog();
            saveDialog.Filter = "PNG files (*.png)|*.png";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                image.Save(saveDialog.FileName);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsManager.SettingsManager.SaveSettings(appSettings);
            Close();
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var colorSelector = new ColorSelectorForm(appSettings.DrawingSettings.Colors, appSettings.DrawingSettings.BgColor);
            colorSelector.ShowDialog();
            if (colorSelector.DialogResult == DialogResult.OK)
            {
                appSettings.DrawingSettings.Colors = colorSelector.Colors;
                appSettings.DrawingSettings.BgColor = colorSelector.BGColor;
                SettingsManager.SettingsManager.SaveSettings(appSettings);

                if (text != null)
                {
                    RedrawImage();
                }
            }
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var properties = new Properties(appSettings, providers);

            if (properties.ShowDialog() == DialogResult.OK)
            {
                appSettings = properties.appSettings;
                RedrawImage();
            }
        }
    }
}
