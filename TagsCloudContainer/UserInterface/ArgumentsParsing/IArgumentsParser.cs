using TagsCloudContainer.Core;

namespace TagsCloudContainer.UserInterface.ArgumentsParsing
{
    public interface IArgumentsParser<in TArguments>
    {
        Parameters ParseArgumentsToParameters(TArguments arguments);
    }
}