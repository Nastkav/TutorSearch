using Infra.Ports;

namespace Infra.DatabaseAdapter;

public class EventRepository : IEventRepository
{
    private readonly ApplicationDbContext _dbContext;

    public EventRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // public async Task AddAsync(Object object)
    // {
    //     await _dbContext.Objects.AddAsync(object);
    //     await _dbContext.SaveChangesAsync();
    // }
    //
    // public async Task DeleteAsync(Guid id)
    // {
    //     var object = _dbContext.Objects.FirstOrDefault(x => x.Id == id);
    //
    //     if (object != null)
    //     {
    //         _dbContext.Objects.Remove(object);
    //         await _dbContext.SaveChangesAsync();
    //     }
    // }
    //
    // public async Task<IEnumerable<Object>> GetAllAsync()
    // {
    //     return await _dbContext.Objects.AsNoTracking().ToListAsync();
    // }
    //
    // public async Task<Object?> GetByIdAsync(Guid id)
    // {
    //     return await _dbContext.Objects.FirstOrDefaultAsync(x => x.Id == id);
    // }
    //
    // public async Task UpdateAsync(Object object)
    // {
    //     _dbContext.Objects.Entry(object).State = EntityState.Modified;
    //     await _dbContext.SaveChangesAsync();
    // }
}