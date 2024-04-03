using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.CheckIns.DoCheckin;
using PassIn.Communication.Responses;

namespace PassIn.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckInController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
        [Route("{attendeeId}")]
        public IActionResult CheckIn([FromRoute] Guid attendeeId)
        {
            var useCase = new DoAttendeeCheckInUseCase();

            var response = useCase.Execute(attendeeId);

            return Created(string.Empty, response);
        }
    }
}
