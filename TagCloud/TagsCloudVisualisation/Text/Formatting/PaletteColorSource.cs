using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualisation.Text.Formatting
{
    [VisibleName("Случаный цвет из предсозданной палитры")]
    public class PaletteColorSource : RandomColorSource
    {
        public PaletteColorSource() : base(
            CreateColors("#B05F6D", "#EB6B56", "#FFC153", "#47B39D"),
            ColorTranslator.FromHtml("#462446"))
        {
        }

        private static IEnumerable<Color> CreateColors(params string[] hexCodes) =>
            hexCodes.Select(ColorTranslator.FromHtml);
    }
}