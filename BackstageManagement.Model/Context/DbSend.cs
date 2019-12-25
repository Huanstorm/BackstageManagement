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
        public static void Send(DbContext dbContext) {
            try
            {
                dbContext.Db.CodeFirst.InitTables(
                    typeof(SystemUserEntity),
                    typeof(RoleEntity),
                    typeof(PermissionEntity),
                    typeof(RolePermissionEntity),
                    typeof(InfoConfigEntity),
                    typeof(LogEntity));
                var users=dbContext.Db.Queryable<SystemUserEntity>().ToList();
                if (users.Count==0)
                {
                    //添加用户初始信息
                    SystemUserEntity userEntity = new SystemUserEntity()
                    {
                        LoginName = "admin",
                        RealName = "administrator",
                        Password = "123",
                        CreateUserId = 1,
                        RoleId = 1,
                        CreationTime = DateTime.Now,
                        IsDeleted = false,
                    };
                    dbContext.Db.Insertable<SystemUserEntity>(userEntity).ExecuteCommand();
                    //添加权限初始信息
                    List<PermissionEntity> permissionEntities = new List<PermissionEntity>();
                    permissionEntities.Add(new PermissionEntity() { 
                        Name="控制台",
                        Url="/Home/Index",
                        CreationTime =DateTime.Now,
                        IsDeleted=false,
                        Type=PermissionType.Menu,
                        Description="控制台",
                    });
                    permissionEntities.Add(new PermissionEntity()
                    {
                        Name = "用户角色管理",
                        CreationTime = DateTime.Now,
                        IsDeleted = false,
                        Type = PermissionType.Menu,
                        Description = "用户角色管理",
                    });
                    permissionEntities.Add(new PermissionEntity()
                    {
                        Name = "用户管理",
                        Url="/User/Index",
                        ParentId=2,
                        CreationTime = DateTime.Now,
                        IsDeleted = false,
                        Type = PermissionType.Menu,
                        Description = "用户管理",
                    });
                    permissionEntities.Add(new PermissionEntity()
                    {
                        Name = "角色管理",
                        Url = "/Role/Index",
                        ParentId = 2,
                        CreationTime = DateTime.Now,
                        IsDeleted = false,
                        Type = PermissionType.Menu,
                        Description = "角色管理",
                    });
                    permissionEntities.Add(new PermissionEntity()
                    {
                        Name = "权限管理",
                        Url = "/Permission/Index",
                        ParentId = 2,
                        CreationTime = DateTime.Now,
                        IsDeleted = false,
                        Type = PermissionType.Menu,
                        Description = "权限管理",
                    });
                    permissionEntities.Add(new PermissionEntity()
                    {
                        Name = "角色权限分配",
                        Url = "/RolePermission/Index",
                        ParentId = 2,
                        CreationTime = DateTime.Now,
                        IsDeleted = false,
                        Type = PermissionType.Menu,
                        Description = "角色权限分配",
                    });
                    dbContext.Db.Insertable(permissionEntities).ExecuteCommand();
                    //添加角色初始信息
                    RoleEntity role = new RoleEntity() { 
                        Name="超级管理员",
                        CreateUserId=1,
                        CreationTime=DateTime.Now,
                        IsDeleted=false,
                        IsEnabled=true,
                        Description="拥有所有权限的角色"
                    };
                    dbContext.Db.Insertable<RoleEntity>(role).ExecuteCommand();
                    //添加角色权限初始信息
                    List<RolePermissionEntity> rolePermissionEntities = new List<RolePermissionEntity>();
                    rolePermissionEntities.Add(new RolePermissionEntity() { 
                        RoleId=1,
                        PermissionId=1,
                        IsDeleted=false,
                        CreationTime=DateTime.Now
                    });
                    rolePermissionEntities.Add(new RolePermissionEntity()
                    {
                        RoleId = 1,
                        PermissionId = 2,
                        IsDeleted = false,
                        CreationTime = DateTime.Now
                    });
                    rolePermissionEntities.Add(new RolePermissionEntity()
                    {
                        RoleId = 1,
                        PermissionId = 3,
                        IsDeleted = false,
                        CreationTime = DateTime.Now
                    });
                    rolePermissionEntities.Add(new RolePermissionEntity()
                    {
                        RoleId = 1,
                        PermissionId = 4,
                        IsDeleted = false,
                        CreationTime = DateTime.Now
                    });
                    rolePermissionEntities.Add(new RolePermissionEntity()
                    {
                        RoleId = 1,
                        PermissionId = 5,
                        IsDeleted = false,
                        CreationTime = DateTime.Now
                    });
                    rolePermissionEntities.Add(new RolePermissionEntity()
                    {
                        RoleId = 1,
                        PermissionId = 6,
                        IsDeleted = false,
                        CreationTime = DateTime.Now
                    });
                    dbContext.Db.Insertable<RolePermissionEntity>(rolePermissionEntities).ExecuteCommand();
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
