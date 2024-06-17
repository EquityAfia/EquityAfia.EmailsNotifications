
using EquityAfia.EmailsNotifications.Application.SendEmail.Command;
using EquityAfia.EmailsNotifications.Domain.EmailRequestAndResponse;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EquityAfia.EmailsNotifications.Api.Controllers
{
    [ApiController]
    [Route("Email-Notifications")]
    public class EmailController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmailController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Send-Email")]
        public async Task<IActionResult> SendEmail([FromBody] SendEmailRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new SendEmailCommand(request);
            var response = await _mediator.Send(command);

            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }
    }
}
