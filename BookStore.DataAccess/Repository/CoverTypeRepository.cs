using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.DataAccess;
using Bookstore.Models;
using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;

namespace BookStore.DataAccess.Repository
{
	internal class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepository
	{
		private ApplicationDbContext _context;
		public CoverTypeRepository(ApplicationDbContext context) : base(context)
		{
			_context = context;
		}
		public void Update(CoverType obj)
		{
			_context.CoverTypes.Update(obj);
		}

	}

}
