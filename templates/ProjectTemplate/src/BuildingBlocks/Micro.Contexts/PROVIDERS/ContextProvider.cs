using System.Diagnostics;
using $safeprojectname$.Accessors;
using Microsoft.AspNetCore.Http;

namespace $safeprojectname$.Providers;

internal sealed class ContextProvider : IContextProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IContextAccessor _contextAccessor;

    public ContextProvider(IHttpContextAccessor httpContextAccessor, IContextAccessor contextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        _contextAccessor = contextAccessor;
    }

    public IContext Current()
    {
        if (_contextAccessor.Context is not null)
        {
            return _contextAccessor.Context;
        }

        IContext context;
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext is not null)
        {
            var userId = httpContext.User.Identity?.Name;
            context = new Context(Activity.Current?.Id ?? ActivityTraceId.CreateRandom().ToString(), userId);
        }
        else
        {
            context = new Context(Activity.Current?.Id ?? ActivityTraceId.CreateRandom().ToString());
        }

        _contextAccessor.Context = context;

        return context;
    }
}