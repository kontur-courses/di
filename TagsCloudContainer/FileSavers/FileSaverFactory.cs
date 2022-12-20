using System;
using TagsCloudContainer.UI;

namespace TagsCloudContainer.FileSavers
{
    public class FileSaverFactory
    {
        private readonly Func<IUi> settingsProvider;

        public FileSaverFactory(Func<IUi> settingsProvider)
        {
            this.settingsProvider = settingsProvider;
        }

        public IFileSaver Create()
        {
            var actualSettings = settingsProvider.Invoke();
            return actualSettings.FormatToSave switch
            {
                "png" => new PngSaver(),
                "jpeg" => new JpegSaver(),
                "gif" => new GifSaver(),
                "bmp" => new BmpSaver(),
                _ => throw new ArgumentException("Wrong file format")
            };
        }
    }
}