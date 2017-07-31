using System.Collections;

namespace FractalPainting.Solved.Step11.Infrastructure
{
	public class DependencyInjector
	{
		public static void Inject<TDependency>(object service, TDependency dependency)
		{
			var need = service as INeed<TDependency>;
			need?.SetDependency(dependency);
		}

		public static void Inject<TDependency>(IEnumerable services, TDependency dependency)
		{
			foreach (var service in services)
				Inject(service, dependency);
		}
	}
}