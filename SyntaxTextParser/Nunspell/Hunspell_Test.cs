using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxTextParser
{
    public class Hunspell_Test
    {
        #region Hunspell_Test

        //public static void GetTest()
        //{
        //    var dictPath = "Dictionaries/En_US/";
        //    using (var hunspell = new Hunspell(dictPath + "index.aff", dictPath + "index.dic"))
        //    {
        //        var text = new List<string> { "Hello", "I", "pirate", "Are" };
        //        foreach (var word in text)
        //        {
        //            var analyze = hunspell.Analyze(word);
        //            foreach (var item in analyze)
        //            {
        //                foreach (var item2 in hunspell.Analyze(item))
        //                {
        //                    Console.Write(item2 + " # ");
        //                }
        //            }
        //            Console.WriteLine();
        //        }
        //        Console.WriteLine("Hunspell - Spell Checking Functions");
        //        Console.WriteLine("¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯");

        //        Console.WriteLine("Check if the word 'Recommendation' is spelled correct");
        //        bool correct = hunspell.Spell("Recommendation");
        //        Console.WriteLine("Recommendation is spelled " +
        //                          (correct ? "correct" : "not correct"));

        //        Console.WriteLine("");
        //        Console.WriteLine("Make suggestions for the word 'Recommendatio'");
        //        List<string> suggestions = hunspell.Suggest("Recommendatio");
        //        Console.WriteLine("There are " +
        //                          suggestions.Count.ToString() + " suggestions");
        //        foreach (string suggestion in suggestions)
        //        {
        //            Console.WriteLine("Suggestion is: " + suggestion);
        //        }

        //        Console.WriteLine("");
        //        Console.WriteLine("Analyze the word 'decompressed'");
        //        List<string> morphs = hunspell.Analyze("decompressed");
        //        foreach (string morph in morphs)
        //        {
        //            Console.WriteLine("Morph is: " + morph);
        //        }

        //        Console.WriteLine("");
        //        Console.WriteLine("Find the word stem of the word 'decompressed'");
        //        List<string> stems = hunspell.Stem("decompressed");
        //        foreach (string stem in stems)
        //        {
        //            Console.WriteLine("Word Stem is: " + stem);
        //        }

        //        Console.WriteLine("");
        //        Console.WriteLine("Generate the plural of 'girl' by providing sample 'boys'");
        //        List<string> generated = hunspell.Generate("girl", "boys");
        //        foreach (string stem in generated)
        //        {
        //            Console.WriteLine("Generated word is: " + stem);
        //        }
        //    }

        //    Console.WriteLine("###########################################");

        //    using (var hyphen = new Hyphen(dictPath + "hyph_en_US.dic"))
        //    {
        //        Console.WriteLine("Get the hyphenation of the word 'Recommendation'");
        //        HyphenResult hyphenated = hyphen.Hyphenate("Recommendation");
        //        Console.WriteLine("'Recommendation' is hyphenated as: " + hyphenated.HyphenatedWord);
        //    }

        //    Console.WriteLine("###########################################");
        //}

        #endregion
    }
}
