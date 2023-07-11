namespace basic.Data;
public class CommonRepository : ICommonRepository
{
  private readonly ApplicationDbContext _context;
  public CommonRepository(ApplicationDbContext context)
  {
    _context = context;
  }
  public async void AddEntity<T>(T entityToAdd) where T : class
  {
    try
    {
      await _context.AddAsync(entityToAdd);

    }
    catch (Exception ex)
    {
      throw new Exception($"Couldn't add entity: {ex.Message}");
    }
  }

  public void DeleteEntity<T>(T entityToDelete) where T : class
  {
    try
    {
      _context.Remove(entityToDelete);
    }
    catch (Exception ex)
    {
      throw new Exception($"Couldn't delete entity: {ex.Message}");
    }
  }

  public async Task<bool> SaveChangesAsync()
  {
    try
    {
      return await _context.SaveChangesAsync() > 0;
    }
    catch (Exception ex)
    {
      throw new Exception($"Couldn't save changes: {ex.Message}");
    }
  }

}