
namespace Bookify.Application.Bookings.CompleteBooking;

public record CompleteBookingCommand(Guid BookingId) : ICommand;