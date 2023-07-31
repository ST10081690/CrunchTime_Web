using CrunchTime_Web.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CrunchTime_Web.Pages.SemesterCRUD;
using CrunchTime_Web.Pages.ModuleCRUD;
using CrunchTime_Web.Pages.ScheduleCRUD;

namespace CrunchTime_Web;

public class UserData : IdentityDbContext<CrunchTimeUser>
{
    public UserData(DbContextOptions<UserData> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        //configuration for user identity class
        builder.ApplyConfiguration(new CT_UserIdentityConfiguration());
    }

    public DbSet<CrunchTime_Web.Pages.SemesterCRUD.SemesterModel> SemesterModel { get; set; }

    public DbSet<CrunchTime_Web.Pages.ModuleCRUD.ModuleModel> ModuleModel { get; set; }

    public DbSet<CrunchTime_Web.Pages.ScheduleCRUD.ScheduleModel> ScheduleModel { get; set; }
}

//user identity configuration class
public class CT_UserIdentityConfiguration : IEntityTypeConfiguration<CrunchTimeUser>
{
    void IEntityTypeConfiguration<CrunchTimeUser>.Configure(EntityTypeBuilder<CrunchTimeUser> builder)
    {
        //setting maximum length for user details
        builder.Property(p => p.UsernameByUser).HasMaxLength(200);

        builder.Property(p => p.FirstName).HasMaxLength(200);

        builder.Property(p => p.Surname).HasMaxLength(200);

        builder.Property(p => p.StudyField).HasMaxLength(400);
    }
}
