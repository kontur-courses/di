using System;
using System.Windows.Forms;
using TagCloud;

namespace GUITagClouder
{
    public class DrawingSettingsForm : Form
    {
        public DrawingSettingsForm(DrawingSettings settings)
        {
            Controls.Add(new Button
            {
                Text = "OK",
                DialogResult = DialogResult.OK,
                Dock = DockStyle.Bottom
            });
            Controls.Add(new PropertyGrid
            {
                SelectedObject = settings,
                Dock = DockStyle.Fill
            });
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Text = "Настройки";
        }
    }
}