using NZ.Walks.Controllers;
using REPO.Core.Contract;
using REPO.Core.Models;
using REPO.EF.Data;
using REPO.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPO.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IBaseRepository<Region> Region { get;private set; }

        public IBaseRepository<Walk> Walk { get;private set; }

        public IUserRepository AppUser { get; private set; }

        public IIMageRepository Image { get; private set; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Region = new BaseRepository<Region>(_context);
            Walk= new BaseRepository<Walk>(_context);
            AppUser = new UserRepository(_context);
            Image = new ImageRepository(_context);
        }



        public async Task CompleteAsync()
        {
           await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
           _context.Dispose();  
        }
    }
}
