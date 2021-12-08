using Autofac;
using TagsCloudContainer.Abstractions;
using TagsCloudContainer.Registrations;

namespace TagsCloudContainer.Defaults;

public class TagPacker : ITagPacker
{
    private readonly ITextAnalyzer textAnalyzer;

    public TagPacker(ITextAnalyzer textAnalyzer)
    {
        this.textAnalyzer = textAnalyzer;
    }

    public IEnumerable<ITag> GetTags()
    {
        var stats = textAnalyzer.AnalyzeText();

        foreach (var pair in stats.Statistics.OrderByDescending(x => x.Value))
        {
            var relativeSize = (double)pair.Value / stats.TotalWordCount;

            var tag = new Tag(pair.Key, relativeSize);

            yield return tag;
        }
    }

    [Register]
    public static void Register(ContainerBuilder builder)
    {
        builder.RegisterType<TagPacker>().AsSelf().As<ITagPacker>();
    }
}
