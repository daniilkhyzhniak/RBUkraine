using Microsoft.EntityFrameworkCore;
using RBUkraine.DAL.Entities.Base;

namespace RBUkraine.DAL.Extensions
{
    public static class DbSetExtensions
    {
        public static void SoftDelete<TEntity>(this DbSet<TEntity> dbSet, TEntity entity)
            where TEntity : BaseEntity
        {
            entity.IsDeleted = true;
            dbSet.Update(entity);
        }
    }
}
