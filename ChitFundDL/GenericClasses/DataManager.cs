//Using System;
//Using Microsoft.VisualBasic;
//Using System.Data;
//Using System.Data.SqlClient;


//Public Class DataManager
//{
//    Public Static DataTable ExecuteQuery(String query)
//    {
//        Try
//        {
//            SqlConnection myConnection = New SqlConnection(ConnectionSettings.cnString);
//            SqlDataAdapter myAdapter = New SqlDataAdapter(query, myConnection);
//            myAdapter.SelectCommand.CommandTimeout = 6000;
//            DataTable dt = New DataTable();
//            myAdapter.Fill(dt);
//            dt.TableName = "";
//            If (dt.Rows.Count > 0)
//    Return dt;
//            Else
//    Return New DataTable();
//        }
//        Catch (Exception ex)
//        {
//            Return New DataTable();
//        }
//    }
//    Public Static Int16 ExecuteScalar(String query)
//    {
//        SqlConnection myConnection;
//        myConnection = New SqlConnection(ConnectionSettings.cnString);
//        Try
//        {
//            myConnection.Open();
//            System.Data.SqlClient.SqlCommand ScalarQuery = New System.Data.SqlClient.SqlCommand(query, myConnection);
//            Return (System.Convert.ToInt32(ScalarQuery.ExecuteScalar()));
//        }
//        Catch (Exception ex)
//        {
//            Interaction.MsgBox(ex.Message);
//        }
//        Finally
//        {
//            If (myConnection.State == ConnectionState.Open)
//                myConnection.Close();
//        }
//    }
//    Public Static void ExecuteNonQuery(String query)
//    {
//        SqlConnection myConnection;
//        myConnection = New SqlConnection(ConnectionSettings.cnString);
//        Try
//        {
//            myConnection.Open();

//            SqlCommand myCommand = New SqlCommand(query, myConnection);
//            myCommand.ExecuteNonQuery();
//        }
//        Catch (Exception ex)
//        {
//            Try
//            {
//                Utils.LogError(ex, "Error in ExecuteQuery() method in DataManager. <BR><BR>" + query);
//            }
//            Catch
//            {
//            }
//            Throw;
//        }
//        Finally
//        {
//            If (myConnection.State == ConnectionState.Open)
//                myConnection.Close();
//        }
//    }
//}

//Public Class DataManagerTransaction
//{
//    Private SqlConnection con;
//    Private SqlTransaction trans;

//    Public void SQLBeginTransaction()
//    {
//        con = New SqlConnection(ConnectionSettings.cnString);
//        con.Open();
//        trans = con.BeginTransaction(IsolationLevel.ReadUncommitted);
//    }

//    Public bool ExecuteNonQuery(String query)
//    {
//        Try
//        {
//            SqlCommand myCommand = New SqlCommand(query, con, trans);
//            myCommand.ExecuteNonQuery();
//            Return True;
//        }
//        Catch (Exception ex)
//        {
//            Return False;
//        }
//    }

//    Public DataTable ExecuteQuery(String query)
//    {
//        Try
//        {
//            // Dim myConnection As SqlConnection = New SqlConnection(ConnectionSettings.cnString)
//            SqlDataAdapter myAdapter = New SqlDataAdapter(New SqlCommand(query, con, trans));
//            DataTable dt = New DataTable();
//            myAdapter.Fill(dt);

//            dt.TableName = "";
//            If (dt.Rows.Count > 0)
//        Return dt;
//            Else
//        Return New DataTable();
//        }
//        Catch (Exception ex)
//        {
//            Try
//            {
//                Utils.LogError(ex, "Error in Transaction ExecuteQuery . " + Constants.vbCrLf + query);
//            }
//            Catch
//            {
//            }
//            Return New DataTable();
//        }
//    }

//    Public void RollBack()
//    {
//        trans.Rollback();
//        con.Close();
//    }

//    Public void SQLCloseTransaction()
//    {
//        trans.Commit();
//        con.Close();
//    }
//}
