using System;
using System.Collections.Generic;
using System.Linq;
using TagCloud.Infrastructure.Graphics;
using TagCloud.Infrastructure.Settings;
using TagCloud.Infrastructure.Settings.UISettingsManagers;
using TagCloud.Infrastructure.Text;

namespace TagCloud.App.CLI
{
    public class TagCloudLayouterCli : IApp
    {
        private readonly IPainter<string> painter;
        private readonly IEnumerable<ISettingsManager> settingsManagers;
        private readonly IReader<string> reader;
        private readonly Func<Settings> settingsFactory;
        private readonly WordAnalyzer<string> wordAnalyzer;

        public TagCloudLayouterCli(
            IReader<string> reader,
            WordAnalyzer<string> wordAnalyzer,
            Func<Settings> settingsFactory, 
            IPainter<string> painter,
            IEnumerable<ISettingsManager> settingsManagers)
        {
            this.reader = reader;
            this.wordAnalyzer = wordAnalyzer;
            this.settingsFactory = settingsFactory;
            this.painter = painter;
            this.settingsManagers = settingsManagers;
        }

        public void Run()
        {
            var automata = new Automata();
            
            var mainState = new State("Main");
            mainState.Show += DisplayState;
            mainState.Act += OnMainInput;
            var helpState = new State("Help");
            helpState.Show += DisplayState;
            helpState.Show += OnHelp;
            var exitState = new State(true);
            exitState.Show += OnExit;
            var aboutState = new State("About");
            aboutState.Show += OnAbout;
            var settingsState = new State("Settings");
            settingsState.Show += DisplayState;
            settingsState.Show += OnSettingsState;

            automata.Init(mainState);
            automata.Add(new Transition(mainState, "help", helpState));
            automata.Add(new Transition(helpState, "quit", mainState));
            automata.Add(new Transition(mainState, "exit", exitState));
            automata.Add(new Transition(helpState, "about", aboutState));
            automata.Add(new Transition(aboutState, ".*", helpState));
            
            automata.Add(new Transition(mainState, "settings", settingsState));
            automata.Add(new Transition(settingsState, "\\D", settingsState));
            var managersStates = GetSettingsManagersStates(settingsManagers);
            AddSettingsManagersTransitions(automata, settingsState, managersStates);
            
            settingsFactory().Import(Program.GetDefaultSettings());
            Console.WriteLine("Default settings imported");
            // var tokens = reader.ReadTokens();
            // var analyzedTokens = wordAnalyzer.Analyze(tokens);
            // Console.WriteLine("Tokens analyzed");
            // using var image = painter.GetImage(analyzedTokens);
            // Console.WriteLine("Layout ready");
            // var imagePath = settingsFactory().ImagePath;
            // Console.WriteLine($"Saving into {Path.GetFullPath(imagePath)}");
            // image.Save(imagePath);
            // Console.WriteLine("Image saved");
            
            while (automata.Show())
            {
                Console.Write("> ");
                var inp = Console.ReadLine();
                Console.Clear();
                automata.Move(inp);
            }
        }

        private void AddSettingsManagersTransitions(Automata automata, State from,  IEnumerable<State> states)
        {
            foreach (var (state, i) in states.Select((state, i) => (state, i)))
            {
                automata.Add(new Transition(from, $"{i}", state));
                automata.Add(new Transition(state, ".*", from));
            }
        }

        private IEnumerable<State> GetSettingsManagersStates(IEnumerable<ISettingsManager> managers)
        {
            foreach (var manager in managers)
            {
                var state = new State(manager.Title);
                state.Show += (sender, args) =>
                {
                    Console.WriteLine(manager.Get());
                };
                state.Act += (sender, args) =>
                {
                    Console.WriteLine(!manager.TrySet(args.Input)
                        ? $"Incorrect input: {args.Input}"
                        : $"{manager.Title} was changed to {args.Input}");
                };
                yield return state;
            }
        }

        private void OnSettingsState(State sender, EventArgs args)
        {
            var i = 0;
            foreach (var manager in settingsManagers)
            {
                Console.WriteLine($"{i}) {manager.Title}\n\t{manager.Help}\n{manager.Get()}\n");
                i++;
            }
            Console.WriteLine($"Choose setting number ");
        }

        private void OnAbout(State sender, EventArgs args)
        {
            Console.WriteLine("I\nAM\nRUS\nLAND");
        }

        private void OnHelp(object sender, EventArgs args)
        {
            Console.WriteLine("Available commands");
            Console.WriteLine("\thelp");
            Console.WriteLine("\texit");
            Console.WriteLine("\tquit");
            Console.WriteLine("\tabout");
            Console.WriteLine("\tsettings");
            Console.WriteLine("Type quit...");
        }

        private void OnExit(State state, EventArgs eventArgs)
        {
            Console.WriteLine("Bye bye");
        }

        private void DisplayState(State sender, EventArgs args)
        {
            Console.WriteLine($"$ {sender.Name}:");
        }

        private void OnMainInput(object sender, ConsoleInputEventArgs args)
        {
            Console.WriteLine($"Type help for help");
        }
    }
}