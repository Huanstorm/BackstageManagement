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
        private string _connectionString = "server=47.104.252.6;uid=zh;pwd=qwe123;database=backstagemanagement";//ConfigurationManager.ConnectionStrings["conn"].ToString();
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
        
        public DbSet<EmployeeEntity> EmployeeDb { get { return new DbSet<EmployeeEntity>(Db); } }
        public DbSet<PermissionEntity> PermissionDb { get { return new DbSet<PermissionEntity>(Db); } }
        public DbSet<Employee_Permission> Employee_PermissionDb { get { return new DbSet<Employee_Permission>(Db); } }
        public DbSet<WorkStationEntity> WorkStationDb { get { return new DbSet<WorkStationEntity>(Db); } }
        public DbSet<InfoConfigEntity> InfoConfigDb { get { return new DbSet<InfoConfigEntity>(Db); } }
        public DbSet<ToolingInfoEntity> ToolingInfoDb { get { return new DbSet<ToolingInfoEntity>(Db); } }
        public DbSet<LogEntity> LogDb { get { return new DbSet<LogEntity>(Db); } }
        public DbSet<UNTScriptEntity> UNTScriptDb { get { return new DbSet<UNTScriptEntity>(Db); } }
        public DbSet<UNTLogEntity> UNTLogDb { get { return new DbSet<UNTLogEntity>(Db); } }
        public DbSet<WorkStationUNTEntity> WorkStationUNTDb { get { return new DbSet<WorkStationUNTEntity>(Db); } }
    }
}