using Microsoft.AspNet.Identity.EntityFramework;
using UowMVC.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Migrations.History;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.Infrastructure.Annotations;

namespace UowMVC.Repository
{
    public class DefaultDataContext :
        IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>
    {
        public virtual DbSet<Media> Images { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<Configuration> Configurations { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<DepartmentRelationship> DepartmentRelationships { get; set; }
        public virtual DbSet<UserGroup> UserGroups { get; set; }
        public virtual DbSet<UserGroupRelationship> UserGroupRelationships { get; set; }
        public virtual DbSet<RolePermission> RolePermissions { get; set; }
        public virtual DbSet<LoginLog> LoginLogs { get; set; }
        public virtual DbSet<DictIndex> DictIndexs { get; set; }
        public virtual DbSet<Dict> Dicts { get; set; }
        public virtual DbSet<Message> Messages { get; set; }

        public virtual DbSet<FriendRelationship> FriendRelationships { get; set; }

        public virtual DbSet<Qrcode> Qrcodes { get; set; }
      
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<NewsCategory> NewsCategories { get; set; }
        public virtual DbSet<NewsViewRecord> NewsViewRecords { get; set; }
      
        public virtual DbSet<Region> Regions { get; set; }

        static DefaultDataContext()
        {

        }
        public DefaultDataContext() : base(nameOrConnectionString: "DefaultConnection")
        {
            Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        // Constructor to use on a DbConnection that is already opened
        public DefaultDataContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
       
    }

}
