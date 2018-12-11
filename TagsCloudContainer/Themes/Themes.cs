using System.Collections.Generic;

namespace TagsCloudContainer.Themes
{
    public class Themes
    {
        public static readonly Dictionary<string, ITheme> ThemesDictionary = new Dictionary<string, ITheme>
        {
            {"classic", new Classic()},
            {"black", new Black()}
        };
    }
}