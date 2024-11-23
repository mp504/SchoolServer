﻿namespace ServerOfSchool.Interfaces
{
    public interface IRepository
    {
        public interface IRepository<T> where T : class
        {
            Task<IEnumerable<T>> GetAllAsync();
            Task<T> GetByIdAsync(int id);
            Task AddAsync(T entity);
            void Remove(T entity);
            Task<bool> SaveChangesAsync();
        }
    }
}
