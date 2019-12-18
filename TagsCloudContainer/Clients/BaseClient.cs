using System;
using System.Drawing;
using TagsCloudContainer.Cloud;
using TagsCloudContainer.Savers;

namespace TagsCloudContainer.Clients
{
    public abstract class BaseClient
    {
        protected readonly TagsCloudSettings CloudSettings;
        protected readonly ServiceSettings ServiceSettings;
        private readonly Func<TagsCloudGenerator> cloudFactory;
        private readonly FileImageSaver saver;

        protected BaseClient(
            TagsCloudSettings cloudSettings,
            ServiceSettings serviceSettings,
            Func<TagsCloudGenerator> cloudFactory,
            FileImageSaver saver)
        {
            CloudSettings = cloudSettings;
            ServiceSettings = serviceSettings;
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