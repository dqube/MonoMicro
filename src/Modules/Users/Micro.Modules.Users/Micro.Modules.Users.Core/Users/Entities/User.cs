using Micro.Abstractions.Kernel.Types;
using Micro.Modules.Users.Core.Users.ValueObjects;

namespace Micro.Modules.Users.Core.Users.Entities
{
    internal class User : Aggregate<UserId>
    {
        private User(UserId id, string name) : base(id)
        {
            Name = name;
            Id = id;
        }


        public string Name { get; private set; }
        public UserId Id { get; private set; }

        public static User Create(UserId userId, string name)
        {
            var user = new User(userId, name);
            return user;
        }
    }
}