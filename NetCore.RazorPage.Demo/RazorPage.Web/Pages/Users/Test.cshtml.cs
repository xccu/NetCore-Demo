using DataAccess;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace RazorPage.Web.Pages.Users;

public class TestModel : PageModel
{

    public IList<User> Users { get; set; } = default!;
    public User User { get; set; } = default!;
    public string Message { get; set; } = string.Empty;

    private readonly UserDbContext _context;

    public TestModel(UserDbContext context)
    {
        _context = context;
    }

    public async Task OnGetAsync()
    {
        Message = "Get used";
        if (_context.User != null)
        {
            Users = await _context.User.ToListAsync();
        }
    }

    public void OnPostMessage()
    {
        Message = "Post used";
        if (_context.User != null)
        {
            Users = _context.User.ToList();
        }
    }

    public void OnPostDetail(string id)
    {
        if (_context.User != null)
        {
            Users = _context.User.ToList();
            User = Users.FirstOrDefault(x => x.Id == id);
        }
    }
}
