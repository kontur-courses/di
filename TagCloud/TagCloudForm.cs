using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace TagCloud
{
    public partial class TagCloudForm : Form
    {
        private PictureBox pictureBox;

        private TagCloudForm()
        {
            InitializeComponent();
            Paint += TagCloudForm_Paint;
        }

        

        private void SetPictureBox()
        {
            pictureBox = new PictureBox
            {
                Size = new Size(1366, 768),
            };
        }
        

        public static void Main()
        {
            Application.Run(new TagCloudForm());
        }
    }
}