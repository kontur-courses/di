﻿using Microsoft.Extensions.DependencyInjection;
using TagsCloudContainer.FrequencyAnalyzers;
using TagsCloudContainer.SettingsClasses;
using TagsCloudContainer.TagCloudBuilder;
using TagsCloudContainer.TextTools;
using TagsCloudVisualization;

namespace TagsCloudContainer
{
    public class DependencyInjectionConfig
    {
        public static IServiceCollection AddCustomServices(IServiceCollection services)
        {
            services.AddSingleton<ITextReader, TextFileReader>();
            services.AddSingleton<IAnalyzer, FrequencyAnalyzer>();
            services.AddTransient<IPointsProvider, SpiralPointsProvider>();
            services.AddTransient<IPointsProvider, RandomPointsProvider>();
            services.AddScoped<TagsCloudLayouter>();
            services.AddScoped<CloudDrawingSettings>();

            return services;
        }
    }
}
