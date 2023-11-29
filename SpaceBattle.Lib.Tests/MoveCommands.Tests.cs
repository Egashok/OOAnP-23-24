using Moq;

namespace SpaceBattle.Lib.Tests;

public class MoveCommandTest
{

    [Fact]
    public void MoveCommandPositive()
    {
        var movable = new Mock<IMovable>();

        movable.SetupGet(m => m.Position).Returns(new Vector(12, 5)).Verifiable();
        movable.SetupGet(m => m.Velocity).Returns(new Vector(-7, 3)).Verifiable();

        ICommand moveCommand = new MoveCommand(movable.Object);

        moveCommand.Execute();

        movable.VerifySet(m => m.Position = new Vector(5, 8), Times.Once);
        movable.VerifyAll();
    }
    [Fact]

    public void MoveCommandNonePositon()
    {
        var movable = new Mock<IMovable>();

        movable.SetupGet(m => m.Position).Throws(new Exception("None position exception"));
        movable.SetupGet(m => m.Velocity).Returns(new Vector(-5, 3)).Verifiable();

    }

    [Fact]
    public void MoveCommandNoneVelocity()
    {
        var movable = new Mock<IMovable>();
        movable.SetupGet(m => m.Velocity).Throws(new Exception("None Velocity exception"));
        movable.SetupGet(m => m.Position).Returns(new Vector(12, 5)).Verifiable();
    }

    [Fact]

    public void MoveCommandUnreal()
    {
        var movable = new Mock<IMovable>();

        movable.SetupGet(m => m.Position).Returns(new Vector(new int[] { 1, 4 })).Verifiable();
        movable.SetupGet(m => m.Velocity).Returns(new Vector(new int[] { 1, 2 })).Verifiable();
        movable.SetupSet(m => m.Position = It.IsAny<Vector>()).Throws(new NotImplementedException());

        ICommand moveCommand = new MoveCommand(movable.Object);

        Assert.Throws<NotImplementedException>(moveCommand.Execute);
        movable.VerifyAll();
    }
}
