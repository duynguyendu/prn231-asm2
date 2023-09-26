using Asm2.eBookStore.EntityModel;
using Microsoft.EntityFrameworkCore;

namespace Asm2.eBookStore.Repository;

public abstract class GenericRepository<T> where T : GenericEntity
{
    protected readonly EBookStoreDbContext context;

    protected GenericRepository(EBookStoreDbContext context)
    {
        this.context = context;
    }

    public DbSet<T> GetByOdata()
    {
        return context.Set<T>();
    }

    public T? GetById(int id)
    {
        return context.Set<T>().FirstOrDefault(x => x.Id == id);
    }

    public void Add(T entity)
    {
        context.Set<T>().Add(entity);
    }

    public void Update(T entity)
    {
        context.Set<T>().Update(entity);
    }

    public void Delete(int id)
    {
        var entity = context.Set<T>().SingleOrDefault(x => x.Id == id);
        if (entity != null)
        {
            context.Set<T>().Remove(entity);
        }
    }
}