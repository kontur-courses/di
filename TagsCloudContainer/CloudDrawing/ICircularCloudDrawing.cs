using System.Collections.Generic;

namespace CloudDrawing
{
    public interface ICircularCloudDrawing
    {
        void SetOptions(ImageSettings imageSettings);
        void DrawWords(IEnumerable<(string, int)> wordsFontSize, WordDrawSettings settings);
        void SaveImage(string filename);
    }
}