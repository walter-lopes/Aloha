namespace Aloha.CQRS.Commands
{
    public interface ICommand
    {
        bool IsValid();
    }
}
