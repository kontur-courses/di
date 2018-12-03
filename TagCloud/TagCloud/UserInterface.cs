namespace TagCloud
{
    abstract class UserInterface
    {
        private readonly TagCloudCreator _creator;

        protected UserInterface(TagCloudCreator creator)
        {
            _creator = creator;
        }

        public abstract void Run(string[] startupArgs);
    }
}