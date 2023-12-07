using Microsoft.AspNetCore.Mvc;

namespace WebApi.Demo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ValuesController : ControllerBase
{
    // GET api/values
    [HttpGet]
    public ActionResult<IEnumerable<ValueDto>> Get()
    {
        var values = new List<ValueDto>()
        {
            new (){ Id = 1, Name = "Name1", Value="Value1" },
            new (){ Id = 2, Name = "Name2", Value="Value2" }
        };
        return values;
    }

    // GET api/values/5
    [HttpGet("{id}")]
    public ActionResult<string> Get(int id)
    {
        return "value";
    }

    // POST api/values
    [HttpPost]
    public IActionResult Post([FromBody]
            [Bind(nameof(ValueDto.Name), nameof(ValueDto.Value))]
            ValueDto dto)
    {

        Console.WriteLine($"{dto.Name}-{dto.Value}");
        return Ok(dto);
    }

    // PUT api/values/5
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] string value)
    {
        return Ok();
    }

    // DELETE api/values/5
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        return Ok();
    }

    // GET api/values/error
    [HttpGet("error")]
    public void Error()
    {
        throw new Exception("Error occured");
    }
}
