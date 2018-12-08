using TagsCloudContainer.Util;
using TagsCloudContainer.Cloud;

namespace TagsCloudContainer
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new AutofacContainer(args);                       

            var exampleImage = new TagCloudRenderer(container.TagCloud, container.FontName, container.Brush)
                .GenerateImage();
            exampleImage.Save(container.OutputPath);
        }
    }
}
