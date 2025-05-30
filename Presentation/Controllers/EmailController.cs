using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using Presentation.Services;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmailController(ICommunicationService communicationService) : ControllerBase
{

    private readonly ICommunicationService _communicationService = communicationService;

    [HttpPost]
    public async Task<IActionResult> SendEmail([FromBody] EmailRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest("Invalid request format.");

        var result = await _communicationService.SendEmailAsync(
             request.Email,
             request.Name,
             request.EventName
            );

        if (result.Success)
            return Ok("Email sent successfully");
        else
            return StatusCode(500, result.Error ?? "Failed to send email.");
    }
}
