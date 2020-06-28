using System.Threading.Tasks;

namespace Aloha.Types
{
    public interface IInitializer
    {
        Task InitializeAsync();
    }
}
