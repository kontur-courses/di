using System;
using Autofac;

namespace TagsCloudConsoleUI.DiContainerBuilder
{
    internal abstract class DiPreset : Module
    {
        protected BuildOptions LoadedOptions;

        protected DiPreset(BuildOptions options)
        {
            LoadedOptions = options ?? throw new ArgumentException("Build options in module can't be null");
        }
    }
}