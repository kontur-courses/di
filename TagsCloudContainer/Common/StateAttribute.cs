using System;

namespace TagsCloudContainer.Common
{
    public class StateAttribute : Attribute
    {
        public State State { get; set; }

        public StateAttribute(State state)
        {
            State = state;
        }
    }
}