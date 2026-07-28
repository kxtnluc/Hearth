using Hearth.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace Hearth.Services.Data;

internal class DbInitializer : IDbInitializer
{
    private readonly HearthDbContext _context;

    public DbInitializer(HearthDbContext context)
    {
        _context = context;
    }

    public void Initialize()
    {
        _context.Database.Migrate();
    }
}
// stopped here Hearth.Services/Data/DbInitializer.cs with claude