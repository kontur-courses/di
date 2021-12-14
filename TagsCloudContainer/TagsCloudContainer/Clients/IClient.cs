using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer
{
    public interface IClient
    {
        Color GetTextColor();
        Color GetBackgoundColor();
        FontFamily GetFontFamily();
        Size GetImageSize();
        void ShowPathToNewFile(string path);
        string GetNameForImage();
        bool IsFinish();
    }
}
