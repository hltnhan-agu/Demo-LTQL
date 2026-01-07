using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;

namespace QLBH.Data
{
    public class NhanVien
    {
        public int ID { get; set; }
        public required string HoVaTen { get; set; }
        public string? DienThoai { get; set; }
        public string? DiaChi { get; set; }
        public required string TenDangNhap { get; set; }
        public required string MatKhau { get; set; }
        public bool QuyenHan { get; set; }

        public virtual ObservableCollectionListSource<HoaDon> HoaDon { get; } = new();
    }
}
