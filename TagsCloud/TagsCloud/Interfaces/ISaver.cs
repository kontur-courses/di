namespace TagsCloudGenerator.Interfaces
{
    public interface ISaver : IFactorial
    {
        bool TrySaveTo(string filePath, System.Drawing.Bitmap bitmap);
    }
}