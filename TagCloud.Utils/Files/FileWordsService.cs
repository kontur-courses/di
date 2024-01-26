using TagCloud.Utils.Files.Interfaces;

namespace TagCloud.Utils.Files;

public class FileWordsService : IWordsService
{
    public IEnumerable<string> GetWords(string path)
    {
        if (!File.Exists(path))
            throw new FileNotFoundException($"По пути {path} не найдено файла");

        foreach (var line in File.ReadLines(path))
            yield return line;
    }
}