using System.Diagnostics;
using System.Text;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer
{
    public class FileToDictionaryConverter : IConverter
    {
        private readonly IWordsFilter filter;
        private readonly IDocParser parser;

        public FileToDictionaryConverter(IWordsFilter filter, IDocParser parser)
        {
            this.filter = filter;
            this.parser = parser;
        }

        public Dictionary<string, int> GetWordsInFile(ICustomOptions options)
        {
            var inputWordPath = Path.Combine(options.TextsPath, options.WordsFileName);
            var bufferedWords = new List<string>();

            if (options.WordsFileName[options.WordsFileName.LastIndexOf('.')..] != ".txt")
                bufferedWords = parser.ParseDoc(inputWordPath);
            else
                bufferedWords = File.ReadAllLines(inputWordPath)
                    .ToList();
            bufferedWords = bufferedWords
                .Select(x => x.ToLower())
                .ToList();
            var tmpFilePath = Path.Combine(options.TextsPath, "tmp.txt");
            File.WriteAllLines(tmpFilePath, bufferedWords);

            var cmd = $"mystem.exe -nig {tmpFilePath}";

            var proc = new ProcessStartInfo
            {
                UseShellExecute = false,
                WorkingDirectory = Path.Combine(options.TextsPath),
                FileName = @"C:\Windows\System32\cmd.exe",
                Arguments = "/C" + cmd,
                RedirectStandardOutput = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                StandardOutputEncoding = Encoding.UTF8,
            };
            var p = Process.Start(proc);

            var taggedWords = p.StandardOutput
                .ReadToEnd()
                .Split("\r\n")
                .ToList();

            File.Delete(tmpFilePath);

            var boringWords = File.ReadAllLines(Path.Combine(options.TextsPath, options.BoringWordsName))
                .Select(x => x.ToLower())
                .ToList();

            var filteredWords = filter.FilterWords(taggedWords, boringWords, options);

            var result = new Dictionary<string, int>();
            filteredWords.ForEach(x =>
            {
                if (result.ContainsKey(x))
                    result[x] += 1;

                else result.Add(x, 1);
            });

            return result
                .ToList()
                .OrderByDescending(x => x.Value)
                .ToDictionary(x => x.Key, x => x.Value);
        }

        private static bool IsFileLocked(FileInfo file)
        {
            try
            {
                using var stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
                stream.Close();
            }
            catch (IOException)
            {
                return true;
            }

            return false;
        }
    }
}