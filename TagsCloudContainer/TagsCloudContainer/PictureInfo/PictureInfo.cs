using System;
using System.Drawing;

namespace TagsCloudContainer
{
    public class PictureInfo
    {
        public PictureInfo(string fileName, string format="png")
        {
            FileName = fileName;
            Format = format;
        }

        public string Format { get; set; }

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
        
        private string fileName;
    }
}