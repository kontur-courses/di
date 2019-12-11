using System.Drawing;
using Autofac;
using TagsCloudGenerator.CloudPrepossessing;
using TagsCloudGenerator.ShapeGenerator;

namespace TagsCloudConsoleUI.DIModules
{
    internal class CircularCloudBasedOnArchimedean : Module
    {
        protected readonly Point Center;
        protected readonly double SpiralStep;

        public CircularCloudBasedOnArchimedean(Point center, double spiralStep)
        {
            Center = center;
            SpiralStep = spiralStep;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(new ArchimedeanShape(Center, SpiralStep))
                .As<IShapeGenerator>();//TODO Don't call constructor
            builder.RegisterType<CircularCloudPrepossessing>().As<ITagsPrepossessing>();
        }
    }
}