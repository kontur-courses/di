using System.Drawing;
 using System.Drawing.Imaging;
 
 namespace TagsCloudVisualization.BitmapSavers
 {
     public class JpegBitmapSaver : IBitmapSaver
     {
         public void Save(Bitmap bitmap, string path) => bitmap.Save($"{path}.jpeg", ImageFormat.Jpeg);
     }
 }