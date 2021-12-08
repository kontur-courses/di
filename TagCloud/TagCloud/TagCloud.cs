using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Autofac;
using TagCloud.Drawing;
using TagCloud.Extensions;
using TagCloud.Layout;
using TagCloud.TextProcessing;
using TagCloud.Utils;

namespace TagCloud
{
    public class TagCloud
    {
        private readonly IContainer _container;
        private List<Dictionary<string, int>> _processedTexts = new();
        private List<Bitmap> _tagClouds;

        public TagCloud()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>();
            builder.RegisterType<Drawer>().As<IDrawer>();
            builder.RegisterType<ArchimedeanSpiral>().As<ICurve>();
            builder.RegisterType<TextProcessor>().AsSelf();
            builder.RegisterType<WordLayouter>().AsSelf();
            builder.RegisterType<CoordinatesConverter>().AsSelf();
            _container = builder.Build();
        }

        public int ProcessText(ITextProcessingOptions options)
        {
            Console.WriteLine("Начинаю обработку текста");
            _processedTexts = _container.Resolve<TextProcessor>().GetInterestingWords(options).ToList();
            Console.WriteLine("Обработка завершена\n");
            return 0;
        }

        public int DrawTagClouds(IDrawerOptions options)
        {
            foreach (var text in _processedTexts)
            {
                Console.WriteLine("Раскладываю текст");
                var layoutedWords = _container.Resolve<WordLayouter>().Layout(options, text);
                Console.WriteLine("Рисую bitmap\n");
                _container.Resolve<IDrawer>().Draw(options, layoutedWords).SaveDefault();
            }
            Console.WriteLine("Готово!\n");
            return 0;
        }
    }
}