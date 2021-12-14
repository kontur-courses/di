using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer
{
    public class App
    {
        private IWordCloudSaver saver;
        private ClientControl clientControl;
        private bool flag = true;
        public App(ClientControl clientControl, IWordCloudSaver saver)
        {
            this.clientControl = clientControl;
            this.saver = saver;
        }

        public void Start(string pathToSaveDir, ImageFormats format)
        {
            while (flag)
            {
                flag = !clientControl.IsFinish();
                var imageName = clientControl.GetNameForImage();
                var customSettings = clientControl.GetImageSettings();
                var pathToImage = saver.SaveCloud(pathToSaveDir, imageName, customSettings, format);
                clientControl.ShowPathToImage(pathToImage);
            }
        }
    }
}
