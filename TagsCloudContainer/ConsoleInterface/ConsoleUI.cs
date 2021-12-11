using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer;

public class ConsoleUI : IUI
{
    private Dictionary<string, IUIAction> actions;

    public ConsoleUI(IUIAction[] uiActions)
    {
        actions = new Dictionary<string, IUIAction>();
        for (var i = 1; i <= uiActions.Length; i++)
            actions.Add(i.ToString(), uiActions[i - 1]);
    }

    public void Run()
    {
        while (true)
        {
            foreach (var (key, action) in actions.OrderBy(pair => pair.Key))
                Console.WriteLine($"{key} - {action.GetDescription()}");
            Console.WriteLine($"{actions.Count + 1} - Exit");
            var answer = Console.ReadLine() ?? "";
            Console.WriteLine();
            Console.WriteLine();
            if (!actions.ContainsKey(answer))
                return;
            actions[answer].Handle();
        }
    }
}
