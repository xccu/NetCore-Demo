using Blazor_Query_Demo.Context;
using Blazor_Query_Demo.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace Blazor_Query_Demo.Pages
{
    public partial class Index
    {
        [Inject]
        IDbContextFactory<PropertyDbContext> DbContextFactory { get; set; }

        private IQueryable<Property> Properties { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var context = DbContextFactory.CreateDbContext();
            Properties = context.Properties;
        }
    }
}
