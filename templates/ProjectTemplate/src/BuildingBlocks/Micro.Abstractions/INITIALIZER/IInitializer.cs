using System.Threading.Tasks;

namespace $safeprojectname$.Initializer;

public interface IInitializer
{
    Task InitAsync();
}