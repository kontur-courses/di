using System;
using System.Drawing;
using CloodLayouter.Infrastructer;

namespace CloodLayouter.App
{
    public class ImageSaver : IImageSaver
    {
        private readonly IProvider<string> directoryProvider;

        private readonly IProvider<Bitmap> imageHolder;
        //TODO  add SaveSettings(file format .png, .bpm, e.t.c.)


        public ImageSaver(IProvider<Bitmap> imageHolder, IProvider<string> directoryProvider)
        {
            this.imageHolder = imageHolder;
            this.directoryProvider = directoryProvider;
        }

        public void Save()
        {
            imageHolder.Get().Save(directoryProvider.Get());
            Console.WriteLine("saved to: " + directoryProvider.Get());
        }
    }
}