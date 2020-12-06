using System;
using System.Drawing;
using TagsCloudContainer.App.Settings;
using TagsCloudContainer.Infrastructure.CloudGenerator;

namespace TagsCloudContainer.App.CloudGenerator
{
    internal class CloudLayouterFactory : ICloudLayouterFactory
    {
        private readonly AppSettings appSettings;

        public CloudLayouterFactory(AppSettings appSettings)
        {
            this.appSettings = appSettings;
        }
        public ICloudLayouter CreateCloudLayouter()
        {
            switch (appSettings.LayouterAlgorithm)
            {
                case CloudLayouterAlgorithm.CircularCloudLayouter:
                    return new CircularCloudLayouter(new Point(appSettings.ImageSettings.Width / 2,
                        appSettings.ImageSettings.Height / 2));
            }

            throw new NotImplementedException("No found this algorithm");
        }
    }
}