using TagsCloud.Interfaces;
using System.Linq;
using System.Collections.Generic;
using TagsCloud.FileReader;
using MyStemWrapper;
using System.Text.RegularExpressions;
using System;
using System.IO;
using System.Reflection;

namespace TagsCloud.WordProcessing
{
    public class DefaultWordValidator : IWordValidator
    {
        private readonly string[] boringWords;
        private readonly MyStem myStem;
        private readonly Regex parsePartOfSpeechRegex;
        private readonly string[] ignoringPartsOfSpeech;
        private readonly Dictionary<string, bool> wasViewed;

        public DefaultWordValidator(SpliterByLine textSpliter, ITextReader fileReader, WordValidatorSettings wordValidatorSettings)
        {
            myStem = new MyStem { PathToMyStem = (Path.Combine(Path.GetFullPath(Path.Combine("..", "..", "..")), "Resources", "mystem.exe")), Parameters = "-li" };
            boringWords = wordValidatorSettings.pathToBoringWords == "" ? new string[]{} : 
                textSpliter.SplitText(fileReader.ReadFile(wordValidatorSettings.pathToBoringWords)).ToArray();
            parsePartOfSpeechRegex = new Regex(@"\w*?=(\w+)");
            ignoringPartsOfSpeech = wordValidatorSettings.ignoringPartsOfSpeech;
            wasViewed = new Dictionary<string, bool>();
        }

        public bool IsValidWord(string word)
        {
            if (wasViewed.TryGetValue(word, out var IsValidWord))
                return IsValidWord;
            var partOfSpechWord = parsePartOfSpeechRegex.Match(myStem.Analysis(word)).Groups[1].Value;
            IsValidWord = word.Length != 0 
                && boringWords.FirstOrDefault(boringWord => boringWord.Equals(word, StringComparison.InvariantCultureIgnoreCase)) == null 
                && !ignoringPartsOfSpeech.Contains(partOfSpechWord)
                && !int.TryParse(word, out var res);
            wasViewed.Add(word, IsValidWord);
            return IsValidWord;
        }
    }
}