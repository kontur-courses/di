using System;
using System.Collections.Generic;
using System.ComponentModel;
using TagsCloudContainer.Preprocessing;

namespace TagsCloudApp
{
    public class SpeechPartParser : IObjectParser<SpeechPart>
    {
        public SpeechPart Parse(string value)
        {
            if (Enum.TryParse<SpeechPart>(value, true, out var speechPart))
                return speechPart;

            throw new ApplicationException("Available speech parts:\n"
                                           + string.Join(Environment.NewLine,
                                               GetEnumValuesDescription(typeof(SpeechPart))));
        }

        private static IEnumerable<string> GetEnumValuesDescription(Type type)
        {
            foreach (var enumName in type.GetEnumNames())
            {
                var enumInfo = type.GetMember(enumName)[0];
                var attributes = enumInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                var description = (attributes.Length > 0) ? ((DescriptionAttribute)attributes[0]).Description : "";
                yield return $"{enumName}: {description}";
            }
        }
    }
}