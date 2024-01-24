using System.Windows.Forms;
using Autofac;

namespace TagsCloud;

public class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        Application.SetHighDpiMode(HighDpiMode.SystemAware);
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        var program = ProgramConstructor.ConstructProgram();
        var cloudForm = program.Resolve<CloudForm>();
        Application.Run(cloudForm);
    }
}