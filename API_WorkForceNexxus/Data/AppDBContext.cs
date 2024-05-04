using Microsoft.EntityFrameworkCore;
using WFN.Models.Models;

namespace API_WorkForceNexxus.Data;

public class AppDBContext : DbContext
{
    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
    {
    }
    
    //Escribir Modelos
    public DbSet<DepartmentModel> Depertments { get; set; }
    public DbSet<DesignationModel> Designations { get; set; }
    public DbSet<EmployeeModel> Employees { get; set; }
    public DbSet<CompanyModel> Company { get; set; }
    public DbSet<HolidayModel> Holidays { get; set; }
    public DbSet<AwardModel> Awards { get; set; }
    public DbSet<NoticeModel> Notices { get; set; }
    public DbSet<AttendenceModel> Attendences { get; set; }

    public DbSet<AllowanceTypeModel> AllowanceType { get; set; }
    public DbSet<AllowanceModel> Allowance { get; set; }
    public DbSet<AllowanceEmployeeModel> AllowanceEmployee { get; set; }

    public DbSet<PaySlipModel> PaySlip { get; set; }
    public DbSet<EmployeePaySlipModel> EmployeePaySlip { get; set; }
    public DbSet<PaySlipAllowanceModel> PaySlipAllowance { get; set; }

    public DbSet<UserModel> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmployeeModel>()
            .HasOne(x => x.DepertmentModel)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

        base.OnModelCreating(modelBuilder);
    }
}