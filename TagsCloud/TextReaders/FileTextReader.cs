using TagsCloud.WordValidators;

namespace TagsCloud.TextReaders;

public class FileTextReader:ITextReader
{
    private IWordValidator validator;
    public FileTextReader(IWordValidator validator)
    {
        this.validator = validator;
    }
        
    public Tuple<string,int>[] GetWords(string filePath)
    {
        var words = File.ReadAllText(filePath).Split('\n');
        return words.Where(w=>validator.IsWordValid(w))
            .GroupBy(word => word)
            .Select(group => Tuple.Create(group.Key, group.Count())).ToArray(); 
    }
}