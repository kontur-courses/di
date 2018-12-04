using System;
using System.Collections.Generic;
using System.Drawing;
using TagCloud.Models;

namespace TagCloud.Utility.Data
{
    public class Logger : ILogger // Should keep data for future using, probably wrong naming(suggested: Data,Info)
    {
        public CloudItem[] Items { get; private set; }
        public Bitmap Picture { get; private set; }
        public List<Exception> Exceptions { get; }

        public Logger()
        {
            Exceptions = new List<Exception>();
        }

        public void Log(object obj)
        {
            switch (obj)
            {
                case CloudItem[] cloudItems:
                    Items = cloudItems;
                    break;
                case Bitmap bitmap:
                    Picture = bitmap;
                    break;
                case Exception e:
                    Exceptions.Add(e);
                    break;
            }
        }
    }
}