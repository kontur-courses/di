namespace TagCloud.Tests.TagParser;

public class TagParserDataProvider
{
    public static IEnumerable<string[]> GetUniqueLowercaseWords()
    {
        yield return new[]
        {
            "каждый",
            "охотник",
            "желает",
            "знать",
            "где",
            "сидит",
            "фазан"
        };
    }    
    
    public static IEnumerable<string[]> GetUniqueMixedCaseWords()
    {
        yield return new[]
        {
            "кАжДый",
            "охоТник",
            "ЖЕЛАЕТ",
            "ЗНаТЬ",
            "Где",
            "СИДИт",
            "фазан"
        };
    }

    public static IEnumerable<string[]> GetRepeatingLowercaseWords()
    {
        yield return new[]
        {
            "каждый",
            "каждый",
            "каждый",
            "охотник",
            "желает",
            "желает",
            "где",
            "где",
            "знать",
            "фазан",
            "фазан",
            "сидит",
            "каждый"
        };
    }

    public static IEnumerable<string[]> GetRepeatingMixedCaseWords()
    {
        yield return new[]
        {
            "кАЖДЫй",
            "каЖдЫй",
            "Каждый",
            "охОтник",
            "желАЕт",
            "жЕЛает",
            "ГДЕ",
            "где",
            "знатЬ",
            "фазАН",
            "фаЗан",
            "сидит",
            "каЖдый"
        };
    }
}