namespace Bookify.Application;

public record SearchApartmentsQuery(DateOnly StartDate, DateOnly EndDate) : IQuery<IReadOnlyList<ApartmentResponse>>;
