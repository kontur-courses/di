using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using CommandLine;
using HomeExerciseTDD.Options;
using HomeExerciseTDD.settings;

namespace HomeExerciseTDD
{
    public class ConsoleCloudClient
    {
        public void ConsoleHandler(string[] args, ContainerBuilder builder)
        {
            Parser.Default
                .ParseArguments<Options.Options>(args)
                .WithParsed(options => 
            {
                FilesOptionHandler(options.WordsPath, options.BoringPath, builder);
                WordOptionHandler(options.Font, options.Coefficient, builder);
                SpiralOptionHandler(options.CenterX, options.CenterY, builder);
                PainterOptionHandler(options.Wight, options.Height, options.ImageName, options.Format, options.Color, builder);
            });
        }

        private void FilesOptionHandler(string wordPath, string boringWordPath, ContainerBuilder builder)
        {
            builder.RegisterType<FileProcessor>().As<IFileProcessor>()
                .WithParameters(new Parameter[] 
                {new NamedParameter("pathWords", wordPath), 
                    new NamedParameter("pathBoringWords", boringWordPath) });
        }

        private void WordOptionHandler(string fontText, int coefficient, ContainerBuilder builder)
        {
            FontFamily font;
            try
            { 
                font = new FontFamily(fontText);
            }
            catch (ArgumentException e)
            {
                font = new FontFamily("Microsoft Sans Serif");
            }
            var wordSettings = new WordSettings(font, coefficient);
            builder.RegisterInstance(wordSettings).As<WordSettings>();
        }
        
        private void SpiralOptionHandler(int x, int y, ContainerBuilder builder)
        {
            var center = new Point(x,y);
            var spiralSettings = new SpiralSettings(center);
            builder.RegisterInstance(spiralSettings).As<SpiralSettings>();
        }

        private void PainterOptionHandler(int width, int height, string fileName, string format, KnownColor knownColor, ContainerBuilder builder)
        {
            var resultFormat = GetImageFormat(format);
            var color = Color.FromKnownColor(knownColor);
            var painterSettings = new PainterSettings(width,height,fileName,resultFormat, color);
            builder.RegisterInstance(painterSettings).As<PainterSettings>();
        }
        
        private static ImageFormat GetImageFormat(string extension)  
        {
            var prop = typeof(ImageFormat)
                .GetProperties()
                .FirstOrDefault(p => p.Name.Equals(extension, StringComparison.InvariantCultureIgnoreCase));

            var result = prop != null
                ? prop.GetValue(prop) as ImageFormat
                : ImageFormat.Png;
    
            return result;  
        }  
        
    }
}