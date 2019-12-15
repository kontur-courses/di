using Autofac;
using System.Drawing;
using TagsCloudGenerator;
using TagsCloudGenerator.CloudPrepossessing;
using TagsCloudGenerator.ShapeGenerator;

namespace TagsCloudConsoleUI.DIPresetModules
{
    internal class CircularRandomCloudModule : Module
    {
        private readonly Color backgroundColor;
        private readonly Color[] paletteColors;
        private readonly Point center;
        private readonly float spiralStep;

        public CircularRandomCloudModule(BuildOptions options)
        {
            backgroundColor = ColorsHexConverter.CreateFromHex(options.BackgroundColor);
            paletteColors = ColorsHexConverter.CreateFromHexEnumerable(options.ColorsPalette);
            center = new Point(options.Width / 2, options.Height / 2);
            spiralStep = options.SpiralStep;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CloudFormat>();
            builder.RegisterType<RandomTagOrder>().As<ITagOrder>();
            builder.RegisterType<FirstBigLetterPreform>().As<ITagTextPreform>();
            builder.RegisterType<RandomlyCloudPainer>().As<ICloudColorPainter>()
                .WithParameters(new[]{
                    new NamedParameter("paletteColors", paletteColors),
                    new NamedParameter("backgroundColor", backgroundColor)
                });

            builder.RegisterType<CircularCloudPrepossessing>().As<ITagsPrepossessing>()
                .WithParameter(new NamedParameter("center", center));

            builder.RegisterType<ArchimedeanShape>().As<IShapeGenerator>()
                .WithParameters(new[]
                {
                    new NamedParameter("center", center),
                    new NamedParameter("spiralStep", spiralStep),
                });
        }
    }
}