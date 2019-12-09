using TagsCloud.Interfaces;

namespace TagsCloud.MenuActions
{
	public class CircularLayouterAction: IMenuAction
	{
		private readonly ILayoutPainter painter;
		private readonly ILayoutConstructor layoutConstructor;
		private readonly SpiralParameters parameters;
		public string Category { get; } = "Создать облако тегов";
		public string Name { get; } = "Circular layouter";
		public string Description { get; } = "Располагает слова вокруг центра";

		public CircularLayouterAction(ILayoutPainter painter, ILayoutConstructor layoutConstructor,
			SpiralParameters parameters)
		{
			this.painter = painter;
			this.layoutConstructor = layoutConstructor;
			this.parameters = parameters;
		}
		
		public void Perform()
		{
			SettingsForm.For(parameters).ShowDialog();
			var newLayout = layoutConstructor.GetLayout();
			painter.PaintTags(newLayout);
		}
	}
}