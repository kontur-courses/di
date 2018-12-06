using System;
using System.Drawing;
using System.IO;
using CloodLayouter.Infrastructer;
using System.Windows.Forms;

namespace CloodLayouter.App
{
    public class GraphicsHolder : IGraphicsHolder
    {
        private readonly Bitmap Image;
        private readonly IImageDirectoryProvider directoryProvider;

        public GraphicsHolder(IImageDirectoryProvider directoryProvider)
        {
            Image = new Bitmap(500,500);
            this.directoryProvider = directoryProvider;
        }
        
        public void Save()
        {
            
            var dialog = new SaveFileDialog
            {
                CheckFileExists = false,
                InitialDirectory = Path.GetFullPath(directoryProvider.ImageDirectory),
                DefaultExt = "png",
                FileName = "image.png",
                Filter = "Изображения (*.png)|*.png" 
            };
            Image.Save(directoryProvider.ImageDirectory + Path.PathSeparator + "saved.png");
            Console.WriteLine("saved to: " + directoryProvider.ImageDirectory + Path.DirectorySeparatorChar +
                              "saved.png");
        }

        public void Draw(Rectangle rectangle, string word)
        {
            var graphics = Graphics.FromImage(Image);
            graphics.DrawString(word,SystemFonts.DefaultFont,Brushes.Blue,rectangle);
        }
    }
}