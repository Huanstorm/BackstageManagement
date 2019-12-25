using BackstageManagement.Model.Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace BackstageManagement.Model.Context
{
    public class DbContext
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["conn"].ToString();
        private SqlSugarClient _db;

        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        

        public SqlSugarClient Db
        {
            get { return _db; }
            set { _db = value; }
        }
        public DbContext()
        {
            try
            {
                Db = new SqlSugarClient(new ConnectionConfig()
                {
                    ConnectionString = _connectionString,
                    DbType = DbType.MySql,
                    InitKeyType = InitKeyType.Attribute,
                    IsAutoCloseConnection = true,
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}