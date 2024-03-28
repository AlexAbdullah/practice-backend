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

    [HttpGet(Name = "GetName")]
    public async Task<List<Person>> Get() => await _namesService.GetAsync();

    [HttpGet("{name}", Name = "GetOneName")]

    public async Task<string> Get(string name)
    {
        var person = await _namesService.GetAsync(name);

        if (person is not null) return person.PersonName.ToLower();
        return "No one is here bro";
    }

    [HttpPost(Name = "PostName")]
    public async Task<IActionResult> Post (string newName)
    {
        var newId = _helper.GenerateRandomNumber(24);

        var nameToBeAdded = new Person
        {
            PersonName = newName,
            Id = newId,
            Quote = "Hello World"
        };

        await _namesService.CreateAsync(nameToBeAdded);
        return CreatedAtAction(nameof(Get), new { id = nameToBeAdded.Id }, nameToBeAdded);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Person updatedName)
    {
        var name = await _namesService.GetAsync(id);

        if (name is null)
        {
            return NotFound();
        }

        updatedName.Id = name.Id;

        await _namesService.UpdateAsync(id, updatedName);

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

