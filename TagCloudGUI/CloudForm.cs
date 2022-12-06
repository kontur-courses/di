using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
 

namespace TagCloudGraphicalUserInterface
{
    public class CloudForm:Form
    {
        public CloudForm(IActionForm[] actionForms, PictureBoxTags pictureBox, AppSettings settings, ImageSettings imageSettings, Palette palette)
        {
          
            ClientSize = new Size(imageSettings.Width, imageSettings.Height);
            var mainMenu = new MenuStrip();
            mainMenu.Items.AddRange(actionForms.ToMenuItems());
            Controls.Add(mainMenu);
            pictureBox.RecreateImage(imageSettings);
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.SizeMode=PictureBoxSizeMode.Zoom;
            Controls.Add(pictureBox);
            DependencyInjector.Inject<IImage>(actionForms, pictureBox);
            DependencyInjector.Inject(actionForms, settings);
            DependencyInjector.Inject(actionForms, palette);
        }

    }
}
