using Common.Model;
using DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Demo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FooController : ControllerBase
{
    [HttpGet("Get")]
    public IActionResult FooGet()
    {
        return Ok(new Foo());
    }

    [HttpGet("Get/{id}")]
    public IActionResult FooGetParam(int id)
    {
        return Ok(new Foo(id,"test"));
    }

    [HttpPost("Post")]
    public IActionResult FooPost(Foo foo)
    {
        //return CreatedAtAction(nameof(FooGetParam), new { id = foo.Id }, foo);
        return CreatedAtAction(nameof(FooGet), foo);
    }

    [HttpPut("Put")]
    public IActionResult FooPut()
    {
        return Ok(new Foo());
    }

    [HttpDelete("Delete")]
    public IActionResult FooDetete()
    {
        return Ok(new Foo());
    }
}