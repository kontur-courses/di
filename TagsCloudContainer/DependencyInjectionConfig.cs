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
            services.AddTransient<FrequencyAnalyzer>();

            return services;
        }
    }
}

// AddTransient
// Transient подразумевает, что сервис создается каждый раз, когда его запрашивают. Этот жизненный цикл лучше всего подходит для легковесных, не фиксирующих состояние, сервисов.

// AddScoped
// Scoped - сервис создаются единожды для каждого запроса.

// AddSingleton
// Singleton - сервис создается при первом запросе (или при запуске ConfigureServices, если вы указываете инстанс там), а затем каждый последующий запрос будет использовать этот же инстанс.
