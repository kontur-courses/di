using System;
using System.Drawing;
using System.Windows.Forms;
using TagCloud.Core.Settings;
using TagCloud.Core.Settings.DefaultImplementations;
using TagCloud.Core.Settings.Interfaces;
using TagCloud.GUI.Extensions;
using TagCloud.GUI.Settings;

namespace TagCloud.GUI
{
    public class MainForm : Form
    {
        private readonly Core.TagCloud tagCloud;
        private readonly ITagCloudSettings tagCloudSettings;
        private readonly PictureBox pictureBox;

        public MainForm(ISettings[] settings, ITagCloudSettings tagCloudSettings, Core.TagCloud tagCloud)
        {
            this.tagCloud = tagCloud;
            this.tagCloudSettings = tagCloudSettings;
            Text = @"TagCloud";
            Size = new Size(800, 600);

            var mainMenu = BuildMainMenu(settings);
            Controls.Add(mainMenu);

            pictureBox = new PictureBox { Dock = DockStyle.Fill };
            Controls.Add(pictureBox);
        }

        private MenuStrip BuildMainMenu(ISettings[] settings)
        {
            var mainMenu = new MenuStrip { Dock = DockStyle.Top };

            var fileItem = new ToolStripMenuItem("File");
            fileItem.DropDownItems.Add(new ToolStripMenuItem(
                "Choose file with tags", null, (o, e) => ChooseFileWithTags()));
            fileItem.DropDownItems.Add(new ToolStripMenuItem(
                "Choose file with boring words", null, (o, e) => ChooseFileWithBoringWords()));
            fileItem.DropDownItems.Add(new ToolStripSeparator());
            fileItem.DropDownItems.Add(new ToolStripMenuItem("Save As", null, (o, e) => SaveAs()));
            fileItem.DropDownItems.Add(new ToolStripMenuItem("Save", null, (o, e) => Save()));
            fileItem.DropDownItems.Add(new ToolStripSeparator());
            fileItem.DropDownItems.Add(new ToolStripMenuItem("Exit", null, (o, e) => Close()));
            mainMenu.Items.Add(fileItem);

            mainMenu.Items.AddRange(settings.ToMenuItems("Settings"));
            mainMenu.Items.Add("Render", null, (s, a) => Render());

            return mainMenu;
        }

        private void ChooseFileWithTags()
        {
            var dialog = new OpenFileDialog { CheckPathExists = true };
            if (dialog.ShowDialog() == DialogResult.OK)
                tagCloudSettings.PathToWords = dialog.FileName;
        }

        private void ChooseFileWithBoringWords()
        {
            var dialog = new OpenFileDialog { CheckPathExists = true };
            if (dialog.ShowDialog() == DialogResult.OK)
                tagCloudSettings.PathToBoringWords = dialog.FileName;
        }

        private void Save()
        {
            if (pictureBox.Image == null)
            {
                MessageBox.Show(@"You should render tag cloud before saving it", @"Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (tagCloudSettings.PathForResultImage == null)
            {
                var dialog = new SaveFileDialog {CheckPathExists = true};
                if (dialog.ShowDialog() == DialogResult.OK)
                    tagCloudSettings.PathForResultImage = dialog.FileName;
            }
            tagCloud.Save(pictureBox.Image);
        }

        private void SaveAs()
        {
            if (pictureBox.Image == null)
            {
                MessageBox.Show(@"You should render tag cloud before saving it", @"Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var dialog = new SaveFileDialog { CheckPathExists = true };
            if (dialog.ShowDialog() != DialogResult.OK) return;
            tagCloudSettings.PathForResultImage = dialog.FileName;
            tagCloud.Save(pictureBox.Image);
        }

        private void Render()
        {
            if (tagCloudSettings.PathToWords == null)
            {
                MessageBox.Show(@"You should choose file with tags before rendering", @"Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var bitmap = tagCloud.MakeTagCloud();
            pictureBox.Image = bitmap;
            Size = bitmap.Size;
            pictureBox.Size = bitmap.Size;
            pictureBox.Refresh();
        }
    }
}
