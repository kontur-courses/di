using System.Drawing;
using System.Windows.Forms;
using GroboContainer.Core;
using GroboContainer.Impl;
using NUnit.Framework.Constraints;
using TagCloud.CloudLayouter;
using TagCloud.Interfaces.GUI.UIActions;

namespace TagCloud.Interfaces.GUI
{
    public class MainForm : Form
    {
        public MainForm(IUIAction[] actions)
        {
            var mainMenu = new MenuStrip();
            mainMenu.Items.AddRange(actions.ToMenuItems());
            Controls.Add(mainMenu);

            ClientSize = new Size(600, 400);
        }
    }
}