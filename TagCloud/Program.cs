using TagCloud.UI;

namespace TagCloud
{
    public class Program
    {
        static void Main(string[] args)
        {
            IUserInterface client = new ConsoleUI();
            client.Run("test.txt");
        }
    }
}
