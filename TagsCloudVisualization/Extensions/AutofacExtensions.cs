using Autofac;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.Extensions
{
    internal static class AutofacExtensions
    {
        public static GeneralSettings ResolveSettings(this IComponentContext ctx) => ctx.Resolve<GeneralSettings>();
    }
}