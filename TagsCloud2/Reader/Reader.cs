namespace TagsCloud2.Reader;

public class Reader: IReader
{
    public List<string> ReadWordsFromFile(string pathToFile)
    {
        if (!File.Exists(pathToFile))
        {
            Console.WriteLine("Файла нет:(");
            throw new FileNotFoundException();
        }
        var words = File.ReadAllLines(pathToFile);
        return new List<string>(words);
    }
}