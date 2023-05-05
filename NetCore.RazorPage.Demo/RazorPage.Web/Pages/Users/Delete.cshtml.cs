﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace RazorPage.Web.Pages.Users;

public class DeleteModel : PageModel
{
    private readonly UserDbContext _context;

    public DeleteModel(UserDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public User User { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(string id)
    {
        if (id == null || _context.User == null)
        {
            return NotFound();
        }

        var user = await _context.User.FirstOrDefaultAsync(m => m.Id == id);

        if (user == null)
        {
            return NotFound();
        }
        else 
        {
            User = user;
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string id)
    {
        if (id == null || _context.User == null)
        {
            return NotFound();
        }
        var user = await _context.User.FindAsync(id);

        if (user != null)
        {
            User = user;
            _context.User.Remove(User);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
