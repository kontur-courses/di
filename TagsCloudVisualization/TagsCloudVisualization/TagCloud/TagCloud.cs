using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using TagsCloudVisualization.TagCloudLayouters;
using TagsCloudVisualization.TagsCloudVisualization;
using Spire.Doc;

namespace TagsCloudVisualization.TagCloud
{
    internal class TagCloud
    {
        private static Dictionary<string, bool> partOfSpeachAdmissibility = new Dictionary<string, bool>()
        {
            {"A", true },
            {"ADV", true },
            {"ADVPRO", true },
            {"ANUM", true },
            {"APRO", true },
            {"COM", true },
            {"CONJ", false },
            {"INTJ", true },
            {"NUM", true },
            {"PART", false },
            {"PR", false },
            {"S", true },
            {"SPRO", true },
            {"V", true }
        };

        private static Dictionary<string, Func<string, string>> fileOpener = new Dictionary<string, Func<string, string>>
        {
            {".txt", name =>
                {
                    using (var file = new StreamReader(name, Encoding.Default))
                    {
                        return file.ReadToEnd();
                    }
                }
            },
            {".doc", name =>
                {
                    var doc = new Document();
                    doc.LoadFromFile(name, FileFormat.Doc);
                    return doc .GetText();
                }
            },
            {
                ".docx", name =>
                {
                    var doc = new Document();
                    doc.LoadFromFile(name, FileFormat.Docx);
                    return doc.GetText();
                }
            },
        };

        public static void Main(string[] args)
        {
            Console.WriteLine("Укажите путь к файлу с тексом");
            var path = Console.ReadLine();
            Console.WriteLine("Укажите координаты центра и радиус облака через пробел");
            var cloudConfig = Console.ReadLine().Split().Select(arg => int.Parse(arg)).ToList();
            Console.WriteLine("Укажите минимальный размер шрифта");
            var textSize = int.Parse(Console.ReadLine());
            Console.WriteLine("Укажите название картинки");
            var name = Console.ReadLine();
            Console.WriteLine("Укажите размер картинки через пробел");
            var imageConfig = Console.ReadLine().Split().Select(arg => int.Parse(arg)).ToList();

            var wordsInfo = getFrequencyDictionary(path);
            var circularCloudLayouter = new CircularCloudLayouter(new Point(cloudConfig[0], cloudConfig[1]), cloudConfig[2]);
            var wordsFonts =  wordsInfo.ToDictionary(element => element.Key, 
                                                     element => new Font("Consolas", textSize * element.Value));
            var wordsVisualization = new WordsCloudVisualization(wordsFonts);
            var rectangles = wordsFonts.Select(element =>
            {
                var size = TextRenderer.MeasureText(element.Key, element.Value);
                return circularCloudLayouter.PutNextRectangle(size);
            });
            var image = wordsVisualization.Draw(rectangles, imageConfig[0], imageConfig[1]);
            image.Save(name + ".png");
        }

        private static Dictionary<string, int> getFrequencyDictionary(string fileName)
        {
            var result = new Dictionary<string, int>();
            var text = fileOpener[Path.GetExtension(fileName) ?? throw new InvalidOperationException()](fileName);
            var stemedText = GetTextStemed(text);
            var regExForStandartWordForm = new Regex("\"lex\":\"(\\w+)\"", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var regExForPartOfSpeach = new Regex("\"gr\":\"(\\w+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            foreach (var line in stemedText.Split('\n'))
            {
                var standartForm = regExForStandartWordForm.Match(line).Groups[1].Value.ToLower();
                var partOfSpeach = regExForPartOfSpeach.Match(line).Groups[1].Value;
                if (standartForm == string.Empty || partOfSpeach == string.Empty || !partOfSpeachAdmissibility[partOfSpeach])
                    continue;
                if (!result.ContainsKey(standartForm))
                    result.Add(standartForm, 0);
                result[standartForm]++;
            }
            return result.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, y => y.Value);
        }

        private static string GetTextStemed(string text)
        {
            File.WriteAllText(@"YandexStem\input.txt", text, Encoding.UTF8);
            var cmd = new Process { StartInfo = {FileName = "cmd.exe", RedirectStandardInput = true, UseShellExecute = false}};
            cmd.Start();
            cmd.StandardInput.WriteLine("cd YandexStem");
            cmd.StandardInput.WriteLine("mystem.exe input.txt output.txt -nig --format json");
            return File.ReadAllText(@"YandexStem\output.txt");
        }
    }
}
