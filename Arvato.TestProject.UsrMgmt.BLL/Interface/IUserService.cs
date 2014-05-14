using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arvato.TestProject.UsrMgmt.Entity.Model;

namespace Arvato.TestProject.UsrMgmt.BLL.Interface
{
    public interface IUserService
    {
        List<User> GetList();
        User GetRecord(int id);
        User GetRecord(string loginID);
        void Save(User user);
        void Delete(User user);
        void Login(User user);
       
    }
}
