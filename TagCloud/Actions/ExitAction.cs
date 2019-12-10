using TagCloud.Models;

namespace TagCloud.Actions
{
    public class ExitAction : IAction
    {
        public string CommandName { get; } = "-exit";

        public string Description { get; } = "exit the program";

        public void Perform(ClientConfig config,ImageSettings imageSettings)
        {
            config.ToExit = true;
        }
    }
}