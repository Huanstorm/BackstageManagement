using BackstageManagement.IRepository;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackstageManagement.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ISqlSugarClient _sqlSugarClient;
        public UnitOfWork(ISqlSugarClient sqlSugarClent) {
            _sqlSugarClient = sqlSugarClent;

        }
        public ISqlSugarClient GetDbClient()
        {
            return _sqlSugarClient;
        }
        public void BeginTran()
        {
            GetDbClient().Ado.BeginTran();
        }

        public void CommitTran()
        {
            try
            {
                GetDbClient().Ado.CommitTran();
            }
            catch (Exception ex)
            {
                GetDbClient().Ado.RollbackTran();
                throw ex;
            }
        }

        

        public void RollbackTran()
        {
            GetDbClient().Ado.RollbackTran();
        }
    }
}
