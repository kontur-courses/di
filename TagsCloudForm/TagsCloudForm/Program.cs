using System.Drawing;
using System.Windows.Forms;
namespace TagsCloudForm
{
    class Program
    {

        public static void Main()
        {
            var form = new CloudForm(30, 10, 30)
            {
                Size = new Size(600, 600)
            };

            Application.Run(form);
        }
    }
}
