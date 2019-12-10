using TagCloud.Models;

namespace TagCloud.Actions
{
    public class NewImageAction : IAction
    {
        public string CommandName { get; } = "-newimage";
        public string Description { get; } = "set parameters for a new image";

        public void Perform(ClientConfig config, ImageSettings imageSettings)
        {
            config.ToCreateNewImage = true;
        }
    }
}