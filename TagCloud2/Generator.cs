using System;
using System.Drawing;
using System.Reflection;
using Ninject;
using TagCloud2.Image;
using TagCloud2.Text;
using TagCloud2.TextGeometry;
using TagCloudVisualisation;
using Ninject.Extensions.Conventions;
using System.Collections.Generic;

namespace TagCloud2
{
    public static class GeneratorHelper
    {
        public static Dictionary<string, Action> dictionary;
        
        public static void Initialize(StandardKernel kernel)
        {
            dictionary = new();
            dictionary.Add("txt", () => kernel.Bind<IFileReader>().To<TxtFileReader>());
            dictionary.Add("docx", () => kernel.Bind<IFileReader>().To<DocxFileReader>());
            dictionary.Add("png", () => kernel.Bind<IImageFormatter>().To<PngImageFormatter>());
            dictionary.Add("jpeg", () => kernel.Bind<IImageFormatter>().To<JpegImageFormatter>());
        }
    }

    public class Generator
    {
        public void Generate(IOptions options)
        {
            var kernel = new StandardKernel();
            GeneratorHelper.Initialize(kernel);
            kernel.Bind(x => x
            .FromThisAssembly()
            .SelectAllClasses()
            .BindAllInterfaces()
            );
            kernel.Bind<Core>().ToSelf();
            kernel.Bind<ICloudLayouter>().To<CircularCloudLayouter>();
            var Spiral = new ArchimedeanSpiral(new Point(500, 500));
            kernel.Bind<ISpiral>().ToConstant(Spiral);
            kernel.Unbind<IFileReader>();
            kernel.Unbind<IImageFormatter>();
            GeneratorHelper.dictionary[options.Format].Invoke();
            GeneratorHelper.dictionary[options.OutputFormat].Invoke();
            var core2 = kernel.Get<Core>();
            core2.Run(options.Path, options.FontName, options.OutputName, options.FontSize);
        }
    }
}
