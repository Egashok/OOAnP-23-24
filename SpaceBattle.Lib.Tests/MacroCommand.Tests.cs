namespace SpaceBattle.Lib.Test;
using Hwdtech;
using Hwdtech.Ioc;
using Moq;

public class MacroCommandTests
{
    public MacroCommandTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        IoC.Resolve<ICommand>("IoC.Register", "Commands.MoveWithAttack", (object[] args) =>
        {
            return new string[] { "Commands.MoveCommand", "Commands.Attack" };
        }).Execute();
    }

    [Fact]
    public void Positive_MacroCommand()
    {
        var moveCommand = new Mock<ICommand>();
        moveCommand.Setup(mv => mv.Execute()).Callback(() => { }).Verifiable();

        IoC.Resolve<ICommand>("IoC.Register", "Commands.MoveCommand", (object[] args) =>
        {
            return moveCommand.Object;
        }).Execute();

        var attackCommand = new Mock<ICommand>();
        attackCommand.Setup(at => at.Execute()).Callback(() => { }).Verifiable();

        IoC.Resolve<ICommand>("IoC.Register", "Commands.Attack", (object[] args) =>
        {
            return attackCommand.Object;
        }).Execute();

        var commands = new CreateMacroCommand("Commands.MoveWithAttack").SeparationCommand();

        var macroCommand = new MacroCommand(commands);
        macroCommand.Execute();

        moveCommand.Verify(mv => mv.Execute(), Times.Once());
        attackCommand.Verify(at => at.Execute(), Times.Once());
    }
}
