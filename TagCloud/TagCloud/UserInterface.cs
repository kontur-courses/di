namespace TagCloud
{
    abstract class UserInterface
    {
        protected readonly TagCloudCreator Creator;

        protected UserInterface(TagCloudCreator creator)
        {
            Creator = creator;
        }

        public abstract void Run(string[] startupArgs);
    }
}