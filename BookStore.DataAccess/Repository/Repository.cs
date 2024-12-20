﻿using Bookstore.DataAccess;
using BookStore.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookStore.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private ApplicationDbContext _context { get; set; }
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext context)
        {
            _context = context;
            this.dbSet = _context.Set<T>();
        }
        public void Add(T entity)
        {   
            dbSet.Add(entity);
        }
        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = dbSet.AsQueryable(); 
            return query.ToList();
        }
        public T GetFirstOrDefault(Expression<Func<T, bool>> filter) 
        {
            IQueryable<T> query = dbSet.AsQueryable();
            query = query.Where(filter); 
            return query.FirstOrDefault();
        }
        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }
        public void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }
    }
}