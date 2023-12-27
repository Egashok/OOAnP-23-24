using Hwdtech;

namespace SpaceBattle.Lib
{
    public class HandlerException : IHandler
    {
        public ICommand SearchHandler(params Type[] args)
        {
            //Получаем типы для поиска обработчика
            var commandType = args[0];
            var exceptionType = args[1];
            //Дерево с типом комманды и с объектом (обработчик или <тип ошибки,обработчик> )
            var tree = IoC.Resolve<Dictionary<Type, object>>("Commands.Tree");

            // Пытаемся получить либо словарь <ошибка,обработчик> либо обработчик
            var subtree = tree.GetValueOrDefault(commandType, tree.GetValueOrDefault(exceptionType, IoC.Resolve<ICommand>("Commands.Handler")));

            //если получен только обработчик
            if (subtree.GetType() != typeof(Dictionary<Type, object>))
            {
                //преобразуем обработчик в айкомманд и возвращаем его
                var handler = (ICommand)subtree;
                return handler;
            }
            // если получен словарь из ошибок и обработчиков
            else
            {
                //берем словарь ошибок и обработчиков
                var exTree = (Dictionary<Type, object>)subtree;
                //если нет обработчика для ошибок то получаем дефолтный для команды из айока
                var handler = (ICommand)exTree.GetValueOrDefault(exceptionType, IoC.Resolve<ICommand>("Commands.Handler", commandType));
                return handler;
            }
        }
    }
}
