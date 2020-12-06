using System;
using System.ComponentModel;
using System.Globalization;
using TagsCloudLayouters.Contracts;

namespace TagsCloudLayouters.Infrastructure
{
    public class ColorizerTypeConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(
            ITypeDescriptorContext context,
            CultureInfo culture,
            object value,
            Type destType)
        {
            if (destType != typeof(string) || value is not IColorizer)
                return base.ConvertTo(context, culture, value, destType);
            return ((IColorizer) value).Name;
        }
    }
}