using System.Drawing;

namespace WinUI.InputModels
{
    public class UserInputColor
    {
        public UserInputColor(string description, Color defaultValue)
        {
            Description = description;
            Picked = defaultValue;
        }

        public string Description { get; }
        public Color Picked { get; set; }
    }
}