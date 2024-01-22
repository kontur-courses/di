using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer.FreqAnalyzer;
using TagsCloudContainer.TextTools;

namespace TagsCloudContainer
{
    public class DependencyInjectionConfig
    {
        public static IServiceCollection AddCustomServices(IServiceCollection services)
        {
            services.AddTransient<TextFileReader>();
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
