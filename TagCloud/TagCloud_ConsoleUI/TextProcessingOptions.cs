using System.Collections.Generic;
using System.Linq;
using CommandLine;
using TagCloud.TextProcessing;

namespace TagCloud_ConsoleUI
{
    [Verb("process", HelpText = "Обработать текст из файла по заданному пути")]
    public class TextProcessingOptions : ITextProcessingOptions
    {
        private HashSet<PartOfSpeech> _excludePartOfSpeech;

        private HashSet<string> _excludeWords;
        private static HashSet<string> DefaultExcludeWords => new() {"быть", "сказать", "мочь"};

        private static HashSet<PartOfSpeech> DefaultExcludePartOfSpeech => new()
        {
            PartOfSpeech.CONJ,
            PartOfSpeech.PART,
            PartOfSpeech.NUM,
            PartOfSpeech.PR,
            PartOfSpeech.ANUM,
            PartOfSpeech.APRO,
            PartOfSpeech.SPRO
        };

        [Option('p', "path", Required = true, HelpText = "Задать путь к файлу")]
        public IEnumerable<string> FilesToProcess { get; set; }

        [Option('i', "include", Required = false,
            HelpText = "Задать список слов обязательных к влючению (при наличии в исходном тексте)", Separator = ':')]
        public IEnumerable<string> IncludeWords { get; set; }

        [Option('e', "exclude", Required = false, HelpText = "Задать список слов для исключения из облака",
            Separator = ':')]
        public IEnumerable<string> ExcludeWords
        {
            get => _excludeWords.Any() ? _excludeWords : DefaultExcludeWords;
            set => _excludeWords = value.ToHashSet();
        }

        [Option('a', "amount", Required = false,
            HelpText =
                "Установить количество слов в итоговом облаке тэгов (при наличии такого количества в исходном тексте)",
            Default = 1000)]
        public int Amount { get; set; }

        [Option('s', "exclude-pos", Required = false, HelpText = "Задать список частей речи для исключения из облака",
            Separator = ':')]
        public IEnumerable<PartOfSpeech> ExcludePartOfSpeech
        {
            get => _excludePartOfSpeech.Any() ? _excludePartOfSpeech : DefaultExcludePartOfSpeech;
            set => _excludePartOfSpeech = value.ToHashSet();
        }
    }
}