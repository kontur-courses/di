using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer
{
    public class ClientControl : IClientControl
    {
        private readonly IClient client;
        private readonly IWordCloudSaver saver;

        public ClientControl(IClient client, IWordCloudSaver saver)
        {
            this.client = client;
            this.saver = saver;
        }

        internal bool IsFinish()
        {
            return client.IsFinish();
        }

        public ImageSettings GetImageSettings() => new ImageSettings
            (
                client.GetImageSize(),
                client.GetFontFamily(),
                client.GetTextColor(),
                client.GetBackgoundColor()
            );

        public string GetNameForImage() => client.GetNameForImage();

        public void ShowPathToImage(string path) => client.ShowPathToNewFile(path);
    }
}
