﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arvato.TestProject.UsrMgmt.DAL.Interface;
using System.Data.SqlClient;
using Arvato.TestProject.UsrMgmt.Entity.Model;

namespace Arvato.TestProject.UsrMgmt.DAL.Repository
{
   public class RoomRepository : BaseRepository,IRoomRepository
    {
       static string connString = "";

       public RoomRepository(SqlConnection dbConnection)
           : base(dbConnection)
       {
           connString = dbConnection.ConnectionString;
       }

       public IQueryable<Room> GetAll()
       {
           try
           {
               using (var session = NHibernateHelper.OpenSession(connString))
               {
                   // var room = new Room
                   //  {
                   var specificFields = session.QueryOver<Room>().List<Room>();
                   //ID = entity.ID,
                   //Name = entity.Name,
                   //Location = entity.Location,
                   //Capacity = entity.Capacity,
                   //IsEnabled = entity.IsEnabled

                   //};
                   return specificFields.AsQueryable<Room>();
               }

           }
           catch (Exception)
           {
               // return false;
               throw;
           }

       }

       public bool Add(Room entity)
       {
           throw new NotImplementedException();
       }


       public void Update(Room entity)
       {
           throw new NotImplementedException();
       }

       public bool Delete(Room entity)
       {
           throw  new NotImplementedException();
       }

       public IQueryable<Room> GetAllEnabled()
       {
           using (var session = NHibernateHelper.OpenSession(connString))
           {
               var roomList = session.QueryOver<Room>().Where(x => x.IsEnabled == true).OrderBy(x => x.Name).Asc.List();

               return roomList.AsQueryable<Room>();
           }
       }

        //public IQueryable<User> SelectRoom()
        //{
        //    try
        //    {
        //        SessionFactory sf = new SessionFactory();
        //        var factory = sf.CreateSessionFactory();

        //        using (var session = factory.OpenSession())
        //        {
        //           // var room = new Room
        //          //  {
        //         var specificFields = session.CreateSQLQuery("SELECT ID, Name, Location, Capacity, IsEnabled FROM Room").List();
        //                //ID = entity.ID,
        //                //Name = entity.Name,
        //                //Location = entity.Location,
        //                //Capacity = entity.Capacity,
        //                //IsEnabled = entity.IsEnabled

        //            //};
        //         return specificFields.AsQueryable<Room>();
        //        }
              
        //    }
        //    catch (Exception)
        //    {
        //       // return false;
        //        throw;
        //    }
        //}
    }
}
