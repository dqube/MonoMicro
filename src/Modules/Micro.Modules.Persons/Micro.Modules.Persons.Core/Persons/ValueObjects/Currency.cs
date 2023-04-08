﻿using Micro.Modules.Persons.Core.Persons.Exceptions;

namespace Micro.Modules.Persons.Core.Persons.ValueObjects
{
    internal record Currency
    {
        private static readonly HashSet<string> AllowedValues = new()
    {
        "PLN", "EUR", "GBP"
    };

        public string Value { get; }

        public Currency(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length != 3)
            {
                throw new InvalidCurrencyException(value);
            }

            value = value.ToUpperInvariant();
            if (!AllowedValues.Contains(value))
            {
                throw new UnsupportedCurrencyException(value);
            }
            Value = value;
        }

        public static implicit operator Currency(string value) => new(value);
        public static implicit operator string(Currency id) => id.Value;
    }
}