using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arvato.TestProject.UsrMgmt.DAL.Interface;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


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
            myCommand.CommandType = CommandType.StoredProcedure;

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
                myCommand.CommandType = CommandType.StoredProcedure; // added by benjamin.

                if (sqlParameter != null)
                    myCommand.Parameters.AddRange(sqlParameter);

                myAdapter.InsertCommand = myCommand;
                myCommand.ExecuteNonQuery();
           
            return true;
        }
        public virtual bool executeInsertQuery(String _query, SqlParameter[] sqlParameter, ref Object result)
        {
            SqlCommand myCommand = new SqlCommand();
            SqlDataAdapter myAdapter = new SqlDataAdapter();


            if (_connection.State == ConnectionState.Closed)
                _connection.Open();

            myCommand.Connection = _connection;
            myCommand.CommandText = _query;
            myCommand.CommandType = CommandType.StoredProcedure; // added by benjamin.

            if (sqlParameter != null)
                myCommand.Parameters.AddRange(sqlParameter);

            myAdapter.InsertCommand = myCommand;
            myCommand.ExecuteNonQuery();
            result = myCommand.Parameters["@RefNum"].Value.ToString();

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
                myCommand.CommandType = CommandType.StoredProcedure;// added by Ben
                SqlParameter returnvalue = new SqlParameter(); // added by ben
                returnvalue.Direction = ParameterDirection.ReturnValue; // added by ben.
                myCommand.Parameters.Add(returnvalue); // added by ben.
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

        /// <method>
        /// Delete Query
        /// </method>
        public virtual bool executeDeleteQuery(String _query, SqlParameter[] sqlParameter) //Beh
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
                myCommand.CommandType = CommandType.StoredProcedure;
                myAdapter.DeleteCommand = myCommand;
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

        public virtual object executeScalarquery(String _query, SqlParameter[] sqlParameter) // added by ben
        {
            SqlCommand myCommand = new SqlCommand();
            SqlDataAdapter myAdapter = new SqlDataAdapter();


            if (_connection.State == ConnectionState.Closed)
                _connection.Open();

            myCommand.Connection = _connection;
            myCommand.CommandText = _query;
            myCommand.CommandType = CommandType.StoredProcedure; // added by benjamin.

            if (sqlParameter != null)
                myCommand.Parameters.AddRange(sqlParameter);

            myAdapter.InsertCommand = myCommand;

            var returnResult = myCommand.ExecuteScalar();
            return returnResult;

             
        }
         public virtual bool executeInsertQueryWithOutPutParameter(String _query, SqlParameter[] sqlParameter)
        {
            SqlCommand myCommand = new SqlCommand();
            SqlDataAdapter myAdapter = new SqlDataAdapter();

            
                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                myCommand.Connection = _connection;
                myCommand.CommandText = _query;
                myCommand.CommandType = CommandType.StoredProcedure; // added by benjamin.

                if (sqlParameter != null)
                    myCommand.Parameters.AddRange(sqlParameter);

                myAdapter.InsertCommand = myCommand;
                myCommand.ExecuteNonQuery();
           
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



                
