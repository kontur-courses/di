namespace TagCloud.Interfaces.GUI.UIActions
{
    public class CloudSettings : IUIAction
    {
        public string Category => "Настройки";
        public string Name => "Настройки облака";
        public string Description => "Позволяет задать количество слов и коеффициент масштабирования слова";
        public void Perform()
        {
            throw new System.NotImplementedException();
        }
    }
}