using Microsoft.EntityFrameworkCore;
using QLBH.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QLBH.GUI
{
    public partial class Frm_LoaiSanPham : Form
    {
        public Frm_LoaiSanPham()
        {
            InitializeComponent();
            
            // Đảm bảo database được tạo
            try
            {
                using (var ctx = new QLBHDbContext())
                {
                    ctx.Database.EnsureCreated();
                }
            }
            catch
            {
                // Bỏ qua nếu có lỗi
            }
        }
        QLBHDbContext context = new QLBHDbContext();

        private void Frm_LoaiSanPham_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                List<LoaiSanPham> lsp = context.LoaiSanPham.AsNoTracking().ToList();

                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = lsp;

                dataGridView1.DataSource = bindingSource;
                dataGridView1.AutoGenerateColumns = true;
                
                // Đảm bảo GridView cho phép thêm hàng mới
                dataGridView1.AllowUserToAddRows = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveChanges();
        }

        private void SaveChanges()
        {
            try
            {
                bool hasChanges = false;
                
                // Duyệt qua tất cả các hàng trong GridView
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    // Bỏ qua hàng mới (chưa có dữ liệu)
                    if (row.IsNewRow)
                        continue;

                    // Lấy giá trị từ GridView
                    object? idValue = row.Cells["ID"].Value;
                    object? tenLoaiValue = row.Cells["TenLoai"].Value;
                    
                    int id = 0;
                    string tenLoai = tenLoaiValue?.ToString() ?? string.Empty;

                    // Parse ID
                    if (idValue != null && int.TryParse(idValue.ToString(), out int parsedId))
                    {
                        id = parsedId;
                    }

                    // Kiểm tra dữ liệu hợp lệ
                    if (string.IsNullOrWhiteSpace(tenLoai))
                    {
                        continue;
                    }

                    // Nếu ID = 0, thêm mới; ngược lại cập nhật
                    if (id == 0)
                    {
                        // Thêm mới
                        LoaiSanPham newLoai = new LoaiSanPham { TenLoai = tenLoai };
                        context.LoaiSanPham.Add(newLoai);
                        hasChanges = true;
                    }
                    else
                    {
                        // Cập nhật - tìm entity trong context
                        LoaiSanPham? existingLoai = context.LoaiSanPham.Local.FirstOrDefault(x => x.ID == id);
                        
                        if (existingLoai == null)
                        {
                            existingLoai = context.LoaiSanPham.FirstOrDefault(x => x.ID == id);
                        }
                        
                        if (existingLoai != null && existingLoai.TenLoai != tenLoai)
                        {
                            existingLoai.TenLoai = tenLoai;
                            context.LoaiSanPham.Update(existingLoai);
                            hasChanges = true;
                        }
                    }
                }

                // Lưu tất cả thay đổi vào database
                if (hasChanges)
                {
                    int saved = context.SaveChanges();
                    MessageBox.Show($"Lưu dữ liệu thành công! ({saved} bản ghi thay đổi)", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Reset context để tránh vấn đề tracking
                    context.ChangeTracker.Clear();
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu nào thay đổi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
