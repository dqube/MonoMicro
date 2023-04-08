using Micro.Modules.Orders.Core.Orders.Exceptions;

namespace Micro.Modules.Orders.Core.Orders.ValueObjects
{
    internal record struct TransferName
    {
        public string Value { get; set; }

        public TransferName(string value)
        {


            if (value.Length > 100)
            {
                throw new InvalidTransferNameException(value);
            }

            Value = value.Trim().Replace(" ", "_");
        }

        public static implicit operator TransferName(string value) => new(value);
        public static implicit operator string(TransferName id) => id.Value;
    }
    public record Tag(string Value)
    {
        public string Value { get; } = Value ?? throw new InvalidTransferNameException(name: Value);

        public static implicit operator Tag(string value) => new(value);
        public static implicit operator string(Tag tag) => tag.Value;
    }
}