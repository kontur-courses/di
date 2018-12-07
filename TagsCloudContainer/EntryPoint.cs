using System.Drawing;
using System.Drawing.Imaging;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using TagsCloudContainer.Drawing;
using TagsCloudContainer.Input;
using TagsCloudContainer.Layout;
using TagsCloudContainer.Processing.Converting;
using TagsCloudContainer.Processing.Filtering;

namespace TagsCloudContainer
{
    public class EntryPoint
    {
        private static void Main(string[] args)
        {
            var imageSettings = new ImageSettings(
                size: new Size(1024, 1024),
                textFont: new Font(FontFamily.GenericMonospace, 10, FontStyle.Regular),
                maxFontSize: 100,
                minFontSize: 10,
                backgroundColor: Color.White,
                textColor: Color.Black);

            var circularCloudLayout = new CircularCloudLayout(new Point(512, 512), new Size(1024, 1024));

            var container = new WindsorContainer();
            container.Kernel.Resolver.AddSubResolver(new ArrayResolver(container.Kernel));

            container.Register(Component.For<IFileReader>().ImplementedBy(typeof(TxtReader)));
            container.Register(Component.For<IWordFilter>().ImplementedBy(typeof(CommonWordsFilter))); 
            container.Register(Component.For<IWordConverter>().ImplementedBy(typeof(EmptyConverter)));
            container.Register(Component.For<WordParser>());
            container.Register(Component.For<ImageSettings>().Instance(imageSettings));
            container.Register(Component.For<IRectangleLayout>().Instance(circularCloudLayout));
            container.Register(Component.For<WordLayout>());
            container.Register(Component.For<IDrawer>().ImplementedBy(typeof(ImageDrawer)));
            container.Register(Component.For<IWriter>().ImplementedBy(typeof(FileWriter)));
            container.Register(Component.For<WriterProvider>().Instance(new WriterProvider(null)));



            var text = container.Resolve<IFileReader>().Read("Resources\\sample.txt");  // CommonFilter очень плохо оптимизирован, поэтому на бОльших текстах его пока нет смысла запускать
            var parsedWords = container.Resolve<WordParser>().ParseWords(text);

            var wordLayout = container.Resolve<WordLayout>();
            var fileWriter = container.Resolve<IWriter>();
            var writerProvider = container.Resolve<WriterProvider>();
            writerProvider.Writer = fileWriter;

            wordLayout.PlaceWords(parsedWords);
            fileWriter.WriteToFile("test.bmp", ImageFormat.Bmp);
        }
    }
}