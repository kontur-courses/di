using System.Collections.Generic;

namespace TagCloudUI.UI
{
    public interface IUserInterface
    {
        public void Run(IEnumerable<string> args);
    }
}