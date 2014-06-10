using Arvato.TestProject.UsrMgmt.DAL.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SqlClient;
using Arvato.TestProject.UsrMgmt.Entity.Model;
using System.Linq;
using System.Configuration;
using System.Collections.Generic;

namespace Arvato.TestProject.UsrMgmt.DAL.Test
{
    
    
    /// <summary>
    ///This is a test class for BookingRepositoryTest and is intended
    ///to contain all BookingRepositoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class BookingRepositoryTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for CheckRoomAvailability
        ///</summary>
        [TestMethod()]
        public void CheckRoomAvailabilityTest()
        {
            SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["usrMgmtConnString"].ConnectionString);
            BookingRepository target = new BookingRepository(dbConnection);

            int bookingID = 0;
            DateTime startDate = new DateTime(2014, 6, 3, 17, 0, 0);
            DateTime endDate = new DateTime(2014, 6, 3, 18, 0, 0);
            int roomID = 2;

            IEnumerable<Booking> actual;
            actual = target.CheckRoomAvailability(bookingID, startDate, endDate, roomID);
            List<Booking> list = actual.ToList<Booking>();

            Assert.IsTrue(list.Count > 0, "Expected conflicting bookings to be returned");
        }
    }
}
