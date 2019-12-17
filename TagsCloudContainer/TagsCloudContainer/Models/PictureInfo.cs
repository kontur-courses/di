using System;
using System.Collections.Generic;

namespace TagsCloudContainer
{
    public class PictureInfo
    {
        public PictureInfo(string fileName, string format="png")
        {
            FileName = fileName;
            Format = format;
        }

        public string Format
        {
            get { return format; }
            set
            {
                if (!availableFormats.Contains(value))
                    throw new ArgumentException("Not valid format");
                format = value;
            }
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

        private string format;
        private string fileName;
        private readonly HashSet<string> availableFormats = new HashSet<string>(
            new[] { "png", "bmp", "gif", "jpeg" });
    }
}