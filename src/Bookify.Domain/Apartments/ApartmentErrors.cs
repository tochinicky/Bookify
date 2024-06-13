namespace Bookify.Domain;

public class ApartmentErrors
{
    public static Error NotFound = new
       ("Booking.Found", "The booking with the specified identifier was not found");

    public static Error Overlap = new
    ("Booking.Overlap", "The current booking is overlapping with the existing one");

    public static Error NotReserved = new
    ("Booking.NotReserved", "The booking is not pending");

    public static Error NotConfirmed = new
   ("Booking.NotConfirmed", "The booking is not pending");

    public static Error AlreadyStarted = new
   ("Booking.AlreadyStarted", "The booking has already started");
}
