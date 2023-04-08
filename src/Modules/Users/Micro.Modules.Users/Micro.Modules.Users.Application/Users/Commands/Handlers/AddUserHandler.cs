using Micro.Abstractions.Handlers;
using Micro.Modules.Users.Core.Users.Entities;
using Micro.Modules.Users.Core.Users.Repositories;
using Microsoft.Extensions.Logging;

namespace Micro.Modules.Users.Application.Users.Handlers
{
    internal sealed class AddUserHandler : ICommandHandler<AddUser>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<AddUserHandler> _logger;
        public AddUserHandler(IUserRepository userRepository, ILogger<AddUserHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }



        public async Task HandleAsync(AddUser command, CancellationToken cancellationToken = default)
        {
            var user = User.Create(
               command.userId,
               command.Name
               );
            await _userRepository.AddAsync(user);
            _logger.LogInformation($"User {command.userId} added sucessfully'.");
            //throw new NotImplementedException();
        }
    }
}