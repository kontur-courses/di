using LightInject;
using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = ContainerProvider.GetContainer();
            var path = Path.Combine(Path.GetFullPath(@"..\..\..\texts"), "test.txt");

            var parsed = container.GetInstance<IParser>().Parse(path);
            var tags = container.GetInstance<ITagComposer>().ComposeTags(parsed);
            var painted = container.GetInstance<ITagPainter>().Paint(tags);
            var cloudPainter = container.GetInstance<TagCloudPainter>();
            
            cloudPainter.Paint(painted);
        }
    }
}
