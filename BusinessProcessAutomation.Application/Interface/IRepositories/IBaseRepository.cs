using System.Linq.Expressions;

namespace BusinessProcessAutomation.Application.Interface.IRepositories
{
    public interface IBaseRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T GetById(int id);
        void Create (T entity);
        void Update (T entity);
        void Delete (T entity);
        void SaveChanges();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
    }
}
