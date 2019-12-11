namespace TagCloud.TextFilterConditions
{
    public interface IFilterCondition
    {
        bool CheckFilterCondition(string word);
    }
}