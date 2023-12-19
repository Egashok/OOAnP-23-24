using Hwdtech;
public class MacroCommand : ICommand
{
    private readonly IList<ICommand> _commands;

    public MacroCommand(IList<ICommand> commands)
    {

        _commands = commands;
    }
    public void Execute() => _commands.ToList().ForEach(
        command => command.Execute()
    );
}
