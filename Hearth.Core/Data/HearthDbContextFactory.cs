// HearthDbContextFactory.cs
// Used when running 'ef migration' commands. Not sure why exactly, but this is needed for EFC to properly assigned itself to things.
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Hearth.Core.Data;

public class HearthDbContextFactory : IDesignTimeDbContextFactory<HearthDbContext>
{
    public HearthDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<HearthDbContext>();

        // Path/connection string here doesn't need to be "real" —
        // it's only used to generate migration files, never actually opened.
        optionsBuilder.UseSqlite("Data Source=design_time_placeholder.db");

        return new HearthDbContext(optionsBuilder.Options);
    }
}