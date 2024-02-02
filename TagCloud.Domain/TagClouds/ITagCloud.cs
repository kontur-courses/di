using System.Drawing;

public interface ITagCloud
{
    Bitmap CreateCloud(string text);
}