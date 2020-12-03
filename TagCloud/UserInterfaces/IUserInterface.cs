using TagCloud.Settings;

namespace TagCloud.UserInterfaces
{
    public interface IUserInterface
    {
        // Данный интерфейс не используется, потому что я не знаю как возвращать в container builder поля этого интерфейса
        public DrawerSettings DrawerSettings { get; }
        public CircularLayouterSettings CircularLayouterSettings { get; }
        public FileReaderSettings FileReaderSettings { get; }
        public LayoutSettings LayoutSettings { get; }
        public SaverSettings SaverSettings { get; }
    }
}