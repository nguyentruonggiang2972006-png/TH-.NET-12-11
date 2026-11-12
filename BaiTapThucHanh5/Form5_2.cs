using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BaiTapWinForms
{
    public class ServiceItem
    {
        public string ServiceName { get; set; }
        public decimal Price { get; set; }
        public override string ToString() => $"{ServiceName} ({Price:N0} đ)";
    }

    public class Form5_2 : Form
    {
        private ComboBox cboCategory;
        private ListBox lstAvailableServices, lstSelectedServices;
        private TextBox txtSubTotal, txtDiscountPercent, txtFinalTotal;
        private Dictionary<string, List<ServiceItem>> serviceData = new Dictionary<string, List<ServiceItem>>();

        public Form5_2()
        {
            this.Text = "BÀI 5.2: BẢNG TÍNH TIỀN DỊCH VỤ & CHIẾT KHẤU";
            this.Size = new Size(680, 480);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Font = new Font("Segoe UI", 9.5F);

            cboCategory = new ComboBox { Location = new Point(115, 17), Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
            cboCategory.SelectedIndexChanged += CboCategory_SelectedIndexChanged;

            lstAvailableServices = new ListBox { Location = new Point(20, 80), Size = new Size(260, 200) };
            lstAvailableServices.DoubleClick += (s, e) => MoveSelectedService();

            lstSelectedServices = new ListBox { Location = new Point(370, 80), Size = new Size(265, 200) };

            Button btnSelect = new Button { Text = ">", Location = new Point(300, 105), Size = new Size(50, 35) };
            btnSelect.Click += (s, e) => MoveSelectedService();

            Button btnRemove = new Button { Text = "<", Location = new Point(300, 150), Size = new Size(50, 35) };
            btnRemove.Click += (s, e) => {
                if (lstSelectedServices.SelectedIndex != -1)
                {
                    lstSelectedServices.Items.RemoveAt(lstSelectedServices.SelectedIndex);
                    CalculateTotal();
                }
            };

            Button btnClearAll = new Button { Text = "<<", Location = new Point(300, 195), Size = new Size(50, 35) };
            btnClearAll.Click += (s, e) => { lstSelectedServices.Items.Clear(); CalculateTotal(); };

            GroupBox grpBilling = new GroupBox { Text = "Thông tin thanh toán", Location = new Point(20, 290), Size = new Size(615, 130) };
            txtSubTotal = new TextBox { Location = new Point(180, 27), Width = 180, ReadOnly = true, Text = "0 VNĐ" };
            txtDiscountPercent = new TextBox { Location = new Point(180, 59), Width = 100, Text = "0" };
            txtDiscountPercent.TextChanged += (s, e) => CalculateTotal();
            txtFinalTotal = new TextBox { Location = new Point(180, 91), Width = 180, ReadOnly = true, Text = "0 VNĐ", ForeColor = Color.Red };

            grpBilling.Controls.AddRange(new Control[] {
                new Label { Text = "Tổng tiền chưa giảm:", Location = new Point(20, 30), AutoSize = true }, txtSubTotal,
                new Label { Text = "Tỷ lệ chiết khấu (%):", Location = new Point(20, 62), AutoSize = true }, txtDiscountPercent,
                new Label { Text = "Thành tiền thanh toán:", Location = new Point(20, 94), AutoSize = true }, txtFinalTotal
            });

            this.Controls.AddRange(new Control[] {
                new Label { Text = "Loại dịch vụ:", Location = new Point(20, 20), AutoSize = true }, cboCategory,
                new Label { Text = "Danh sách dịch vụ:", Location = new Point(20, 55), AutoSize = true }, lstAvailableServices,
                new Label { Text = "Dịch vụ đã chọn:", Location = new Point(370, 55), AutoSize = true }, lstSelectedServices,
                btnSelect, btnRemove, btnClearAll, grpBilling
            });

            LoadSampleData();
        }

        private void LoadSampleData()
        {
            serviceData["Khám bệnh"] = new List<ServiceItem> {
                new ServiceItem { ServiceName = "Khám tổng quát", Price = 200000 },
                new ServiceItem { ServiceName = "Khám chuyên khoa Nội", Price = 150000 }
            };
            serviceData["Xét nghiệm"] = new List<ServiceItem> {
                new ServiceItem { ServiceName = "Xét nghiệm máu tổng quát", Price = 250000 },
                new ServiceItem { ServiceName = "Xét nghiệm sinh hóa", Price = 350000 }
            };
            serviceData["Chụp X-Quang"] = new List<ServiceItem> {
                new ServiceItem { ServiceName = "X-Quang tim phổi thẳng", Price = 150000 },
                new ServiceItem { ServiceName = "X-Quang cột sống", Price = 220000 }
            };
            serviceData["Vắc-xin"] = new List<ServiceItem> {
                new ServiceItem { ServiceName = "Vắc-xin Cúm mùa", Price = 350000 },
                new ServiceItem { ServiceName = "Vắc-xin Viêm gan B", Price = 200000 }
            };

            foreach (var key in serviceData.Keys) cboCategory.Items.Add(key);
            if (cboCategory.Items.Count > 0) cboCategory.SelectedIndex = 0;
        }

        private void CboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstAvailableServices.Items.Clear();
            string cat = cboCategory.SelectedItem?.ToString();
            if (cat != null && serviceData.ContainsKey(cat))
            {
                foreach (var item in serviceData[cat]) lstAvailableServices.Items.Add(item);
            }
        }

        private void MoveSelectedService()
        {
            if (lstAvailableServices.SelectedItem is ServiceItem item)
            {
                lstSelectedServices.Items.Add(item);
                CalculateTotal();
            }
        }

        private void CalculateTotal()
        {
            decimal subTotal = 0;
            foreach (ServiceItem item in lstSelectedServices.Items) subTotal += item.Price;

            if (!decimal.TryParse(txtDiscountPercent.Text, out decimal disc) || disc < 0 || disc > 100) disc = 0;

            decimal finalTotal = subTotal * (1 - disc / 100);
            txtSubTotal.Text = $"{subTotal:N0} VNĐ";
            txtFinalTotal.Text = $"{finalTotal:N0} VNĐ";
        }
    }
}