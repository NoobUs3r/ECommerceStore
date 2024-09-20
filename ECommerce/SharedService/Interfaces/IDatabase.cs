namespace SharedService.Interfaces;

public interface IDatabase<T>
{
    Task<long> Create(T item);

    Task<T> Read(long id);

    Task Update(T item);

    Task Delete(long id);

    Task<IEnumerable<T>> GetAll();
}
