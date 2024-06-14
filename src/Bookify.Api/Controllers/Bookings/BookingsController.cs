using Bookify.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.Api;

[ApiController]
[Route("api/bookings")]
public class BookingsController : ControllerBase
{
    private readonly ISender _sender;

    public BookingsController(ISender sender)
    {
        _sender = sender;
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookings(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetBookingQuery(id);

        var result = await _sender.Send(query);
        return result.IsSuccess ? Ok(result.Value) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> ReserveBooking(ReserveBookingRequest request, CancellationToken cancellationToken)
    {
        var command = new ReservedBookingCommand(request.ApartmentId, request.UserId, request.StartDate,
        request.EndDate);

        var result = await _sender.Send(command);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        return CreatedAtAction(nameof(GetBookings), new { id = result.Value }, result.Value);
    }
}
