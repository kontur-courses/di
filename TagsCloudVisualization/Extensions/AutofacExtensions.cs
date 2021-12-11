using Autofac;

namespace TagsCloudVisualization.Extensions
{
    internal static class AutofacExtensions
    {
        public static Settings ResolveSettings(this IComponentContext ctx) => ctx.Resolve<Settings>();
    }
}