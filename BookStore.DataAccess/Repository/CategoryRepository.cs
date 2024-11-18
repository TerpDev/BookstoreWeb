using Bookstore.DataAccess;
using Bookstore.Models;
using BookStore.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository 
    {
        private ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        //public void Save()
        //{
        //    _context.SaveChanges();
        //}
        public void Update(Category obj)
        {
            _context.Categories.Update(obj);
        }
    }
}
