using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace QLBH.Data
{
    public class QLBHDbContext : DbContext
    {
        public DbSet<LoaiSanPham> LoaiSanPham { get; set; }
        public DbSet<HangSanXuat> HangSanXuat { get; set; }
        public DbSet<SanPham> SanPham { get; set; }
        public DbSet<NhanVien> NhanVien { get; set; }
        public DbSet<KhachHang> KhachHang { get; set; }
        public DbSet<HoaDon> HoaDon { get; set; }
        public DbSet<HoaDon_ChiTiet> HoaDon_ChiTiet { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Sử dụng connection string cố định
                string connectionString = "Server=.;Database=QLBH;User ID=sa;Password=red;MultipleActiveResultSets=True;TrustServerCertificate=True;";
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Tự động tạo database nếu chưa tồn tại
            // Có thể bỏ comment nếu muốn auto-create
        }
    }
}
