using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using UowMVC.Domain;
using UowMVC.SDK;

namespace UowMVC.Repository.Migrations
{

    public sealed class Configuration : DbMigrationsConfiguration<DefaultDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(UowMVC.Repository.DefaultDataContext context)
        {

            var userStore = new UserStore<ApplicationUser, ApplicationRole, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>(context);
            var userManager = new UserManager<ApplicationUser, string>(userStore);

            context.Roles.AddOrUpdate(x => x.Name, new ApplicationRole
            {
                Name = ApplicationRoleTypeEnum.Administrator.ToDescription(),
                CreateAt = DateTime.Now,
                Type = ApplicationRoleTypeEnum.Administrator,
            });
            context.Roles.AddOrUpdate(x => x.Name, new ApplicationRole
            {
                Name = ApplicationRoleTypeEnum.Admin.ToDescription(),
                CreateAt = DateTime.Now,
                Type = ApplicationRoleTypeEnum.Admin,
            });
            context.Roles.AddOrUpdate(x => x.Name, new ApplicationRole
            {
                Name = ApplicationRoleTypeEnum.User.ToDescription(),
                CreateAt = DateTime.Now,
                Type = ApplicationRoleTypeEnum.User,
            });
            context.SaveChanges();
            var superadmin = userManager.FindByName("superadmin");
            if (superadmin == null)
            {
                superadmin = new ApplicationUser
                {
                    Num = "00001",
                    UserName = "superadmin",
                    Name = "超级管理员",
                    IsSuperAdmin = true,
                    Type = ApplicationUserTypeEnum.Administrator,
                    CreateAt = DateTime.Now,
                };
                userManager.Create(superadmin, "123456");
                userManager.AddToRole(superadmin.Id, ApplicationRoleTypeEnum.Administrator.ToDescription());
            }
            else
            {
                superadmin.IsSuperAdmin = true;
                context.Entry(superadmin).State = EntityState.Modified;
            }
            if (userManager.IsInRole(superadmin.Id, ApplicationRoleTypeEnum.Administrator.ToDescription()) == false)
            {
                userManager.AddToRole(superadmin.Id, ApplicationRoleTypeEnum.Administrator.ToDescription());
            }
            var admin = userManager.FindByName("admin");
            if (admin == null)
            {
                admin = new ApplicationUser
                {
                    Num = "00001",
                    UserName = "admin",
                    Name = "管理员",
                    IsSuperAdmin = false,
                    Type = ApplicationUserTypeEnum.Administrator,
                    CreateAt = DateTime.Now,
                };
                userManager.Create(admin, "123456");
                userManager.AddToRole(admin.Id, ApplicationRoleTypeEnum.Admin.ToDescription());
            }
            if (userManager.IsInRole(admin.Id, ApplicationRoleTypeEnum.Admin.ToDescription()) == false)
            {
                userManager.AddToRole(admin.Id, ApplicationRoleTypeEnum.Admin.ToDescription());
            }
            var user = userManager.FindByName("user");
            if (user == null)
            {
                user = new ApplicationUser
                {
                    Num = "00002",
                    UserName = "user",
                    Name = "smaple-user",
                    Type = ApplicationUserTypeEnum.User,
                    CreateAt = DateTime.Now,
                };
                userManager.Create(user, "123456");
            }
            if (!context.Configurations.Any(x => x.Key == ConfigKeys.Site_Name_Key))
            {
                context.Configurations.Add(new Domain.Configuration
                {
                    Key = ConfigKeys.Site_Name_Key,
                    No = 1,
                    Type = ConfigurationTypeEnum.String,
                    Value = "Uow MVC"
                });
            }
            if (!context.Configurations.Any(x => x.Key == ConfigKeys.Site_Logo_Key))
            {
                context.Configurations.Add(new Domain.Configuration
                {
                    Key = ConfigKeys.Site_Logo_Key,
                    No = 2,
                    Type = ConfigurationTypeEnum.Image,
                    Value = "/images/logo.png"
                });
            }
            if (!context.Configurations.Any(x => x.Key == ConfigKeys.Log_IsLog_Key))
            {
                context.Configurations.Add(new Domain.Configuration
                {
                    Key = ConfigKeys.Log_IsLog_Key,
                    No = 3,
                    Type = ConfigurationTypeEnum.Bool,
                    Value = "是"
                });
            }
            if (!context.Configurations.Any(x => x.Key == ConfigKeys.User_Default_Avater_Key))
            {
                context.Configurations.Add(new Domain.Configuration
                {
                    Key = ConfigKeys.User_Default_Avater_Key,
                    No = 4,
                    Type = ConfigurationTypeEnum.Image,
                    Value = "/images/default-avatar.png"
                });
            }
            var superadminRole = context.Roles.FirstOrDefault(x => x.Type == ApplicationRoleTypeEnum.Administrator);
            var superadminRolePermissions = context.RolePermissions.Where(x => x.Role.Id == superadminRole.Id);
            if (!superadminRolePermissions.Any())
            {
                var menus = context.Menus.ToList();
                foreach (var m in menus)
                {
                    var permission = new RolePermission
                    {
                        Menu = m,
                        Role = superadminRole,
                    };
                    context.RolePermissions.Add(permission);
                }
            }

            var adminRole = context.Roles.FirstOrDefault(x => x.Type == ApplicationRoleTypeEnum.Admin);
            var adminRolePermissions = context.RolePermissions.Where(x => x.Role.Id == adminRole.Id);
            if (!adminRolePermissions.Any())
            {
                var menus = context.Menus.Where(x => x.IsControlPanel == false).ToList();
                foreach (var m in menus)
                {
                    var permission = new RolePermission
                    {
                        Menu = m,
                        Role = superadminRole,
                    };
                    context.RolePermissions.Add(permission);
                }
            }

            context.SaveChanges();
        }
    }
}
