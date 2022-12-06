using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;

namespace TagCloudGraphicalUserInterface
{
    public static class SettingsForm
    {
        public static SettingsForm<TSettings> For<TSettings>(TSettings settings)
        {
            return new SettingsForm<TSettings>(settings);
        }
    }

    public class SettingsForm<TSettings> : Form
    {
        public SettingsForm(TSettings settings)
        {
            var acceptButton = new Button
            {
                Text = "OK",
                DialogResult = DialogResult.OK,
                Dock = DockStyle.Bottom,
            };
            Controls.Add(acceptButton);
            Controls.Add(new PropertyGrid
            {
                SelectedObject = settings,
                Dock = DockStyle.Fill
            });
            AcceptButton = acceptButton;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Text = "Настройки";
        }
    }
}
