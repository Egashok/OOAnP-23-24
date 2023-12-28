using Hwdtech;
using Hwdtech.Ioc;
using Moq;

namespace SpaceBattle.Lib.Tests
{
    public class HandlerExceptionTests
    {
        public HandlerExceptionTests()
        {
            var defaultHandler = new Mock<ICommand>();
            new InitScopeBasedIoCImplementationCommand().Execute();
            IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
            IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Commands.Handler", (object[] args) => { return defaultHandler.Object; }).Execute();
        }

        [Fact]
        public void PositiveHandlerException()
        {
            var testTree = new Dictionary<Type, object>();
            var exTree = new Dictionary<Type, object>();

            var mockHandler = new Mock<ICommand>();
            mockHandler.Setup(mh => mh.Execute()).Callback(() => { }).Verifiable();

            var mockCommand = new Mock<ICommand>();
            var mockException = new Mock<Exception>();

            exTree.Add(mockException.Object.GetType(), mockHandler.Object);
            testTree.Add(mockCommand.Object.GetType(), exTree);

            IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Commands.Tree", (object[] args) => testTree).Execute();

            var handlerException = new HandlerException();

            var handler = handlerException.SearchHandler(mockCommand.Object.GetType(), mockException.Object.GetType());

            handler.Execute();

            mockHandler.Verify(mc => mc.Execute(), Times.Once());
        }
        [Fact]
        public void NegativeHandlerException()
        {
            var mockHandler = new Mock<ICommand>();
            var mockCommand = new Mock<ICommand>();
            var mockException = new Mock<Exception>();

            var testTree = new Dictionary<Type, object>();

            testTree.Add(mockException.Object.GetType(), mockHandler.Object);

            IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Commands.Tree", (object[] args) => testTree).Execute();

            var handlerException = new HandlerException();

            Assert.Equal(mockHandler.Object, handlerException.SearchHandler(mockCommand.Object.GetType(), mockException.Object.GetType()));
        }
    }
}
