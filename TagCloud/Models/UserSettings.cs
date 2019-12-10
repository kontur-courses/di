using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud.Models
{
    public class UserSettings
    {
        public ImageSettings ImageSettings { get; }
        public string PathToRead { get; }
        public UserSettings(ImageSettings imageSettings, string pathToRead)
        {
            ImageSettings = imageSettings;
            PathToRead = pathToRead;
        }
    }
}
