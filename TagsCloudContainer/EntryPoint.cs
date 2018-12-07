using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using Castle.MicroKernel.Registration;
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
            //var text = new TxtReader().Read("Resources\\article.txt");
            //var parser = new WordParser(new[] {new CommonWordsFilter()}, new IWordConverter[0]);
            //var parsed = parser.ParseWords(text);

            var parsed = new Dictionary<string, int>()
            {
                {"hello1", 30},
                {"hello2", 14},
                {"hello3", 10},
                {"hello4", 2},
            };

            var imageSettings = new ImageSettings(
                size: new Size(1024, 1024),
                textFont: new Font(FontFamily.GenericMonospace, 10, FontStyle.Regular),
                maxFontSize: 40,
                backgroundColor: Color.White,
                textColor: Color.Black);

            var circularCloudLayout = new CircularCloudLayout(new Point(512, 512), new Size(1024, 1024));


            var container = new WindsorContainer();

            container.Register(Component.For<ImageSettings>().Instance(imageSettings));
            container.Register(Component.For<IRectangleLayout>().Instance(circularCloudLayout));
            container.Register(Component.For<WordLayout>());
            container.Register(Component.For<IDrawer>().ImplementedBy(typeof(ImageDrawer)));
            container.Register(Component.For<IWriter>().ImplementedBy(typeof(FileWriter)));


            var wordLayout = container.Resolve<WordLayout>();
            var fileWriter = container.Resolve<IWriter>();

            wordLayout.PlaceWords(parsed);
            fileWriter.WriteToFile("test.bmp", ImageFormat.Bmp);
        }
    }
}