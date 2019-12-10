namespace TagCloudForm.TextFilterConditions
{
    public interface IFilterCondition
    {
        bool CheckFilterCondition(string word);
    }
}