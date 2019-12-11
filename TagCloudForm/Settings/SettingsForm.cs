using System;
using System.Windows.Forms;

namespace TagCloudForm.Settings
{
    public class SettingsForm<TSettings> : Form
    {
        private SettingsForm(TSettings settings)
        {
            var okButton = new Button
            {
                Text = "OK",
                DialogResult = DialogResult.OK,
                Dock = DockStyle.Bottom,
            };
            Controls.Add(okButton);
            Controls.Add(new PropertyGrid
            {
                SelectedObject = settings,
                Dock = DockStyle.Fill
            });
            AcceptButton = okButton;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Text = "Настройки";
        }

        public static SettingsForm<TSettings> For(TSettings settings)
        {
            return new SettingsForm<TSettings>(settings);
        }
    }
}