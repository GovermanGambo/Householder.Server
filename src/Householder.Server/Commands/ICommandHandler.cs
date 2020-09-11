using System.Threading.Tasks;

namespace Householder.Server.Commands
{
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        Task Handle(TCommand command);
    }
}