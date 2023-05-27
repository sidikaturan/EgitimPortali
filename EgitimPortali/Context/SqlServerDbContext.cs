using EgitimPortali.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net.Sockets;
using System.Security.Claims;

namespace EgitimPortali.Context
{
    public class SqlServerDbContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public int GetMyName()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            }
            return Convert.ToInt16(result);
        }
        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            var deger = GetMyName();
            foreach (var item in ChangeTracker.Entries().Where(e => e.State
             == EntityState.Deleted))
            {
                item.State = EntityState.Modified;
                item.CurrentValues["IsDeleted"] = true;
                item.CurrentValues["DeletedAt"] = DateTime.Now;
                item.CurrentValues["DeletedBy"] = deger;
            }
            foreach (var item in ChangeTracker.Entries().Where(e => e.State
            == EntityState.Added))
            {
                item.CurrentValues["CreatedBy"] = deger;
            }
            return base.SaveChanges();
        }
        public DbSet<Kategoriler> Kategorilers { get; set; }
        public DbSet<AnaSayfa> AnaSayfas { get; set; }
        public DbSet<DersIcerikleri> DersIcerikleris { get; set; }
        public DbSet<Dersler> Derslers { get; set; }
        public DbSet<Hakkimizda> Hakkimizdas { get; set; }
        public DbSet<Iletisim> Iletisims { get; set; }
        public DbSet<Konular> Konulars { get; set; }
        public DbSet<Kullanicilar> Kullanicilars { get; set; }
        public DbSet<KullanicilarinRolleri> KullanicilarinRolleris { get; set; }
        public DbSet<Reklamlar> Reklamlars { get; set; }
        public DbSet<Roller> Rollers { get; set; }
        public DbSet<Sorular> Sorulars { get; set; }
        public DbSet<SorularinCevaplari> SorularinCevaplaris { get; set; }
        public DbSet<Yorumlar> Yorumlars { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<TestSoru> TestSorus { get; set; }
        public DbSet<TestCevap> TestCevaps { get; set; }
        public DbSet<DersTakip> DersTakips { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region Users table
            modelBuilder.Entity<Kullanicilar>()
                .HasIndex(b => b.Mail)
                .IsUnique();
            #endregion
            #region Soft Delete
            modelBuilder.Entity<AnaSayfa>().HasQueryFilter(b => !b.IsDeleted);
            modelBuilder.Entity<DersIcerikleri>().HasQueryFilter(b => !b.IsDeleted);
            modelBuilder.Entity<Dersler>().HasQueryFilter(b => !b.IsDeleted);
            modelBuilder.Entity<DersTakip>().HasQueryFilter(b => !b.IsDeleted);
            modelBuilder.Entity<Hakkimizda>().HasQueryFilter(b => !b.IsDeleted);
            modelBuilder.Entity<Iletisim>().HasQueryFilter(b => !b.IsDeleted);
            modelBuilder.Entity<Kategoriler>().HasQueryFilter(b => !b.IsDeleted);
            modelBuilder.Entity<Konular>().HasQueryFilter(b => !b.IsDeleted);
            modelBuilder.Entity<Kullanicilar>().HasQueryFilter(b => !b.IsDeleted);
            modelBuilder.Entity<KullanicilarinRolleri>().HasQueryFilter(b => !b.IsDeleted);
            modelBuilder.Entity<Reklamlar>().HasQueryFilter(b => !b.IsDeleted);
            modelBuilder.Entity<Roller>().HasQueryFilter(b => !b.IsDeleted);
            modelBuilder.Entity<Sorular>().HasQueryFilter(b => !b.IsDeleted);
            modelBuilder.Entity<SorularinCevaplari>().HasQueryFilter(b => !b.IsDeleted);
            modelBuilder.Entity<Test>().HasQueryFilter(b => !b.IsDeleted);
            modelBuilder.Entity<TestSoru>().HasQueryFilter(b => !b.IsDeleted);
            modelBuilder.Entity<TestCevap>().HasQueryFilter(b => !b.IsDeleted);
            modelBuilder.Entity<Yorumlar>().HasQueryFilter(b => !b.IsDeleted);
            #endregion

            #region UserRoles


            modelBuilder.Entity<KullanicilarinRolleri>().HasOne(ur => ur.Kullanicilar).WithMany(u => u.KullanicilarinRolleris)
                .HasForeignKey(u => u.KullaniciID);

            modelBuilder.Entity<KullanicilarinRolleri>().HasOne(ur => ur.Roller).WithMany(r => r.KullanicilarinRolleris)
                .HasForeignKey(u => u.RolID);

            #endregion
        }
    }
}