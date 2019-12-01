using System;
using System.Drawing;
using System.Windows.Forms;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Solved.Step07.Infrastructure.Injection;
using FractalPainting.Solved.Step07.Infrastructure.UiActions;
using Ninject;

namespace FractalPainting.Solved.Step07.App
{
    public class MainForm : Form
    {
        public MainForm(IUiAction[] actions,
            PictureBoxImageHolder pictureBox,
            Palette palette)
        {
            var imageSettings = CreateSettingsManager().Load().ImageSettings;
            ClientSize = new Size(imageSettings.Width, imageSettings.Height);

            var mainMenu = new MenuStrip();
            mainMenu.Items.AddRange(actions.ToMenuItems());
            Controls.Add(mainMenu);

            pictureBox.RecreateImage(imageSettings);
            pictureBox.Dock = DockStyle.Fill;
            Controls.Add(pictureBox);

            DependencyInjector.Inject<IImageHolder>(actions, pictureBox);
            DependencyInjector.Inject<IImageDirectoryProvider>(actions, CreateSettingsManager().Load());
            DependencyInjector.Inject<IImageSettingsProvider>(actions, CreateSettingsManager().Load());
            DependencyInjector.Inject(actions, palette);
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