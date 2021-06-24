using System.Threading.Tasks;

namespace Joker.Mvc.Initializers
{
    public interface IInitializer
    {
        Task InitializeAsync();
    }
}