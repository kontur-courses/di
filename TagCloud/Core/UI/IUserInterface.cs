using System.Collections.Generic;

namespace TagCloud.Core.UI
{
    public interface IUserInterface
    {
        public void Run(IEnumerable<string> args);
    }
}