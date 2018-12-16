using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using TagCloud;
using TagCloud.Layouters;

namespace GUITagClouder
{
    public class CloudImageHolder : PictureBox, IImageHolder
    {
        private CloudSettings cloudSettings;
        private DrawingSettings drawingSettings;
        private IPathProvider appPathProvider;

        public CloudImageHolder(CloudSettings initialCloudSettings, DrawingSettings initialDrawingSettings,
            IPathProvider appPathProvider)
        {
            cloudSettings = initialCloudSettings;
            drawingSettings = initialDrawingSettings;
            this.appPathProvider = appPathProvider;
        }

        public Size GetImageSize() =>
            Image.Size;

        public Graphics StartDrawing()=>
            Graphics.FromImage(Image);

        public void UpdateUi()
        {
            Refresh();
            Application.DoEvents();
        }
        
        //TODO Remove overloads somehow
        public void RecreateImage() =>
            RecreateImage(cloudSettings, drawingSettings);
        
        public void RecreateImage(DrawingSettings newSettings)=>
            RecreateImage(cloudSettings,newSettings);
        
        public void RecreateImage(CloudSettings newSettings)=>
            RecreateImage(newSettings,drawingSettings);

        private void RecreateImage(CloudSettings cloudSettings, DrawingSettings drawingSettings)
        {
            if(appPathProvider.SourcePath == null)
                return;
            var maker = Cloud.CreateMaker(cloudSettings, drawingSettings);
            maker.UpdateFrom(File.OpenRead(appPathProvider.SourcePath));
            //TODO change draw cloud signature.
            Image = maker.DrawCloud();
            
            //This objects is different only first time. I hope its normal.
            this.cloudSettings = cloudSettings;
            this.drawingSettings = drawingSettings;
        }

        public void SaveImage()
        {
            Image.Save(appPathProvider.TargetPath);
        }
    }
}