using System;
using System.Drawing;
using System.Windows.Forms;
using FractalPainting.App.Actions;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;
using Ninject;

namespace FractalPainting.App
{
    public class MainForm : Form
    {
        //public MainForm()
        //    : this(
        //        new IUiAction[]
        //        {
        //            new SaveImageAction(),
        //            new DragonFractalAction(),
        //            new KochFractalAction(),
        //            new ImageSettingsAction(),
        //            new PaletteSettingsAction()
        //        })
        //{
        //}

        public MainForm(IUiAction[] actions, PictureBoxImageHolder pictureBox, Palette palette,
            ImageSettings imageSettings)
        {
            ClientSize = new Size(imageSettings.Width, imageSettings.Height);
            var mainMenu = new MenuStrip();
            mainMenu.Items.AddRange(actions.ToMenuItems());
            Controls.Add(mainMenu);

            pictureBox.RecreateImage(imageSettings);
            pictureBox.Dock = DockStyle.Fill;
            Controls.Add(pictureBox);

            //DependencyInjector.Inject<IImageHolder>(actions, pictureBox);
            //DependencyInjector.Inject<IImageDirectoryProvider>(actions, appSettings);
            //DependencyInjector.Inject(actions, palette);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            Text = "Fractal Painter";
        }
    }
}