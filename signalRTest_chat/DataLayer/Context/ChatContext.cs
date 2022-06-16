using DataLayer.Entities.Chats;
using DataLayer.Entities.Roles;
using DataLayer.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Context
{
    public class ChatContext : DbContext
    {
        public ChatContext(DbContextOptions<ChatContext> contextOptions) : base(contextOptions)
        {

        }

        #region Entities
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatGroup> ChatGroups { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region //Error: FOREIGN KEY constraint may cause or multiple cascade paths handled
            //var cascades = modelBuilder.Model.GetEntityTypes()
            //    .SelectMany(t => t.GetForeignKeys())
            //    .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            //foreach (var fk in cascades)
            //{
            //    //FOREIGN KEY constraint may cause or multiple cascade paths handled
            //    fk.DeleteBehavior = DeleteBehavior.Restrict;
            //}

            //with fluent-api
            modelBuilder.Entity<Chat>()
                            .HasOne(b => b.User)
                            .WithMany(b => b.Chats)
                            .HasForeignKey(b => b.UserId)
                            .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<UserGroup>()
                         .HasOne(b => b.User)
                         .WithMany(b => b.UserGroups)
                         .HasForeignKey(b => b.UserId)
                         .OnDelete(DeleteBehavior.Restrict);
            #endregion

            base.OnModelCreating(modelBuilder);
        }

    }
}
