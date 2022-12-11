// Copyright (c) Nate McMaster.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Drawing;
using McMaster.Extensions.CommandLineUtils;
using TagsCloud2;
using TagsCloudVisualization;

class Program
{
    static void Main(string[] args)
    {
        var saver = new ImageSaver();
        var frequencyCompiler = new FrequencyCompiler();
        var lemmatizer = new Lemmatizer(@"D:\шпора-2022\di\TagsCloud2\Lemmatizer\mystem.exe");
        var reader = new Reader();
        var tagsCloudMaker = new TagsCloudMaker();
        var layouter = new CircularCloudLayouter(new Point(0, 0));
        var bitmapMaker = new BitmapMaker(layouter);
        var sizeDefiner = new SizeDefiner();

        var words = reader.ReadWordsFromFile(@"D:\\шпора-2022\\di\\TagsCloud2\\inputWords.txt");
        var normalizeWords = lemmatizer.Lemmatize(words);
        var frequencyDict = frequencyCompiler.GetFrequencyOfWords(normalizeWords);
        var frequencyList = frequencyCompiler.GetFrequencyList(frequencyDict, 100);
        var tagsCloudBitmap = tagsCloudMaker.MakeTagsCloud(frequencyList, "Arial", 100,
            Brushes.Purple, new Size(5000, 5000), bitmapMaker, sizeDefiner, true);
        saver.SaveImage(@"D:\шпора-2022\di\TagsCloud2", @"img", @"png", tagsCloudBitmap);

        var proceed = Prompt.GetYesNo("Do you want to proceed with this demo?",
            defaultAnswer: true,
            promptColor: ConsoleColor.Black,
            promptBgColor: ConsoleColor.White);

        if (!proceed) return;

        var name = Prompt.GetString("What is your name?",
            promptColor: ConsoleColor.White,
            promptBgColor: ConsoleColor.DarkGreen);

        Console.WriteLine($"Hello, there { name ?? "anonymous console user"}.");

        var age = Prompt.GetInt("How old are you?",
            promptColor: ConsoleColor.White,
            promptBgColor: ConsoleColor.DarkRed);

        var password = Prompt.GetPassword("What is your password?",
            promptColor: ConsoleColor.White,
            promptBgColor: ConsoleColor.DarkBlue);

        Console.Write($"Your password contains {password.Length} characters. ");
        switch (password.Length)
        {
            case int _ when (password.Length < 2):
                Console.WriteLine("Your password is so short you might as well not have one.");
                break;
            case int _ when (password.Length < 4):
                Console.WriteLine("Your password is too short. You should pick a better one");
                break;
            case int _ when (password.Length < 10):
                Console.WriteLine("Your password is too okay, I guess.");
                break;
            default:
                Console.WriteLine("Your password is probably adequate.");
                break;
        }
    }
}