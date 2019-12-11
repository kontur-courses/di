using System.Text;
using CliWrap;

namespace TagsCloudContainer.CommandsExecuting
{
    public class CmdCommandExecutor
    {
        public string ExecuteCommand(string command)
        {
            var executeResult = new Cli("cmd")
                .SetStandardOutputEncoding(Encoding.UTF8)
                .SetArguments("/c" + command)
                .Execute();
            return executeResult.StandardOutput;
        }
    }
}