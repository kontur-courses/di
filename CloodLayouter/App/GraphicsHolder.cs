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
        private readonly ITagDrawingSettingsProvider tagSettings;

        public GraphicsHolder(IImageDirectoryProvider directoryProvider, IImageSettingsProvider imageSettingsProvider,
            ITagDrawingSettingsProvider tagSetting)
        {
            Image = new Bitmap(imageSettingsProvider.ImageSize.Width, imageSettingsProvider.ImageSize.Height);
            this.directoryProvider = directoryProvider;
            this.tagSettings = tagSetting;
        }
        
        
        
        public void Save()
        {
            var savePath = directoryProvider.ImageDirectory + Path.DirectorySeparatorChar + "saved.png";
            Image.Save(savePath);
            Console.WriteLine("saved to: " + savePath);
        }


        public SizeF Measure(Tag tag)
        {
            var graphics = Graphics.FromImage(Image);
            return graphics.MeasureString(tag.Word, tag.Font);
        }

        public void Draw(Rectangle rectangle, Tag tag)
        {
            var graphics = Graphics.FromImage(Image);
            graphics.DrawString(tag.Word,tag.Font, Brushes.Blue,rectangle);
            
        }
    }
}