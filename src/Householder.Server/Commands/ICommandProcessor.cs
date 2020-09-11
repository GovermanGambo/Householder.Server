using System.Threading.Tasks;

namespace Householder.Server.Commands
{
    public interface ICommandProcessor
    {
        Task ProcessAsync(ICommand command);
    }
}