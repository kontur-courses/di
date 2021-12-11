namespace TagsCloudContainer.Interfaces
{
    public interface IUIAction
    {
        string GetDescription();

        void Handle();
    }
}
