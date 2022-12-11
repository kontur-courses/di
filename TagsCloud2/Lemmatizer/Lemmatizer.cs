namespace TagsCloud2.Lemmatizer;

public class Lemmatizer: ILemmatizer
{
    private MystemHandler.MystemMultiThread mystemThread; 
    //mystemExePath = @"D:\шпора-2022\di\TagsCloud2\Lemmatizer\mystem.exe"
    
    public Lemmatizer(string mystemExePath)
    { 
        mystemThread = new MystemHandler.MystemMultiThread(1, mystemExePath);
    }
    public List<string> Lemmatize(List<string> words)
    {
        var result = new List<string>();
        foreach (var enumerator in words.Select(word => 
                     mystemThread.StemWords(word.ToLower()).GetEnumerator()))
        {
            
            enumerator.MoveNext();
            if (!enumerator.Current.IsSlug)
            {
                result.Add(enumerator.Current.Lemma);
            }
        }

        return result;
    }
}