using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace TagsCloudVisualizationDIConsoleClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Contains("-h") || args.Contains("--help"))
            {
                Console.WriteLine("HELP");
                return;
            }

            if (args.Length <2 || args.Length > 4)
                throw new ArgumentException("Incorrect Number Of MystemArgs");

            var pathToFile = args.ElementAtOrDefault(0);
            var pathToSave = args.ElementAtOrDefault(1);

            Console.WriteLine(pathToFile);
            Console.WriteLine(pathToSave);

            var safeDirectory = pathToSave.Remove(pathToSave.LastIndexOf('\\'));
            CheckArguments(pathToFile, safeDirectory);
            var imageFormat = ImageFormat.Png;


            var excludedWordsDocument = args.ElementAtOrDefault(2);
            List<string> excludedWordList = MakeExcluedeWordList(excludedWordsDocument);

            TagsCloudVisualizationDI.Program.Main(pathToFile, pathToSave, imageFormat, excludedWordList);
        }


        private static List<string> MakeExcluedeWordList(string excludedWordsDocumentPath)
        {
            if (excludedWordsDocumentPath == null)
                return null;

            if (!File.Exists(excludedWordsDocumentPath))
                throw new Exception($"Giving path to file: {excludedWordsDocumentPath} is not valid, NOTSYSTEM");

            // в файле слова по одному в строке
            return File.ReadLines(excludedWordsDocumentPath).Select(w => w.ToLower()).ToList();
        }


        private static void CheckArguments(string pathToFile, string pathToSafeFile)
        {
            if (!File.Exists(pathToFile))
                throw new Exception($"Giving path to file: {pathToFile} is not valid, NOTSYSTEM");

            if (!Directory.Exists(pathToSafeFile))
                throw new Exception($"Giving path to safefile: {pathToSafeFile} is not valid, NOTSYSTEM");
        }
    }
}
