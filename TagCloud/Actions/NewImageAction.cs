namespace TagCloud.Actions
{
    public class NewImageAction : IAction
    {
        public string CommandName { get; } = "- newimage";

        public void Perform(ClientConfig config)
        {
            config.ToCreateNewImage = true;
        }
    }
}