using System.Collections.Generic;

namespace TagsCloudContainer
{
    public interface IUserHandler
    {
        InputInfo GetInputInfo();
        void WriteToUser(IEnumerable<string> messages);
    }
}