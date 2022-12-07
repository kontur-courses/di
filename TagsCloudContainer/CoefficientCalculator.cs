using System;
using System.Drawing;

namespace TagsCloudContainer
{
    /// <summary>
    /// Считает коэффицент для размера шрифта в зависимости от размера холста.
    /// Гарантирует что картинка не поедет при нестандартных размерах.
    /// </summary>
    public class CoefficientCalculator
    {
        private Size canvasSize;
        private int border;

        public CoefficientCalculator(CustomSettings settings)
        {
            canvasSize = new Size(settings.CanvasWidth, settings.CanvasHeight);
            this.border = settings.CanvasBorder;
        }

        public int CalculateCoefficient() => Math.Min(canvasSize.Width - border, canvasSize.Height - border) / 100 * 2;
    }
}