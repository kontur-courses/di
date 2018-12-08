using CloodLayouter.Infrastructer;

namespace CloodLayouter.App
{
    public class ImageSaver : IImageSaver
    {
        private readonly ISaveDirectoryProvider directoryProvider;

        private readonly IImageHolder imageHolder;
        //TODO  add SaveSettings(file format .png, .bpm, e.t.c.)


        public ImageSaver(IImageHolder imageHolder, ISaveDirectoryProvider directoryProvider, FileWordProvider provider)
        {
            this.imageHolder = imageHolder;
            this.directoryProvider = directoryProvider;
        }

        public void Save()
        {
            imageHolder.Image.Save(directoryProvider.DirectoryToSave);
        }
    }
}