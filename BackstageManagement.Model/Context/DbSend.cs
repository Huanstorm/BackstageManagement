using BackstageManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackstageManagement.Model.Context
{
    public class DbSend
    {
        public static async Task Send(DbContext dbContext) {
            try
            {

                dbContext.Db.CodeFirst.InitTables(
                    typeof(EmployeeEntity),
                    typeof(PermissionEntity),
                    typeof(Employee_Permission),
                    typeof(InfoConfigEntity),
                    typeof(LogEntity));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
