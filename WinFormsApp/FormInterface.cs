using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;
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
        private TagsCloudLayouter layouter;
        private IEnumerable<(string, int)> text;

        private AppSettings appSettings;
        private ServiceProvider serviceProvider;

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

            appSettings = SettingsManager.SettingsManager.LoadSettings(); // TODO: load settings from file, ex.: SettingsManager.LoadSettings();
            appSettings.DrawingSettings = new();

            var services = DependencyInjectionConfig.AddCustomServices(new ServiceCollection());
            serviceProvider = services.BuildServiceProvider();
            layouter = serviceProvider.GetService<TagsCloudLayouter>();
            
            this.Size = appSettings.DrawingSettings.Size;

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

                // Redraw image on change settings
                layouter.Initialize(appSettings.DrawingSettings, text);
                layouter.GetTextImages();

                this.Size = appSettings.DrawingSettings.Size;
                gr = this.CreateGraphics();
                if (text != null)
                {
                    gr.DrawImage(Visualizer.Draw(appSettings.DrawingSettings.Size, layouter.GetTextImages()), new Point(0, 0));
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var saveDialog = new SaveFileDialog();
            saveDialog.Filter = "PNG files (*.png)";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                // TODO: Save file
                
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsManager.SettingsManager.SaveSettings(appSettings);
            Close();
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var colorSelector = new ColorSelectorForm(appSettings.DrawingSettings.Colors);
            colorSelector.ShowDialog();
            if(colorSelector.DialogResult == DialogResult.OK)
            {
                appSettings.DrawingSettings.Colors = colorSelector.Colors;
                SettingsManager.SettingsManager.SaveSettings(appSettings);

                // TODO: Draw cloud with new settings
                //Visualizer.Draw(appSettings.DrawingSettings.Size, layouter.GetTextImages()).Save(appSettings.OutImagePath);
                if (text != null)
                {
                    gr.DrawImage(Visualizer.Draw(appSettings.DrawingSettings.Size, layouter.GetTextImages()), new Point(0, 0));
                }
            }
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var properties = new Properties();

            if (properties.ShowDialog() == DialogResult.OK)
            {
                // TODO: Parse settings
            }
        }
    }
}
