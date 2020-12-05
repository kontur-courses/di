using System.Collections.Generic;
using System.Linq;

namespace TagCloud.App.CLI
{
    public class Automata
    {
        private State currentState;
        private readonly Dictionary<State, IEnumerable<Transition>> transitionsMap;

        public Automata()
        {
            transitionsMap = new Dictionary<State, IEnumerable<Transition>>();
        }
        
        public Automata(State initialState, IEnumerable<Transition> transitions) : this()
        {
            currentState = initialState;
            foreach (var transition in transitions) 
                AddTransition(transition);
        }
        
        public void Add(Transition transition)
        {
            AddTransition(transition);
        }

        private void AddTransition(Transition transition)
        {
            if (!transitionsMap.TryGetValue(transition.Origin, out var allTransitions))
                allTransitions = new List<Transition>();
            transitionsMap[transition.Origin] = allTransitions.Append(transition);
        }

        public void Move(string input)
        {
            currentState.OnAct(new ConsoleInputEventArgs(input));
            Transfer(input);
        }

        private void Transfer(string input)
        {
            if (!transitionsMap.TryGetValue(currentState, out var transitions))
                return;
            var possibleTransition = transitions.FirstOrDefault(transition => transition.DoesTransfer(input));
            if (possibleTransition == null) 
                return;
            currentState = possibleTransition.Destination;
        }

        public bool Show()
        {
            currentState.OnShow();
            return !currentState.IsFinal;
        }

        public void Init(State mainState)
        {
            currentState = mainState;
        }
    }
}