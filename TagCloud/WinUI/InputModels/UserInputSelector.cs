using System;
using System.Linq;

namespace WinUI.InputModels
{
    public class UserInputSelector<T>
    {
        public UserInputSelector(string description, UserInputSelectorItem<T>[] available)
        {
            Description = description;
            Available = available;
            Selected = Available[0];
        }

        public string Description { get; }
        public UserInputSelectorItem<T>[] Available { get; }
        public UserInputSelectorItem<T> Selected { get; set; }

        public void SetSelected(string name)
        {
            Selected = Available.Single(x => x.Name == name);
            SelectedChanged.Invoke(Selected);
        }

        public event Action<UserInputSelectorItem<T>> SelectedChanged;
    }
}