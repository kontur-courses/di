using System;
using System.Drawing;
using System.Windows.Forms;
using FractalPainting.App.Actions;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.Injection;
using FractalPainting.Infrastructure.UiActions;
using Ninject;

namespace FractalPainting.App
{
    public class MainForm : Form
    {
        public MainForm(IUiAction[] actions)
        {
            var imageSettings = CreateSettingsManager().Load().ImageSettings;
            ClientSize = new Size(imageSettings.Width, imageSettings.Height);

            var mainMenu = new MenuStrip();
            mainMenu.Items.AddRange(actions.ToMenuItems());
            Controls.Add(mainMenu);

            var pictureBox = new PictureBoxImageHolder();
            pictureBox.RecreateImage(imageSettings);
            pictureBox.Dock = DockStyle.Fill;
            Controls.Add(pictureBox);

            DependencyInjector.Inject<IImageHolder>(actions, pictureBox);
            DependencyInjector.Inject<IImageDirectoryProvider>(actions, CreateSettingsManager().Load());
            DependencyInjector.Inject<IImageSettingsProvider>(actions, CreateSettingsManager().Load());
            DependencyInjector.Inject(actions, new Palette());
        }

        private static SettingsManager CreateSettingsManager()
        {
            var container = new StandardKernel();
            container.Bind<IObjectSerializer>().To<XmlObjectSerializer>();
            container.Bind<IBlobStorage>().To<FileBlobStorage>();
            return container.Get<SettingsManager>();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            Text = "Fractal Painter";
        }
    }
}