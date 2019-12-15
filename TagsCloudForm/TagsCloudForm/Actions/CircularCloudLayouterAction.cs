using System.Runtime.CompilerServices;
using TagsCloudForm.CloudPainters;
using TagsCloudForm.UiActions;

[assembly: InternalsVisibleTo("TagsCloudTests")]
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
