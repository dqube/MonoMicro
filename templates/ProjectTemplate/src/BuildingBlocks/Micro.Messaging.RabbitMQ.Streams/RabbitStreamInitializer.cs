using Microsoft.Extensions.Hosting;

namespace $safeprojectname$;

internal sealed class RabbitStreamInitializer : IHostedService
{
    private readonly RabbitStreamManager _streamManager;

    public RabbitStreamInitializer(RabbitStreamManager streamManager) => _streamManager = streamManager;

    public Task StartAsync(CancellationToken cancellationToken) => _streamManager.InitAsync();

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}