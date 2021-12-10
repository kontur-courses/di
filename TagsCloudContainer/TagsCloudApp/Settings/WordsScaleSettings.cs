using TagsCloudApp.Parsers;
using TagsCloudApp.RenderCommand;
using TagsCloudContainer.DependencyInjection;
using TagsCloudContainer.MathFunctions;
using TagsCloudContainer.Settings;

namespace TagsCloudApp.Settings
{
    public class WordsScaleSettings : IWordsScaleSettings
    {
        public IMathFunction Function { get; }

        public WordsScaleSettings(
            IServiceResolver<MathFunctionType, IMathFunction> functionResolver,
            IRenderOptions renderOptions,
            IEnumParser parser)
        {
            var type = parser.Parse<MathFunctionType>(renderOptions.WordsScale);
            Function = functionResolver.GetService(type);
        }
    }
}