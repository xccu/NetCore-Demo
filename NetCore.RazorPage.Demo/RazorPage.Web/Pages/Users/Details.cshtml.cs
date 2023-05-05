﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;


namespace RazorPage.Web.Pages.Users;

public class DetailsModel : PageModel
{
    private readonly UserDbContext _context;

    public DetailsModel(UserDbContext context)
    {
        _context = context;
    }

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
}
