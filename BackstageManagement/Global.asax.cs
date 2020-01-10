using Autofac;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac.Integration.Mvc;
using SqlSugar;
using System.Configuration;
using BackstageManagement.Model.Context;
using BackstageManagement.Common;

namespace BackstageManagement
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            BackstageManagement.Model.Context.DbContext dbContext = new BackstageManagement.Model.Context.DbContext();
            DbSend.Send(dbContext);
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();
            var basepath = AppDomain.CurrentDomain.RelativeSearchPath;

            builder.Register<ISqlSugarClient>(c =>
            {
                var sqlSugarClient = new SqlSugarClient(new ConnectionConfig
                {
                    ConnectionString = ConfigurationManager.ConnectionStrings["conn"].ToString(),
                    DbType = DbType.MySql,
                    InitKeyType = InitKeyType.Attribute,
                    IsAutoCloseConnection = true,
                });

                sqlSugarClient.Aop.OnLogExecuting = (sql, pars) =>
                {
                   //LogHelper.WriteDebug($"OnLogExecuting sql:{sql} parameters:{Newtonsoft.Json.JsonConvert.SerializeObject(pars)}");
                };

                sqlSugarClient.Aop.OnExecutingChangeSql = (sql, pars) =>
                {
                    return new KeyValuePair<string, SugarParameter[]>(sql, pars);
                };

                sqlSugarClient.Aop.OnLogExecuted = (sql, pars) =>
                {
                    LogHelper.WriteDebug($"OnLogExecuted sql:{sql} parameters:{Newtonsoft.Json.JsonConvert.SerializeObject(pars)} time:{sqlSugarClient.Ado.SqlExecutionTime.TotalMilliseconds}ms");
                };

                sqlSugarClient.Aop.OnError = (ex) =>
                {
                    LogHelper.WriteError($"OnLogExecuted sql:{ex.Sql} parameters:{Newtonsoft.Json.JsonConvert.SerializeObject(ex.Parametres)} time{sqlSugarClient.Ado.SqlExecutionTime.TotalMilliseconds}ms", ex);
                };
                return sqlSugarClient;
            });
            var repositoryDllFile = Path.Combine(basepath, "BackstageManagement.Repository.dll");
            var assemblysRepository = Assembly.LoadFrom(repositoryDllFile);
            builder.RegisterAssemblyTypes(assemblysRepository).AsImplementedInterfaces();

            var servicesDllFile = Path.Combine(basepath, "BackstageManagement.Services.dll");
            var assemblysServices = Assembly.LoadFrom(servicesDllFile);
            builder.RegisterAssemblyTypes(assemblysServices).AsImplementedInterfaces();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
      
            LogHelper.InitLog4Net(AppDomain.CurrentDomain.BaseDirectory + "log4net.config");
        }
        protected void Application_Error()
        {            
            LogHelper.WriteFatal("Application_Error", Server.GetLastError());
        }

    }
}
