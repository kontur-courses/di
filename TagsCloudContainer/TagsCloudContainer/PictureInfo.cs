using System;
using System.Drawing;

namespace TagsCloudContainer
{
    public class PictureInfo
    {
        public PictureInfo(string fileName, Point tagCloudCenter)
        {
            FileName = fileName;
            TagCloudCenter = tagCloudCenter;
        }

        public string FileName
        {
            get { return fileName; }
            set
            {
                if (value.Length == 0)
                    throw new ArgumentException("File name must be not empty");
                fileName = value;
            }
        }

        public Point TagCloudCenter { get; set; }

        private string fileName;
    }
}