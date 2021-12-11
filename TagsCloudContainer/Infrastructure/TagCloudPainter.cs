using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer;

public class TagCloudPainter
{
    private ICloudLayouter layouter;
    private Settings settings;

    public TagCloudPainter(ICloudLayouter layouter,
        Settings settings)
    {
        this.layouter = layouter;
        this.settings = settings;
    }

    public string Paint(IEnumerable<PaintedTag> tags)
    {    
        var bm = new Bitmap(settings.ImageSize.Height, settings.ImageSize.Width);
        var graphics = Graphics.FromImage(bm);
        graphics.Clear(settings.Palette.Background);

        foreach (var tag in PutCloudTags(tags))
        {
            graphics.DrawString(tag.Text, tag.Label.Font, 
                new SolidBrush(tag.Color), tag.Rectangle);
        }

        var layoutsPath = Path.Combine(Path.GetFullPath(@"..\..\..\"), "layouts");
        var savePath = $"{layoutsPath}\\layout_{DateTime.Now:HHmmssddMM}.{settings.Format}";
        bm.Save(savePath, settings.Format);
        return savePath;
    }

    public IEnumerable<CloudTag> PutCloudTags(IEnumerable<PaintedTag> tags)
    {
        foreach (var tag in tags)
        {
            var fontSize = ((float)tag.Frequency) * settings.Font.Size * 28;
            var label = new Label { AutoSize = true };
            label.Font = new Font(settings.Font.FontFamily, fontSize, settings.Font.Style);
            label.Text = tag.Text;
            var size = label.GetPreferredSize(new Size(1000, 1000));
            var rectangle = layouter.PutNextRectangle(size);
            yield return new CloudTag(tag, label, rectangle);            
        }
    }
}