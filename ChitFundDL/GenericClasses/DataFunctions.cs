//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Globalization;
//using System.IO;
//using System.Linq;
//using System.Reflection;
//using System.Runtime.CompilerServices;
//using System.Security;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.VisualBasic;
//using System.Data.SqlClient;
//using SQL;
//using SQL1;
//using System.Data;

//public static class DataFunctions
//{
//    private static SqlConnection con;
//    private static SqlTransaction Trans;
//    public static String CurrentUserName;
//    public static void InsertExceptionTrackingDC(Exception Ex, String strFormName = "", String strFunctionName = "")
//    {
//        try
//        {
//            SqlParameter[] @params = new SqlParameter[5];
//            @params[0] = new SqlParameter("@colErrorMessage", Ex.Message);
//            @params[1] = new SqlParameter("@colStackTrace", Ex.StackTrace);
//            @params[2] = new SqlParameter("@colLoginBy", CurrentUserName);
//            @params[3] = new SqlParameter("@FormName", strFormName);
//            @params[4] = new SqlParameter("@FunctionName", strFunctionName);
//            SQLHelper.ExecuteNonQuery(ConnectionSettings.cnString, CommandType.StoredProcedure, "ET_InsertExceptionTracking_SP", @params);
//        }
//        catch (Exception ex1)
//        {
//        }
//    }

//    public static bool CheckFullPremission(String UserName)
//    {
//        try
//        {
//            SqlParameter[] @params = new SqlParameter[1];
//            @params[0] = new SqlParameter("@UserName", UserName);
//            DataTable dt = SQLHelper.ExecuteDataTable(ConnectionSettings.cnString, CommandType.StoredProcedure, "GetFullAccess_SP", @params);
//            if (dt != null && dt.Rows.Count > 0)
//    return true;
//            else
//    return false;
//        }
//        catch (Exception ex)
//        {
//            return false;
//        }
//    }
//    public static void InsertExceptionTrackingFormDC(int UserID, String VersionNo)
//    {
//        try
//        {
//            SqlParameter[] @params = new SqlParameter[2];
//            @params[0] = new SqlParameter("@UserID", UserID);
//            @params[1] = new SqlParameter("@VersionNo", VersionNo);
//            SQLHelper.ExecuteNonQuery(ConnectionSettings.cnString, CommandType.StoredProcedure, "ET_InsertExceptionTrackingForm_SP", @params);
//        }
//        catch (Exception ex1)
//        {
//        }
//    }
//    public static void UpdateExceptionTrackingFormDC(int UserID, String strFormName)
//    {
//        try
//        {
//            SqlParameter[] @params = new SqlParameter[2] ;
//            @params[0] = new SqlParameter("@UserID", UserID);
//            @params[1] = new SqlParameter("@FormName", strFormName);
//            SQLHelper.ExecuteNonQuery(ConnectionSettings.cnString, CommandType.StoredProcedure, "ET_UpdateExceptionTrackingForm_SP", @params);
//        }
//        catch (Exception ex1)
//        {
//        }
//    }
//    public static void RemoveExceptionTrackingFormDC(int UserID)
//    {
//        try
//        {
//            SqlParameter[] @params = new SqlParameter[2] ;
//            @params[0] = new SqlParameter("@UserID", UserID);
//            SQLHelper.ExecuteNonQuery(ConnectionSettings.cnString, CommandType.StoredProcedure, "ET_RemoveExceptionTrackingForm_SP", @params);
//        }
//        catch (Exception ex1)
//        {
//        }
//    }
//    public static int EBF_CheckValidation(String Type, int uqQCBatchID, int QCSampleID, int QCTypeID)
//    {
//        try
//        {
//            SqlParameter[] @params = new SqlParameter[4] ;
//            @params[0] = new SqlParameter("@Type", Type);
//            @params[1] = new SqlParameter("@QCBatchID", uqQCBatchID);
//            @params[2] = new SqlParameter("@QCSampleID", QCSampleID);
//            @params[3] = new SqlParameter("@QCTypeID", QCTypeID);
//            int Count = SQLHelper.ExecuteScalar(ConnectionSettings.cnString, CommandType.StoredProcedure, "EBF_QCSampleCheckStatus_SP", @params);
//            return Count;
//        }
//        catch (Exception ex)
//        {
//            return 0;
//        }
//    }
//    public static bool EBF_DeleteSampleResult(DataRow[] drSample, String Mode)
//    {
//        try
//        {
//            if (!drSample == null && drSample.Length)
//            {
//                DataRow dr;
//                foreach (var dr in drSample)
//                {
//                    if (dr("All") == true)
//                    {
//                        SqlParameter[] @params = new SqlParameter[2] ;
//                        if (dr.Table.Columns.Contains("coluqSampleParameterID") == true)
//                            @params[0] = new SqlParameter("@coluqSampleParameterID", dr("coluqSampleParameterID"));
//                        if (dr.Table.Columns.Contains("SampleParameterID") == true)
//                            @params[0] = new SqlParameter("@coluqSampleParameterID", dr("SampleParameterID"));
//                        @params[1] = new SqlParameter("@colMode", Mode);
//                        SQLHelper.ExecuteNonQuery(ConnectionSettings.cnString, CommandType.StoredProcedure, "RE_DeleteSampleResult_SP", @params);

//                        SqlParameter[] params1 = new SqlParameter[2] ;
//                        if (dr.Table.Columns.Contains("coluqSampleTestID") == true)
//                            params1[0] = new SqlParameter("@coluqSampleTestID", dr("coluqSampleTestID"));
//                        if (dr.Table.Columns.Contains("SampleTestID") == true)
//                            params1[0] = new SqlParameter("@coluqSampleTestID", dr("SampleTestID"));
//                        params1[1] = new SqlParameter("@colFunctionTypeID", 5);
//                        SQLHelper.ExecuteNonQuery(ConnectionSettings.cnString, CommandType.StoredProcedure, "Env_SetDeleteStatus_SP", params1);
//                    }
//                }
//            }
//            return true;
//        }
//        catch (Exception ex)
//        {
//            return false;
//        }
//    }
//    public static bool EBF_DeleteQCSampleResult(DataRow dr, String Type)
//    {
//        try
//        {
//            SqlParameter[] @params = new SqlParameter[4] ;
//            @params[0] = new SqlParameter("@Type", Type);
//            if (Type == "Blank")
//            {
//                if (dr.Table.Columns.Contains("coluqTestParameterID") == true)
//                    @params[1] = new SqlParameter("@QCSampleID", dr("coluqTestParameterID"));
//                 else if (dr.Table.Columns.Contains("TestParameterID") == true)
//                    @params[1] = new SqlParameter("@QCSampleID", dr("TestParameterID"));
//            }
//             else if (Type == "Spike")
//            {
//                if (dr.Table.Columns.Contains("coluqTestQCSpike") == true)
//                    @params[1] = new SqlParameter("@QCSampleID", dr("coluqTestQCSpike"));
//                 else if (dr.Table.Columns.Contains("TestQCSpike") == true)
//                    @params[1] = new SqlParameter("@QCSampleID", dr("TestQCSpike"));
//            }
//             else if (Type == "Standard")
//            {
//                if (dr.Table.Columns.Contains("coluqTestQCStandardSpikeID") == true)
//                    @params[1] = new SqlParameter("@QCSampleID", dr("coluqTestQCStandardSpikeID"));
//                 else if (dr.Table.Columns.Contains("TestQCstdSpikeID") == true)
//                    @params[1] = new SqlParameter("@QCSampleID", dr("TestQCstdSpikeID"));
//            }
//            if (dr.Table.Columns.Contains("coluqQCTypeID") == true)
//                @params[2] = new SqlParameter("@QCTypeID", dr("coluqQCTypeID"));
//             else if (dr.Table.Columns.Contains("uqQCTypeID") == true)
//                @params[2] = new SqlParameter("@QCTypeID", dr("uqQCTypeID"));
//            if (dr.Table.Columns.Contains("coluqQCBatchID") == true)
//                @params[3] = new SqlParameter("@uqQCBatchID", dr("coluqQCBatchID"));
//             else if (dr.Table.Columns.Contains("uqQCBatchID") == true)
//                @params[3] = new SqlParameter("@uqQCBatchID", dr("uqQCBatchID"));
//            SQLHelper.ExecuteNonQuery(ConnectionSettings.cnString, CommandType.StoredProcedure, "EBF_DeleteQCSampleResult_SP", @params);

//            SqlParameter[] params1 = new SqlParameter[2] ;
//            if (dr.Table.Columns.Contains("coluqQCBatchID") == true)
//                params1[0] = new SqlParameter("@QCBatch", dr("coluqQCBatchID"));
//             else if (dr.Table.Columns.Contains("uqQCBatchID") == true)
//                params1[0] = new SqlParameter("@QCBatch", dr("uqQCBatchID"));
//            params1[1] = new SqlParameter("@QCType", dr("RunType"));
//            SQLHelper.ExecuteNonQuery(ConnectionSettings.cnString, CommandType.StoredProcedure, "EBF_UpdateQCBatchQCType_SP", params1);
//            return true;
//        }
//        catch (Exception ex)
//        {
//            return false;
//        }
//    }
//    public static String GetConnectionJobIDType()
//    {
//        try
//        {
//            DataTable dt = SQLHelper.ExecuteDataTable(ConnectionSettings.cnString, CommandType.StoredProcedure, "ConnectionJobIDType_SP");
//            if (!dt == null && dt.Rows.Count > 0)
//            {
//                if (!IsDBNull(dt.Rows(0)(0)) && Len(dt.Rows(0)(0)) > 0)
//    return dt.Rows(0)(0);
//                Else
//    return "Normal";
//            }
//            Else
//    return "Normal";
//        }
//        catch (Exception ex)
//        {
//            return "Normal";
//        }
//    }
//    public static DataTable GetUserPerCheck(String ModuleName, String FormName, int PermInt)
//    {
//        try
//        {
//            SqlParameter[] @params = new SqlParameter[3] ;
//            @params[0] = new SqlParameter("@ModuleName", ModuleName);
//            @params[1] = new SqlParameter("@FormName", FormName);
//            @params[2] = new SqlParameter("@Permission", PermInt);
//            return SQLHelper.ExecuteDataTable(ConnectionSettings.cnString, CommandType.StoredProcedure, "SL_GetPermited_SP", @params);
//        }
//        catch (Exception ex)
//        {
//            return new DataTable();
//        }
//    }
//    public static DateTime GetServerTimes()
//    {
//        try
//        {
//            DataTable d = SQLHelper.ExecuteDataTable(ConnectionSettings.cnString, CommandType.StoredProcedure, "public_GetServerTime_SP");
//            if (d!= null)
//    return d.Rows(0)(0);
//            Else
//    return default(DateTime);
//        }
//        catch (Exception ex)
//        {
//            DataFunctions.InsertExceptionTrackingDC(ex, "DataFunctions", System.Reflection.MethodInfo.GetCurrentMethod().Name);
//            return default(DateTime);
//        }
//    }
//    public static DataTable GetUsersByPermission(String ModuleName, String FormName, String Permission)
//    {
//        try
//        {
//            int iPermission;
//            iPermission = 0;
//            switch (Permission)
//            {
//                Case "None" : 
//                    {
//                        iPermission = 2;
//                        break;
//                    }

//                Case "View" : 
//                    {
//                        iPermission = 4;
//                        break;
//                    }

//                Case "Enter" : 
//                    {
//                        iPermission = 8;
//                        break;
//                    }

//                Case "Validate" : 
//                    {
//                        iPermission = 16;
//                        break;
//                    }

//                Case "Approve" : 
//                    {
//                        iPermission = 32;
//                        break;
//                    }

//                Case "Edit" : 
//                    {
//                        iPermission = 64;
//                        break;
//                    }

//                Case "Sign Off" : 
//                    {
//                        iPermission = 100;
//                        break;
//                    }

//                Case "Delete" : 
//                    {
//                        iPermission = 128;
//                        break;
//                    }
//            }
//            SqlParameter[] @params = new SqlParameter[3] ;
//            @params[0] = new SqlParameter("@ModuleName", ModuleName);
//            @params[1] = new SqlParameter("@FormName", FormName);
//            @params[2] = new SqlParameter("@Permission", iPermission);
//            return SQLHelper.ExecuteDataTable(ConnectionSettings.cnString, CommandType.StoredProcedure, "public_GetUsersByPermission_SP", @params);
//        }
//        catch (Exception ex)
//        {
//            Interaction.MsgBox(ex.Message);
//            return new DataTable();
//        }
//    }
//    public static int CheckQualifier(String Qualifier)
//    {
//        try
//        {
//            SqlParameter[] @params = new SqlParameter[1] ;
//            @params[0] = new SqlParameter("@Qualifier", Qualifier);
//            return SQLHelper.ExecuteScalar(ConnectionSettings.cnString, CommandType.StoredProcedure, "public_CheckQualifier_SP", @params);
//        }
//        catch (Exception ex)
//        {
//            return 0;
//        }
//    }
//    public static DataTable GetFtpConection()
//    {
//        String DataSource = "FTPConnection";
//        SqlParameter[] @params = new SqlParameter[] { new SqlParameter("@colDataSource", DataSource) };
//        DataTable d = SQLHelper.ExecuteDataTable(ConnectionSettings.cnString, CommandType.StoredProcedure, "FtpPath_select_SP", @params);
//        return d;
//    }
//    public static void InsertFtpConection(String ServerName, String UserName, String Password, String FTPPath)
//    {
//        String DataSource = "FTPConnection";
//        SqlParameter[] Aparams = new SqlParameter[5] ;
//        Aparams[0] = new SqlParameter("@colDataSource", DataSource);
//        Aparams[1] = new SqlParameter("@colServerName", ServerName);
//        Aparams[2] = new SqlParameter("@colUserName", UserName);
//        Aparams[3] = new SqlParameter("@colPassword", Password);
//        Aparams[4] = new SqlParameter("@colFTPPath", FTPPath);
//        SQLHelper.ExecuteNonQuery(ConnectionSettings.cnString, CommandType.StoredProcedure, "FtpPath_Insert_SP", Aparams);
//    }
//    public static void UpdateFtpConection(String ServerName, String UserName, String Password, String FTPPath)
//    {
//        String DataSource = "FTPConnection";
//        SqlParameter[] Aparams = new SqlParameter[5] ;
//        Aparams[0] = new SqlParameter("@colDataSource", DataSource);
//        Aparams[1] = new SqlParameter("@colServerName", ServerName);
//        Aparams[2] = new SqlParameter("@colUserName", UserName);
//        Aparams[3] = new SqlParameter("@colPassword", Password);
//        Aparams[4] = new SqlParameter("@colFTPPath", FTPPath);
//        SQLHelper.ExecuteNonQuery(ConnectionSettings.cnString, CommandType.StoredProcedure, "FtpPath_Update_SP", Aparams);
//    }
//    public static void UpdateJobIDSampleType(String SampleType)
//    {
//        SqlParameter[] Aparams = new SqlParameter[1] ;
//        Aparams[0] = new SqlParameter("@colSampleType", SampleType);
//        SQLHelper.ExecuteNonQuery(ConnectionSettings.cnString, CommandType.StoredProcedure, "JobIDSampleType_Update_SP", Aparams);
//    }
//    public static void UpdateLanguage(String Language)
//    {
//        SqlParameter[] Aparams = new SqlParameter[1] ;
//        Aparams[0] = new SqlParameter("@colLanguage", Language);
//        SQLHelper.ExecuteNonQuery(ConnectionSettings.cnString, CommandType.StoredProcedure, "Language_Update_SP", Aparams);
//    }
//    public static DataTable GetQueryDataDF(String qCode)
//    {
//        SqlParameter[] @params = new SqlParameter[] { new SqlParameter("@keycode", qCode) };
//        DataTable d = SQLHelper.ExecuteDataTable(ConnectionSettings.cnString, CommandType.StoredProcedure, "LIMS_QuerySelect_SP", @params);
//        return d;
//    }
//    public static int public_CheckUserFullAccess(String UserName)
//    {
//        try
//        {
//            SqlParameter[] @params = new SqlParameter[] { new SqlParameter("@colUserName", UserName) };
//            DataTable d = SQLHelper.ExecuteDataTable(ConnectionSettings.cnString, CommandType.StoredProcedure, "public_CheckUserFullAccess_SP", @params);
//            if (!d == null && d.Rows.Count > 0 && !IsDBNull(d.Rows(0)("Permission")) && Len(d.Rows(0)("Permission")) > 0)
//    return d.Rows(0)("Permission");
//            Else
//    return 0;
//        }
//        catch (Exception ex)
//        {
//            return 0;
//        }
//    }

//    public static String GetSVQueryDataDF(String qCode)
//    {
//        SqlParameter[] @params = new SqlParameter[] { new SqlParameter("@keycode", qCode) };
//        return SQLHelper.ExecuteScalar(ConnectionSettings.cnString, CommandType.StoredProcedure, "LIMS_QuerySelect_SP", @params);
//    }

//    public static SqlTransaction SqlBeginTransaction()
//    {
//        con = new SqlConnection(ConnectionSettings.cnString);
//        con.Open();
//        Trans = con.BeginTransaction(IsolationLevel.ReadUncommitted);
//        return Trans;
//    }

//    public static void RollBack()
//    {
//        Trans.Rollback();
//        con.Close();
//    }

//    public static void SQLCloseTransaction()
//    {
//        Trans.Commit();
//        con.Close();
//    }
//    public static DataSet getDate(String coluqSampleTestID)
//    {
//        SqlParameter[] @params = new SqlParameter[1] ;
//        @params[0] = new SqlParameter("@coluqSampleTEstID", coluqSampleTestID);

//        return SQLHelper.ExecuteDataset(ConnectionSettings.cnString, CommandType.StoredProcedure, "public_GetPreviousFunctionDate_SP", @params);
//    }
//    public static DataTable GetHoursbyTAT(String TAT, String choice)
//    {
//        SqlParameter[] @params = new SqlParameter[2] ;
//        @params[0] = new SqlParameter("@colChoice", choice);
//        @params[1] = new SqlParameter("@colTAT", TAT);
//        DataTable d = SQLHelper.ExecuteDataTable(ConnectionSettings.cnString, CommandType.StoredProcedure, "SL_GetHoursbyTAT_SP", @params);
//        return d;
//    }
//    public static String GetDefaultCompuntListName()
//    {
//        try
//        {
//            DataTable dt = SQLHelper.ExecuteDataTable(ConnectionSettings.cnString, CommandType.StoredProcedure, "GetDefaultCompoundListName_SP");
//            if (!dt == null && dt.Rows.Count > 0)
//            {
//                if (!IsDBNull(dt.Rows(0)(0)) && Len(dt.Rows(0)(0)) > 0)
//    return dt.Rows(0)(0);
//                Else
//    return String.Empty;
//            }
//            Else
//    return String.Empty;
//        }
//        catch (Exception ex)
//        {
//            Interaction.MsgBox("Error retrieving Default Comp List Name");
//            return String.Empty;
//        }
//    }
//    public static String GetDefaultRPDSource()
//    {
//        try
//        {
//            DataTable dt = SQLHelper.ExecuteDataTable(ConnectionSettings.cnString, CommandType.StoredProcedure, "GetDefaultRPDSource_SP");
//            if (!dt == null && dt.Rows.Count > 0)
//            {
//                if (!IsDBNull(dt.Rows(0)(0)) && Len(dt.Rows(0)(0)) > 0)
//    return dt.Rows(0)(0);
//                Else
//    return String.Empty;
//            }
//            Else
//    return String.Empty;
//        }
//        catch (Exception ex)
//        {
//            return String.Empty;
//        }
//    }
//    public static String GetDefaultConstantByVal(String Constant)
//    {
//        try
//        {
//            SqlParameter[] @params = new SqlParameter[1] ;
//            @params[0] = new SqlParameter("@Constant", Constant);
//            DataTable dt = SQLHelper.ExecuteDataTable(ConnectionSettings.cnString, CommandType.StoredProcedure, "GetDefaultConstantByVal_SP", @params);
//            if (!dt == null && dt.Rows.Count > 0)
//            {
//                if (!IsDBNull(dt.Rows(0)(0)) && Len(dt.Rows(0)(0)) > 0)
//    return dt.Rows(0)(0);
//                Else
//    return String.Empty;
//            }
//            Else
//    return String.Empty;
//        }
//        catch (Exception ex)
//        {
//            return String.Empty;
//        }
//    }
//}

//public static Class GeneralDataFunction
//{
//    public static DataTable GetUnits()
//    {
//        return DataFunctions.GetQueryDataDF("UNITS");
//    }
//    public static DataTable GetGetParameters()
//    {
//        return DataFunctions.GetQueryDataDF("PARAMETER_NAME");
//    }
//    public static DataTable GetTAT()
//    {
//        return DataFunctions.GetQueryDataDF("TAT");
//    }
//    public static DataTable GetUser()
//    {
//        return DataFunctions.GetQueryDataDF("User");
//    }
//    public static DataTable GetMatrix()
//    {
//        return DataFunctions.GetQueryDataDF("ANALYTICAL_MATRIX");
//    }
//    public static DataTable GetLIMS()
//    {
//        return DataFunctions.GetQueryDataDF("LIMS");
//    }
//}
