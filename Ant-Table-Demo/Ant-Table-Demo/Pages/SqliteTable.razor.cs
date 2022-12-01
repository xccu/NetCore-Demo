using Ant_Table_Demo.DataContext;
using Ant_Table_Demo.DataContext.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace Ant_Table_Demo.Pages
{
    public partial class SqliteTable : IDisposable
    {
        [Inject]
        IDbContextFactory<PropertyDbContext> DbContextFactory { get; set; }

        private PropertyDbContext DbContext;

        private IQueryable<Property> Properties { get; set; }

        protected override async Task OnInitializedAsync()
        {
            DbContext = DbContextFactory.CreateDbContext();
            Properties = DbContext.Properties;
        }



        public void Dispose()
        {
            DbContext?.Dispose();
        }
    }
}
