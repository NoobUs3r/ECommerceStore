using SharedService.Interfaces;

namespace InfrastructureService.Services;

public class InMemoryDatabase<T> : IDatabase<T> where T : IDatabaseEntity
{
    private readonly List<T> _dataStore = [];

    public async Task<long> Create(T item)
    {
        _dataStore.Add(item);
        Console.WriteLine("Item added.");
        return _dataStore.Count - 1;
    }

    public async Task<T> Read(long id)
    {
        var item = _dataStore.FirstOrDefault(x => x.Id == id);
        if (item == null)
        {
            Console.WriteLine("Item not found.");
            return default(T);
        }
        return item;
    }

    public async Task Update(T item) // TODO: doesn't make sense
    {
        var index = _dataStore.FindIndex(x => x.Id == item.Id);
        if (index != -1)
        {
            _dataStore[index] = item;
            Console.WriteLine("Item updated.");
        }
        else
        {
            Console.WriteLine("Item not found for update.");
        }
    }

    public async Task Delete(long id)
    {
        var item = _dataStore.FirstOrDefault(x => x.Id == id);
        if (item != null)
        {
            _dataStore.Remove(item);
            Console.WriteLine("Item deleted.");
        }
        else
        {
            Console.WriteLine("Item not found for deletion.");
        }
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return _dataStore;
    }
}
