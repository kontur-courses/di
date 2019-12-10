using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudForm.Common
{
    public interface IPalette
    {
        Color PrimaryColor { get; set; }

        Color SecondaryColor { get; set; }

        Color BackgroundColor { get; set; }
    }
}
