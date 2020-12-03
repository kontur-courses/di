using System.Drawing;
using Autofac;
using TagsCloudContainer.TagsCloudContainer;
using TagsCloudContainer.TagsCloudContainer.Interfaces;
using TagsCloudContainer.TagsCloudVisualization;
using TagsCloudContainer.TagsCloudVisualization.Interfaces;

namespace TagsCloudUI
{
    public class TagsCloudUIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TextWriter>().As<ITextWriter>();
            builder.RegisterType<TextParser>().As<ITextParser>();
            builder.RegisterType<WordValidator>().As<IWordValidator>();
            builder.Register((c, p) => new ArchimedeanSpiral(p.Named<Point>("center"),
                p.Named<double>("distanceBetweenLoops"), p.Named<double>("angleDelta"))).As<ISpiral>();
            builder.Register((c, p) => new CircularCloudLayouter(p.Named<ISpiral>("spiral"), p.Named<Point>("center")))
                .As<ILayouter>();
            builder.Register((c, p) =>
                new TagsCloudContainer.TagsCloudContainer.TagsCloudContainer(p.Named<ITextParser>("parser"),
                    p.Named<ILayouter>("layouter"))).As<ITagsContainer>();
            builder.Register((c, p) =>
                new TagsCloudForm(p.Named<ITagsContainer>("container"), p.Named<string>("text")));
        }
    }
}