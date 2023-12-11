using Microsoft.AspNetCore.Mvc;

namespace WebApi.Demo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ValuesController : ControllerBase
{
    private List<ValueDto> _values = default;

    public ValuesController()
    {
        _values = new()
        {
            new (){ Id = 1, Name = "Name1", Value="Value1" },
            new (){ Id = 2, Name = "Name2", Value="Value2" }
        };
    }

    // GET api/values
    [HttpGet]
    public ActionResult<IEnumerable<ValueDto>> Get()
    {
        this.HttpContext.Request.Headers.TryGetValue("Cache-Control", out var v);
        Console.WriteLine(v);
        this.HttpContext.Response.Headers.Append("API-VALUE-TEST", "Get Value");
        
        return _values;
    }

    // GET api/values/5
    [HttpGet("{id}")]
    public ActionResult<string> Get(int id)
    {

        return _values.FirstOrDefault(t=>t.Id==id)?.Value ?? "value";
    }

    [HttpGet("name/{name}")]
    public ActionResult<string> GetByName(string name)
    {
        return _values.FirstOrDefault(t => t.Name == name)?.Value ?? "value";
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
        return Ok(new ValueDto() { Id = id, Name = value, Value = value });
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
