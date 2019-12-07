using System;
using System.Drawing;
using System.Windows.Forms;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Solved.Step08.Infrastructure.Injection;
using FractalPainting.Solved.Step08.Infrastructure.UiActions;

namespace FractalPainting.Solved.Step08.App
{
    public class MainForm : Form
    {
        public MainForm(IUiAction[] actions,
            PictureBoxImageHolder pictureBox,
            Palette palette,
            ImageSettings imageSettings,
            IImageDirectoryProvider imageDirectoryProvider)
        {
            ClientSize = new Size(imageSettings.Width, imageSettings.Height);

            var mainMenu = new MenuStrip();
            mainMenu.Items.AddRange(actions.ToMenuItems());
            Controls.Add(mainMenu);

            pictureBox.RecreateImage(imageSettings);
            pictureBox.Dock = DockStyle.Fill;
            Controls.Add(pictureBox);

            DependencyInjector.Inject<IImageHolder>(actions, pictureBox);
            DependencyInjector.Inject(actions, imageDirectoryProvider);
            DependencyInjector.Inject(actions, palette);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            Text = "Fractal Painter";
        }
    }
}