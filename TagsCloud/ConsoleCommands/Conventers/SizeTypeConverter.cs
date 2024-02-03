using System.ComponentModel;
using System.Drawing;

namespace TagsCloud.ConsoleCommands.Conventers;

public class SizeTypeConverter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
        return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
    }

    public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture,
        object value)
    {
        if (value is string stringValue)
        {
            string[] sizeValues = stringValue.Split(',');

            if (sizeValues.Length == 2 &&
                int.TryParse(sizeValues[0], out int width) &&
                int.TryParse(sizeValues[1], out int height))
            {
                return new Size(width, height);
            }
        }

        throw new ArgumentException();
    }
}