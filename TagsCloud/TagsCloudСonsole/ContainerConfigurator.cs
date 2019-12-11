using System;
using System.Collections.Generic;
using Autofac;
using DocoptNet;
using TagsCloudTextProcessing.Excluders;
using TagsCloudTextProcessing.Formatters;
using TagsCloudTextProcessing.Readers;
using TagsCloudTextProcessing.Shufflers;
using TagsCloudTextProcessing.Splitters;
using TagsCloudTextProcessing.Tokenizers;

namespace TagsCloudConsole
{
    public static class ContainerConfigurator
    {
        public static IContainer Configure(IDictionary<string, ValueObject> parameters)
        {
            var builder = new ContainerBuilder();
            builder
                .RegisterType<Application>()
                .WithParameter("wordsToExcludePath",parameters["--exclude"].ToString());
            
            //text processing part
            ConfigureTextReader(parameters["--input"].ToString(),parameters["--input_ext"].ToString(), builder);
            
            builder.RegisterType<TextSplitter>()
                .As<ITextSplitter>()
                .WithParameter("splitPatter",parameters["--split_pattern"].ToString());
            
            builder.RegisterType<WordsFormatterLowercaseAndTrim>().As<IWordsFormatter>();
            builder.RegisterType<WordsExcluder>().As<IWordsExcluder>();
            builder.RegisterType<Tokenizer>().As<ITokenizer>();
            
            
            ConfigureShuffler(parameters["--shuffle"].ToString(), parameters["--seed"], builder);
            
            
            
            

            
            //builder.RegisterType<SpiralCloudLayouter>().As(typeof(ICloudLayouter));
            //builder.RegisterType<EmployeeService>().As<IEmployeeService>();
            //builder.RegisterType<PrintService>().As<IPrintService>();
            return builder.Build();
            //todo container
        }
        
        private static void ConfigureTextReader(string path, string extension, ContainerBuilder builder)
        {
            switch (extension)
            {
                case "docx":
                    builder.RegisterType<DocxTextReader>()
                        .As<ITextReader>()
                        .WithParameter("path",path);
                    break;
                case "pdf":
                    builder.RegisterType<PdfTextReader>()
                        .As<ITextReader>()
                        .WithParameter("path",path);
                    break;
                default:
                    builder.RegisterType<TxtTextReader>()
                        .As<ITextReader>()
                        .WithParameter("path", path);
                    break;
            }
        }
        
        private static void ConfigureShuffler(string shuffleType, ValueObject seed, ContainerBuilder builder)
        {
            switch (shuffleType)
            {
                case "a":
                    builder.RegisterType<TokenShufflerAscending>()
                        .As<ITokenShuffler>();
                    break;
                case "d":
                    builder.RegisterType<TokenShufflerDescending>()
                        .As<ITokenShuffler>();
                    break;
                default:
                {
                    var randomSeed= Environment.TickCount;
                    if (seed.IsInt)
                        randomSeed = seed.AsInt;
                    builder.RegisterType<TokenShufflerRandom>()
                        .As<ITokenShuffler>()
                        .WithParameter("randomSeed", randomSeed);
                    break;
                }
            }
        }
    }
}