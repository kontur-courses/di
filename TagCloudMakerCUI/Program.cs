using System.Collections;
using TagCloud;
using Autofac;

namespace TagCloudMakerCUI
{
    class Program
    {
        void ConfigurationPoint()
        {
            var container = new ContainerBuilder();
        }
        static void Main(string[] args)
        {
            var q = new WordProcessor(new []{""}, new[] { "" }, true);
        }
    }
}
