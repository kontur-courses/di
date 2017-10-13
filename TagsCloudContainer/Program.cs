using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInjector;
using TagsCloudContainer;
using TagsCloudContainer.Implementation;

namespace TagsCloudContainer
{
    class Program
    {
        private static readonly Container Container;
        private static string fileName = "words.txt";

        static Program()
        {
            string[] boringWords = new[] { "Аврора", "Агата", "Александрина", "Алира", "Альберта", "Авигея" };
            Font _font = new Font(FontFamily.GenericMonospace, 16, FontStyle.Bold, GraphicsUnit.Point);
            Point _center = new Point(500, 500);

            Container = new Container();
            Container.Register<IFileParser>(() => new TxtFileParser(fileName));
            Container.Register<BoringWordsFormater>(() => new BoringWordsFormater(boringWords));
            Container.RegisterCollection<IWordFormater>(new[] { typeof(BoringWordsFormater), typeof(LowerCaseFormater) });
            Container.Register<IWordPreprocessor, SimpleWordPreprocessor>();
            Container.Register<ITagsData, TagsData>();
            Container.Register<ITagSizeNormalizer>(() => new TagSizeNormalizer(_font));
            Container.Register<ICircularCloudLayouter>(() => new CircularCloudLayouter(_center));
            Container.Register<ITagsCloudContainer, TagsCloudContainer>();
            Container.RegisterCollection<IColorPicker>(new[] { typeof(RandomColorPicker), typeof(WhiteColorPicker) });
            Container.Register<ITagsCloudVisualizator>(() => new TagsCloudVisualizator(Container.GetInstance<ITagsCloudContainer>(), Container.GetAllInstances<IColorPicker>().ToArray(), _center, _font));
            Container.Register<ITagCloudSaver>(() => new TagsCloudSaver(Container.GetInstance<ITagsCloudVisualizator>(), "bitmap.png", ImageFormat.Png));
            Container.Verify();
        }

        static void Main(string[] args)
        {
            using (Container)
            {
                var tagCloudSaver = Container.GetInstance<ITagCloudSaver>();
                tagCloudSaver.Save();
            }
        }
    }
}
