using BookNetCore.DataAccess.Data;
using BookNetCore.DataAccess.Repository.IRepository;
using BookNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookNetCore.DataAccess.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {

        private ApplicationDbContext _db;
        public ApplicationUserRepository(ApplicationDbContext db):base(db) 
        {
            _db = db;
        }
      

    }
}
