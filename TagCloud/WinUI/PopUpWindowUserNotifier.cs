using System.Windows.Forms;

namespace WinUI
{
    public class PopUpWindowUserNotifier : IUserNotifier
    {
        public void Notify(string message) => MessageBox.Show(
            message, 
            "Tag cloud layouter", 
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
    }
}