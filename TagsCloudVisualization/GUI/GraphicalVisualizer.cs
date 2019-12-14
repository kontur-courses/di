using System;
using System.Drawing;
using System.Windows.Forms;
using TagsCloudVisualization.Core;
using TagsCloudVisualization.GUI;
using TagsCloudVisualization.Settings;
using TagsCloudVisualization.UI;
using TagsCloudVisualization.VisualizerActions.GuiActions;

namespace TagsCloudVisualization
{
    public class GraphicalVisualizer : Form, IVisualizer
    {
        private readonly AppSettings appSettings;
        private readonly TagCloudContainer container;

        public GraphicalVisualizer(IGuiAction[] actions, AppSettings appSettings, TagCloudContainer container)
        {
            this.appSettings = appSettings;
            this.container = container;
            WindowState = FormWindowState.Maximized;
            ClientSize = new Size(appSettings.ImageSettings.Width, appSettings.ImageSettings.Height);

            var mainMenu = new MenuStrip();
            mainMenu.Items.AddRange(actions.ToMenuItems());
            Controls.Add(mainMenu);

            appSettings.ImageHolder.SetImageSize(appSettings.ImageSettings);
            appSettings.ImageHolder.Dock = DockStyle.Fill;
            Controls.Add(appSettings.ImageHolder);
        }

        public void Start(string[] args)
        {
            Application.Run(this);
        }

        public Bitmap GetTagCloud()
        {
            return container.GetTagCloud(appSettings.CurrentFile);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            Text = "TagCloudVisualizer";
        }
    }
}