using System.Collections.Generic;

namespace Arvato.TestProject.UsrMgmt.DAL.Interface
{
    public interface IBaseRepository<T> where T : class
    {

        IEnumerable<T> GetAll();
        bool Add(T entity);
        void Update(T entity);
        bool Delete(T entity);

        // Other ideas to implement...

        //IEnumerable<T> GetAll();
        //IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
        //void Add(T entity);
        //void Delete(T entity);
        //void Edit(T entity);
        //void Save();
        //void Detach(T entity);
        //T GetSingleById(Expression<Func<T, bool>> predicateId);
        //T GetSingleById(int id);

    }
}
