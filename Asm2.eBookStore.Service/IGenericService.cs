using Asm2.eBookStore.EntityModel;
using Microsoft.EntityFrameworkCore;

namespace Asm2.eBookStore.Service;

public interface IGenericService<T>
    where T : GenericEntity
{
    public DbSet<T> Get();
    public T? GetById(int id);
    public Task<T> Add(T entity);
    public Task<T> Update(T entity);
    public Task DeleteById(int id);
}
