using System;
using System.Drawing;
using System.Windows.Forms;
using FractalPainting.App.Actions;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;

namespace FractalPainting.App
{
    public class MainForm : Form
    {
        private ImageSettings settings;

        public MainForm(IUiAction[] actions, ImageSettings settings, PictureBoxImageHolder holder)
        {
            this.settings = settings;
            ClientSize = new Size(settings.Width, settings.Height);

            var mainMenu = new MenuStrip();
            mainMenu.Items.AddRange(actions.ToMenuItems());
            Controls.Add(mainMenu);

            holder.RecreateImage(settings);
            holder.Dock = DockStyle.Fill;
            Controls.Add(holder);

            //DependencyInjector.Inject<IImageHolder>(actions, pictureBox);
            //DependencyInjector.Inject<IImageDirectoryProvider>(actions, CreateSettingsManager().Load());
            //DependencyInjector.Inject<IImageSettingsProvider>(actions, CreateSettingsManager().Load());
            //DependencyInjector.Inject(actions, new Palette());
        }

        //private static SettingsManager CreateSettingsManager()
        //{
        //    var container = new StandardKernel();
        //    container.Bind<IObjectSerializer>().To<XmlObjectSerializer>();
        //    container.Bind<IBlobStorage>().To<FileBlobStorage>();
        //    return container.Get<SettingsManager>();
        //}

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            Text = "Fractal Painter";
        }
    }
}