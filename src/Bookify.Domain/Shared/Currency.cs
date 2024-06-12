namespace Bookify.Domain;

public record Currency
{
    internal static readonly Currency None = new("");
    public static readonly Currency Usd = new("USD");
    public static readonly Currency Eur = new("EUR");
    public static readonly Currency Ngn = new("NGN");
    private Currency(string code) => Code = code;
    public string Code { get; init; }
    public static readonly IReadOnlyCollection<Currency> All = new[]{
        Usd,Eur,Ngn
    };
    public static Currency FromCode(string code)
    {
        return All.FirstOrDefault(c => c.Code == code) ?? throw new ApplicationException("Currency is invalid");
    }
}
