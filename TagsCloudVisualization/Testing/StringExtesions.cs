namespace Testing;

public static class StringExtesions
{
    public static int SubstringCount(this string str, string sub)
    {
        int count = 0;
        int i = str.IndexOf(sub);
        while (i != -1)
        {
            i = str.IndexOf(sub, i + 1);
            count++;
        }

        return count;
    }
}