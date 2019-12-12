using NHunspell;
using TagsCloudGenerator.Core.Filters;
using TagsCloudGenerator.Core.Layouters;
using TagsCloudGenerator.Core.Normalizers;
using TagsCloudGenerator.Core.Spirals;

namespace TagsCloudGenerator.Core.Translators
{
    public class TextToTagsTranslatorFactory
    {   
        public static TextToTagsTranslator Create(float alpha, float stepPhi)
        {
            return new TextToTagsTranslator(
                new WordsNormalizer(), 
                new Hunspell(@"HunspellDictionaries\ru.aff", @"HunspellDictionaries\ru.dic"),
                new WordsFilter(),
                new SpiralRectangleCloudLayouter(new ArchimedeanSpiral(alpha, stepPhi))
            );                
        }
    }
}