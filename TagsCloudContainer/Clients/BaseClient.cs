using System;
using System.Drawing;
using TagsCloudContainer.Savers;

namespace TagsCloudContainer.Clients
{
    public abstract class BaseClient
    {
        protected readonly Func<TagsCloudSettings> SettingsFactory;
        private readonly Func<TagsCloud> cloudFactory;
        private readonly IImageSaver saver;

        protected BaseClient(Func<TagsCloudSettings> settingsFactory, Func<TagsCloud> cloudFactory, IImageSaver saver)
        {
            SettingsFactory = settingsFactory;
            this.cloudFactory = cloudFactory;
            this.saver = saver;
        }

        public abstract void Run();

        protected Image CreateTagsCloud(TagsCloudSettings settings)
        {
            return cloudFactory().Create(settings);
        }

        protected void SaveTagsCloud(string path, Image image)
        {
            saver.Save(path, image);
        }
    }
}