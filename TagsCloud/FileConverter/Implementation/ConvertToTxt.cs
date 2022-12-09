using System.Text;
using Spire.Doc;

namespace TagsCloud.FileConverter.Implementation;

public class ConvertToTxt : IFileConverter
{
    public string Convert(string path)
    {
        var document = new Document();
        document.LoadFromFile(path);
        document.SaveToTxt("temp.txt", Encoding.UTF8);
        return "temp.txt";
    }
}