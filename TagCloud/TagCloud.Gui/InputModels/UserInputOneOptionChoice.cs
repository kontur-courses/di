using System.Collections.Generic;
using System.Linq;

namespace TagCloud.Gui.InputModels
{
    public class UserInputOneOptionChoice<T> : UserInputChoiceBase<T>
    {
        public UserInputOneOptionChoice(string description, IEnumerable<UserInputSelectorItem<T>> available)
            : base(description, available)
        {
            Selected = ItemsByNames.First().Value;
        }

        public UserInputSelectorItem<T> Selected { get; private set; }

        public void SetSelected(string name)
        {
            Selected = ItemsByNames[name];
        }
    }
}