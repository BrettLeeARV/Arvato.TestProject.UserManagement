﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arvato.TestProject.UsrMgmt.Entity.Model;
using System.Data;

namespace Arvato.TestProject.UsrMgmt.DAL.Interface
{
    public interface IUserRepository : IBaseRepository<User>, IDisposable
    {

        bool Login(User entity);
        bool IsExistingLoginID(string LoginID, int ID);
        
    }
}
