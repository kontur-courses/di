using System.Drawing;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace TagsCloudContainer.UserInterface.Window
{
    public sealed class BitmapForm : MetroForm
    {
        public BitmapForm(Bitmap bitmap)
        {
            ShadowType = MetroFormShadowType.None;
            Size = bitmap.Size;
            Controls.Add(new PictureBox {Image = bitmap, Size = bitmap.Size, Dock = DockStyle.Fill});
        }
    }
}