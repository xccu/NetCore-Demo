using Ant_Table_Demo.DataContext;
using Ant_Table_Demo.DataContext.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace Ant_Table_Demo.Pages
{
    public partial class Index
    {
        [Inject]
        IDbContextFactory<PropertyDbContext> DbContextFactory { get; set; }

        private PropertyDbContext DbContext;

        private IQueryable<Property> Properties { get; set; }

        protected override async Task OnInitializedAsync()
        {
            DbContext = DbContextFactory.CreateDbContext();
        }

        public void InitialData()
        {
            if (DbContext.Properties.Count() > 0)
                return;
            DbContext.Properties.Add(new Property() { Id = 1, Address = "Elizabeth Rd", Suburb = "Sydney", Postcode = "2000", State = "NSW" });
            DbContext.Properties.Add(new Property() { Id = 2, Address = "Queen St", Suburb = "Rosebery", Postcode = "2018", State = "NSW" });
            DbContext.Properties.Add(new Property() { Id = 3, Address = "Queen St", Suburb = "Chatswood", Postcode = "2067", State = "NSW" });
            DbContext.Properties.Add(new Property() { Id = 4, Address = "Ravenswood Ave", Suburb = "Carlingford", Postcode = "2118", State = "NSW" });
            DbContext.Properties.Add(new Property() { Id = 5, Address = "Ravenswood Ave", Suburb = "Epping", Postcode = "2121", State = "NSW" });
            DbContext.SaveChanges();
        }
    }
}
