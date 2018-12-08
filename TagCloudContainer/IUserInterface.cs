namespace TagCloudContainer
{
    public interface IUserInterface
    {
        Config GetConfig(string[] args, out bool toExit);
    }
}
