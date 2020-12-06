using System.Windows.Forms;
using TagsCloud.ClientGUI.Infrastructure;
using TagsCloud.Common;
using TagsCloud.Core;

namespace TagsCloud.ClientGUI
{
    public class TagsCloudPainter
    {
        private readonly PathSettings pathSettings;
        private readonly CloudVisualization visualizer;

        public TagsCloudPainter(PictureBoxImageHolder pictureBox, 
            PathSettings pathSettings, CloudVisualization visualizer)
        {
            PictureBox = pictureBox;
            this.pathSettings = pathSettings;
            this.visualizer = visualizer;
        }

        public PictureBoxImageHolder PictureBox { get; }

        public void Paint(ICircularCloudLayouter cloud)
        {
            var words = TagsHelper.GetWords(pathSettings.PathToText, pathSettings.PathToBoringWords,
                pathSettings.PathToDictionary, pathSettings.PathToAffix);

            visualizer.Paint(cloud, words);

            PictureBox.Refresh();
            Application.DoEvents();
        }
    }
}