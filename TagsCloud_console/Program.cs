using Autofac;
using Autofac.Core;
using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using TagsCloud;
using TagsCloud.DI;
using TagsCloud.Layouters;
using TagsCloud.Renderers;
using TagsCloud.WordsFiltering;

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
                var knownFilters = container.Resolve<IFilter[]>().Cast<object>();
                var selectedFilters = TryParseObjects(knownFilters, opts.Filters.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries));
                if (selectedFilters == null) return;

                var knownLayouters = container.Resolve<ITagsCloudLayouter[]>().Cast<object>();
                var parsedLayouters = TryParseObjects(knownLayouters, new string[] { opts.Layouter });
                if (parsedLayouters == null) return;
                if (parsedLayouters.Length != 1)
                {
                    Console.Error.WriteLine($"One layouter must be selected");
                    return;
                }
                var selectedLayouter = parsedLayouters[0];

                var knownRenderers = container.Resolve<ITagsCloudRenderer[]>().Cast<object>();
                var parsedRenderers = TryParseObjects(knownRenderers, new string[] { opts.Renderer });
                if (parsedRenderers == null) return;
                if (parsedRenderers.Length != 1)
                {
                    Console.Error.WriteLine($"One renderer must be selected");
                    return;
                }
                var selectedRenderer = parsedRenderers[0];

                var wordsLoader = container.Resolve<WordsLoader>();
                var words = wordsLoader.LoadWords(opts.InputFile);

                var wordsFilterer = container.Resolve<WordsFilterer>(new NamedParameter("filters", selectedFilters.Cast<IFilter>().ToArray()));
                var filteredWords = wordsFilterer.FilterWords(words);

                var tagCloud = container.Resolve<TagsCloudGenerator>(
                    new NamedParameter("layouter", selectedLayouter as ITagsCloudLayouter),
                    new NamedParameter("renderer", selectedRenderer as ITagsCloudRenderer));
                var image = tagCloud.GenerateCloud(filteredWords);

                var imageSaveHelper = container.Resolve<ImageSaveHelper>();
                imageSaveHelper.SaveTo(image, opts.OutputFile);
            });

            Console.WriteLine("OK");
            Console.ReadKey();
        }

        private static object[] TryParseObjects(IEnumerable<object> knownObjects, string[] settings)
        {
            var res = new List<object>();

            var regexObjectWithSettings = new Regex(@"(\S+)\((\S+)\)");
            foreach (var item in settings)
            {
                var matchObjectWithSettings = regexObjectWithSettings.Match(item);
                string neededObjectTypeName = matchObjectWithSettings.Success
                    ? matchObjectWithSettings.Groups[1].Value
                    : item;
                var findedObject = knownObjects.FirstOrDefault(o => o.GetType().Name == neededObjectTypeName);
                if (findedObject == null)
                {
                    Console.Error.WriteLine($"Can't parse object '{item}'");
                    return null;
                }

                if (matchObjectWithSettings.Success)
                {
                    var objectSettings = matchObjectWithSettings.Groups[2].Value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    if (!TryParseSettings(findedObject, objectSettings))
                        return null;
                }

                res.Add(findedObject);
            }

            return res.ToArray();
        }

        private static bool TryParseSettings(object obj, string[] options)
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
