using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UseCase.Data.Model;
using System;

namespace UseCase.Data.Context
{
    public partial class UseCaseContext : IdentityDbContext<
        User, Role, Guid,
        IdentityUserClaim<Guid>, UserRole, IdentityUserLogin<Guid>,
        IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public UseCaseContext(DbContextOptions<UseCaseContext> options)
            : base(options)
        {
        }  
        
        public virtual DbSet<Cashier> Cashiers { get; set; }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<ApiRole> ApiRoles { get; set; }
        public virtual DbSet<Corporation> Corporations{ get; set; }
        public virtual DbSet<Subscription> Subscriptions { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);



            builder.Entity<UserRole>(i =>
            {
                i.ToTable("UserRoles");
                i.HasKey(x => new { x.RoleId, x.UserId });
                i.HasOne(ur => ur.Role)
                   .WithMany(r => r.UserRoles)
                   .HasForeignKey(ur => ur.RoleId)
                   .IsRequired();

                i.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            builder.Entity<User>(b =>
            {

                b.ToTable("Users");
                b.HasKey(x => x.Id);

            });



            builder.Entity<Role>(i =>
            {
                i.ToTable("Roles");
                i.HasKey(x => x.Id);
            });

            builder.Entity<IdentityUserLogin<Guid>>(i =>
            {
                i.ToTable("UserLogins");
                i.HasKey(x => new { x.ProviderKey, x.LoginProvider });
            });
            builder.Entity<IdentityRoleClaim<Guid>>(i =>
            {
                i.ToTable("RoleClaims");
                i.HasKey(x => x.Id);
            });
            builder.Entity<IdentityUserClaim<Guid>>(i =>
            {
                i.ToTable("UserClaims");
                i.HasKey(x => x.Id);
            });
            builder.Entity<IdentityUserToken<Guid>>(i =>
            {
                i.ToTable("UserTokens");
                i.HasKey(x => x.UserId);
            });



        }
    }
}