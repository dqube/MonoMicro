using System.Threading.Tasks;

namespace Micro.Abstractions.Initializer;

public interface IInitializer
{
    Task InitAsync();
}