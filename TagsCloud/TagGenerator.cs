using System.Drawing;
using System.Windows.Forms;
using TagsCloud.Settings;

namespace TagsCloud;

public class TagGenerator
{
    private readonly ICircularCloudLayouter cloudLayouter;
    private readonly TagSettings settings;
    public TagGenerator(ICircularCloudLayouter cloudLayouter, TagSettings settings)
    {
        this.cloudLayouter = cloudLayouter;
        this.settings = settings;
        
    }

    public IEnumerable<Tag> GetTagsList(IEnumerable<WordInfo> wordInfos)
    {
        foreach (var info in wordInfos)
        {
            var font = new Font(settings.FontFamily, info.Count * settings.Size);
            var textSize = TextRenderer.MeasureText(info.Word, font);
            var textRectangle = cloudLayouter.PutNextRectangle(new Size(textSize.Width, textSize.Height));
            yield return new Tag(font, info.Word, textRectangle, settings.Color);
        }
    }
}