using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace TagsCloudVisualizationDI.Saving
{
    public class DefaultSaver : ISaver
    {
        private string SavePath { get; }

        public DefaultSaver(string savePath, ImageFormat imageFormat)
        {
            SavePath = savePath + '.' + imageFormat;
        }

        public string GetSavePath()
        {
            return SavePath;
        }
    }
}
