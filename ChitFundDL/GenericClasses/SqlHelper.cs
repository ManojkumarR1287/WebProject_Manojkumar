using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SQL
{
    public sealed class SQLHelper
    {
        //  Developer       - Manojkumar
        //  Application     - SQL Helper
        //  Date            - 21/03/2023

        #region Class Variables

        private static SqlTransaction _sqltrn = null;
        private static SqlConnection _sqlcon = null;
        private static SqlCommand _sqlcom = null;
        private static Boolean _result = false;
        private static Object _value = null;

        private static DataTable _resulttable = null;
        private static String typevalue = String.Empty;
        private static Dictionary<String, SqlDbType> _output = null;
        private static Dictionary<String, String> _paranames = null;

        #endregion

        #region Static Values

        public static string ConnectionValue = string.Empty;

        #endregion

        #region Private Methods

        private static void CreateParameters(List<SqlParameter> parameters)
        {
            if (parameters != null)
            {
                _output = null;
                _paranames = null;

                _output = new Dictionary<string, SqlDbType>();
                _paranames = new Dictionary<string, string>();

                if (_sqlcom.Parameters.Count != 0)
                    _sqlcom.Parameters.Clear();

                foreach (SqlParameter parameter in parameters)
                {
                    if (parameter.Direction == ParameterDirection.InputOutput)
                    {
                        _output.Add(parameter.SourceColumn, parameter.SqlDbType);
                        _paranames.Add(parameter.SourceColumn, parameter.ParameterName);
                    }

                    if (parameter.Direction == ParameterDirection.Output)
                    {
                        parameter.Value = DBNull.Value;
                        _output.Add(parameter.SourceColumn, parameter.SqlDbType);
                        _paranames.Add(parameter.SourceColumn, parameter.ParameterName);
                    }
                    _sqlcom.Parameters.Add(parameter);
                }

                if (_output.Count != 0)
                {
                    _resulttable = new DataTable("Result");
                    DataColumn col = null;

                    SqlDbType dbtype;
                    foreach (String key in _output.Keys)
                    {
                        dbtype = (SqlDbType)_output[key];
                        col = new DataColumn(key, Type.GetType(GetColumnType(dbtype)));
                        _resulttable.Columns.Add(col);
                    }
                }
            }
        }
        private static String GetColumnType(SqlDbType sqlType)
        {
            switch (sqlType)
            {
                case SqlDbType.BigInt:
                    return "System.Int64";

                case SqlDbType.Binary:
                case SqlDbType.Image:
                case SqlDbType.Timestamp:
                case SqlDbType.VarBinary:
                    return "System.Byte[]";

                case SqlDbType.Bit:
                    return "System.Boolean";

                case SqlDbType.Char:
                case SqlDbType.NChar:
                case SqlDbType.NText:
                case SqlDbType.NVarChar:
                case SqlDbType.Text:
                case SqlDbType.VarChar:
                case SqlDbType.Xml:
                    return "System.String";

                case SqlDbType.DateTime:
                case SqlDbType.SmallDateTime:
                case SqlDbType.Date:
                case SqlDbType.Time:
                case SqlDbType.DateTime2:
                    return "System.DateTime";

                case SqlDbType.Decimal:
                case SqlDbType.Money:
                case SqlDbType.SmallMoney:
                    return "System.Decimal";

                case SqlDbType.Float:
                    return "System.Double";

                case SqlDbType.Int:
                    return "System.Int32";

                case SqlDbType.Real:
                    return "System.Single";

                case SqlDbType.UniqueIdentifier:
                    return "System.Guid";

                case SqlDbType.SmallInt:
                    return "System.Int16";

                case SqlDbType.TinyInt:
                    return "System.Byte";

                case SqlDbType.Variant:
                case SqlDbType.Udt:
                    return "System.Object";

                case SqlDbType.Structured:
                    return "System.Data.DataTable";

                case SqlDbType.DateTimeOffset:
                    return "System.DateTimeOffset";

                default:
                    throw new ArgumentOutOfRangeException("Invalid SQL Data type");
            }
        }
        private static void FillTable()
        {
            if (_resulttable != null)
            {
                DataRow row = _resulttable.NewRow();

                foreach (String item in _output.Keys)
                    row[item] = _sqlcom.Parameters[_paranames[item].ToString()].Value;

                _resulttable.Rows.Add(row);
            }
        }

        #endregion

        #region Execute SQLHelper

        public static void CreateObjects(Boolean istransaction)
        {
            _sqlcon = new SqlConnection(SQLHelper.ConnectionValue);
            _sqlcon.Open();

            if (istransaction)
                _sqltrn = _sqlcon.BeginTransaction(IsolationLevel.Serializable);

            _sqlcom = new SqlCommand();
            _sqlcom.Connection = _sqlcon;
            _sqlcom.CommandType = CommandType.StoredProcedure;

            if (istransaction)
                _sqlcom.Transaction = _sqltrn;
        }
        public static void CommitTransction()
        {
            if (_sqltrn != null)
                _sqltrn.Commit();
        }
        public static void RollBackTransction()
        {
            if (_sqltrn != null)
                _sqltrn.Rollback();
        }
        public static void ClearObjects()
        {
            if (_sqlcom != null)
            {
                if (_sqlcom.Parameters.Count != 0)
                    _sqlcom.Parameters.Clear();

                _sqlcom.Cancel();
                _sqlcom.Dispose();

                _sqlcom = null;
            }

            if (_sqltrn != null)
            {
                _sqltrn.Dispose();
                _sqltrn = null;
            }

            if (_sqlcon != null)
            {
                if (_sqlcon.State == ConnectionState.Open)
                    _sqlcon.Close();

                _sqlcon.Dispose();
                _sqlcon = null;
            }

            if (_output != null)
                _output = null;

            if (_paranames != null)
                _paranames = null;
        }

        public static Boolean SQLHelper_ExecuteNonQuery(string Procedure_Name)
        {
            try
            {
                _result = false;
                _sqlcom.CommandText = Procedure_Name;
                _sqlcom.ExecuteNonQuery();
                _result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _result;
        }
        public new static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            try
            {
                // create & open a SqlConnection, and dispose of it after we are done.
                SqlConnection cn = new SqlConnection(connectionString);
                try
                {
                    cn.Open();

                    // call the overload that takes a connection in place of the connection string
                    return ExecuteNonQuery(cn, commandType, commandText, commandParameters);
                }
                finally
                {
                    cn.Dispose();
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public new static int ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            try
            {
                // create a command and prepare it for execution
                SqlCommand cmd = new SqlCommand();
                int retval;

                PrepareCommand(cmd, connection, (SqlTransaction)null, commandType, commandText, commandParameters);

                // finally, execute the command.
                retval = cmd.ExecuteNonQuery();

                // detach the SqlParameters from the command object, so they can be used again
                cmd.Parameters.Clear();

                return retval;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        private static void PrepareCommand(SqlCommand command, SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters)
        {
            try
            {
                // if the provided connection != open, we will open it
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                // associate the connection with the command
                command.Connection = connection;

                // set the command text (stored procedure name or SQL statement)
                command.CommandText = commandText;
                command.CommandTimeout = 6000;
                // if we were provided a transaction, assign it.
                if (!(transaction == null))
                    command.Transaction = transaction;

                // set the command type
                command.CommandType = commandType;

                // attach the command parameters if they are provided
                if (!(commandParameters == null))
                    AttachParameters(command, commandParameters);

                return;
            }
            catch (Exception)
            {
            }
        }
        private static void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
        {
            try
            {
                foreach (var p in commandParameters)
                {
                    // check for derived output value with no value assigned
                    if (p.Direction == ParameterDirection.InputOutput & p.Value == null)
                        p.Value = null;
                    command.Parameters.Add(p);
                }
            }
            catch (Exception)
            {
            }
        } // AttachParameters


        public static Boolean SQLHelper_ExecuteNonQuery(string Procedure_Name, List<SqlParameter> parameters)
        {
            try
            {
                _result = false;
                _sqlcom.CommandText = Procedure_Name;
                SQLHelper.CreateParameters(parameters);
                _sqlcom.ExecuteNonQuery();
                SQLHelper.FillTable();
                _result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _result;
        }

        public static Object SQLHelper_ExecuteScalar(string Procedure_Name)
        {
            try
            {
                _value = null;
                _sqlcom.CommandText = Procedure_Name;
                _value = _sqlcom.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _value;
        }
        public static Object SQLHelper_ExecuteScalar(string Procedure_Name, List<SqlParameter> parameters)
        {
            try
            {
                _value = null;
                _sqlcom.CommandText = Procedure_Name;
                SQLHelper.CreateParameters(parameters);
                _value = _sqlcom.ExecuteScalar();
                SQLHelper.FillTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _value;
        }

        public static DataTable SQLHelper_ExecuteReader(string Procedure_Name)
        {
            DataTable _data = null;
            try
            {
                _data = null;
                _sqlcom.CommandText = Procedure_Name;
                SqlDataAdapter _adapter = new SqlDataAdapter(_sqlcom);
                DataSet dataset = new DataSet("SQLHelper");
                _adapter.Fill(dataset);
                _data = dataset.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _data;
        }
        public static DataTable SQLHelper_ExecuteReader(string Procedure_Name, List<SqlParameter> parameters)
        {
            DataTable _data = null;
            try
            {
                _data = null;
                _sqlcom.CommandText = Procedure_Name;
                SQLHelper.CreateParameters(parameters);
                SqlDataAdapter _adapter = new SqlDataAdapter(_sqlcom);
                DataSet dataset = new DataSet("SQLHelper");
                _adapter.Fill(dataset);
                _data = dataset.Tables[0];
                SQLHelper.FillTable();
                _sqltrn.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _data;
        }

        public static DataTable SQLHelper_OutputValues()
        {
            return _resulttable;
        }


   
            #region ExecuteDataTable

            // Execute a SqlCommand (that returns a resultset and takes no parameters) against the database specified in 
            // the connection string. 
            // e.g.:  
            // Dim ds As DataSet = SqlHelper.ExecuteDataTable("", commandType.StoredProcedure, "GetOrders")
            // Parameters:
            // -connectionString - a valid connection string for a SqlConnection
            // -commandType - the CommandType (stored procedure, text, etc.)
            // -commandText - the stored procedure name or T-SQL command
            // Returns: a dataset containing the resultset generated by the command
            public static DataTable ExecuteDataTable(string connectionString, CommandType commandType, string commandText)

            {
                try
                {
                    // pass through the call providing null for the set of SqlParameters
                    return ExecuteDataTable(connectionString, commandType, commandText, default);
                }
                catch (Exception)
                {
                    return default;
                }
            } // ExecuteDataTable

            // Execute a SqlCommand (that returns a resultset) against the database specified in the connection string 
            // using the provided parameters.
            // e.g.:  
            // Dim ds as Dataset = ExecuteDataTable(connString, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24))
            // Parameters:
            // -connectionString - a valid connection string for a SqlConnection
            // -commandType - the CommandType (stored procedure, text, etc.)
            // -commandText - the stored procedure name or T-SQL command
            // -commandParameters - an array of SqlParamters used to execute the command
            // Returns: a dataset containing the resultset generated by the command
            public new static DataTable ExecuteDataTable(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)


            {
                try
                {
                    // create & open a SqlConnection, and dispose of it after we are done.
                    var cn = new SqlConnection(connectionString);
                    try
                    {
                        cn.Open();

                        // call the overload that takes a connection in place of the connection string
                        return ExecuteDataTable(cn, commandType, commandText, commandParameters);
                    }
                    finally
                    {
                        cn.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    return default;
                }
            } // ExecuteDataTable

            // Execute a stored procedure via a SqlCommand (that returns a resultset) against the database specified in 
            // the connection string using the provided parameter values.  This method will discover the parameters for the 
            // stored procedure, and assign the values based on parameter order.
            // This method provides no access to output parameters or the stored procedure's return value parameter.
            // e.g.:  
            // Dim ds as Dataset= ExecuteDataTable(connString, "GetOrders", 24, 36)
            // Parameters:
            // -connectionString - a valid connection string for a SqlConnection
            // -spName - the name of the stored procedure
            // -parameterValues - an array of objects to be assigned as the input values of the stored procedure
            // Returns: a dataset containing the resultset generated by the command
            public static DataTable ExecuteDataTable(string connectionString, string spName, params object[] parameterValues)

            {
                try
                {
                    SqlParameter[] commandParameters;

                    // if we receive parameter values, we need to figure out where they go
                    if (parameterValues != null & parameterValues.Length > 0)
                    {
                        // pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                        commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);

                        // assign the provided values to these parameters based on parameter order
                        AssignParameterValues(commandParameters, parameterValues);

                        // call the overload that takes an array of SqlParameters
                        return ExecuteDataTable(connectionString, CommandType.StoredProcedure, spName, commandParameters);
                    }
                    // otherwise we can just call the SP without params
                    else
                    {
                        return ExecuteDataTable(connectionString, CommandType.StoredProcedure, spName);
                    }
                }
                catch (Exception)
                {
                    return default;
                }
            } // ExecuteDataTable

            // Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlConnection. 
            // e.g.:  
            // Dim ds as Dataset = ExecuteDataTable(conn, CommandType.StoredProcedure, "GetOrders")
            // Parameters:
            // -connection - a valid SqlConnection
            // -commandType - the CommandType (stored procedure, text, etc.)
            // -commandText - the stored procedure name or T-SQL command
            // Returns: a dataset containing the resultset generated by the command
            public static DataTable ExecuteDataTable(SqlConnection connection, CommandType commandType, string commandText)

            {
                try
                {
                    // pass through the call providing null for the set of SqlParameters
                    return ExecuteDataTable(connection, commandType, commandText, default);
                }
                catch (Exception)
                {
                    return default;
                }
            } // ExecuteDataTable

            // Execute a SqlCommand (that returns a resultset) against the specified SqlConnection 
            // using the provided parameters.
            // e.g.:  
            // Dim ds as Dataset = ExecuteDataTable(conn, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24))
            // Parameters:
            // -connection - a valid SqlConnection
            // -commandType - the CommandType (stored procedure, text, etc.)
            // -commandText - the stored procedure name or T-SQL command
            // -commandParameters - an array of SqlParamters used to execute the command
            // Returns: a dataset containing the resultset generated by the command
            public static DataTable ExecuteDataTable(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)


            {

                try
                {
                    // create a command and prepare it for execution
                    var cmd = new SqlCommand();
                    var ds = new DataTable();
                    SqlDataAdapter da;

                    PrepareCommand(cmd, connection, null, commandType, commandText, commandParameters);

                    // create the DataAdapter & DataSet
                    da = new SqlDataAdapter(cmd);

                    // fill the DataSet using default values for DataTable names, etc.
                    da.Fill(ds);

                    // detach the SqlParameters from the command object, so they can be used again
                    cmd.Parameters.Clear();

                    // return the dataset
                    return ds;
                }
                catch (Exception)
                {
                    return default;
                }
            } // ExecuteDataTable

            // Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlConnection 
            // using the provided parameter values.  This method will discover the parameters for the 
            // stored procedure, and assign the values based on parameter order.
            // This method provides no access to output parameters or the stored procedure's return value parameter.
            // e.g.:  
            // Dim ds As Dataset = ExecuteDataTable(conn, "GetOrders", 24, 36)
            // Parameters:
            // -connection - a valid SqlConnection
            // -spName - the name of the stored procedure
            // -parameterValues - an array of objects to be assigned as the input values of the stored procedure
            // Returns: a dataset containing the resultset generated by the command
            public static DataTable ExecuteDataTable(SqlConnection connection, string spName, params object[] parameterValues)

            {
                try
                {
                    // Return ExecuteDataTable(connection, spName, parameterValues)
                    SqlParameter[] commandParameters;

                    // if we receive parameter values, we need to figure out where they go
                    if (parameterValues != null & parameterValues.Length > 0)
                    {
                        // pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                        commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection.ConnectionString, spName);

                        // assign the provided values to these parameters based on parameter order
                        AssignParameterValues(commandParameters, parameterValues);

                        // call the overload that takes an array of SqlParameters
                        return ExecuteDataTable(connection, CommandType.StoredProcedure, spName, commandParameters);
                    }
                    // otherwise we can just call the SP without params
                    else
                    {
                        return ExecuteDataTable(connection, CommandType.StoredProcedure, spName);
                    }
                }
                catch (Exception)
                {
                    return default;
                }
            } // ExecuteDataTable


            // Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlTransaction. 
            // e.g.:  
            // Dim ds As Dataset = ExecuteDataTable(trans, CommandType.StoredProcedure, "GetOrders")
            // Parameters
            // -transaction - a valid SqlTransaction
            // -commandType - the CommandType (stored procedure, text, etc.)
            // -commandText - the stored procedure name or T-SQL command
            // Returns: a dataset containing the resultset generated by the command
            public static DataTable ExecuteDataTable(SqlTransaction transaction, CommandType commandType, string commandText)

            {
                try
                {
                    // pass through the call providing null for the set of SqlParameters
                    return ExecuteDataTable(transaction, commandType, commandText, default);
                }
                catch (Exception)
                {
                    return default;
                }
            } // ExecuteDataTable

            // Execute a SqlCommand (that returns a resultset) against the specified SqlTransaction
            // using the provided parameters.
            // e.g.:  
            // Dim ds As Dataset = ExecuteDataTable(trans, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24))
            // Parameters
            // -transaction - a valid SqlTransaction 
            // -commandType - the CommandType (stored procedure, text, etc.)
            // -commandText - the stored procedure name or T-SQL command
            // -commandParameters - an array of SqlParamters used to execute the command
            // Returns: a dataset containing the resultset generated by the command
            public static DataTable ExecuteDataTable(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)


            {
                try
                {
                    // create a command and prepare it for execution
                    var cmd = new SqlCommand();
                    var ds = new DataTable();
                    SqlDataAdapter da;

                    PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters);

                    // create the DataAdapter & DataSet
                    da = new SqlDataAdapter(cmd);

                    // fill the DataSet using default values for DataTable names, etc.
                    da.Fill(ds);

                    // detach the SqlParameters from the command object, so they can be used again
                    cmd.Parameters.Clear();

                    // return the dataset
                    return ds;
                }
                catch (Exception)
                {
                    return default;
                }
            } // ExecuteDataTable

            // Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified
            // SqlTransaction using the provided parameter values.  This method will discover the parameters for the 
            // stored procedure, and assign the values based on parameter order.
            // This method provides no access to output parameters or the stored procedure's return value parameter.
            // e.g.:  
            // Dim ds As Dataset = ExecuteDataTable(trans, "GetOrders", 24, 36)
            // Parameters:
            // -transaction - a valid SqlTransaction 
            // -spName - the name of the stored procedure
            // -parameterValues - an array of objects to be assigned as the input values of the stored procedure
            // Returns: a dataset containing the resultset generated by the command
            public static DataTable ExecuteDataTable(SqlTransaction transaction, string spName, params object[] parameterValues)

            {
                try
                {
                    SqlParameter[] commandParameters;

                    // if we receive parameter values, we need to figure out where they go
                    if (parameterValues != null & parameterValues.Length > 0)
                    {
                        // pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                        commandParameters = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection.ConnectionString, spName);

                        // assign the provided values to these parameters based on parameter order
                        AssignParameterValues(commandParameters, parameterValues);

                        // call the overload that takes an array of SqlParameters
                        return ExecuteDataTable(transaction, CommandType.StoredProcedure, spName, commandParameters);
                    }
                    // otherwise we can just call the SP without params
                    else
                    {
                        return ExecuteDataTable(transaction, CommandType.StoredProcedure, spName);
                    }
                }
                catch (Exception)
                {
                    return default;
                }
            } // ExecuteDataTable

            private static void AssignParameterValues(SqlParameter[] commandParameters, object[] parameterValues)
            {
                try
                {
                    short i;
                    short j;

                    if (commandParameters is null & parameterValues is null)
                    {
                        // do nothing if we get no data
                        return;
                    }

                    // we must have the same number of values as we pave parameters to put them in
                    if (commandParameters.Length != parameterValues.Length)
                    {
                        throw new ArgumentException("Parameter count does not match Parameter Value count.");
                    }

                    // value array
                    j = (short)(commandParameters.Length - 1);
                    var loopTo = j;
                    for (i = 0; i <= loopTo; i++)
                        commandParameters[i].Value = parameterValues[i];
                }
                catch (Exception)
                {
                }
            }

        #endregion



            #region ExecuteDataset

            // Execute a SqlCommand (that returns a resultset and takes no parameters) against the database specified in 
            // the connection string. 
            // e.g.:  
            // Dim ds As DataSet = SqlHelper.ExecuteDataset("", commandType.StoredProcedure, "GetOrders")
            // Parameters:
            // -connectionString - a valid connection string for a SqlConnection
            // -commandType - the CommandType (stored procedure, text, etc.)
            // -commandText - the stored procedure name or T-SQL command
            // Returns: a dataset containing the resultset generated by the command
            public static DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText)

            {
                try
                {
                    // pass through the call providing null for the set of SqlParameters
                    return ExecuteDataset(connectionString, commandType, commandText, default);
                }
                catch (Exception)
                {
                    return default;
                }
            } // ExecuteDataset

            // Execute a SqlCommand (that returns a resultset) against the database specified in the connection string 
            // using the provided parameters.
            // e.g.:  
            // Dim ds as Dataset = ExecuteDataset(connString, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24))
            // Parameters:
            // -connectionString - a valid connection string for a SqlConnection
            // -commandType - the CommandType (stored procedure, text, etc.)
            // -commandText - the stored procedure name or T-SQL command
            // -commandParameters - an array of SqlParamters used to execute the command
            // Returns: a dataset containing the resultset generated by the command
            public static DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)


            {
                try
                {
                    // create & open a SqlConnection, and dispose of it after we are done.
                    var cn = new SqlConnection(connectionString);
                    try
                    {
                        cn.Open();

                        // call the overload that takes a connection in place of the connection string
                        return ExecuteDataset(cn, commandType, commandText, commandParameters);
                    }
                    finally
                    {
                        cn.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    return default;
                }
            } // ExecuteDataset

            // Execute a stored procedure via a SqlCommand (that returns a resultset) against the database specified in 
            // the connection string using the provided parameter values.  This method will discover the parameters for the 
            // stored procedure, and assign the values based on parameter order.
            // This method provides no access to output parameters or the stored procedure's return value parameter.
            // e.g.:  
            // Dim ds as Dataset= ExecuteDataset(connString, "GetOrders", 24, 36)
            // Parameters:
            // -connectionString - a valid connection string for a SqlConnection
            // -spName - the name of the stored procedure
            // -parameterValues - an array of objects to be assigned as the input values of the stored procedure
            // Returns: a dataset containing the resultset generated by the command
            public static DataSet ExecuteDataset(string connectionString, string spName, params object[] parameterValues)

            {
                try
                {
                    SqlParameter[] commandParameters;

                    // if we receive parameter values, we need to figure out where they go
                    if (parameterValues != null & parameterValues.Length > 0)
                    {
                        // pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                        commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);

                        // assign the provided values to these parameters based on parameter order
                        AssignParameterValues(commandParameters, parameterValues);

                        // call the overload that takes an array of SqlParameters
                        return ExecuteDataset(connectionString, CommandType.StoredProcedure, spName, commandParameters);
                    }
                    // otherwise we can just call the SP without params
                    else
                    {
                        return ExecuteDataset(connectionString, CommandType.StoredProcedure, spName);
                    }
                }
                catch (Exception)
                {
                    return default;
                }
            } // ExecuteDataset

            // Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlConnection. 
            // e.g.:  
            // Dim ds as Dataset = ExecuteDataset(conn, CommandType.StoredProcedure, "GetOrders")
            // Parameters:
            // -connection - a valid SqlConnection
            // -commandType - the CommandType (stored procedure, text, etc.)
            // -commandText - the stored procedure name or T-SQL command
            // Returns: a dataset containing the resultset generated by the command
            public static DataSet ExecuteDataset(SqlConnection connection, CommandType commandType, string commandText)

            {
                try
                {
                    // pass through the call providing null for the set of SqlParameters
                    return ExecuteDataset(connection, commandType, commandText, default);
                }
                catch (Exception)
                {
                    return default;
                }
            } // ExecuteDataset

            // Execute a SqlCommand (that returns a resultset) against the specified SqlConnection 
            // using the provided parameters.
            // e.g.:  
            // Dim ds as Dataset = ExecuteDataset(conn, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24))
            // Parameters:
            // -connection - a valid SqlConnection
            // -commandType - the CommandType (stored procedure, text, etc.)
            // -commandText - the stored procedure name or T-SQL command
            // -commandParameters - an array of SqlParamters used to execute the command
            // Returns: a dataset containing the resultset generated by the command
            public static DataSet ExecuteDataset(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)


            {
                try
                {
                    // create a command and prepare it for execution
                    var cmd = new SqlCommand();
                    var ds = new DataSet();
                    SqlDataAdapter da;

                    PrepareCommand(cmd, connection, null, commandType, commandText, commandParameters);

                    // create the DataAdapter & DataSet
                    da = new SqlDataAdapter(cmd);

                    // fill the DataSet using default values for DataTable names, etc.
                    da.Fill(ds);

                    // detach the SqlParameters from the command object, so they can be used again
                    cmd.Parameters.Clear();

                    // return the dataset
                    return ds;
                }
                catch (Exception)
                {
                    return default;
                }
            } // ExecuteDataset

            // Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlConnection 
            // using the provided parameter values.  This method will discover the parameters for the 
            // stored procedure, and assign the values based on parameter order.
            // This method provides no access to output parameters or the stored procedure's return value parameter.
            // e.g.:  
            // Dim ds As Dataset = ExecuteDataset(conn, "GetOrders", 24, 36)
            // Parameters:
            // -connection - a valid SqlConnection
            // -spName - the name of the stored procedure
            // -parameterValues - an array of objects to be assigned as the input values of the stored procedure
            // Returns: a dataset containing the resultset generated by the command
            public static DataSet ExecuteDataset(SqlConnection connection, string spName, params object[] parameterValues)

            {
                try
                {
                    // Return ExecuteDataset(connection, spName, parameterValues)
                    SqlParameter[] commandParameters;

                    // if we receive parameter values, we need to figure out where they go
                    if (parameterValues != null & parameterValues.Length > 0)
                    {
                        // pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                        commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection.ConnectionString, spName);

                        // assign the provided values to these parameters based on parameter order
                        AssignParameterValues(commandParameters, parameterValues);

                        // call the overload that takes an array of SqlParameters
                        return ExecuteDataset(connection, CommandType.StoredProcedure, spName, commandParameters);
                    }
                    // otherwise we can just call the SP without params
                    else
                    {
                        return ExecuteDataset(connection, CommandType.StoredProcedure, spName);
                    }
                }
                catch (Exception)
                {
                    return default;
                }
            } // ExecuteDataset


            // Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlTransaction. 
            // e.g.:  
            // Dim ds As Dataset = ExecuteDataset(trans, CommandType.StoredProcedure, "GetOrders")
            // Parameters
            // -transaction - a valid SqlTransaction
            // -commandType - the CommandType (stored procedure, text, etc.)
            // -commandText - the stored procedure name or T-SQL command
            // Returns: a dataset containing the resultset generated by the command
            public static DataSet ExecuteDataset(SqlTransaction transaction, CommandType commandType, string commandText)

            {
                try
                {
                    // pass through the call providing null for the set of SqlParameters
                    return ExecuteDataset(transaction, commandType, commandText, default);
                }
                catch (Exception)
                {
                    return default;
                }
            } // ExecuteDataset

            // Execute a SqlCommand (that returns a resultset) against the specified SqlTransaction
            // using the provided parameters.
            // e.g.:  
            // Dim ds As Dataset = ExecuteDataset(trans, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24))
            // Parameters
            // -transaction - a valid SqlTransaction 
            // -commandType - the CommandType (stored procedure, text, etc.)
            // -commandText - the stored procedure name or T-SQL command
            // -commandParameters - an array of SqlParamters used to execute the command
            // Returns: a dataset containing the resultset generated by the command
            public static DataSet ExecuteDataset(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)


            {
                try
                {
                    // create a command and prepare it for execution
                    var cmd = new SqlCommand();
                    var ds = new DataSet();
                    SqlDataAdapter da;

                    PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters);

                    // create the DataAdapter & DataSet
                    da = new SqlDataAdapter(cmd);

                    // fill the DataSet using default values for DataTable names, etc.
                    da.Fill(ds);

                    // detach the SqlParameters from the command object, so they can be used again
                    cmd.Parameters.Clear();

                    // return the dataset
                    return ds;
                }
                catch (Exception)
                {
                    return default;
                }
            } // ExecuteDataset

            // Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified
            // SqlTransaction using the provided parameter values.  This method will discover the parameters for the 
            // stored procedure, and assign the values based on parameter order.
            // This method provides no access to output parameters or the stored procedure's return value parameter.
            // e.g.:  
            // Dim ds As Dataset = ExecuteDataset(trans, "GetOrders", 24, 36)
            // Parameters:
            // -transaction - a valid SqlTransaction 
            // -spName - the name of the stored procedure
            // -parameterValues - an array of objects to be assigned as the input values of the stored procedure
            // Returns: a dataset containing the resultset generated by the command
            public static DataSet ExecuteDataset(SqlTransaction transaction, string spName, params object[] parameterValues)

            {
                try
                {
                    SqlParameter[] commandParameters;

                    // if we receive parameter values, we need to figure out where they go
                    if (parameterValues != null & parameterValues.Length > 0)
                    {
                        // pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                        commandParameters = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection.ConnectionString, spName);

                        // assign the provided values to these parameters based on parameter order
                        AssignParameterValues(commandParameters, parameterValues);

                        // call the overload that takes an array of SqlParameters
                        return ExecuteDataset(transaction, CommandType.StoredProcedure, spName, commandParameters);
                    }
                    // otherwise we can just call the SP without params
                    else
                    {
                        return ExecuteDataset(transaction, CommandType.StoredProcedure, spName);
                    }
                }
                catch (Exception)
                {
                    return default;
                }
            } // ExecuteDataset

        #endregion

        #region ExecuteScalar

        // Execute a SqlCommand (that returns a 1x1 resultset and takes no parameters) against the database specified in 
        // the connection string. 
        // e.g.:  
        // Dim orderCount As Integer = CInt(ExecuteScalar(connString, CommandType.StoredProcedure, "GetOrderCount"))
        // Parameters:
        // -connectionString - a valid connection string for a SqlConnection 
        // -commandType - the CommandType (stored procedure, text, etc.) 
        // -commandText - the stored procedure name or T-SQL command 
        // Returns: an object containing the value in the 1x1 resultset generated by the command
        public static object ExecuteScalar(string connectionString, CommandType commandType, string commandText)

        {
            try
            {
                // pass through the call providing null for the set of SqlParameters
                return ExecuteScalar(connectionString, commandType, commandText, default);
            }
            catch (Exception)
            {
                return null;
            }
        } // ExecuteScalar

        // Execute a SqlCommand (that returns a 1x1 resultset) against the database specified in the connection string 
        // using the provided parameters.
        // e.g.:  
        // Dim orderCount As Integer = Cint(ExecuteScalar(connString, CommandType.StoredProcedure, "GetOrderCount", new SqlParameter("@prodid", 24)))
        // Parameters:
        // -connectionString - a valid connection string for a SqlConnection 
        // -commandType - the CommandType (stored procedure, text, etc.) 
        // -commandText - the stored procedure name or T-SQL command 
        // -commandParameters - an array of SqlParamters used to execute the command 
        // Returns: an object containing the value in the 1x1 resultset generated by the command 
        public static object ExecuteScalar(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)


        {
            try
            {
                // create & open a SqlConnection, and dispose of it after we are done.
                var cn = new SqlConnection(connectionString);
                try
                {
                    cn.Open();

                    // call the overload that takes a connection in place of the connection string
                    return ExecuteScalar(cn, commandType, commandText, commandParameters);
                }
                finally
                {
                    cn.Dispose();
                }
            }
            catch (Exception)
            {
                return null;
            }
        } // ExecuteScalar

        // Execute a stored procedure via a SqlCommand (that returns a 1x1 resultset) against the database specified in 
        // the connection string using the provided parameter values.  This method will discover the parameters for the 
        // stored procedure, and assign the values based on parameter order.
        // This method provides no access to output parameters or the stored procedure's return value parameter.
        // e.g.:  
        // Dim orderCount As Integer = CInt(ExecuteScalar(connString, "GetOrderCount", 24, 36))
        // Parameters:
        // -connectionString - a valid connection string for a SqlConnection 
        // -spName - the name of the stored procedure 
        // -parameterValues - an array of objects to be assigned as the input values of the stored procedure 
        // Returns: an object containing the value in the 1x1 resultset generated by the command 
        public static object ExecuteScalar(string connectionString, string spName, params object[] parameterValues)

        {
            try
            {
                SqlParameter[] commandParameters;

                // if we receive parameter values, we need to figure out where they go
                if (parameterValues != null & parameterValues.Length > 0)
                {
                    // pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                    commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);

                    // assign the provided values to these parameters based on parameter order
                    AssignParameterValues(commandParameters, parameterValues);

                    // call the overload that takes an array of SqlParameters
                    return ExecuteScalar(connectionString, CommandType.StoredProcedure, spName, commandParameters);
                }
                // otherwise we can just call the SP without params
                else
                {
                    return ExecuteScalar(connectionString, CommandType.StoredProcedure, spName);
                }
            }
            catch (Exception)
            {
                return null;
            }
        } // ExecuteScalar

        // Execute a SqlCommand (that returns a 1x1 resultset and takes no parameters) against the provided SqlConnection. 
        // e.g.:  
        // Dim orderCount As Integer = CInt(ExecuteScalar(conn, CommandType.StoredProcedure, "GetOrderCount"))
        // Parameters:
        // -connection - a valid SqlConnection 
        // -commandType - the CommandType (stored procedure, text, etc.) 
        // -commandText - the stored procedure name or T-SQL command 
        // Returns: an object containing the value in the 1x1 resultset generated by the command 
        public static object ExecuteScalar(SqlConnection connection, CommandType commandType, string commandText)

        {
            try
            {
                // pass through the call providing null for the set of SqlParameters
                return ExecuteScalar(connection, commandType, commandText, default);
            }
            catch (Exception)
            {
                return null;
            }
        } // ExecuteScalar

        // Execute a SqlCommand (that returns a 1x1 resultset) against the specified SqlConnection 
        // using the provided parameters.
        // e.g.:  
        // Dim orderCount As Integer = CInt(ExecuteScalar(conn, CommandType.StoredProcedure, "GetOrderCount", new SqlParameter("@prodid", 24)))
        // Parameters:
        // -connection - a valid SqlConnection 
        // -commandType - the CommandType (stored procedure, text, etc.) 
        // -commandText - the stored procedure name or T-SQL command 
        // -commandParameters - an array of SqlParamters used to execute the command 
        // Returns: an object containing the value in the 1x1 resultset generated by the command 
        public static object ExecuteScalar(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)


        {
            try
            {
                // create a command and prepare it for execution
                var cmd = new SqlCommand();
                object retval;

                PrepareCommand(cmd, connection, null, commandType, commandText, commandParameters);

                // execute the command & return the results
                retval = cmd.ExecuteScalar();

                // detach the SqlParameters from the command object, so they can be used again
                cmd.Parameters.Clear();

                return retval;
            }
            catch (Exception)
            {
                return null;
            }
        } // ExecuteScalar

        // Execute a stored procedure via a SqlCommand (that returns a 1x1 resultset) against the specified SqlConnection 
        // using the provided parameter values.  This method will discover the parameters for the 
        // stored procedure, and assign the values based on parameter order.
        // This method provides no access to output parameters or the stored procedure's return value parameter.
        // e.g.:  
        // Dim orderCount As Integer = CInt(ExecuteScalar(conn, "GetOrderCount", 24, 36))
        // Parameters:
        // -connection - a valid SqlConnection 
        // -spName - the name of the stored procedure 
        // -parameterValues - an array of objects to be assigned as the input values of the stored procedure 
        // Returns: an object containing the value in the 1x1 resultset generated by the command 
        public static object ExecuteScalar(SqlConnection connection, string spName, params object[] parameterValues)

        {
            try
            {
                SqlParameter[] commandParameters;

                // if we receive parameter values, we need to figure out where they go
                if (parameterValues != null & parameterValues.Length > 0)
                {
                    // pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                    commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection.ConnectionString, spName);

                    // assign the provided values to these parameters based on parameter order
                    AssignParameterValues(commandParameters, parameterValues);

                    // call the overload that takes an array of SqlParameters
                    return ExecuteScalar(connection, CommandType.StoredProcedure, spName, commandParameters);
                }
                // otherwise we can just call the SP without params
                else
                {
                    return ExecuteScalar(connection, CommandType.StoredProcedure, spName);
                }
            }
            catch (Exception)
            {
                return null;
            }
        } // ExecuteScalar

        // Execute a SqlCommand (that returns a 1x1 resultset and takes no parameters) against the provided SqlTransaction.
        // e.g.:  
        // Dim orderCount As Integer  = CInt(ExecuteScalar(trans, CommandType.StoredProcedure, "GetOrderCount"))
        // Parameters:
        // -transaction - a valid SqlTransaction 
        // -commandType - the CommandType (stored procedure, text, etc.) 
        // -commandText - the stored procedure name or T-SQL command 
        // Returns: an object containing the value in the 1x1 resultset generated by the command 
        public static object ExecuteScalar(SqlTransaction transaction, CommandType commandType, string commandText)

        {
            try
            {
                // pass through the call providing null for the set of SqlParameters
                return ExecuteScalar(transaction, commandType, commandText, default);
            }
            catch (Exception)
            {
                return null;
            }
        } // ExecuteScalar

        // Execute a SqlCommand (that returns a 1x1 resultset) against the specified SqlTransaction
        // using the provided parameters.
        // e.g.:  
        // Dim orderCount As Integer = CInt(ExecuteScalar(trans, CommandType.StoredProcedure, "GetOrderCount", new SqlParameter("@prodid", 24)))
        // Parameters:
        // -transaction - a valid SqlTransaction  
        // -commandType - the CommandType (stored procedure, text, etc.) 
        // -commandText - the stored procedure name or T-SQL command 
        // -commandParameters - an array of SqlParamters used to execute the command 
        // Returns: an object containing the value in the 1x1 resultset generated by the command 
        public static object ExecuteScalar(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)


        {
            try
            {
                // create a command and prepare it for execution
                var cmd = new SqlCommand();
                object retval;

                PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters);

                // execute the command & return the results
                retval = cmd.ExecuteScalar();

                // detach the SqlParameters from the command object, so they can be used again
                cmd.Parameters.Clear();

                return retval;
            }
            catch (Exception)
            {
                return null;
            }
        } // ExecuteScalar

        // Execute a stored procedure via a SqlCommand (that returns a 1x1 resultset) against the specified SqlTransaction 
        // using the provided parameter values.  This method will discover the parameters for the 
        // stored procedure, and assign the values based on parameter order.
        // This method provides no access to output parameters or the stored procedure's return value parameter.
        // e.g.:  
        // Dim orderCount As Integer = CInt(ExecuteScalar(trans, "GetOrderCount", 24, 36))
        // Parameters:
        // -transaction - a valid SqlTransaction 
        // -spName - the name of the stored procedure 
        // -parameterValues - an array of objects to be assigned as the input values of the stored procedure 
        // Returns: an object containing the value in the 1x1 resultset generated by the command 
        public static object ExecuteScalar(SqlTransaction transaction, string spName, params object[] parameterValues)

        {
            try
            {
                SqlParameter[] commandParameters;

                // if we receive parameter values, we need to figure out where they go
                if (parameterValues != null & parameterValues.Length > 0)
                {
                    // pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                    commandParameters = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection.ConnectionString, spName);

                    // assign the provided values to these parameters based on parameter order
                    AssignParameterValues(commandParameters, parameterValues);

                    // call the overload that takes an array of SqlParameters
                    return ExecuteScalar(transaction, CommandType.StoredProcedure, spName, commandParameters);
                }
                // otherwise we can just call the SP without params
                else
                {
                    return ExecuteScalar(transaction, CommandType.StoredProcedure, spName);
                }
            }
            catch (Exception)
            {
                return null;
            }
        } // ExecuteScalar

        #endregion
        #endregion
    }


    public sealed partial class SqlHelperParameterCache
    {

        #region private methods, variables, and constructors


        // Since this class provides only static methods, make the default constructor private to prevent 
        // instances from being created with "new SqlHelperParameterCache()".
        private SqlHelperParameterCache()
        {
        } // New 

        private static Hashtable paramCache = Hashtable.Synchronized(new Hashtable());

        // resolve at run time the appropriate set of SqlParameters for a stored procedure
        // Parameters:
        // - connectionString - a valid connection string for a SqlConnection
        // - spName - the name of the stored procedure
        // - includeReturnValueParameter - whether or not to include their return value parameter>
        // Returns: SqlParameter()
        private static SqlParameter[] DiscoverSpParameterSet(string connectionString, string spName, bool includeReturnValueParameter, params object[] parameterValues)


        {

            try
            {
                var cn = new SqlConnection(connectionString);
                var cmd = new SqlCommand(spName, cn);
                SqlParameter[] discoveredParameters;

                try
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlCommandBuilder.DeriveParameters(cmd);
                    if (!includeReturnValueParameter)
                    {
                        cmd.Parameters.RemoveAt(0);
                    }

                    discoveredParameters = new SqlParameter[cmd.Parameters.Count];
                    cmd.Parameters.CopyTo(discoveredParameters, 0);
                }
                finally
                {
                    cmd.Dispose();
                    cn.Dispose();

                }

                return discoveredParameters;
            }
            catch (Exception)
            {
                return null;
            }
        } // DiscoverSpParameterSet

        // deep copy of cached SqlParameter array
        private static SqlParameter[] CloneParameters(SqlParameter[] originalParameters)
        {
            try
            {
                short i;
                short j = (short)(originalParameters.Length - 1);
                var clonedParameters = new SqlParameter[(j + 1)];

                var loopTo = j;
                for (i = 0; i <= loopTo; i++)
                    clonedParameters[i] = originalParameters[i];

                return clonedParameters;
            }
            catch (Exception)
            {
                return null;
            }
        } // CloneParameters

        #endregion

        #region caching functions

        // add parameter array to the cache
        // Parameters
        // -connectionString - a valid connection string for a SqlConnection 
        // -commandText - the stored procedure name or T-SQL command 
        // -commandParameters - an array of SqlParamters to be cached 
        public static void CacheParameterSet(string connectionString, string commandText, params SqlParameter[] commandParameters)

        {
            try
            {
                string hashKey = connectionString + ":" + commandText;

                paramCache[hashKey] = commandParameters;
            }
            catch (Exception)
            {
            }
        } // CacheParameterSet

        // retrieve a parameter array from the cache
        // Parameters:
        // -connectionString - a valid connection string for a SqlConnection 
        // -commandText - the stored procedure name or T-SQL command 
        // Returns: an array of SqlParamters 
        public static SqlParameter[] GetCachedParameterSet(string connectionString, string commandText)
        {
            try
            {
                string hashKey = connectionString + ":" + commandText;
                SqlParameter[] cachedParameters = (SqlParameter[])paramCache[hashKey];

                if (cachedParameters is null)
                {
                    return null;
                }
                else
                {
                    return CloneParameters(cachedParameters);
                }
            }
            catch (Exception)
            {
                return null;
            }
        } // GetCachedParameterSet

        #endregion

        #region Parameter Discovery Functions
        // Retrieves the set of SqlParameters appropriate for the stored procedure
        // 
        // This method will query the database for this information, and then store it in a cache for future requests.
        // Parameters:
        // -connectionString - a valid connection string for a SqlConnection 
        // -spName - the name of the stored procedure 
        // Returns: an array of SqlParameters
        public static SqlParameter[] GetSpParameterSet(string connectionString, string spName)
        {
            try
            {
                return GetSpParameterSet(connectionString, spName, false);
            }
            catch (Exception)
            {
                return null;
            }
        } // GetSpParameterSet 

        // Retrieves the set of SqlParameters appropriate for the stored procedure
        // 
        // This method will query the database for this information, and then store it in a cache for future requests.
        // Parameters:
        // -connectionString - a valid connection string for a SqlConnection
        // -spName - the name of the stored procedure 
        // -includeReturnValueParameter - a bool value indicating whether the return value parameter should be included in the results 
        // Returns: an array of SqlParameters 
        public static SqlParameter[] GetSpParameterSet(string connectionString, string spName, bool includeReturnValueParameter)

        {
            try
            {
                SqlParameter[] cachedParameters;
                string hashKey;

                hashKey = string.Format( connectionString + ":" + spName, includeReturnValueParameter == true? ":include ReturnValue Parameter": "");

                cachedParameters = (SqlParameter[])paramCache[hashKey];

                if (cachedParameters is null)
                {
                    paramCache[hashKey] = DiscoverSpParameterSet(connectionString, spName, includeReturnValueParameter);
                    cachedParameters = (SqlParameter[])paramCache[hashKey];

                }

                return CloneParameters(cachedParameters);
            }
            catch (Exception)
            {
                return null;
            }
        } // GetSpParameterSet
        #endregion

    }
}