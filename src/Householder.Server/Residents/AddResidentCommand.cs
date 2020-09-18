using CQRS.Command.Abstractions;

namespace Householder.Server.Residents
{
    public class AddResidentCommand
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}