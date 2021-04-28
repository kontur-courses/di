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
        public MainForm(IUiAction[] actions, IImageHolder imageHolder, Palette palette, SettingsManager settingsManager)
        {
            var imageSettings = settingsManager.Load().ImageSettings;
            ClientSize = new Size(imageSettings.Width, imageSettings.Height);

            var mainMenu = new MenuStrip();
            mainMenu.Items.AddRange(actions.ToMenuItems());
            Controls.Add(mainMenu);

            Controls.Add(imageHolder as PictureBox);
 
            DependencyInjector.Inject<IImageHolder>(actions, imageHolder);
            DependencyInjector.Inject<IImageDirectoryProvider>(actions, settingsManager.Load());
            DependencyInjector.Inject<IImageSettingsProvider>(actions, settingsManager.Load());
            DependencyInjector.Inject(actions, palette);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            Text = "Fractal Painter";
        }
    }
}