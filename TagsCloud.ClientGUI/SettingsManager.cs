using System;
using System.IO;
using System.Windows.Forms;
using TagsCloud.ClientGUI.Infrastructure;

namespace TagsCloud.ClientGUI
{
    public class SettingsManager
    {
        public AppSettings Load()
        {
            return new AppSettings
            {
                ImagesDirectory = ".",
                ImageSettings = new ImageSettings()
            };
        }
    }
}
