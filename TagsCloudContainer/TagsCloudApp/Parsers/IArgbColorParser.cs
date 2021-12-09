using System.Drawing;

namespace TagsCloudApp.Parsers
{
    public interface IArgbColorParser
    {
        Color Parse(string value);
    }
}