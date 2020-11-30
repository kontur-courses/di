using System.Drawing;
using Autofac;
using TagsCloudContainer.TagsCloudVisualization;

namespace TagsCloudContainer
{
    public class ContainerConfig : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TextWriter>().As<ITextWriter>();
            builder.RegisterType<TextParser>().As<ITextParser>();
            builder.RegisterType<WordValidator>().As<IWordValidator>();
            builder.Register((c, p) => new CircularCloudLayouter(p.Named<Point>("center"))).As<ILayouter>();
            builder.Register(
                (c, p) => new TagsCloudForm(p.Named<ITextParser>("parser"), p.Named<ILayouter>("layouter")));
        }
    }
}