using System.ComponentModel;
using System.Globalization;
using CircularCloudLayouter.WeightedLayouter.Forming;
using CircularCloudLayouter.WeightedLayouter.Forming.Standard;

namespace TagCloudApp.Infrastructure;

public class FormFactorConverter : TypeConverter
{
    private static readonly Type FormFactorType = typeof(FormFactor);

    private static readonly IReadOnlyDictionary<string, FormFactor> FormFactors = new Dictionary<string, FormFactor>
    {
        {"Rectangle", new RectangleFormFactor()},
        {"Ellipse", new EllipseFormFactor()},
        {"Cross", new CrossFormFactor()},
        {"X", new XFormFactor()}
    };

    public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext? context) =>
        new(FormFactors.Values.ToArray());

    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType) =>
        sourceType == typeof(string);

    public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType) =>
        destinationType?.IsAssignableTo(FormFactorType) ?? false;

    public override object ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        if (value is string formFactorName)
        {
            return FormFactors[formFactorName];
        }

        throw GetConvertFromException(value);
    }

    public override object? ConvertTo(
        ITypeDescriptorContext? context,
        CultureInfo? culture,
        object? value,
        Type destinationType
    )
    {
        if (destinationType != typeof(string) || value is null)
            throw GetConvertToException(value, destinationType);

        var sourceType = value.GetType();
        foreach (var (name, formFactor) in FormFactors)
        {
            if (formFactor.GetType() == sourceType)
                return name;
        }

        return null;
    }

    public override bool GetStandardValuesSupported(ITypeDescriptorContext? context) => true;
    public override bool GetStandardValuesExclusive(ITypeDescriptorContext? context) => true;
}