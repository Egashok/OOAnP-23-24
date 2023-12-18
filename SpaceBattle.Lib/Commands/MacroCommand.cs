namespace SpaceBattle.Lib;

public class MacroCommand : ICommand
{
    private ICommand[] _commands;

    public MacroCommand(ICommand[] commands){

        _commands = commands;
    }
    public void Execute() => _commands.ToList().ForEach(
        command => command.Execute()
    );
}