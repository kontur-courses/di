using System.Linq;

namespace WinUI
{
    public class UserInputSelector<T>
    {
        public string Description { get; set; }
        public UserInputSelectorItem<T> Selected { get; set; }
        public UserInputSelectorItem<T>[] Available { get; set; }

        public void SetSelected(string name) =>
            Selected = Available.Single(x => x.Name == name);
    }
}