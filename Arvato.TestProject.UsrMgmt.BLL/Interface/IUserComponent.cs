using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arvato.TestProject.UsrMgmt.Entity.Model;

namespace Arvato.TestProject.UsrMgmt.BLL.Interface
{
    public interface IUserComponent : IDisposable
    {
        List<User> GetList();
        List<User> GetListWithRole();
        User GetRecord(int id);
        User GetRecord(string loginID);
        void Save(User user);
        void Delete(User user);
        bool Login(User user);
        void Update(User user);
        bool IsExistingLoginID(string LoginID, int ID);
    }
}
