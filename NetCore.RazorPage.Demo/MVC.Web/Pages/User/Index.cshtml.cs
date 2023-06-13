using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MVC.Web.Pages.User;

public class IndexModel : PageModel
{
    private readonly UserDbContext _context;

    public IndexModel(UserDbContext context)
    {
        _context = context;
    }

    public IList<DataAccess.Models.User> User { get; set; } = default!;

    public async Task OnGetAsync()
    {
        User = new List<DataAccess.Models.User>();
        //if (_context.User != null)
        //{
        //    User = await _context.User.ToListAsync();
        //}
    }
}
