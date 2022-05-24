using EmailPrototypeServer.Models;
using EmailPrototypeServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmailPrototypeServer.RestControllers;

[ApiController]
[Route("api/email")]
public class EmailController : Controller
{
    private EmailService _service;
    private readonly ILogger<EmailController> _logger;
    public EmailController(ILogger<EmailController> logger, EmailService service)
    {
        _service = service;
        this._logger = logger;
    }

    [HttpGet]
    public ActionResult<List<Email>> GetAll() => _service.GetAll();

    [HttpGet("{id}")]
    public ActionResult<Email> Get(string id)
    {
        var email = _service.Get(id);
        if (email == null) return NotFound();
        return email;
    }

    [HttpPost]
    public IActionResult Create([FromBody] Email emailT)
    {
        _service.Create(emailT);
        return CreatedAtAction(nameof(Create), new { id = emailT.Id }, emailT);
    }

    [HttpPut("{id}")]
    public IActionResult Update(string id, [FromBody] Email email)
    {
        if (id != email.Id)
            return BadRequest();
        _service.Update(id, email);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
        var email = _service.Get(id);

        if (email is null)
            return NotFound();

        _service.Delete(id);

        return NoContent();
    }
}