using TagsCloudForm.Common;
using TagsCloudForm.UiActions;

namespace TagsCloudForm.Actions
{
    public class CircularCloudLayouterAction : IUiAction
    {
        private readonly IPainterFactory factory;
        public CircularCloudLayouterAction(IPainterFactory factory)
        {
            this.factory = factory;
        }
        public string Category => "CircularCloud";
        public string Name => "Layouter";
        public string Description => "Создание облака";

        public void Perform()
        {
            factory.Create().Paint();
        }
    }
}
