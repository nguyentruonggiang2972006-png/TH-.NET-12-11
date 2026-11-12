using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BaiTapWinForms
{
    public class Employee
    {
        public string EmpId { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public DateTime HireDate { get; set; }
        public string NodeKey { get; set; }
    }

    public class Form5_4 : Form
    {
        private TreeView tvDepartments;
        private ListView lsvEmployees;
        private List<Employee> employees = new List<Employee>();

        public Form5_4()
        {
            this.Text = "BÀI 5.4: TRÌNH QUẢN LÝ PHÒNG BAN & NHÂN VIÊN (TREEVIEW & LISTVIEW)";
            this.Size = new Size(820, 500);
            this.StartPosition = FormStartPosition.CenterParent;
            this.Font = new Font("Segoe UI", 9.5F);

            // Tạo ImageList bằng code (không cần file ảnh ngoài)
            ImageList imgSmall = new ImageList { ImageSize = new Size(16, 16) };
            imgSmall.Images.Add(CreateColorIcon(16, Color.Goldenrod));
            imgSmall.Images.Add(CreateColorIcon(16, Color.DodgerBlue));

            ImageList imgLarge = new ImageList { ImageSize = new Size(32, 32) };
            imgLarge.Images.Add(CreateColorIcon(32, Color.Goldenrod));
            imgLarge.Images.Add(CreateColorIcon(32, Color.DodgerBlue));

            SplitContainer split = new SplitContainer { Dock = DockStyle.Fill, SplitterDistance = 260 };

            tvDepartments = new TreeView { Dock = DockStyle.Fill, ImageList = imgSmall };
            tvDepartments.AfterSelect += TvDepartments_AfterSelect;
            split.Panel1.Controls.Add(tvDepartments);

            Panel topRightPanel = new Panel { Dock = DockStyle.Top, Height = 42 };
            ComboBox cboViewMode = new ComboBox { Location = new Point(105, 8), Width = 150, DropDownStyle = ComboBoxStyle.DropDownList };
            cboViewMode.Items.AddRange(new object[] { View.Details, View.SmallIcon, View.LargeIcon, View.Tile });
            cboViewMode.SelectedIndexChanged += (s, e) => { if (cboViewMode.SelectedItem is View v) lsvEmployees.View = v; };
            topRightPanel.Controls.AddRange(new Control[] { new Label { Text = "Chế độ xem:", Location = new Point(10, 11), AutoSize = true }, cboViewMode });

            lsvEmployees = new ListView
            {
                Dock = DockStyle.Fill,
                FullRowSelect = true,
                GridLines = true,
                SmallImageList = imgSmall,
                LargeImageList = imgLarge,
                View = View.Details
            };
            lsvEmployees.Columns.Add("Mã NV", 90);
            lsvEmployees.Columns.Add("Họ Tên", 170);
            lsvEmployees.Columns.Add("Chức vụ", 140);
            lsvEmployees.Columns.Add("Ngày vào làm", 120);

            cboViewMode.SelectedIndex = 0;

            split.Panel2.Controls.Add(lsvEmployees);
            split.Panel2.Controls.Add(topRightPanel);
            this.Controls.Add(split);

            // Khởi tạo dữ liệu nhân viên mẫu
            employees.AddRange(new List<Employee> {
                new Employee { EmpId = "NV001", FullName = "Nguyễn Văn An", Position = "Backend Leader", HireDate = new DateTime(2021, 3, 15), NodeKey = "TEAM_DEV" },
                new Employee { EmpId = "NV002", FullName = "Trần Thị Bình", Position = "Frontend Dev", HireDate = new DateTime(2022, 7, 1), NodeKey = "TEAM_DEV" },
                new Employee { EmpId = "NV003", FullName = "Lê Hoàng Nam", Position = "QA Tester", HireDate = new DateTime(2023, 1, 10), NodeKey = "TEAM_QA" },
                new Employee { EmpId = "NV004", FullName = "Phạm Minh Châu", Position = "HR Specialist", HireDate = new DateTime(2020, 5, 20), NodeKey = "TEAM_REC" },
                new Employee { EmpId = "NV005", FullName = "Hoàng Đức Duy", Position = "Kế toán trưởng", HireDate = new DateTime(2019, 11, 5), NodeKey = "DEPT_ACC" }
            });

            // Xây dựng cây phòng ban
            TreeNode root = new TreeNode("Công ty Công nghệ ABC", 0, 0) { Tag = "COMPANY" };
            TreeNode deptIT = new TreeNode("Phòng Kỹ thuật", 0, 0) { Tag = "DEPT_IT" };
            deptIT.Nodes.Add(new TreeNode("Nhóm Lập trình", 0, 0) { Tag = "TEAM_DEV" });
            deptIT.Nodes.Add(new TreeNode("Nhóm Kiểm thử (QA)", 0, 0) { Tag = "TEAM_QA" });

            TreeNode deptHR = new TreeNode("Phòng Nhân sự", 0, 0) { Tag = "DEPT_HR" };
            deptHR.Nodes.Add(new TreeNode("Nhóm Tuyển dụng", 0, 0) { Tag = "TEAM_REC" });

            TreeNode deptAcc = new TreeNode("Phòng Kế toán", 0, 0) { Tag = "DEPT_ACC" };

            root.Nodes.AddRange(new TreeNode[] { deptIT, deptHR, deptAcc });
            tvDepartments.Nodes.Add(root);
            tvDepartments.ExpandAll();
            tvDepartments.SelectedNode = root;
        }

        private Bitmap CreateColorIcon(int size, Color color)
        {
            Bitmap bmp = new Bitmap(size, size);
            using (Graphics g = Graphics.FromImage(bmp))
            using (Brush b = new SolidBrush(color))
                g.FillEllipse(b, 2, 2, size - 4, size - 4);
            return bmp;
        }

        private List<string> GetNodeAndChildKeys(TreeNode node)
        {
            List<string> keys = new List<string>();
            if (node.Tag != null) keys.Add(node.Tag.ToString());
            foreach (TreeNode child in node.Nodes) keys.AddRange(GetNodeAndChildKeys(child));
            return keys;
        }

        private void TvDepartments_AfterSelect(object sender, TreeViewEventArgs e)
        {
            lsvEmployees.Items.Clear();
            var validKeys = GetNodeAndChildKeys(e.Node);
            foreach (var emp in employees.Where(emp => validKeys.Contains(emp.NodeKey)))
            {
                ListViewItem item = new ListViewItem(emp.EmpId, 1);
                item.SubItems.Add(emp.FullName);
                item.SubItems.Add(emp.Position);
                item.SubItems.Add(emp.HireDate.ToString("dd/MM/yyyy"));
                lsvEmployees.Items.Add(item);
            }
        }
    }
}