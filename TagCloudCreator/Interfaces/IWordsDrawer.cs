using System.Drawing;

namespace TagCloudCreator.Interfaces;

public interface IWordsDrawer
{
    public Size GetRectSizeFor(string word);
    
}