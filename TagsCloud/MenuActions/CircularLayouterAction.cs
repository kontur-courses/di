using TagsCloud.Interfaces;

namespace TagsCloud.MenuActions
{
	public class CircularLayouterAction: IMenuAction
	{
		private readonly ILayoutPainter _painter;
		private readonly ILayoutConstructor _layoutConstructor;
		private readonly SpiralSettings _settings;
		public string Category { get; } = "Создать облако тегов";
		public string Name { get; } = "Circular layouter";
		public string Description { get; } = "Располагает слова вокруг центра";

		public CircularLayouterAction(ILayoutPainter painter, ILayoutConstructor layoutConstructor,
			SpiralSettings settings)
		{
			_painter = painter;
			_layoutConstructor = layoutConstructor;
			_settings = settings;
		}
		
		public void Perform()
		{
			SettingsForm.For(_settings).ShowDialog();
			var newLayout = _layoutConstructor.GetLayout();
			_painter.PaintTags(newLayout);
		}
	}
}