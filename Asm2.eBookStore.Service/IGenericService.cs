using Asm2.eBookStore.EntityModel;
using Microsoft.EntityFrameworkCore;

namespace Asm2.eBookStore.Service;

public interface IGenericService<T> where T : GenericEntity
{
    public DbSet<T> Get();
    public T? GetById(int id);
    public T Add(T author);
    public T Update(T entity);
    public void DeleteById(int id);
}