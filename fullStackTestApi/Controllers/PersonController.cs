using fullStackTestApi.Models;
using fullStackTestApi.Services;
using Microsoft.AspNetCore.Mvc;
using fullStackTestApi.Helpers;

namespace fullStackTestApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonController : ControllerBase
{
    private readonly ILogger<PersonController> _logger;
    private readonly PersonService _namesService;
    private readonly IHelper _helper;

    public PersonController(ILogger<PersonController> logger, PersonService namesService, IHelper helper)
    {
        _logger = logger;
        _namesService = namesService;
        _helper = helper;
    }

    [HttpGet(Name = "GetAllPersons")]
    public async Task<List<Person>> Get() => await _namesService.GetAsync();

    [HttpGet("{id}", Name = "GetOnePerson")]

    public async Task<Person?> Get(string id) => await _namesService.GetAsync(id);

    [HttpPost(Name = "CreatePerson")]
    public async Task<IActionResult> Post (Person newPerson)
    {
        await _namesService.CreateAsync(newPerson);
        return CreatedAtAction(nameof(Get), new { id = newPerson.Id }, newPerson);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Person updatedPerson)
    {
        var person = await _namesService.GetAsync(id);

        if (person is null)
        {
            return NotFound();
        }

        updatedPerson.Id = person.Id;

        await _namesService.UpdateAsync(id, updatedPerson);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var name = await _namesService.GetAsync(id);

        if (name is null)
        {
            return NotFound();
        }

        await _namesService.RemoveAsync(id);

        return NoContent();
    }
}

