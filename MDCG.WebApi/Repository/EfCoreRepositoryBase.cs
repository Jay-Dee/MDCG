﻿using MDCG.WebApi.Data;
using MDCG.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MDCG.WebApi.Repository
{
    public abstract class EfCoreRepositoryBase<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class, IEntity {

        private readonly MDCGDbContext _context;

        public EfCoreRepositoryBase(MDCGDbContext context) {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<TEntity> Add(TEntity entity) {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Delete(int id) {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity == null) {
                return entity;
            }

            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> Get(int id) {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<List<TEntity>> GetAll() {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> Update(TEntity entity) {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

    }
}
