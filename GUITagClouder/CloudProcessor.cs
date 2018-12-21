using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using Extensions;
using TagCloud;
using TagCloud.Layouters;

namespace GUITagClouder
{
    public class CloudProcessor : PictureBox
    {
        private CloudSettings cloudSettings;
        private DrawingSettings drawingSettings;

        public CloudProcessor(CloudSettings initialCloudSettings, DrawingSettings initialDrawingSettings)
        {
            cloudSettings = initialCloudSettings;
            drawingSettings = initialDrawingSettings;
        }
        
        public string SourcePath {private get; set; }
        
        public void RecreateImage(DrawingSettings newSettings)
        {
            drawingSettings = newSettings;
            RecreateImage();
        }

        public void RecreateImage(CloudSettings newSettings)
        {
            cloudSettings = newSettings;
            RecreateImage();
        }

        public void RecreateImage()
        {
            if(SourcePath == null)
                return;
            Image?.Dispose();
            Result.Of(() => Cloud.CreateMaker(cloudSettings, drawingSettings))
                .ThenAct(maker => maker.UpdateFrom(File.OpenRead(SourcePath)))
                .Then(maker => maker.DrawCloud())
                .Then(image => Image = image)
                .OnFail(e => MessageBox.Show(e));
        }

        public void SaveImage(string targetPath)=>
            Image.Save(targetPath);
    }
}