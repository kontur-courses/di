using TagsCloudForm.UiActions;

namespace TagsCloudForm.Actions
{
    internal class CircularCloudLayouterAction : IUiAction
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
