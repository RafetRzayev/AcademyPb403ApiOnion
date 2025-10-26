using Microsoft.EntityFrameworkCore;

namespace Academy.Infrastructure.DataContext;

public class DataInitializer
{
    private readonly AppDbContext _dbContext;

    public DataInitializer(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task UpdateDatabase()
    {
        await _dbContext.Database.MigrateAsync();
    }
}
