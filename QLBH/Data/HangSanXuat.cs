using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;

namespace QLBH.Data
{
    public class HangSanXuat
    {
        public int ID { get; set; }
        public required string TenHangSanXuat { get; set; }

        public virtual ObservableCollectionListSource<SanPham> SanPham { get; } = new();
    }
}
