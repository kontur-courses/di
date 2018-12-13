//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Reflection;
//using Autofac;
//using TagsCloudContainer.CommandLineParser;
//using TagsCloudContainer.Configuration;
//using TagsCloudContainer.Converter;
//using TagsCloudContainer.DataReader;
//using TagsCloudContainer.Filter;
//using TagsCloudContainer.ImageWriter;
//using TagsCloudContainer.Preprocessor;
//using TagsCloudContainer.TagsGenerator;
//using TagsCloudContainer.Visualizer;
//using TagsCloudContainer.WordsCounter;
//
//namespace TagsCloudContainer
//{
//    internal class TagsCloud
//    {
//        public static void Main(string[] args)
//        {
//            var configuration = new SimpleCommandLineParser().Parse(args);
//

//
//            var container = BuildContainer(configuration);
//
//            var wordReader = container.Resolve<IDataReader>();
//            var words = wordReader.Read(configuration.PathToWordsFile);
//
//            var preprocessor = container.Resolve<IPreprocessor>();
//            var converter = container.Resolve<IConverter>();
//            var filter = new BoringWordsFilter(new HashSet<string> {"as", "the"});
//            var preprocessedWords = preprocessor.Process(words,
//                new[] {converter}, new[] {filter});
//
//            var wordsCounter = container.Resolve<IWordCounter>();
//            var wordsFrequency = wordsCounter.GetWordsFrequency(preprocessedWords);
//
//            var tagsGenerator = container.Resolve<ITagsGenerator>();
//            var tags = tagsGenerator.GenerateTags(wordsFrequency);
//
//            var visualizer = container.Resolve<IVisualizer>();
//            var image = visualizer.Visualize(tags);
//
//            var imageWriter = container.Resolve<IImageWriter>();
//            imageWriter.Write(image, configuration.OutFileName, "png", configuration.DirectoryToSave);
//
//
//        }
//
//    }
//}