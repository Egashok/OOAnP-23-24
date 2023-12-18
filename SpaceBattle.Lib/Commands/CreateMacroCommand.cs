namespace SpaceBattle.Lib;
using Hwdtech;

public class CreateMacroCommand
{
    private readonly string _dependencyName;

    public CreateMacroCommand(string dependencyName)
    {
        _dependencyName = dependencyName;
    }

    public ICommand[] CreateCommand()
    {
        var cmds = new List<ICommand>();
        var cmdNames = IoC.Resolve<string[]>(_dependencyName);

        cmdNames.ToList().ForEach(cmd_name =>
        {
            cmds.Add(IoC.Resolve<ICommand>(cmd_name));
        });

        return cmds.ToArray();
    }
}