using TagCloud.Renderers;
using TagCloud.Settings;

namespace TagCloud.Commands
{
    public class CreateImageCommand : ICommand
    {
        private readonly ResultSettings settings;

        private readonly IRender render;

        public CreateImageCommand(IRender render, ResultSettings settings)
        {
            this.render = render;
            this.settings = settings;
        }

        public string CommandId { get; } = "create";
        public string Description { get; } = "Create image from tag cloud";
        public string Usage { get; } = "create <FileName>";

        public ICommandResult Handle(string[] args)
        {
            if (args.Length == 0)
                return new CommandResult(false, "You must specify file name");

            settings.Name = args[0];
            render.Render();
            return new CommandResult(true, $"Created in {settings.OutputPath}");
        }
    }
}
