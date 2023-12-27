using Hwdtech;

namespace SpaceBattle.Lib
{
    public class HandlerException:IHandler
    {
        private  Type _commandType;
        private  Type _exceptionType;

        public ICommand SearchHandler(params Type[] args)
        {
        //Получаем типы для поиска обработчика
            _commandType =  args[0];
            _exceptionType = args[1];
            //Дерево с типом комманды и с объектом (обработчик или <тип ошибки,обработчик> )
            var tree = IoC.Resolve<Dictionary<Type, object>>("Commands.Tree");

            // Пытаемся получить обработчик по комманде либо словарь <ошибка,обработчик> либо обработчик
            var subtree = tree.GetValueOrDefault(_commandType, tree.GetValueOrDefault(_exceptionType, IoC.Resolve<ICommand>("Commands.Handler")));

            //если получен только обработчик
            if (subtree.GetType() != typeof(Dictionary<Type, object>))
            {
                //преобразуем обработчик в айкомманд и возвращаем его
                var handler = (ICommand) subtree;
                return handler;
            }   
            // если получен словарь из ошибок и обработчиков
            else
            {
                var exTree = (Dictionary<Type, object>)subtree;
                var handler = (ICommand)exTree.GetValueOrDefault(_exceptionType, IoC.Resolve<ICommand>("Commands.Handler", _commandType));
                return handler;
            }
        }
    }
}