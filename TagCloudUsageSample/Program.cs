using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core.Registration;
using Autofac.Features.AttributeFilters;
using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TagsCloudVisualization;
using TagsCloudVisualization.Layouters;
using TagsCloudVisualization.WordReaders;
using TagsCloudVisualization.WordReaders.FormatDecoders;
using TagsCloudVisualization.WordReaders.WordProcessors;
using TagsCloudVisualization.WordReaders.WordValidators;

namespace TagCloudUsageSample
{
    internal static class Program
    {
        private static IContainer container;
        
        internal static void Main(string[] args)
        {
            Parser.Default
                .ParseArguments<ClTextOptions>(args)
                .WithParsed(options =>
                {
                    if (!options.Validate(out var message))
                    {
                        Console.WriteLine(message);
                        return;
                    }
            
                    options.CreateTags(out var firstFileName);
                    if (firstFileName is not null && options.Open)
                        System.Diagnostics.Process.Start(firstFileName);
                });
        }
    }
}