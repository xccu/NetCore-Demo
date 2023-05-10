using DataAccess;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace RazorPage.Web.Pages.Users;

public class CreateModel : PageModel
{
    private readonly UserDbContext _context;

    public CreateModel(UserDbContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public User User { get; set; } = default!;
    

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
      if (!ModelState.IsValid || _context.User == null || User == null)
        {
            return Page();
        }

        _context.User.Add(User);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
