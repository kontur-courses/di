using System.Collections.Generic;

namespace TagsCloudContainer.Core.UserInterfaces
{
    interface IUi
    {
        void Run(IEnumerable<string> userInput);
    }
}