using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloudGraphicalUserInterface
{
    public class AppSettings : IDirectoryProvider
    {
        public string ImagesDirectory { get; set; }
        public ImageSettings ImageSettings { get; set; }
    }
}
