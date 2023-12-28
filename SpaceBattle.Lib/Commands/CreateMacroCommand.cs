using Hwdtech;

public class CreateMacroCommand
{
    private readonly string _commandsNames;

    public CreateMacroCommand(string commandsNames)
    {
        _commandsNames = commandsNames;
    }

    public ICommand[] SeparationCommand()
    {
        var commandList = new List<ICommand>();
        var commandArrayNames = IoC.Resolve<string[]>(_commandsNames);

        foreach (var commandName in commandArrayNames)
        {
            commandList.Add(IoC.Resolve<ICommand>(commandName));
        }

        return commandList.ToArray();
    }
}
