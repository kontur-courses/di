using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TagsCloudVisualization.Services;
using TagsCloudVisualization.UI.Actions;

namespace TagsCloudVisualization
{
    public partial class MainForm : Form
    {
        private readonly Panel imagePanel;
        private readonly Panel buttonPanel;

        public MainForm(IUiAction[] menuActions, PictureBoxImageHolder imageHolderBox)
        {
            WindowState = FormWindowState.Maximized;
            buttonPanel = InitializeButtonPanel(menuActions);
            imagePanel = InitializeImagePanel(imageHolderBox);
            ResizeImagePanel();
            Controls.Add(buttonPanel);
            Controls.Add(imagePanel);
        }

        protected override void OnResize(EventArgs e)
        {
            ResizeImagePanel();
        }

        private void ResizeImagePanel()
        {
            imagePanel.Size = new Size(ClientSize.Width, ClientSize.Height - buttonPanel.Height);
        }

        private FlowLayoutPanel InitializeImagePanel(PictureBox imageHolderBox)
        {
            imageHolderBox.Dock = DockStyle.Fill;
            imageHolderBox.SizeMode = PictureBoxSizeMode.AutoSize;
            var panel = new FlowLayoutPanel
            {
                AutoScroll = true,
                Location = new Point(0, buttonPanel.Height)
            };
            panel.Controls.Add(imageHolderBox);
            return panel;
        }

        private Panel InitializeButtonPanel(IEnumerable<IUiAction> actions)
        {
            var buttonSize = new Size(200, 80);
            var panel = new Panel
            {
                AutoSize = true,
                Dock = DockStyle.Fill,
                BackColor = Color.LightSteelBlue,
                MaximumSize = new Size(0, buttonSize.Height)
            };
            var buttonPosition = Point.Empty;
            foreach (var action in actions)
            {
                var button = CreateButtonFromAction(action, buttonSize, buttonPosition);
                buttonPosition = new Point(buttonPosition.X + buttonSize.Width, 0);
                panel.Controls.Add(button);
            }
            return panel;
        }

        private Button CreateButtonFromAction(IUiAction action, Size size, Point position)
        {
            var button = new Button();
            button.Click += action.Perform;
            button.BackColor = Color.AliceBlue;
            button.Text = action.Name;
            button.Font = new Font(FontFamily.GenericMonospace, 15, FontStyle.Bold);
            button.Size = size;
            button.Location = position;
            return button;
        }
    }
}