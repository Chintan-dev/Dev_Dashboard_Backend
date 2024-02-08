using Microsoft.EntityFrameworkCore;

namespace Dev_Dashboard.Model;

public partial class DevDashboardContext : DbContext
{
    private readonly IConfiguration _configuration;
    public DevDashboardContext(DbContextOptions<DevDashboardContext> options, IConfiguration configuration)
       : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<UserAssignMenu> UserAssignMenus { get; set; }

    public virtual DbSet<UserDetail> UserDetails { get; set; }

    public virtual DbSet<UserMenu> UserMenus { get; set; }

    public virtual DbSet<UserMenuIdInOrder> UserMenuIdInOrders { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseSqlServer("Server=DESKTOP-8LFU985;Database=Dev_Dashboard;trusted_connection= true;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.RoleName).HasMaxLength(10);
        });

        modelBuilder.Entity<UserAssignMenu>(entity =>
        {
            entity.ToTable("UserAssignMenu");

            entity.Property(e => e.MenuId).HasColumnName("Menu_Id");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.Menu).WithMany(p => p.UserAssignMenus)
                .HasForeignKey(d => d.MenuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserAssignMenu_UserMenu");

            entity.HasOne(d => d.User).WithMany(p => p.UserAssignMenus)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserAssignMenu_UserDetails");
        });

        modelBuilder.Entity<UserDetail>(entity =>
        {
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Email).HasMaxLength(30);
            entity.Property(e => e.RoleId).HasColumnName("Role_Id");
            entity.Property(e => e.Username).HasMaxLength(20);

            entity.HasOne(d => d.Role).WithMany(p => p.UserDetails)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserDetails_Roles");
        });

        modelBuilder.Entity<UserMenu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Usermenu");

            entity.ToTable("UserMenu");

            entity.Property(e => e.MenuDescription)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Path)
              .HasMaxLength(50)
              .IsUnicode(false);
            entity.Property(e => e.MenuName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserMenuIdInOrder>(entity =>
        {
            entity.ToTable("UserMenu_id_inOrder");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.UserId).HasColumnName("User_Id");
            entity.Property(e => e.UserMenuIdInOrder1).HasColumnName("UserMenu_Id_InOrder");

            entity.HasOne(d => d.User).WithMany(p => p.UserMenuIdInOrders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserMenu_id_inOrder_UserDetails");

            entity.HasOne(d => d.UserMenuIdInOrder1Navigation).WithMany(p => p.UserMenuIdInOrders)
                .HasForeignKey(d => d.UserMenuIdInOrder1)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserMenu_id_inOrder_UserMenu");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
