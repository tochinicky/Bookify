﻿namespace Bookify.Domain;

public record Money(decimal Amount, Currency Currency)
{
    public static Money operator +(Money first, Money second)
    {
        if (first.Currency != second.Currency)
        {
            throw new InvalidOperationException("Currency have to be equal");
        }
        return new Money(first.Amount + second.Amount, first.Currency);
    }
    public static Money Zero() => new Money(0, Currency.None);

    public bool IsZero() => this == Zero();

    public static Money Zero(Currency currency) => new Money(0, currency);
}
