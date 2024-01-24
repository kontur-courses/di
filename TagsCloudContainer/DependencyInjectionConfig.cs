using Microsoft.Extensions.DependencyInjection;
using TagsCloudContainer.FrequencyAnalyzers;
using TagsCloudContainer.TextTools;

namespace TagsCloudContainer
{
    public class DependencyInjectionConfig
    {
        public static IServiceCollection AddCustomServices(IServiceCollection services)
        {
            services.AddScoped<TextFileReader>();
            services.AddScoped<FrequencyAnalyzer>();

            return services;
        }
    }
}
