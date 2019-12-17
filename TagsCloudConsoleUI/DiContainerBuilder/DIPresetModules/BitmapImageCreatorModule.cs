using System.Drawing;
using Autofac;
using TagsCloudGenerator;

namespace TagsCloudConsoleUI.DiContainerBuilder
{
    internal class BitmapImageCreatorModule : DiPreset
    {
        public BitmapImageCreatorModule(BuildOptions options) : base(options)
        { }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BitmapCloudBuilder>().As<ICloudBuilder<Bitmap>>();
        }
    }
}