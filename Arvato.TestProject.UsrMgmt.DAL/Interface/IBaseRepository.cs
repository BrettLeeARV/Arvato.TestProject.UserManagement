using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Data;

namespace Arvato.TestProject.UsrMgmt.DAL.Interface
{
    public interface IBaseRepository<T> where T : class
    {
        //IQueryable<T> GetAll();
        //IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        //void Add(T entity);
        //void Delete(T entity);
        //void Edit(T entity);
        //void Save();
        //void Detach(T entity);
        //T GetSingleById(Expression<Func<T, bool>> predicateId);

        IQueryable<T> GetAll();
        bool Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        //T GetSingleById(int id);
    }
}
