using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;

namespace QLBH.Data
{
    public class KhachHang
    {
        public int ID { get; set; }
        public required string HoVaTen { get; set; }// required means non-nullable
        public string? DienThoai { get; set; } // ? means nullable
        public string? DiaChi { get; set; }

        public virtual ObservableCollectionListSource<HoaDon> HoaDon { get; } = new();
    }
}
