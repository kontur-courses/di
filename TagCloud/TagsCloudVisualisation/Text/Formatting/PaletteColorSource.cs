using System.Drawing;
using System.Linq;

namespace TagsCloudVisualisation.Text.Formatting
{
    [VisibleName("Случаный цвет из предсозданной палитры")]
    public class PaletteColorSource : RandomColorSource
    {
        private static readonly string[] colorsHexCodes = {"#B05F6D", "#EB6B56", "#FFC153", "#47B39D"};

        public PaletteColorSource() : base(CreateColors(colorsHexCodes))
        {
        }

        public override Color BackgroundColor { get; } = ColorTranslator.FromHtml("#462446");

        private static Color[] CreateColors(params string[] hexCodes) =>
            hexCodes.Select(ColorTranslator.FromHtml).ToArray();
    }
}