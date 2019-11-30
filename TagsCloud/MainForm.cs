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
			ClientSize = new Size(imageSettings.Width, imageSettings.Height);

			var mainMenu = new MenuStrip();
			mainMenu.Items.AddRange(actions.ToMenuItems());
			Controls.Add(mainMenu);

			pictureBox.Dock = DockStyle.Fill;
			Controls.Add(pictureBox);

			Text = "Tags cloud";
		}
	}
}