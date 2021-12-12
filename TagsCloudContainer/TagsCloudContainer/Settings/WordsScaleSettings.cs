using TagsCloudContainer.DependencyInjection;
using TagsCloudContainer.MathFunctions;

namespace TagsCloudContainer.Settings
{
    public class WordsScaleSettings : IWordsScaleSettings
    {
        public IMathFunction Function { get; }

        public WordsScaleSettings(
            IRenderSettings settings,
            IServiceResolver<MathFunctionType, IMathFunction> functionResolver)
        {
            Function = functionResolver.GetService(settings.WordsScale);
        }
    }
}