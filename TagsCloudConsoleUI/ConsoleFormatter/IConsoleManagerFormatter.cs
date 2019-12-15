namespace TagsCloudConsoleUI
{
    internal interface IConsoleManagerFormatter
    {
        string InitialMessage { get; }
        string ErrorMessage { get; }
        string ParseCommandErrorMessage { get; }
        string ErrorSymbol { get; }
        string BorderString(int width);
        string SuccessfulMessage(string path);
    }
}