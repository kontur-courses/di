using System;
using System.Drawing;
using System.Windows.Forms;
using FractalPainting.Infrastructure;

namespace FractalPainting.App
{
	public class MainForm : Form
	{
		public MainForm(ImageSettings imageSettings, PictureBoxImageHolder pictureBox, IUiAction[] actions)
		{
			ClientSize = new Size(imageSettings.Width, imageSettings.Height);

			var mainMenu = new MenuStrip();
			mainMenu.Items.AddRange(actions.ToMenuItems());
			Controls.Add(mainMenu);

			pictureBox.RecreateImage(imageSettings);
			pictureBox.Dock = DockStyle.Fill;
			Controls.Add(pictureBox);
		}

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
			Text = "Fractal Painter";
		}
	}
}