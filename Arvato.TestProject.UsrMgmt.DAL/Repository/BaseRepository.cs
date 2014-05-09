using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arvato.TestProject.UsrMgmt.DAL.Interface;
using System.Data.SqlClient;
using System.Data;

namespace Arvato.TestProject.UsrMgmt.DAL.Repository
{
    public class BaseRepository : IDisposable
    {
        private SqlConnection _connection;

        public BaseRepository()
        {
        }

        public BaseRepository(SqlConnection connection)
        {
            this._connection = connection;
        }

        protected SqlConnection Connection
        {
            get { return _connection; }
            set { _connection = value; }
        }

        #region General CRUD methods

        
        #endregion

        #region Private ADO.NET DB Access Methods
      /// <method>
        /// Select Query
        /// </method>
        public virtual DataTable executeSelectQuery(String _query, SqlParameter[] sqlParameter)
        {
            SqlCommand myCommand = new SqlCommand();
            DataTable dataTable = new DataTable();
            dataTable = null;
            DataSet ds = new DataSet();
            SqlDataAdapter myAdapter = new SqlDataAdapter();

            
            if (_connection.State == ConnectionState.Closed)
                _connection.Open();

            myCommand.Connection = _connection;
            myCommand.CommandText = _query;

            if (sqlParameter != null)
                myCommand.Parameters.AddRange(sqlParameter);

            myCommand.ExecuteNonQuery();                
            myAdapter.SelectCommand = myCommand;
            myAdapter.Fill(ds);
            dataTable = ds.Tables[0];
          
            return dataTable;
        }

        /// <method>
        /// Insert Query
        /// </method>
        public virtual bool executeInsertQuery(String _query, SqlParameter[] sqlParameter)
        {
            SqlCommand myCommand = new SqlCommand();
            SqlDataAdapter myAdapter = new SqlDataAdapter();

           
                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                myCommand.Connection = _connection;
                myCommand.CommandText = _query;

                if (sqlParameter != null)
                    myCommand.Parameters.AddRange(sqlParameter);

                myAdapter.InsertCommand = myCommand;
                myCommand.ExecuteNonQuery();
           
            return true;
        }

        /// <method>
        /// Update Query
        /// </method>
        public virtual bool executeUpdateQuery(String _query, SqlParameter[] sqlParameter)
        {
            SqlCommand myCommand = new SqlCommand();
            SqlDataAdapter myAdapter = new SqlDataAdapter();

            try
            {
                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                myCommand.Connection = _connection;
                myCommand.CommandText = _query;
                myCommand.Parameters.AddRange(sqlParameter);
                myAdapter.UpdateCommand = myCommand;
                myCommand.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
            //    Console.Write("Error - Connection.executeUpdateQuery - Query: 
            //" + _query + " \nException: " + e.StackTrace.ToString());
                return false;
            }
            finally
            {
            }
            return true;
        }
        #endregion

        #region IDisposable

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
                if (disposing)
                    _connection.Dispose();

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion


    }
}
