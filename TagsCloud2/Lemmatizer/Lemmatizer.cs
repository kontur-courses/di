namespace TagsCloud2.Lemmatizer;

public class Lemmatizer: ILemmatizer
{
    private MystemHandler.MystemMultiThread mystemThread;
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
            var currentItem = enumerator.Current;
            if (!currentItem.IsSlug)
            {
                result.Add(currentItem.Lemma);
            }
        }

        return result;
    }
}