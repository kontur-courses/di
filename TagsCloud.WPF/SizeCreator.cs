using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Size = System.Drawing.Size;

namespace TagsCloudVisualization.WPF;

internal static class SizeCreator
{
    private static readonly Random Random = new();
    private const int BorderLength = 5;

    public static Size GetRandomRectangleSize(int randomFrom, int randomTo) =>
        new(Random.Next(randomFrom, randomTo), Random.Next(randomFrom, randomTo));

    public static Size GetRectangleSize(TextBox tb)
    {
#pragma warning disable CS0618
        var formattedText = new FormattedText(tb.Text, CultureInfo.CurrentUICulture,
#pragma warning restore CS0618
            FlowDirection.LeftToRight,
            new Typeface(tb.FontFamily, 
                tb.FontStyle, 
                tb.FontWeight, 
                tb.FontStretch),
            tb.FontSize,
            Brushes.Black, 
            new NumberSubstitution());

        return new Size((int) formattedText.Width + BorderLength, (int) formattedText.Height + BorderLength);
    }
}