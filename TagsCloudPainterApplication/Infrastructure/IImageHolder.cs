namespace TagsCloudPainterApplication.Infrastructure;

public interface IImageHolder
{
    Size GetImageSize();
    Graphics StartDrawing();
    void UpdateUi();
    void SaveImage(string fileName);
}