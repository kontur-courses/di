using Autofac;
using TagsCloudContainer.Abstractions;
using TagsCloudContainer.Registrations;

namespace TagsCloudContainer.Defaults;

public class StemNormalizer : IWordNormalizer
{
    private readonly MyStem.MyStem myStem;

    public StemNormalizer(MyStem.MyStem myStem)
    {
        this.myStem = myStem;
    }

    public string Normalize(string word)
    {
        return myStem.AnalyzeWord(word).Stem;
    }

    [Register]
    public static void Register(ContainerBuilder builder)
    {
        builder.RegisterType<StemNormalizer>().AsSelf().As<IWordNormalizer>();
    }
}
