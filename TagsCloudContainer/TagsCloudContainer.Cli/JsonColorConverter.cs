using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace TagsCloudContainer.Cli;

public partial class JsonColorConverter : JsonConverter<Color>
{
    [GeneratedRegex(@"#(?<r>\d{1,2})#(?<g>\d{1,2})#(?<b>\d{1,2})(#(?<a>\d{1,2}))?", RegexOptions.Compiled)]
    private partial Regex HexColorRegex();

    public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
            throw ReaderException;
        var s = reader.GetString()!;
        
        if (Enum.TryParse<KnownColor>(s, out var knownColor))
            return Color.FromKnownColor(knownColor);
        
        var regex = HexColorRegex();
        var hexMatch = regex.Match(s);
        
        if (hexMatch.Success)
        {
            var r = Convert.ToInt32(hexMatch.Groups["r"].Value, 16);
            var g = Convert.ToInt32(hexMatch.Groups["g"].Value, 16);
            var b = Convert.ToInt32(hexMatch.Groups["b"].Value, 16);
            var a = Convert.ToInt32(hexMatch.Groups.ContainsKey("a") ? hexMatch.Groups["a"].Value : "ff", 16);
            if (r is > 0 and <= 255 &&
                g is > 0 and <= 255 &&
                b is > 0 and <= 255 &&
                a is > 0 and <= 255)
                return Color.FromArgb(a, r, g, b);
        }

        throw ReaderException;
    }

    private static Exception ReaderException =>
        new ArgumentException("Value of reader is not parsable to color", "reader");

    public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options)
    {
        if (value.IsNamedColor)
            writer.WriteStringValue(value.Name);
        else
            writer.WriteStringValue(
                $"#{Convert.ToString(value.R, 16)}" +
                $"#{Convert.ToString(value.G, 16)}" +
                $"#{Convert.ToString(value.B, 16)}" +
                $"#{Convert.ToString(value.A, 16)}");
    }
}