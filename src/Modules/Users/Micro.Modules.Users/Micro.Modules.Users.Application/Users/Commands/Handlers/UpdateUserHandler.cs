using Micro.Abstractions.Handlers;
using Micro.Modules.Users.Core.Users.Entities;
using Micro.Modules.Users.Core.Users.Repositories;
using Microsoft.Extensions.Logging;

namespace Micro.Modules.Users.Application.Users.Handlers
{
    internal sealed class UpdateUserHandler : ICommandHandler<UpdateUser>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<AddUserHandler> _logger;
        public UpdateUserHandler(IUserRepository userRepository, ILogger<AddUserHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }
        public async Task HandleAsync(UpdateUser command, CancellationToken cancellationToken = default)
        {
            var user = User.Create(
               command.userId,
               command.Name
               );
            await _userRepository.UpdateAsync(user);
            _logger.LogInformation($"User {command.userId} updated sucessfully'.");
        }
    }
}