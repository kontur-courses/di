using System.Drawing;
using TagsCloudVisualization.Infrastructure.Parsers;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.InfrastructureUI
{
    public interface IImageHolder
    {
        Size GetImageSize();

        Graphics StartDrawing();

        void UpdateUi();

        void RecreateImage(ImageSettings settings);

        void SaveImage(string fileName);

        void SetFile(string path);

        void SetParser(IParser parser);
    }
}