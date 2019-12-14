using System;
using System.Collections.Generic;
using System.Linq;
using UIConsole.SystemCommands;

namespace UIConsole
{
    public class ConsoleUserInterface
    {
        internal const string HelloMessage =
            "Debug Console is running...\nДобро пожаловать в консоль отладки\nВведите l-cmd, чтоб узнать список возможных команд";

        private readonly Dictionary<string, IConsoleCommand> commands;

        internal bool IsStopped { get; set; }

        public ConsoleUserInterface(IConsoleCommand[] commandsForRegister)
        {
            commands = new Dictionary<string, IConsoleCommand>();

            InitializeBaseCommand();

            foreach (var command in commandsForRegister)
                AddCommand(command);

            IsStopped = true;
        }

        private void InitializeBaseCommand()
        {
            AddCommand(new ConsoleCommandList());
            AddCommand(new ClearConsoleText());
            AddCommand(new Exit());
        }

        private void AddCommand(IConsoleCommand consoleCommand) => commands.Add(consoleCommand.Name.ToLower(), consoleCommand);

        public void Run()
        {
            IsStopped = false;

            PrintInConsole(HelloMessage);

            while (true)
            {
                if(IsStopped) break;

                try
                {
                    var inputString = Console.ReadLine();
                    var command = GetCommand(inputString);
                    command.Execute(this, GetArgs(command, inputString));
                }
                catch (Exception e)
                {
                    PrintInConsole(e.Message);
                }
            }
        }

        internal IEnumerable<IConsoleCommand> GetCommandsList() => commands.Values.ToList();

        private IConsoleCommand GetCommand(string input)
        {
            var splittingCommand = input.Split();
            return commands[splittingCommand[0].ToLower()];
        }

        private Dictionary<string, object> GetArgs(IConsoleCommand consoleCommand, string input)
        {
            var output = new Dictionary<string, object>();

            var args = input.Split().Skip(1).ToList();
            
            if (consoleCommand.ArgsName.Count != args.Count)
                throw new ArgumentException("Указаны не все аргументы");

            var argsName = consoleCommand.ArgsName;
            for (var i = 0; i < argsName.Count; i++)
                output.Add(argsName[i], GetArg(i));
            return output;

            string GetArg(int i)
            {
                try { return args[i]; }
                catch { return null; }
            }
        }

        internal static void PrintInConsole(string str)
        {
            Console.WriteLine("\n--------------------");
            Console.WriteLine(str);
            Console.WriteLine("--------------------\n");
        }

        internal static void ClearConsole() => Console.Clear();
    }
}
