using System;
using System.Collections.Generic;
using System.Text;

namespace QLBH.Data
{
    public class HangSanXuat
    {
        public int ID { get; set; }
        public string TenHangSanXuat { get; set; }

        public virtual ObservableCollectionListSource<SanPham> SanPham { get; } = new();
    }
}
