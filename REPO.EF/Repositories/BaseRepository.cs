using Microsoft.EntityFrameworkCore;
using REPO.Core.Contract;
using REPO.EF.Data;
using System.Linq.Expressions;


namespace REPO.EF.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
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
           return  await _context.Set<T>().FindAsync(id);
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
            T? item =await  GetByIdAsync(id);
            if (item == null)
                return null;

            _context.Set<T>().Remove(item);
            return item;
        }

        public async Task<IEnumerable<T>> GetAllAsync(string[] includes = null)
        {
            IQueryable<T> query=_context.Set<T>();
            if(includes == null)
            {
                return await query.ToListAsync();
            }

            foreach(string incluse in includes)
            {
                query=query.Include(incluse);
            }
            return await query.ToListAsync();

        }

        public async Task<T?> GetByIdAsync(Expression<Func<T,bool>>match,string[] includes = null)
        {
           IQueryable<T?>query=_context.Set<T>();    
            if(includes!= null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.FirstOrDefaultAsync(match);
        }

        
            
        public async Task<IEnumerable<T>> GetFiltered_OrderedAsync(Expression<Func<T, bool>> critiria,Expression<Func<T,object>>orderUsing, string[] includes = null, bool isAssending=true)
        {
            IQueryable<T>query=_context.Set<T>();
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

       





        public async Task<IEnumerable<T>> GetFilteredAsyncUsingReflecton( string filterProperty,string filterValue, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
        
            if(!string.IsNullOrEmpty(filterProperty)&& filterValue != null)
            {
                var parameter = Expression.Parameter(typeof(T), "x");
                var property = Expression.Property(parameter, filterProperty);


                var propertyType = property.Type;

                object convertedValue = Convert.ChangeType(filterValue, propertyType);

                var constant=Expression.Constant(convertedValue,propertyType);
                var equality = Expression.Equal(property, constant);
                var lambda = Expression.Lambda<Func<T, bool>>(equality, parameter);
        
                query= query.Where(lambda);
            }
            return await query.ToListAsync();
        }

       public async Task<IEnumerable<T>> GetFiltered_OrderedAsyncUsingReflecton(string filterProperty, string filterValue, string orderProperty, string[] includes = null,  bool IsAssending = true )
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            if (!string.IsNullOrEmpty(filterProperty) && filterValue != null)
            {
                var parameter = Expression.Parameter(typeof(T), "x");
                var property = Expression.Property(parameter, filterProperty);

                var propertyType = property.Type;

                object convertedValue = Convert.ChangeType(filterValue, propertyType);

                var constant = Expression.Constant(convertedValue,propertyType);
                var equality = Expression.Equal(property, constant);   //Equal this build the == part  first param for left side and second for right side
                var lambda = Expression.Lambda<Func<T, bool>>(equality, parameter);
                 
                query = query.Where(lambda);
            }



            if(!string.IsNullOrEmpty(orderProperty))
            {
                var parameter=Expression.Parameter(typeof(T), "x");
                var property=Expression.Property(parameter, orderProperty);
                var propertyType=property.Type;

                var lambda = Expression.Lambda(property, parameter);

                string methodName=IsAssending ? "OrderBy" :"OrderByDescending";

                var result = Expression.Call(
                    typeof(Queryable),
                    methodName,
                    new Type[] { typeof(T), propertyType },
                    query.Expression,
                    Expression.Quote(lambda));

                query=query.Provider.CreateQuery<T>(result);    

            }


            return await query.ToListAsync();
        }

        public  async Task<IEnumerable<T>> GetOrderedBy(string orderUsing, string[] includes = null, bool isAssending = true)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            if (!string.IsNullOrEmpty(orderUsing))
            {
                var parameter = Expression.Parameter(typeof(T), "x");

                //property is x.somthing           the 'x' is parameter amd the somthing is 'orderUsing' 
                var property = Expression.Property(parameter, orderUsing);
                var propertyType = property.Type;

                var lambda = Expression.Lambda(property, parameter);

                string methodName = isAssending ? "OrderBy" : "OrderByDescending";

                var result = Expression.Call(
                    typeof(Queryable),   //static class that contain the OrderBy Method
                    methodName,          // ORderBy  or ORderByDesincding
                    new Type[] { typeof(T), propertyType }, //  This tells the compiler the generic types
                    query.Expression, // The current LINQ expression tree.
                    Expression.Quote(lambda));//Wraps the lambda so it's treated as an expression

                query = query.Provider.CreateQuery<T>(result);

            }


            return await query.ToListAsync();

        }
    }
}
