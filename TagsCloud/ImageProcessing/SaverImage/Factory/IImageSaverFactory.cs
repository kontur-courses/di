using System;
using TagsCloud.ImageProcessing.SaverImage.ImageSavers;

namespace TagsCloud.ImageProcessing.SaverImage.Factory
{
    public interface IImageSaverFactory
    {
        IImageSaver Create();
        IImageSaverFactory Register(string imageEntesion, Func<IImageSaver> creationFunc);
    }
}
