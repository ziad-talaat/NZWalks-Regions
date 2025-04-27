using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using REPO.Core.Contract;
using REPO.Core.Models;
using REPO.EF.Data;
using System.Globalization;
using System.Linq.Expressions;
using System.Security.Cryptography;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace REPO.EF.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        public BaseRepository()
        {

        }
        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task CeateAsync(T item)
        {
            await _context.Set<T>().AddAsync(item);
        }
        public async Task UpdateAsync(T item)
        {
            _context.Update(item);
        }

        public async Task<T> DeleteAsync(Guid id)
        {
            T? item = await GetByIdAsync(id);
            if (item == null)
                return null;

            _context.Set<T>().Remove(item);
            return item;
        }

        public async Task<IEnumerable<TResult>> GetAllAsync<TResult>(Expression<Func<T, bool>> filter, Expression<Func<T, TResult>> selection)
        {
            return await _context.Set<T>().Where(filter).Select(selection).ToListAsync();
        }


        public async Task<IEnumerable<T>> GetAllAsync(string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes == null)
            {
                return await query.ToListAsync();
            }

            foreach (string incluse in includes)
            {
                query = query.Include(incluse);
            }
            return await query.ToListAsync();

        }

        public async Task<T?> GetByIdAsync(Expression<Func<T, bool>> match, string[] includes = null)
        {
            IQueryable<T?> query = _context.Set<T>();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.FirstOrDefaultAsync(match);
        }



        public async Task<IEnumerable<T>> GetFiltered_OrderedAsync(Expression<Func<T, bool>> critiria, Expression<Func<T, object>> orderUsing, string[] includes = null, bool isAssending = true)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            query = query.Where(critiria);

            query = (isAssending == true) ? query.OrderBy(orderUsing) : query.OrderByDescending(orderUsing);

            return await query.ToListAsync();

        }

        public async Task<IEnumerable<T>> GetFilteredAsync(Expression<Func<T, bool>> critiria, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.Where(critiria).ToListAsync();
        }


        public async Task<T?> GetOne(Expression<Func<T, bool>> item)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(item) ?? null;
        }





        public async Task<IEnumerable<T>> GetFilteredAsyncUsingReflecton(string filterProperty, string filterValue, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            query = BuildFilter(query, filterProperty, filterValue);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetFiltered_OrderedAsyncUsingReflecton(string filterProperty, string filterValue, string orderProperty, string[] includes = null, bool IsAssending = true)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            query = BuildFilter(query, filterProperty, filterValue);



            query = BuildSort(query, orderProperty, IsAssending);


            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetOrderedBy(string orderUsing, string[] includes = null, bool isAssending = true)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            query = BuildSort(query, orderUsing, isAssending);


            return await query.ToListAsync();
        }



        public async Task<IEnumerable<T>> GetFilteredSortedPageAsync(string? filterOn, string? filterQuery,
          string? sortBy, bool isAssending, string[] includes = null, int pageNumber = 1, int pageSize = 10)
        {
            IQueryable<T> query = _context.Set<T>();

            includes ??= Array.Empty<string>();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            query = BuildFilter(query, filterOn, filterQuery);



            query = BuildSort(query, sortBy, isAssending);


            int skip = (pageNumber - 1) * pageSize;

            query = query.Skip(skip).Take(pageSize);


            return await query.ToListAsync();

        }



        public async Task<IEnumerable<T>> GetSortedPageAsync(string? sortBy, bool isAssending, string[] includes = null, int pageNumber = 1, int pageSize = 10)
        {

            IQueryable<T> query = _context.Set<T>();

            includes ??= Array.Empty<string>();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }
             int skip = (pageNumber - 1) * pageSize;
           
             query = query.Skip(skip).Take(pageSize);
           
             query = BuildSort(query, sortBy, isAssending);
           
             return await query.ToListAsync();
        }






            





        private IQueryable<T> BuildSort(IQueryable<T> query, string? sortBy, bool isAssending = true)
        {
            if (!string.IsNullOrEmpty(sortBy))
            {
                //x => ...
                var parameter = Expression.Parameter(typeof(T), "x");


                // x.sortBy
                var property = Expression.Property(parameter, sortBy);

                //get the type of property at runtime like string or what
                var propertyType = property.Type;

                //x => x.proeprty
                var lambda = Expression.Lambda(property, parameter);

                string methodName = isAssending ? "OrderBy" : "OrderByDescending";

                var result = Expression.Call(

                    typeof(Queryable), //class that contains the method
                    methodName,    //the method to call
                    new Type[] { typeof(T), propertyType }, //  These are the generic types for the   OrderBy<T, TKey>.
                    query.Expression,  //the base query  ex->  _context.User

                    Expression.Quote(lambda)//wraps the lambda so that it is treated as a query expression, not executable code.
                    );
                query = query.Provider.CreateQuery<T>(result);  //This actually executes the expression tree and creates a new `IQueryable<T>` that includes the ordering you just dynamically added.
                                                                // `CreateQuery < T >` tells EF Core to **build the final LINQ query** using your custom-built expression.
            }
            return query;
        }






        private IQueryable<T> BuildFilter(IQueryable<T> query, string? filterOn, string? filterQuery)
        {
            if (!string.IsNullOrEmpty(filterOn) && filterQuery != null)
            {
                var parameter = Expression.Parameter(typeof(T), "x");

                //x.filterOn
                var property = Expression.Property(parameter, filterOn);

                var propertyType = property.Type;

                object convertedValue = Convert.ChangeType(filterQuery, propertyType);

                // have the value 
                var constant = Expression.Constant(convertedValue, propertyType);

                // x.property==value
                var equality = Expression.Equal(property, constant);   //Equal this build the == part  first param for left side and second for right side

                //x=>x.property==value
                var lambda = Expression.Lambda<Func<T, bool>>(equality, parameter);

                query = query.Where(lambda);
            }

            return query;





        }
    }
}
