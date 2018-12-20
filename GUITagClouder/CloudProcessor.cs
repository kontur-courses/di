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
        
        public string SourcePath { private get; set; }

        public Size GetImageSize() =>
            Image.Size;

        public Graphics StartDrawing()=>
            Graphics.FromImage(Image);

        public void UpdateUi()
        {
            Refresh();
            Application.DoEvents();
        }  
        
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
            //Combobox will solve this problem. 
            //TODO must dispose cloud
            Result.Of(() => Cloud.CreateMaker(cloudSettings, drawingSettings))
                .ThenAct(maker => maker.UpdateFrom(File.OpenRead(SourcePath)))
                .Then(maker => maker.DrawCloud())
                .Then(image => Image = image)
                .OnFail(e => MessageBox.Show(e));
            
//            var maker = Cloud.CreateMaker(cloudSettings, drawingSettings);
//            maker.UpdateFrom(File.OpenRead(appPathProvider.SourcePath));
//            Image = maker.DrawCloud();
            
        }

        public void SaveImage(string targetPath)=>
            Image.Save(targetPath);
    }
}