namespace SpaceBattle.Lib.Test;
using Hwdtech;
using Hwdtech.Ioc;
using Moq;

public class CollisionTreeBuildTest
{
    public CollisionTreeBuildTest()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();

        IoC.Resolve<ICommand>("Scopes.Current.Set",
            IoC.Resolve<object>("Scopes.New",
                IoC.Resolve<object>("Scopes.Root"))
        ).Execute();
    }
    [Fact]
    public void NegativeBuildCollisionTreeNoneFile()
    {

        var mockCollisionTreeBuild = new CollisionTreeBuilder();

        IoC.Resolve<ICommand>("IoC.Register",
            "Collision.BuildTree", (object[] args) => mockCollisionTreeBuild).Execute();

        var TestCollisionTreeCommand = new BuildCollision("none");

        Assert.Throws<FileNotFoundException>(() => TestCollisionTreeCommand.Execute());
    }

    [Fact]
    public void PositiveBuildCollisionTree()
    {
        var tree = new Dictionary<int, object>();
        IoC.Resolve<ICommand>("IoC.Register",
            "Collision.CollisionTree", (object[] args) => tree).Execute();

        var mockCollisionTreeBuild = new CollisionTreeBuilder();
        mockCollisionTreeBuild.BuildArrayFile("../../../Data.txt");

        Assert.Equal(2, IoC.Resolve<IDictionary<int, object>>("Collision.CollisionTree").Count);
        Assert.Equal(2, ((IDictionary<int, object>)IoC.Resolve<IDictionary<int, object>>("Collision.CollisionTree")[1]).Count);
    }
    [Fact]
    public void PositiveReadBuildCollisionTree()
    {
        var mockCollisionTreeBuild = new Mock<ICollisionTreeBuild>();
        mockCollisionTreeBuild.Setup(x => x.BuildArrayFile(It.IsAny<string>())).Verifiable();

        IoC.Resolve<ICommand>("IoC.Register",
            "Collision.BuildTree", (object[] args) => mockCollisionTreeBuild.Object).Execute();

        var TestCollisionTreeCommand = new BuildCollision("any");

        TestCollisionTreeCommand.Execute();

        mockCollisionTreeBuild.Verify(x => x.BuildArrayFile("any"), Times.Once());
    }
}
