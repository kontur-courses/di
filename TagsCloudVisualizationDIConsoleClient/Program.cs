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

            if (args.Length < 2 || args.Length > 4)
                throw new ArgumentException("Incorrect Number Of MystemArgs");

            var pathToFile = args.ElementAtOrDefault(0);
            var pathToSave = args.ElementAtOrDefault(1);

            var lastIndexOfSlashes = pathToSave.LastIndexOf('\\');
            var lastDotIndex = pathToSave.LastIndexOf('.');
            var safeDirectory = pathToSave.Remove(lastIndexOfSlashes);
            CheckArguments(pathToFile, safeDirectory);

            var possibleFormat =
                pathToSave.Substring(lastDotIndex);

            var pathWithoutFormat = pathToSave.Substring(0, lastDotIndex);

            var imageFormat = CheckPossibleFormat(possibleFormat);


            var excludedWordsDocument = args.ElementAtOrDefault(2);
            var excludedWordList = MakeExcludeWordList(excludedWordsDocument);

            TagsCloudVisualizationDI.Program.Main(pathToFile, pathWithoutFormat, imageFormat, excludedWordList);
        }

        private static ImageFormat CheckPossibleFormat(string possibleFormat)
        {
            return (possibleFormat.ToLower()) switch
            {
                ".png" => ImageFormat.Png,
                ".jpeg" => ImageFormat.Jpeg,
                ".jpg" => ImageFormat.Jpeg,
                ".bmp" => ImageFormat.Bmp,
                ".tiff" => ImageFormat.Tiff,
                ".ico" => ImageFormat.Icon,
                ".gif" => ImageFormat.Gif,
                _ => ImageFormat.Png,
            };
        }

        private static List<string> MakeExcludeWordList(string excludedWordsDocumentPath)
        {
            if (excludedWordsDocumentPath == null)
                return null;

            if (!File.Exists(excludedWordsDocumentPath))
                throw new Exception($"Giving path to file: {excludedWordsDocumentPath} is not valid, NOTSYSTEM");

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
