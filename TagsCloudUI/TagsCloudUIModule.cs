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
            var parser = new TextParser(new WordValidator());
            var spiral = new ArchimedeanSpiral(new Point(200, 200), 0.2, 1.0);
            var layouter = new CircularCloudLayouter(spiral, new Point(200, 200));
            var container = new TagsCloudContainer.TagsCloudContainer.TagsCloudContainer(parser, layouter);

            builder.RegisterInstance(parser).As<ITextParser>();
            builder.RegisterInstance(spiral).As<ISpiral>();
            builder.RegisterInstance(layouter).As<ILayouter>();
            builder.RegisterInstance(container).As<ITagsContainer>();
            builder.RegisterType<TagsCloudForm>();
        }
    }
}