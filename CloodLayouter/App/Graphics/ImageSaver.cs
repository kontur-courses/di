using System;
using System.Drawing;
using CloodLayouter.Infrastructer;

namespace CloodLayouter.App
{
    public class ImageSaver : IImageSaver
    {
        private readonly IDrawer drawer;
        private string filePathToSave;
        //TODO  add SaveSettings(file format .png, .bpm, e.t.c.)


        public ImageSaver(IDrawer drawer, string filePathToSave)
        {
            this.drawer = drawer;
            this.filePathToSave = filePathToSave;
        }

        public void Save()
        {
            drawer.Draw().Save(filePathToSave);
            Console.WriteLine("saved to: " + filePathToSave);
        }
    }
}