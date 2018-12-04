using System;
using System.IO;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using TagsCloud.CloudLayouter;
using TagsCloud.WordPrework;

namespace TagsCloud
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new WindsorContainer();
            container.Register(Component.For<IPointGenerator>().ImplementedBy<SpiralPointGenerator>());

           
        }
    }
}
