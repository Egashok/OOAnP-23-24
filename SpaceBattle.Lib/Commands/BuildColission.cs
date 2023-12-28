namespace SpaceBattle.Lib;
using Hwdtech;

public class BuildCollision : ICommand
{
    private readonly string _path;

    public BuildCollision(string path)
    {
        _path = path;
    }

    public void Execute() => IoC.Resolve<ICollisionTreeBuild>("Collision.BuildTree").BuildArrayFile(_path);
}
