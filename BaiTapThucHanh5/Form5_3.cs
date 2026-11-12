using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BaiTapWinForms
{
    public class Product
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; }
    }

    public class Form5_3 : Form
    {
        private TextBox txtProductId, txtProductName, txtUnitPrice, txtQuantity, txtSearch;
        private ComboBox cboCategory;
        private DataGridView dgvProducts;
        private List<Product> productList = new List<Product>();
        private BindingSource bindingSource = new BindingSource();

        public Form5_3()
        {
            this.Text = "BÀI 5.3: QUẢN LÝ DANH SÁCH SẢN PHẨM (DATAGRIDVIEW)";
            this.Size = new Size(760, 520);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Font = new Font("Segoe UI", 9.5F);

            GroupBox grpInfo = new GroupBox { Text = "Thông tin sản phẩm", Location = new Point(15, 12), Size = new Size(710, 120) };
            txtProductId = new TextBox { Location = new Point(95, 27), Width = 140 };
            txtProductName = new TextBox { Location = new Point(335, 27), Width = 350 };
            txtUnitPrice = new TextBox { Location = new Point(95, 72), Width = 140 };
            txtQuantity = new TextBox { Location = new Point(335, 72), Width = 110 };
            cboCategory = new ComboBox { Location = new Point(545, 72), Width = 140, DropDownStyle = ComboBoxStyle.DropDownList };
            cboCategory.Items.AddRange(new object[] { "Điện thoại", "Laptop", "Phụ kiện", "Gia dụng" });
            cboCategory.SelectedIndex = 0;

            grpInfo.Controls.AddRange(new Control[] {
                new Label { Text = "Mã SP:", Location = new Point(20, 30), AutoSize = true }, txtProductId,
                new Label { Text = "Tên SP:", Location = new Point(260, 30), AutoSize = true }, txtProductName,
                new Label { Text = "Đơn giá:", Location = new Point(20, 75), AutoSize = true }, txtUnitPrice,
                new Label { Text = "Số lượng:", Location = new Point(260, 75), AutoSize = true }, txtQuantity,
                new Label { Text = "Danh mục:", Location = new Point(465, 75), AutoSize = true }, cboCategory
            });

            GroupBox grpActions = new GroupBox { Text = "Chức năng", Location = new Point(15, 140), Size = new Size(710, 65) };
            Button btnAdd = new Button { Text = "Thêm", Location = new Point(20, 22), Size = new Size(85, 32) };
            Button btnEdit = new Button { Text = "Sửa", Location = new Point(115, 22), Size = new Size(85, 32) };
            Button btnDelete = new Button { Text = "Xóa", Location = new Point(210, 22), Size = new Size(85, 32) };
            txtSearch = new TextBox { Location = new Point(390, 25), Width = 200 };
            Button btnSearch = new Button { Text = "Tìm kiếm", Location = new Point(600, 22), Size = new Size(90, 32) };

            grpActions.Controls.AddRange(new Control[] { btnAdd, btnEdit, btnDelete, txtSearch, btnSearch });

            dgvProducts = new DataGridView
            {
                Location = new Point(15, 215),
                Size = new Size(710, 250),
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            this.Controls.AddRange(new Control[] { grpInfo, grpActions, dgvProducts });

            // Dữ liệu mẫu
            productList.Add(new Product { ProductId = "SP01", ProductName = "iPhone 16 Pro Max", UnitPrice = 32000000, Quantity = 12, Category = "Điện thoại" });
            productList.Add(new Product { ProductId = "SP02", ProductName = "MacBook Air M3", UnitPrice = 27500000, Quantity = 8, Category = "Laptop" });
            bindingSource.DataSource = productList;
            dgvProducts.DataSource = bindingSource;

            // Xử lý sự kiện
            btnAdd.Click += (s, e) => {
                if (string.IsNullOrWhiteSpace(txtProductId.Text) || string.IsNullOrWhiteSpace(txtProductName.Text))
                {
                    MessageBox.Show("Mã SP và Tên SP không được để trống!"); return;
                }
                if (productList.Any(p => p.ProductId.Equals(txtProductId.Text.Trim(), StringComparison.OrdinalIgnoreCase)))
                {
                    MessageBox.Show("Mã sản phẩm đã tồn tại!"); return;
                }
                if (decimal.TryParse(txtUnitPrice.Text, out decimal price) && int.TryParse(txtQuantity.Text, out int qty))
                {
                    productList.Add(new Product { ProductId = txtProductId.Text.Trim(), ProductName = txtProductName.Text.Trim(), UnitPrice = price, Quantity = qty, Category = cboCategory.Text });
                    RefreshGrid();
                }
                else MessageBox.Show("Đơn giá và Số lượng phải là số hợp lệ!");
            };

            btnEdit.Click += (s, e) => {
                var prod = productList.FirstOrDefault(p => p.ProductId.Equals(txtProductId.Text.Trim(), StringComparison.OrdinalIgnoreCase));
                if (prod != null && decimal.TryParse(txtUnitPrice.Text, out decimal price) && int.TryParse(txtQuantity.Text, out int qty))
                {
                    prod.ProductName = txtProductName.Text.Trim(); prod.UnitPrice = price; prod.Quantity = qty; prod.Category = cboCategory.Text;
                    RefreshGrid();
                }
                else MessageBox.Show("Không tìm thấy Mã SP hoặc dữ liệu không hợp lệ!");
            };

            btnDelete.Click += (s, e) => {
                if (dgvProducts.CurrentRow?.DataBoundItem is Product selected)
                {
                    if (MessageBox.Show($"Bạn có chắc chắn muốn xóa '{selected.ProductName}'?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        productList.Remove(selected);
                        RefreshGrid();
                    }
                }
            };

            btnSearch.Click += (s, e) => {
                string kw = txtSearch.Text.Trim().ToLower();
                RefreshGrid(string.IsNullOrEmpty(kw) ? productList : productList.Where(p => p.ProductName.ToLower().Contains(kw)).ToList());
            };

            dgvProducts.CellClick += (s, e) => {
                if (e.RowIndex >= 0 && dgvProducts.CurrentRow?.DataBoundItem is Product sel)
                {
                    txtProductId.Text = sel.ProductId; txtProductName.Text = sel.ProductName;
                    txtUnitPrice.Text = sel.UnitPrice.ToString(); txtQuantity.Text = sel.Quantity.ToString(); cboCategory.Text = sel.Category;
                }
            };
        }

        private void RefreshGrid(List<Product> list = null)
        {
            bindingSource.DataSource = list ?? productList;
            bindingSource.ResetBindings(false);
        }
    }
}