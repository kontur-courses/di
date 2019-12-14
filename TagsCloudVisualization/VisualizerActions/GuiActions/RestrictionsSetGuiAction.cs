using TagsCloudVisualization.GUI;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.VisualizerActions.GuiActions
{
    public class RestrictionsSetGuiAction : RestrictionsSetAction, IGuiAction
    {
        public RestrictionsSetGuiAction(AppSettings appSettings) : base(appSettings)
        {}

        public override string GetActionDescription()
        {
            return "Наложить ограничения";
        }

        public override string GetActionName()
        {
            return "Ограничения...";
        }

        protected override Restrictions GetRestrictions()
        {
            var restrictions = appSettings.Restrictions;
            SettingsForm.For(restrictions).ShowDialog();
            return restrictions;
        }

        public MenuCategory GetMenuCategory()
        {
            return MenuCategory.Settings;
        }
    }
}