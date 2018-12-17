using System.Drawing;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using TagsCloudContainer.Drawing;
using TagsCloudContainer.Input;
using TagsCloudContainer.Layout;
using TagsCloudContainer.Processing.Converting;
using TagsCloudContainer.Processing.Filtering;
using TagsCloudContainer.UI;

namespace TagsCloudContainer
{
    public class EntryPoint
    {
        private static void Main(string[] args)
        {
            var container = new WindsorContainer();
            container.Kernel.Resolver.AddSubResolver(new ArrayResolver(container.Kernel));

            container.Register(
                Component.For<IUI>().ImplementedBy<ConsoleUI>(),
                Component.For<IFileReader>().ImplementedBy<TxtReader>(),

                Component.For<IWordFilter>().ImplementedBy<DefaultFilter>(),
                Component.For<IWordFilter>().ImplementedBy<CommonWordsFilter>(),
                Component.For<IWordFilter>().ImplementedBy<BlackListFilter>(),

                Component.For<IWordConverter>().ImplementedBy<EmptyConverter>(),

                Component.For<WordParser>(),
                Component.For<ImageSettings>().DependsOn(
                    Dependency.OnValue("size", new Size(1024, 1024)),
                    Dependency.OnValue("textFont", new Font(FontFamily.GenericMonospace, 10, FontStyle.Regular)),
                    Dependency.OnValue("maxFontSize", 100),
                    Dependency.OnValue("minFontSize", 10),
                    Dependency.OnValue("backgroundColor", Color.White),
                    Dependency.OnValue("textColor", Color.Red)),

                Component.For<IRectangleLayout>().ImplementedBy<CircularCloudLayout>().DependsOn(
                    Dependency.OnValue("center", new Point(512, 512)),
                    Dependency.OnValue("size", new Size(1024, 1024))),

                Component.For<WordLayout>(),
                Component.For<IDrawer>().ImplementedBy<ImageDrawer>(),
                Component.For<IWriter>().ImplementedBy<FileWriter>()
            );


            var ui = container.Resolve<IUI>();

            string textFile;
            string imageFile;

            (textFile, imageFile) = ui.RetrievePaths(args);

            //.TransformWords(textFile, imageFile);
        }
    }
}