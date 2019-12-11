using System.Windows.Forms;

namespace TagCloud.Actions
{
    public class NewImageAction : IAction
    {
        public string CommandName { get; } = "-newimage";
        public string Description { get; } = "set parameters for a new image";

        public void Perform(ClientConfig config)
        {
            if (config.IsRunning)
            {
                Application.Exit();
                config.IsRunning = false;
            }

            config.ToCreateNewImage = true;
        }
    }
}