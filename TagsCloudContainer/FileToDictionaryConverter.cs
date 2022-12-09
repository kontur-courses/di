using System.Diagnostics;

namespace TagsCloudContainer
{
    public class FileToDictionaryConverter : IConverter
    {
        private readonly IWordsFilter filter;

        public FileToDictionaryConverter(IWordsFilter filter)
        {
            this.filter = filter;
        }

        public Dictionary<string, int> GetWordsInFile(ICustomOptions options)
        {
            var cmd = $"mystem.exe {Path.Combine(options.TextsPath, options.WordsFileName)} out.txt -nig";

            var proc = new ProcessStartInfo
            {
                UseShellExecute = true,
                WorkingDirectory = Path.Combine(options.TextsPath),
                FileName = @"C:\Windows\System32\cmd.exe",
                Arguments = "/C" + cmd,
                WindowStyle = ProcessWindowStyle.Hidden
            };

            Process.Start(proc);

            var boringWords = File.ReadAllLines(Path.Combine(options.TextsPath, options.BoringWordsName))
                .Select(x => x.ToLower())
                .ToList();

            var taggedWordFilePath = Path.Combine(options.TextsPath, "out.txt");

            while (IsFileLocked(new FileInfo(taggedWordFilePath)))
                Task.Delay(5);

            var taggedWords = File.ReadAllLines(taggedWordFilePath)
                .ToList();

            File.Delete(taggedWordFilePath);

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

    public interface IConverter
    {
        Dictionary<string, int> GetWordsInFile(ICustomOptions options);
    }
}