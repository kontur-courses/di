using System;
using System.Drawing;
using TagsCloudContainer.Cloud;
using TagsCloudContainer.Savers;

namespace TagsCloudContainer.Clients
{
    public abstract class BaseClient
    {
        protected readonly TagsCloudSettings Settings;
        private readonly Func<TagsCloud> cloudFactory;
        private readonly IImageSaver saver;

        protected BaseClient(TagsCloudSettings settings, Func<TagsCloud> cloudFactory, IImageSaver saver)
        {
            Settings = settings;
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