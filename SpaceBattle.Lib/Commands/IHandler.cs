namespace SpaceBattle.Lib;

public interface IHandler
{
    ICommand SearchHandler(params Type[] args);
}
