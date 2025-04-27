using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPO.Core.Models
{
    [NotMapped]
    public class Page<T>where T : class
    {
        public List<T>Items{ get; set; }
        public int PageNumber{ get; set; }
        public int ToTalPages{ get; set; }
        public bool HasNext => PageNumber < ToTalPages;
        public bool HasPrevious => PageNumber > 1;

        public Page(List<T> items,int count,int pageNumber=1,int pageSize=10)
        {
            Items = items;
            PageNumber = pageNumber;
            ToTalPages = count/pageSize;
        }

        public static async Task<Page<T>> GetPageData(IQueryable<T> query,int pageNumber,int pageSize)
        {
            var count=await query.CountAsync();
            var items = await query.ToListAsync();

            var pageData=items.Skip((pageNumber-1)*pageSize).Take(pageSize).ToList();

            return new Page<T>(pageData, count, pageNumber, pageSize);
            
        }

    }
}
