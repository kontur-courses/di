using TagsCloud.Interfaces;

namespace TagsCloud.MenuActions
{
	public class CircularLayouterAction: IMenuAction
	{
		private readonly LayoutPainter painter;
		private readonly ILayoutConstructor layoutConstructor;
		private readonly SpiralSettings settings;
		public string Category { get; } = "Создать облако тегов";
		public string Name { get; } = "Circular layouter";
		public string Description { get; } = "Располагает слова вокруг центра";

		public CircularLayouterAction(LayoutPainter painter, ILayoutConstructor layoutConstructor,
			SpiralSettings settings)
		{
			this.painter = painter;
			this.layoutConstructor = layoutConstructor;
			this.settings = settings;
		}
		
		public void Perform()
		{
			SettingsForm.For(settings).ShowDialog();
			var newLayout = layoutConstructor.GetLayout();
			painter.PaintTags(newLayout);
		}
	}
}