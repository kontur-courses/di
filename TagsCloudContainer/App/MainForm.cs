using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NUnit.Framework;
using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Infrastructure.UiActions;

namespace TagsCloudContainer
{
    public partial class MainForm : Form
    {
        //public MainForm()
        //{
        //    InitializeComponent();

        //}

        public MainForm(IUiAction[] actions, PictureBoxImageHolder pictureBox,
            ImageSettings imageSettings)
        {
            ClientSize = new Size(imageSettings.Width, imageSettings.Height);

            var mainMenu = new MenuStrip();
            mainMenu.Items.AddRange(actions.ToMenuItems());
            Controls.Add(mainMenu);

            pictureBox.RecreateImage(imageSettings);
            pictureBox.Dock = DockStyle.Fill;
            Controls.Add(pictureBox);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            Text = "Tags cloud visualization";
        }


        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    var cloudLayouter = new CircularCloudLayouter(new Point(600, 350), true);
        //    var tags = new ReadTagsFromTxt().ReadTagsFromFile("test.txt");
        //    var brush = new SolidBrush(Color.Black);
        //    var tagsLayouter = new TagsLayouter(cloudLayouter, tags, new FontFamily("Arial"), 60.0f
        //        , brush, new Bitmap(1200, 700));
        //    tagsLayouter.PutAllTags();
        //    foreach (var tag in tagsLayouter.Tags)
        //    {
        //        e.Graphics.DrawString(tag.TagText, tag.TagFont, brush, tag.TagRect);
        //    }
        //}
    }
}
