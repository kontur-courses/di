namespace TagsCloudGenerator.Interfaces
{
    public interface ISaver
    {
        bool TrySaveTo(string filePath, System.Drawing.Bitmap bitmap);
    }
}