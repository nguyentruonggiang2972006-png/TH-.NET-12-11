using System;
using System.Drawing;
using System.Windows.Forms;

namespace BaiTapWinForms
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Form menu = new Form
            {
                Text = "DANH SÁCH BÀI TẬP THỰC HÀNH (5.1 - 5.4)",
                Size = new Size(370, 290),
                StartPosition = FormStartPosition.CenterScreen,
                FormBorderStyle = FormBorderStyle.FixedSingle,
                MaximizeBox = false,
                Font = new Font("Segoe UI", 9.5F)
            };

            Button b1 = new Button { Text = "Bài 5.1 - Đăng ký & ErrorProvider", Location = new Point(45, 25), Size = new Size(260, 40) };
            b1.Click += (s, e) => new Form5_1().ShowDialog();

            Button b2 = new Button { Text = "Bài 5.2 - Tính tiền Dịch vụ (ListBox)", Location = new Point(45, 75), Size = new Size(260, 40) };
            b2.Click += (s, e) => new Form5_2().ShowDialog();

            Button b3 = new Button { Text = "Bài 5.3 - Quản lý Sản phẩm (DataGridView)", Location = new Point(45, 125), Size = new Size(260, 40) };
            b3.Click += (s, e) => new Form5_3().ShowDialog();

            Button b4 = new Button { Text = "Bài 5.4 - Phòng ban (TreeView & ListView)", Location = new Point(45, 175), Size = new Size(260, 40) };
            b4.Click += (s, e) => new Form5_4().ShowDialog();

            menu.Controls.AddRange(new Control[] { b1, b2, b3, b4 });
            Application.Run(menu);
        }
    }
}