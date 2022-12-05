using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloudGraphicalUserInterface
{
    public class DependencyInjector
    {
        public static void Inject<TDependency>(object service, TDependency dependency)
        {
            var need = service as IDependency<TDependency>;
            need?.SetDependency(dependency);
        }

        public static void Inject<TDependency>(IEnumerable services, TDependency dependency)
        {
            foreach (var service in services)
                Inject(service, dependency);
        }
    }
}
