using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer
{
    public class App
    {
        private WordCloudSaver saver;
        private ClientControl clientControl;
        public App(ClientControl clientControl, WordCloudSaver saver)
        {
            this.clientControl = clientControl;
            this.saver = saver;
        }

        public void Start()
        {
            while (true)
            {
                var imageName = clientControl.GetNameForImage();
                var customSettings = clientControl.GetImageSettings();
                var pathToImage = saver.SaveCloud(imageName, customSettings);
                clientControl.ShowPathToImage(pathToImage);
            }
        }
    }
}
