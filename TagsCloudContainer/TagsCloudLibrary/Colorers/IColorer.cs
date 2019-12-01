using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TagsCloudLibrary.Colorers
{
    public interface IColorer
    {
        string Name { get; }
        Color ColorForWord(string word, double factor);
    }
}
