using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagCloud.Infrastructure.Graphics;
using TagCloud.Infrastructure.Settings;
using TagCloud.Infrastructure.Settings.UISettingsManagers;

namespace TagCloud.App.CLI
{
    public class TagCloudLayouterCli : IApp
    {
        private readonly Func<Settings> settingsFactory;
        private readonly IEnumerable<ISettingsManager> settingsManagers;
        private readonly IImageGenerator generator;

        public TagCloudLayouterCli(Func<Settings> settingsFactory, IEnumerable<ISettingsManager> settingsManagers, IImageGenerator generator)
        {
            this.settingsFactory = settingsFactory;
            this.settingsManagers = settingsManagers;
            this.generator = generator;
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
            automata.Add(new Transition(mainState, "exit", exitState));
            automata.Add(new Transition(helpState, "about", aboutState));
            automata.Add(new Transition(helpState, @".*", mainState));
            automata.Add(new Transition(aboutState, ".*", helpState));

            automata.Add(new Transition(mainState, "set", settingsState));
            automata.Add(new Transition(mainState, "settings", settingsState));
            var managersStates = GetSettingsManagersStates(settingsManagers);
            AddSettingsManagersTransitions(automata, settingsState, managersStates);
            automata.Add(new Transition(settingsState, @"\D*", mainState));

            var generateState = new State("Generation");
            generateState.Show += OnGenerateState;
            automata.Add(new Transition(mainState, "gen", generateState));
            automata.Add(new Transition(mainState, "generate", generateState));
            automata.Add(new Transition(generateState, ".*", mainState));

            settingsFactory().Import(Program.GetDefaultSettings());

            while (automata.Show())
            {
                Console.Write("> ");
                var inp = Console.ReadLine();
                Console.Clear();
                automata.Move(inp);
            }
        }

        private void OnGenerateState(State sender, EventArgs args)
        {
            using var image = generator.Generate();
            Console.WriteLine("Layout ready");
            var imagePath = settingsFactory().ImagePath;
            Console.WriteLine($"Image will be saved into {Path.GetFullPath(imagePath)}");
            image.Save(imagePath, settingsFactory().Format);
            Console.WriteLine("Image saved");
        }

        private void AddSettingsManagersTransitions(Automata automata, State from, IEnumerable<State> states)
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
                    Console.WriteLine(
                        $"CHANGING\n\t{manager.Title}\ninfo\t{manager.Help}\nvalue\t{manager.Get()}\ninput new value");
                };
                state.Act += (sender, args) =>
                {
                    Console.WriteLine(!manager.TrySet(args.Input)
                        ? $"Incorrect input: {args.Input}"
                        : $"{manager.Title} was changed to '{manager.Get()}'");
                };
                yield return state;
            }
        }

        private void OnSettingsState(State sender, EventArgs args)
        {
            foreach (var (manager, i) in settingsManagers.Select((manager, i) => (manager, i)))
                Console.WriteLine($"{i})\t{manager.Title}\ninfo\t{manager.Help}\nvalue\t{manager.Get()}\n");
            Console.WriteLine("Choose setting number ");
        }

        private void OnAbout(State sender, EventArgs args)
        {
            Console.WriteLine("I\nAM\nRUS\nLAND");
        }

        private void OnHelp(object sender, EventArgs args)
        {
            Console.WriteLine("Available commands in main");
            Console.WriteLine("\thelp");
            Console.WriteLine("\texit");
            Console.WriteLine("\tsettings");
            Console.WriteLine("\tgenerate");
            Console.WriteLine("press Enter key to go back");
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
            if (!args.IsTransfer)
                Console.WriteLine($"'{args.Input}' not found. Type help for help");
        }
    }
}