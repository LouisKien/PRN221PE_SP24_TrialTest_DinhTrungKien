using System.Linq.Expressions;

namespace PRN221PE_SP24_TrialTest_DinhTrungKien.Repo.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "", int? pageIndex = null, int? pageSize = null);

        TEntity GetByID(object id);

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> expression);

        void Insert(TEntity entity);

        void Delete(object id);

        void Delete(TEntity entityToDelete);

        void Update(TEntity entityToUpdate);

        public int Count(Expression<Func<TEntity, bool>> filter = null);
    }
}
