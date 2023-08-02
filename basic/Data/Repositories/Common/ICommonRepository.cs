namespace basic.Data.Repositories.Common;
public interface ICommonRepository
{
  public Task<bool> SaveChangesAsync();
  public void DeleteEntity<T>(T entityToDelete) where T : class;
  public void AddEntity<T>(T entityToAdd) where T : class;
}