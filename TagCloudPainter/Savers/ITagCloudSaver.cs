namespace TagCloudPainter.Savers;

public interface ITagCloudSaver
{
    void SaveTagCloud(string outputPath, string inputPath);
}