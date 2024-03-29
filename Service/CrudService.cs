using System;
using System.Linq;
using System.Linq.Expressions;
using Omu.ProDinner.Core.Model;
using Omu.ProDinner.Core.Repository;
using Omu.ProDinner.Core.Service;

namespace Omu.ProDinner.Service
{
    public class  CrudService<T> : ICrudService<T> where T : DelEntity, new()
    {
        protected IRepo<T> repo;

        public CrudService(IRepo<T> repo)
        {
            this.repo = repo;
        }

        public IQueryable<T> GetAll()
        {
            return repo.GetAll();
        }

        public T Get(int id)
        {
            return repo.Get(id);
        }

        public virtual int Create(T item)
        {
            var newItem = repo.Insert(item);
            repo.Save();
            return newItem.Id;
        }

        public void Save()
        {
            repo.Save();
        }

        public virtual void Delete(int id)
        {
            repo.Delete(repo.Get(id));
            repo.Save();
        }

        public void Restore(int id)
        {
            repo.Restore(repo.Get(id));
            repo.Save();
        }

        public void BatchDelete(int[] ids)
        {
            foreach (var id in ids)
            {
                repo.Get(id).IsDeleted = true;
            }

            repo.Save();
        }

        public void BatchRestore(int[] ids)
        {
            foreach (var id in ids)
            {
                repo.Get(id).IsDeleted = false;
            }

            repo.Save();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate, bool showDeleted = false)
        {
            return repo.Where(predicate, showDeleted);
        }
    }
}
