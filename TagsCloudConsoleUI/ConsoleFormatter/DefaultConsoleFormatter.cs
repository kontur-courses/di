namespace TagsCloudConsoleUI
{
    internal class DefaultConsoleFormatter : IConsoleManagerFormatter
    {
        public string InitialMessage => "Tag Cloud Generator\nWrite \"--help\" to see commands:";
        public string ErrorMessage => "####### Image created unsuccessful, reasons: #######";
        public string ParseCommandErrorMessage => "####### Incorrect command: #######";
        public string ErrorSymbol => "*\t";

        public string BorderString(int width) => new string('=', width);

        public string SuccessfulMessage(string path) =>
            $"&&&&&&  Image created successful, saved to {path}  &&&&&&";
    }
}