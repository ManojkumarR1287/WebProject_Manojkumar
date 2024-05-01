using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SQL1
{
    public static class ConnectionSettings
    {
        private static string _cnString;
        private static string _cnSDFString;
        private static string _cnSamplingFileType;
        private static string _SQLUserID;
        private static string _SQLPassword;
        private static string _SQLServerName;
        private static string _SQLDatabaseName;
        private static  bool _SQLHasNetworkLibrary;

        public static bool SQLHasNetworkLibrary
        {
            get
                {
                return _SQLHasNetworkLibrary;
            }
            set
                {
                _SQLHasNetworkLibrary = value;
            }
        }
        public static String SQLUserID
        {
            get
                {
                return _SQLUserID;
            }
            set
                {
                _SQLUserID = value;
            }
        }
        public static String SQLPassword
        {
            get
                {
                return _SQLPassword;
            }
            set
                {
                _SQLPassword = value;
            }
        }
        public static String SQLServerName
        {
            get
                {
                return _SQLServerName;
            }
            set
                {
                _SQLServerName = value;
            }
        }
        public static String SQLDatabaseName
        {
            get
                {
                return _SQLDatabaseName;
            }
            set
                {
                _SQLDatabaseName = value;
            }
        }

        public static String cnString
        {
            get
                {
                return _cnString;
            }
            set
                {
                _cnString = value;
            }
        }
        public static String cnSDFString
        {
            get
                {
                return _cnSDFString;
            }
            set
                {
                _cnSDFString = value;
            }
        }
        public static String cnSamplingFileType
        {
            get
                {
                return _cnSamplingFileType;
            }
            set
                {
                _cnSamplingFileType = value;
            }
        }
    }

}

