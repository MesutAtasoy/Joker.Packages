using System.Threading.Tasks;

namespace Joker.Shared.Initializers
{
    public interface IInitializer
    {
        Task InitializeAsync();
    }
}
