using System;
using System.Drawing;
using System.Reflection;
using Ninject;
using TagCloud2.Image;
using TagCloud2.Text;
using TagCloud2.TextGeometry;
using TagCloudVisualisation;
using Ninject.Extensions.Conventions;

namespace TagCloud2
{
    public class Generator
    {
        public void Generate()
        {
            var kernel = new StandardKernel();
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
            kernel.Bind<IFileReader>().To<DocxFileReader>();
            var core2 = kernel.Get<Core>();
            core2.Run("input.docx", SystemFonts.DefaultFont, "output.jpg");
        }
    }
}
