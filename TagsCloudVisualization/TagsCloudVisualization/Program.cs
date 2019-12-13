using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using Autofac;
using TagsCloudVisualization.Interfaces;
using TagsCloudVisualization.Providers;
using TagsCloudVisualization.Providers.Layouter;
using TagsCloudVisualization.Providers.Layouter.Interfaces;
using TagsCloudVisualization.Providers.Layouter.Spirals;
using TagsCloudVisualization.Providers.Sizable;
using TagsCloudVisualization.Providers.Sizable.Interfaces;
using TagsCloudVisualization.Settings;
using TagsCloudVisualization.WordSource;
using TagsCloudVisualization.WordSource.Changers;
using TagsCloudVisualization.WordSource.Interfaces;
using TagsCloudVisualization.WordSource.Readers;
using TagsCloudVisualization.WordSource.Selectors;

namespace TagsCloudVisualization
{
    internal class Program
    {
        internal static void Main()
        {
            var workingDirectory = Environment.CurrentDirectory;
            var pathToResources = workingDirectory;
            var fullPath = workingDirectory + "\\cloud.bmp";
            for (var i = 0; i < 3; i++)
            {
                pathToResources = Directory.GetParent(pathToResources).FullName;
            }

            pathToResources += "\\Resources";
            var textDirectory = pathToResources + "\\HarryPotter.txt";

            var readerSettings=new ReaderSettings(textDirectory,1000,"");
            var drawerSettings= new DrawerSettings(new SolidBrush(Color.Red), Color.Blue, new Font("ComicSans", 1));
            var layouterSettings=new LayouterSettings(new Point(0,0),1,SpiralType.Rectangle );
            var container = PrepareContainer();
            var bitmap=container.Resolve<TagCreator<string>>().DrawTag(readerSettings, drawerSettings, layouterSettings);

            Saver(bitmap, pathToResources);
        }

        private static void Saver(Bitmap bitmap, string pathToResources)
        {
            ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
            var myEncoder = System.Drawing.Imaging.Encoder.Quality;
            var myEncoderParameter = new EncoderParameter(myEncoder, 0L);
            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            myEncoderParameters.Param[0] = myEncoderParameter;
            bitmap.Save(pathToResources + "/TagCloud.jpg", jpgEncoder, myEncoderParameters);
            Console.WriteLine("rectangle saved to: " + pathToResources);
        }

        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }

            return null;
        }

        //1. Need to write out badWordsList and reed it from resources folder also the same with HarryPotter book
        //2. Need to Fix Encoding
        //3. Need to add more functions
        private static IContainer PrepareContainer()
        {
            var builder = new ContainerBuilder();
            
            builder.RegisterType<TextReaderFactory>().AsSelf().SingleInstance();
            builder.RegisterType<TextSplitter>().As<IObjectReader<string>>().SingleInstance();
            builder.RegisterType<WordChangerFactory>().As<IChangerFactory<string>>().SingleInstance();
            builder.RegisterType<WordSelectorFactory>().As<ISelectorFactory<string>>().SingleInstance();
            builder.RegisterType<WordSourceProvider>().As<IObjectProvider<string>>().SingleInstance();

            builder.RegisterType<FrequencyProvider<string>>().As<IFrequencyProvider<string>>();

            builder.RegisterType<SizableSelector>().As<ISizableSelector<string,int>>().SingleInstance();
            builder.RegisterType<SizableProvider>().As<ISizableProvider<string>>().SingleInstance();

            builder.RegisterType<SpiralFactory>().AsSelf().SingleInstance();
            builder.RegisterType<DrawableCloudLayouter>().As<IDrawableProvider<string>>();

            builder.RegisterType<TagDrawer>().As<IDrawer<string>>().SingleInstance();

            builder.RegisterType<TagCreator<string>>().AsSelf().SingleInstance();


            return builder.Build();
        }
    }
}