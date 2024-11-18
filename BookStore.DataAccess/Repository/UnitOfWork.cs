using Bookstore.DataAccess;
using BookStore.DataAccess.Repository.IRepository;

namespace BookStore.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Category = new CategoryRepository(_context);
        }

        public ICategoryRepository Category { get; private set; }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
