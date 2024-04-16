using Haram.Remittance.Currencies;
using Haram.Remittance.Customers;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using Haram.Remittance.Remmitances;
using Haram.Remittance.Remittances;
using System.Reflection.Emit;
using System.Numerics;

namespace Haram.Remittance.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class RemittanceDbContext :
    AbpDbContext<RemittanceDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    public DbSet<RemittanceClass> Remittances { get; set; }

    public DbSet<Customer> Customers { get; set; }

    public DbSet<Currency> Currencies { get; set; }
    public DbSet<RemittanceStatus> RemittanceStatuses { get; set; }

    public DbSet<IdentityUser> Users { get; set; }

    public DbSet<IdentityRole> Roles { get; set; }

    public DbSet<IdentityClaimType> ClaimTypes { get; set; }

    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }

    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }

    public DbSet<IdentityLinkUser> LinkUsers { get; set; }

    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }

    public DbSet<Tenant> Tenants { get; set; }

    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    public RemittanceDbContext(DbContextOptions<RemittanceDbContext> options)
        : base(options)
    { 
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        /* Configure your own tables/entities inside here */

        builder.Entity<RemittanceClass>(builder =>
        {
            builder.ToTable(RemittanceConsts.DbTablePrefix + "Remittances", RemittanceConsts.DbSchema);
            builder.ConfigureByConvention(); // Auto configure for base class props
            builder.HasMany(rc => rc.HistoryStatus)
            .WithOne(rs => rs.Remittance)
            .HasForeignKey(rs => rs.RemittanceId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(rc => rc.Currency)
            .WithMany()
            .HasForeignKey(rc => rc.CurrencyId);
            builder.HasOne(rc => rc.Sender)
            .WithMany()
            .HasForeignKey(rc => rc.SenderId);
            builder.HasOne(rc => rc.Receiver)
            .WithMany()
            .HasForeignKey(rc => rc.ReceiverId).IsRequired(false);
        });

        builder.Entity<RemittanceStatus>(builder =>
        {
            builder.ToTable(RemittanceConsts.DbTablePrefix + "RemittanceStatuses", RemittanceConsts.DbSchema);
            builder.ConfigureByConvention(); // Auto configure for base class props
            builder.HasOne(s => s.Remittance)
             .WithMany(r => r.HistoryStatus)
             .HasForeignKey(s => s.RemittanceId)
             .IsRequired(false);
        });

        builder.Entity<Currency>(c =>
        {
            c.ToTable(RemittanceConsts.DbTablePrefix + "Currencies", RemittanceConsts.DbSchema);
            c.ConfigureByConvention(); //auto configure for the base class props
            c.Property(attr => attr.Name).IsRequired();
            c.Property(attr => attr.Symbol).IsRequired();
            c.HasIndex(attr => attr.Name).IsUnique();
            c.HasIndex(attr => attr.Symbol).IsUnique();
        });

        builder.Entity<Customer>(c =>
        {
            c.ToTable(RemittanceConsts.DbTablePrefix + "Customers", RemittanceConsts.DbSchema);
            c.ConfigureByConvention(); //auto configure for the base class props
            c.Property(attr => attr.FirstName).IsRequired();
            c.Property(attr => attr.LastName).IsRequired();
            c.Property(attr => attr.FatherName).IsRequired();
            c.Property(attr => attr.MotherName).IsRequired();
            c.Property(attr => attr.BirthDate).IsRequired();
            c.Property(attr => attr.Phone).IsRequired();
            c.Property(attr => attr.Gender).IsRequired();
            c.HasIndex(attr => new { attr.FirstName, attr.LastName, attr.FatherName,attr.MotherName }).IsUnique();
        });
    }
}
