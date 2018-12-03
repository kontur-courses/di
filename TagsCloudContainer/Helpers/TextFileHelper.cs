using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TagsCloudContainer.Helpers
{
    public static class TextFileHelper
    {
        public static string ReadFromFile(string path)
        {
            var encoding = Encoding.GetEncoding(1251);
            using (var sr = new StreamReader(path, encoding))
            {
                return sr.ReadToEnd();
            }
        }

        public static void Rebuildtext(string text)
        {
            var l = ForgeTextLines(text).ToList();

            for(var i = 0; i < l.Count(); i++)
            {
                l[i] = RemoveNonLetterSymbols(l[i]);
                if (string.IsNullOrEmpty(l[i]))
                {
                    l.RemoveAt(i);
                    i--;
                }
            }

            WriteToTxt(l);
        }

        public static string RemoveNonLetterSymbols(string text)
        {
            var arr = text.Where(c => char.IsLetter(c) || char.IsSeparator(c)).ToArray();

            return new string(arr);
        }

        public static IEnumerable<string> ForgeTextLines(string rawText)
        {
            var x = rawText.Split();
            foreach (var s in x)
            {
                if (!string.IsNullOrEmpty(s))
                    yield return s;
            }
        }

        public static void WriteToTxt(IEnumerable<string> textLines)
        {
            var filePath = Assembly.GetExecutingAssembly().Location;
            var directory = Path.GetDirectoryName(filePath);

            using (var file =
                new StreamWriter(Path.Combine(directory, "1984_lines.txt")))
            {
                foreach (var line in textLines)
                {
                        file.WriteLine(line, Encoding.GetEncoding(1251));
                }
            }
        }
    }
}
