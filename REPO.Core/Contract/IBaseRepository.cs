
using Microsoft.EntityFrameworkCore.Storage;
using REPO.Core.DTO;
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
         Task<T?> GetOne(Expression<Func<T, bool>> item);
        Task<IEnumerable<T>> GetAllAsync(string[] includes = null);
        Task<IEnumerable<TResult>> GetAllAsync<TResult>(Expression<Func<T,bool>>filter,Expression<Func<T,TResult>> selection);

        



        Task<IEnumerable<T>> GetFilteredSortedPageAsync(string? filterOn, string? filterQuery,
           string? sortBy, bool isAssending, string[]includes=null, int pageNumber = 1, int pageSize = 10);


         Task<IEnumerable<T>> GetSortedPageAsync(string? sortBy, bool isAssending, string[] includes = null, int pageNumber = 1, int pageSize = 10);



        Task<T?> GetByIdAsync(Expression<Func<T, bool>> match, string[] includes = null);

        Task<IEnumerable<T>> GetFilteredAsync(Expression<Func<T, bool>> critiria ,string[] includes = null);
        Task<IEnumerable<T>> GetFiltered_OrderedAsync(Expression<Func<T, bool>> critiria, Expression<Func<T, object>> orderUsing, string[] includes = null,  bool isAssending = true);



        Task<IEnumerable<T>> GetOrderedBy( string orderUsing, string[] includes = null,  bool isAssending = true);



        Task<IDbContextTransaction> BeginTransactionAsync();

        Task<IEnumerable<T>> GetFilteredAsyncUsingReflecton(string filterProperty, string filterValue, string[] includes = null);
        Task<IEnumerable<T>> GetFiltered_OrderedAsyncUsingReflecton(string filterProperty, string filterValue, string orderProperty, string[] includes = null, bool IsAssending = true);
    }
}
