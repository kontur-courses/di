using System;
using System.Drawing;
using System.Windows.Forms;

namespace TagsCloudVisualization
{
    public partial class GetParamsForm : Form
    {
        public GetParamsForm()
        {
            InitializeComponent();
        }

        private void generate_Click(object sender, EventArgs e)
        {
            var sCount = rectanglesCount.Text;
            if (!int.TryParse(sCount, out var iCount))
            {
                MessageBox.Show("Введите только число в поле количества прямоугольников");
                return;
            }

            var sCenterX = centerX.Text;
            if (!int.TryParse(sCenterX, out var iCenterX))
            {
                MessageBox.Show("Введите только число координаты X центра в поле ввода");
                return;
            }

            var sCenterY = centerY.Text;
            if (!int.TryParse(sCenterY, out var iCenterY))
            {
                MessageBox.Show("Введите только число координаты Y центра в поле ввода");
                return;
            }

            var sSpiralStep = spiralStep.Text;
            if (!int.TryParse(sSpiralStep, out var iSpiralStep))
            {
                MessageBox.Show("Введите шаг спирали целым числом");
                return;
            }

            var drawForm = new CloudDrawer(iCount, new Point(iCenterX, iCenterY), iSpiralStep);
            drawForm.Show();
        }
    }
}