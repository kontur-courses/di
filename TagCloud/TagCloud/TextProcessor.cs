using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;

namespace TagCloud
{
    public class TextProcessor
    {
        public Dictionary<string, int> GetInterestingWords(string filepath, int amount = 1000)
        {
            //filepath = @"c:\users\roman\desktop\input.txt";
            var process = new Process();
            process.StartInfo.FileName = "mystem.exe";
            process.StartInfo.UseShellExecute = false;

            var tempPath = @"c:\temp\output.txt";
            var arguements = "-nld";
            
            process.StartInfo.Arguments = $"{arguements} {filepath} {tempPath}";
            process.Start();
            using var reader = new StreamReader(tempPath);
            var result = reader.ReadToEnd();
            return result.Split("\n", StringSplitOptions.RemoveEmptyEntries)
                .GroupBy(s => s)
                .ToDictionary(g => g.Key, g => g.ToList().Count);
        }
    }

    public class Word
    {
        public readonly Font Font;
        public readonly Rectangle Rectangle;
        public readonly string Text;

        public Word(string text, Font font, Rectangle rectangle)
        {
            Text = text;
            Font = font;
            Rectangle = rectangle;
        }
    }
}