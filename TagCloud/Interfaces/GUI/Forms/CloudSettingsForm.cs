using System;
using System.Drawing;
using System.Windows.Forms;
using TagCloud.CloudVisualizerSpace.CloudViewConfigurationSpace;

namespace TagCloud.Interfaces.GUI.Forms
{
    class CloudSettingsForm : Form
    {
        private CloudViewConfiguration configuration;

        public CloudSettingsForm(CloudViewConfiguration configuration)
        {
            this.configuration = configuration;
            InitializeForm();
        }

        private void InitializeForm()
        {
            Controls.Add(new PropertyGrid
            {
                SelectedObject = configuration,
                Dock = DockStyle.Fill,
                MaximumSize = new Size(Size.Width, Size.Height - 20)
            });

            var fontFamily = new Button
            {
                Text = "Выбрать шрифт",
                Dock = DockStyle.Bottom
            };
            fontFamily.Click += HandleFontFamilyButton;

            Controls.Add(fontFamily);

            Controls.Add(new Button {Text = "OK", Dock = DockStyle.Bottom, DialogResult = DialogResult.OK});
        }

        private void HandleFontFamilyButton(object sender, EventArgs e)
        {
            var dialog = new FontDialog();

            var result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                configuration.FontFamily = dialog.Font.FontFamily;
            }
        }
    }
}
