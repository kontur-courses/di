using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TagsCloudVisualization.AppSettings;
using TagsCloudVisualization.Canvases;
using TagsCloudVisualization.FormAction;

namespace TagsCloudVisualization
{
    public class MainForm : Form
    {
        public MainForm(IEnumerable<IFormAction> actions, ImageSettings imageSettings, ICanvas canvas)
        {
            var mainMenu = new MenuStrip();
            mainMenu.Items.AddRange(actions.ToMenuItems());
            Controls.Add(mainMenu);

            var imageHolder = (Canvas) canvas;
            imageHolder.RecreateImage(imageSettings);
            imageHolder.Dock = DockStyle.Fill;
            Controls.Add(imageHolder);
            Size = new Size(imageSettings.Width, imageSettings.Height);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            Text = "Tag Cloud Visualizer";
        }
    }
}