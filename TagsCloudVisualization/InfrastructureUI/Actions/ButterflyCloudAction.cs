using System.Drawing;
using System.Windows.Forms;
using TagsCloudVisualization.Infrastructure.Algorithm.Curves;
using TagsCloudVisualization.InfrastructureUI.Painters;

namespace TagsCloudVisualization.InfrastructureUI.Actions
{
    public class ButterflyCloudAction : IUiAction
    {
        private readonly IImageHolder imageHolder;
        private readonly CloudPainter painter;
        private readonly SetTextAction setTextAction;

        public ButterflyCloudAction(CloudPainter painter,
            IImageHolder imageHolder, SetTextAction setTextAction)
        {
            this.setTextAction = setTextAction;
            this.painter = painter;
            this.imageHolder = imageHolder;
        }

        public Category Category => Category.Cloud;
        public string Name => "Бабочка";
        public string Description => "";

        public void Perform()
        {
            var dialog = setTextAction.FileDialog();
            var result = dialog.ShowDialog();

            if (result != DialogResult.OK) return;

            var path = dialog.FileName;
            var size = imageHolder.GetImageSize();
            var butterfly = new Butterfly(new Point(size.Width / 2, size.Height / 2));
            painter.Paint(path, butterfly);
        }
    }
}