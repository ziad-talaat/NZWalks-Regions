
using System.Linq.Expressions;
namespace REPO.Core.Contract
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(Guid id);
        Task CeateAsync(T item);
        Task UpdateAsync(T item);
        Task<T?> DeleteAsync(Guid id);

        Task<IEnumerable<T>> GetAllAsync(string[] includes = null);
        Task<T?> GetByIdAsync(Expression<Func<T, bool>> match, string[] includes = null);

        Task<IEnumerable<T>> GetFilteredAsync(Expression<Func<T, bool>> critiria ,string[] includes = null);
        Task<IEnumerable<T>> GetFiltered_OrderedAsync(Expression<Func<T, bool>> critiria, Expression<Func<T, object>> orderUsing, string[] includes = null,  bool isAssending = true);






       //Task<IEnumerable<T>> GetFilteredAsyncUsingReflecton(string filterProperty, string filterValue, string[] includes = null);
    }
}
