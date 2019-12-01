using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using TagsCloudContainer.CloudLayouters;
using TagsCloudContainer.WordCounters;

namespace TagsCloudContainer
{
    class Program
    {
        static void Main(string[] args)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterType<ICloudLayouter>().As<CircularCloudLayouter>().WithParameter("center", new Point());
            containerBuilder.RegisterType<IWordCounter>().As<SimpleWordCounter>();
        }
    }
}
