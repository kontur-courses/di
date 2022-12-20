using System;
using TagsCloudContainer.FileOpeners;
using TagsCloudContainer.UI;

namespace TagsCloudContainer.FileReaders
{
    public class FileReaderFactory
    {
        private readonly Func<IUi> settingsProvider;

        public FileReaderFactory(Func<IUi> settingsProvider)
        {
            this.settingsProvider = settingsProvider;
        }

        public IFileReader Create()
        {
            var actualSettings = settingsProvider.Invoke();
            var index = actualSettings.PathToOpen.LastIndexOf('.');
            var format = actualSettings.PathToOpen.Substring(index + 1);
            return format switch
            {
                "txt" => new TxtReader(),
                _ => throw new ArgumentException("Wrong path to open")
            };
        }
    }
}