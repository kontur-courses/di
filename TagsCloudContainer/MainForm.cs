using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer
{
    public partial class MainForm : Form
    {
        public MainForm(Settings settings, IUiAction actions)
        {
            ClientSize = settings.ImageSize;
            var mainMenu = new MenuStrip();
            //InitializeComponent();
        }
    }
}
