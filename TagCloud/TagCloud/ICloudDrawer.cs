namespace TagCloud;

public interface ICloudDrawer
{
    public Bitmap Draw(IEnumerable<Label> texts);
}