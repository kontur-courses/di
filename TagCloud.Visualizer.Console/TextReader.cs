using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using GemBox.Document;
using MicrosoftWord = Microsoft.Office.Interop.Word;

namespace TagCloud.Visualizer.Console
{
    public static class TextReader
    {
        private static readonly Regex WordRegex = new Regex("\\w+");

        private static readonly string InputPath = Path.Combine(Directory.GetCurrentDirectory(),
            "..",
            "..",
            "..",
            "..",
            $"{nameof(TagCloud)}.{nameof(Visualizer)}",
            "source");

        private static readonly HashSet<string> WordsToExclude = new HashSet<string>
        {
            "и",
            "а",
            "в",
            "о",
            "от",
            "да",
            "нет",
            "то",
            "с",
            "по",
            "к",
            "около",
            "но",
            "через",
            "что",
            "где",
            "когда",
            "откуда",
            "куда",
            "ну",
            "до",
            "эти",
            "со",
            "же",
            "при",
            "их",
            "он",
            "не",
            "ни",
            "ей",
            "ему",
            "есть",
            "мог",
            "могла",
            "была",
            "вы",
            "за",
            "я",
            "был",
            "быть",
            "есть",
            "у",
            "ты",
            "бы",
            "это",
            "так",
            "из",
            "на",
            "мы",
            "тут",
            "во",
            "ней",
            "нему",
            "сам",
            "него",
            "неё",
            "опять",
            "тем",
            "или",
            "для",
            "мой",
            "эту",
            "вам",
            "про",
            "без",
            "им",
            "ей",
            "неё",
            "кто",
            "над",
            "уж",
            "эта",
            "тот",
            "этот",
            "нею",
            "вот",
            "его",
            "ту",
            "ж",
            "же",
            "т",
            "хоть",
            "таки"
        };

        public static IEnumerable<string> GetWords(InputOptions inputOptions, string sourceFolderPath)
        {
            IEnumerable<string> words;
            if (!inputOptions.IsDocOrDocx)
            {
                words = File.ReadAllLines(Path.Combine(sourceFolderPath,
                        $"{inputOptions.FileName}.{inputOptions.FileExtension}"))
                    .Select(word => word.ToLower());
            }

            words = GetWordsFromWordDocument(inputOptions, sourceFolderPath);
            return words
                .Where(word => !WordsToExclude.Contains(word));
        }

        public static IEnumerable<string> GetWords(InputOptions inputOption)
        {
            return GetWords(inputOption, InputPath);
        }

        private static IEnumerable<string> GetWordsFromWordDocument(InputOptions inputOptions, string sourceFolderPath)
        {
            var document = DocumentModel.Load(Path.Combine(sourceFolderPath,
                $"{inputOptions.FileName}.{inputOptions.FileExtension}"));
            var text = document.Content.ToString();
            return text.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
        }
    }
}