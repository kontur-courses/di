using System;
using System.Drawing;
using System.Windows.Forms;
using FractalPainting.App.Actions;
using FractalPainting.Infrastructure;

namespace FractalPainting.App
{
	public class MainForm : Form
	{
		private readonly PictureBoxImageHolder pictureBox;
		private readonly ImageSettings imageSettings;

		public MainForm() : this(new IUiAction[] { new SaveImageAction(), new DragonFractalAction(), new TriangleFractalAction(),  new ImageSettingsAction(), })
		{

		}

		public MainForm(IUiAction[] actions)
		{
			pictureBox = new PictureBoxImageHolder();
			var mainMenu = new MenuStrip();
			var settingsManager = new SettingsManager(new XmlObjectSerializer(), new FileBlobStorage());
			var settings = settingsManager.Load();
			imageSettings = settings.ImageSettings;

			mainMenu.Items.AddRange(actions.ToMenuItems());
			Controls.Add(mainMenu);

			pictureBox.Dock = DockStyle.Fill;
			Controls.Add(pictureBox);

			ClientSize = new Size(imageSettings.Width, imageSettings.Height);

			DependencyInjector.Inject<IImageHolder>(actions, pictureBox);
			DependencyInjector.Inject<IImageDirectorySettings>(actions, settings);
			DependencyInjector.Inject(actions, imageSettings);
		}

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
			Text = "Fractal Painter";
			pictureBox.RecreateImage(imageSettings);
		}
	}
}
