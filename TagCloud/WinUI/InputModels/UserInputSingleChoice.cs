using System.Collections.Generic;
using System.Linq;

namespace WinUI.InputModels
{
    public class UserInputSingleChoice<T> : UserInputChoiceBase<T>
    {
        public UserInputSingleChoice(string description, IEnumerable<UserInputSelectorItem<T>> available)
            : base(description, available)
        {
            Selected = ItemsByNames.First().Value;
        }

        public UserInputSelectorItem<T> Selected { get; private set; }

        public void SetSelected(string name)
        {
            Selected = ItemsByNames[name];
            OnAfterSelectionChanged(Selected);
        }
    }
}