using Autofac;
using System.Drawing;
using TagsCloudGenerator;

namespace TagsCloudConsoleUI.DIPresetModules
{
    internal class BitmapImageCreatorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BitmapCloudBuilder>().As<ICloudBuilder<Bitmap>>();
        }
    }
}