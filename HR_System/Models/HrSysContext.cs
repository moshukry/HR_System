using Microsoft.EntityFrameworkCore;

namespace HR_System.Models
{
    public class HrSysContext : DbContext
    {
        public HrSysContext(DbContextOptions<HrSysContext> option) : base(option)
        {
        }
        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<AttDep> Att_dep { get; set; } = null!;
        public virtual DbSet<Crud> CRUDs { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Group> Groups { get; set; } = null!;
        public virtual DbSet<Page> Pages { get; set; } = null!;
        public virtual DbSet<Setting> Settings { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Vacation> Vacations { get; set; } = null!;

    }
}
