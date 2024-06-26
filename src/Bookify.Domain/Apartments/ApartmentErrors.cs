namespace Bookify.Domain;

public class ApartmentErrors
{
    public static Error NotFound = new
       ("Property.Found",
        "The property with the specified identifier was not found");
}
