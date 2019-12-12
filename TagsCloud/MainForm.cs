using System.Drawing;
using System.Windows.Forms;
using TagsCloud.Interfaces;
using TagsCloud.MenuActions;

namespace TagsCloud
{
	public class MainForm: Form
	{
		public MainForm(IMenuAction[] actions, ImageSettings imageSettings, PictureBoxImageHolder pictureBox)
		{
			var mainMenu = new MenuStrip();
			mainMenu.Items.AddRange(actions.ToMenuItems());
			Controls.Add(mainMenu);

			pictureBox.RecreateImage(imageSettings);
			pictureBox.Dock = DockStyle.Fill;
			Controls.Add(pictureBox);

			WindowState = FormWindowState.Maximized;
			Text = "Tags cloud";
		}
	}
}