using DataAccess;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace RazorPage.Web.Pages.Users;

public class IndexModel : PageModel
{
    private readonly UserDbContext _context;

    public IndexModel(UserDbContext context)
    {
        _context = context;
    }

    public IList<User> User { get;set; } = default!;

    public async Task OnGetAsync()
    {
        if (_context.User != null)
        {
            User = await _context.User.ToListAsync();
        }
    }
}
