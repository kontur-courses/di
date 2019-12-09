namespace TagCloud.Actions
{
    public class ExitAction : IAction
    {
        public string CommandName => "- exit";

        public void Perform(ClientConfig config)
        {
            config.ToExit = true;
        }
    }
}