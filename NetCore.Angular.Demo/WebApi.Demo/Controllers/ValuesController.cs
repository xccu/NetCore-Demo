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
            new (){ Name = "Name1", Value="Value1" },
            new (){ Name = "Name2", Value="Value2" }
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

        return Ok(dto);
    }

    // PUT api/values/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/values/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }

    // GET api/values/error
    [HttpGet("error")]
    public void Error()
    {
        throw new Exception("Error occured");
    }
}
