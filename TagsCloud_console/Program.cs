using Autofac;
using Autofac.Core;
using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TagsCloud;
using TagsCloud.DI;
using TagsCloud.Layouters;
using TagsCloud.Renderers;

namespace TagsCloud_console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new TagsCloudModule());
            var container = builder.Build();

            Parser.Default.ParseArguments<InputOptions>(args).WithParsed(opts =>
            {
                var layouters = container.Resolve<ITagsCloudLayouter[]>();
                var selectedLayouterName = opts.Layouter;
                if (!layouters.Select(l => l.GetType().Name).Contains(selectedLayouterName))
                {
                    Console.Error.WriteLine($"There is no such layouter: {selectedLayouterName}");
                    return;
                }
                var selectedLayouter = layouters.First(l => l.GetType().Name == selectedLayouterName);
                if (!ParseSettings(selectedLayouter, opts.LayouterSettings.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)))
                    return;

                var renderers = container.Resolve<ITagsCloudRenderer[]>();
                var selectedRendererName = opts.Renderer;
                if (!renderers.Select(r => r.GetType().Name).Contains(selectedRendererName))
                {
                    Console.Error.WriteLine($"There is no such renderer: {selectedRendererName}");
                    return;
                }
                var selectedRenderer = renderers.First(r => r.GetType().Name == selectedRendererName);
                if (!ParseSettings(selectedRenderer, opts.RendererSettings.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)))
                    return;

                var tagCloud = container.Resolve<TagsCloudGenerator>(
                    new NamedParameter("layouter", selectedLayouter),
                    new NamedParameter("renderer", selectedRenderer));
                tagCloud.GenerateCloud(opts.InputFile).SaveTo(opts.OutputFile);
            });

            Console.WriteLine("OK");
            Console.ReadKey();
        }

        private static bool ParseSettings(object obj, string[] options)
        {
            var props = new Dictionary<string, PropertyInfo>();
            foreach (var p in obj.GetType().GetProperties().Where(p => p.CanWrite))
                props.Add(p.Name, p);

            foreach (var option in options)
            {
                var kv = option.Split(':');
                if (kv.Length != 2)
                {
                    Console.Error.WriteLine($"Can't parse '{option}' as option of {obj.GetType().Name}");
                    return false;
                }
                
                var propName = kv[0];
                if (!props.TryGetValue(propName, out var propertyInfo))
                {
                    Console.Error.WriteLine($"Can't parse '{option}' as option of {obj.GetType().Name}");
                    return false;
                }

                if (!TryParseProperty(kv[1], propertyInfo, obj))
                    return false;
            }

            return true;
        }

        private static bool TryParseProperty(string optionString, PropertyInfo propertyInfo, object obj)
        {
            void printError() =>
                Console.Error.WriteLine($"Can't parse '{optionString}' as value of {propertyInfo.Name}");

            if (propertyInfo.PropertyType == typeof(bool))
            {
                if (!bool.TryParse(optionString, out var val))
                {
                    printError();
                    return false;
                }
                propertyInfo.SetValue(obj, val);
                return true;
            }

            if (propertyInfo.PropertyType == typeof(int))
            {
                if (!int.TryParse(optionString, out var val))
                {
                    printError();
                    return false;
                }
                propertyInfo.SetValue(obj, val);
                return true;
            }

            if (propertyInfo.PropertyType == typeof(System.Drawing.Font))
            {
                var fontNames = System.Drawing.FontFamily.Families.Select(f => f.Name);
                if (!fontNames.Contains(optionString))
                {
                    printError();
                    return false;
                }
                var font = new System.Drawing.Font(optionString, 16);
                propertyInfo.SetValue(obj, font);
                return true;
            }

            if (propertyInfo.PropertyType == typeof(System.Drawing.Color))
            {
                var knownColorsNames = Enum.GetNames(typeof(System.Drawing.KnownColor));
                if (!knownColorsNames.Contains(optionString))
                {
                    printError();
                    return false;
                }
                var color = System.Drawing.Color.FromName(optionString);
                propertyInfo.SetValue(obj, color);
                return true;
            }

            printError();
            return false;
        }
    }
}
