using System;
using System.Drawing;
using System.Windows.Forms;
using TagsCloudVisualization.GUI;
using TagsCloudVisualization.GUI.GuiActions;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization
{
    public class GraphicalVisualizer : Form
    {
        public GraphicalVisualizer(IGuiAction[] actions, AppSettings appSettings)
        {
            ClientSize = new Size(appSettings.ImageSettings.Width, appSettings.ImageSettings.Height);

            var mainMenu = new MenuStrip();
            mainMenu.Items.AddRange(actions.ToMenuItems());
            Controls.Add(mainMenu);
            
            appSettings.ImageHolder.SetImageSize(appSettings.ImageSettings);
            appSettings.ImageHolder.Dock = DockStyle.Fill;
            Controls.Add(appSettings.ImageHolder);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            Text = "TagCloudVisualizer";
        }
    }
}