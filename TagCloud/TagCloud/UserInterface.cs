namespace TagCloud
{
    internal abstract class UserInterface
    {
        protected readonly TagCloudCreator Creator;

        protected UserInterface(TagCloudCreator creator)
        {
            this.Creator = creator;
        }

        public abstract void Run(string[] startupArgs);
    }
}
