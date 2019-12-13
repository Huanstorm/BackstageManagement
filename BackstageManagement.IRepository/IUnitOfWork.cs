using SqlSugar;

namespace BackstageManagement.IRepository
{
    public interface IUnitOfWork
    {
        ISqlSugarClient GetDbClient();
        void BeginTran();
        void CommitTran();
        void RollbackTran();
    }
}
