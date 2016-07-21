using System;
using System.Drawing;
using System.Windows.Forms;
using FractalPainting.App.Actions;
using FractalPainting.Infrastructure;
using NUnit.Framework;

namespace FractalPainting.App
{
	public class MainForm : Form
	{
		private readonly PictureBoxImageHolder pictureBox;
		private readonly ImageSettings imageSettings;

		public MainForm() : this(new IUiAction[] { new SaveImageAction(), new DragonFractalAction(), new KochFractalAction(),  new ImageSettingsAction(), new PaletteSettingsAction(),  })
		{

		}

		public MainForm(IUiAction[] actions)
		{
			pictureBox = new PictureBoxImageHolder();
			var mainMenu = new MenuStrip();

			var settingsManager = new SettingsManager(new XmlObjectSerializer(), new FileBlobStorage());
			var settings = settingsManager.Load();
			imageSettings = settings.ImageSettings;
			var palette = new Palette();

			mainMenu.Items.AddRange(actions.ToMenuItems());
			Controls.Add(mainMenu);

			pictureBox.Dock = DockStyle.Fill;
			Controls.Add(pictureBox);

			ClientSize = new Size(imageSettings.Width, imageSettings.Height);

			DependencyInjector.Inject<IImageHolder>(actions, pictureBox);
			DependencyInjector.Inject<IImageDirectoryProvider>(actions, settings);
			DependencyInjector.Inject<IImageSettingsProvider>(actions, settings);
			DependencyInjector.Inject(actions, palette);
		}

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
			Text = "Fractal Painter";
			pictureBox.RecreateImage(imageSettings);
		}
	}
}
