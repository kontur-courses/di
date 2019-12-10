using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TagsCloudVisualization.Actions;

namespace TagsCloudVisualization
{
    public partial class MainForm : Form
    {
        public MainForm(IUiAction[] menuActions, PictureBoxImageHolder imageHolderBox)
        {
            WindowState = FormWindowState.Maximized;
            var buttonPanel = InitializeButtonPanel(menuActions);
            var panel = InitializeImagePanel(imageHolderBox, new Padding(0, buttonPanel.Height,0,0));
            Controls.Add(buttonPanel);
            Controls.Add(panel);
        }

        private FlowLayoutPanel InitializeImagePanel(PictureBox imageHolderBox, Padding panelPadding)
        {
            imageHolderBox.Dock = DockStyle.Fill;
            imageHolderBox.SizeMode = PictureBoxSizeMode.AutoSize;
            var imagePanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
            };
            imagePanel.VerticalScroll.Maximum = 200;
            imageHolderBox.AutoScrollOffset = new Point(100, 100);
            imagePanel.DockPadding.Top = panelPadding.Top;
            imagePanel.Controls.Add(imageHolderBox);
            return imagePanel;
        }

        private Panel InitializeButtonPanel(IEnumerable<IUiAction> actions)
        {
            var buttonSize = new Size(200, 80);
            var buttonPanel = new Panel
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
                buttonPanel.Controls.Add(button);
            }
            return buttonPanel;
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