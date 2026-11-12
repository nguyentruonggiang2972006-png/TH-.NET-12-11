using System;
using System.Drawing;
using System.Windows.Forms;

namespace BaiTapWinForms
{
    public class Form5_1 : Form
    {
        private TextBox txtUsername, txtPassword, txtConfirmPassword;
        private DateTimePicker dtpBirthDate;
        private RadioButton radMale, radFemale;
        private CheckBox chkTerms;
        private ErrorProvider epCheck;

        public Form5_1()
        {
            this.Text = "BÀI 5.1: ĐĂNG KÝ TÀI KHOẢN & ERRORPROVIDER";
            this.Size = new Size(460, 420);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Font = new Font("Segoe UI", 9.5F);

            epCheck = new ErrorProvider { BlinkStyle = ErrorBlinkStyle.NeverBlink };

            // Nhóm 1: Thông tin cá nhân
            GroupBox grpPersonal = new GroupBox { Text = "Thông tin cá nhân", Location = new Point(20, 15), Size = new Size(400, 140) };
            txtUsername = new TextBox { Location = new Point(150, 27), Width = 200 };
            txtPassword = new TextBox { Location = new Point(150, 62), Width = 200, UseSystemPasswordChar = true };
            txtConfirmPassword = new TextBox { Location = new Point(150, 97), Width = 200, UseSystemPasswordChar = true };

            grpPersonal.Controls.AddRange(new Control[] {
                new Label { Text = "Tên đăng nhập:", Location = new Point(20, 30), AutoSize = true }, txtUsername,
                new Label { Text = "Mật khẩu:", Location = new Point(20, 65), AutoSize = true }, txtPassword,
                new Label { Text = "Xác nhận MK:", Location = new Point(20, 100), AutoSize = true }, txtConfirmPassword
            });

            // Nhóm 2: Thông tin bổ sung
            GroupBox grpExtra = new GroupBox { Text = "Thông tin bổ sung", Location = new Point(20, 165), Size = new Size(400, 140) };
            dtpBirthDate = new DateTimePicker { Location = new Point(150, 27), Width = 200, Format = DateTimePickerFormat.Short };
            radMale = new RadioButton { Text = "Nam", Location = new Point(150, 63), AutoSize = true, Checked = true };
            radFemale = new RadioButton { Text = "Nữ", Location = new Point(230, 63), AutoSize = true };
            chkTerms = new CheckBox { Text = "Tôi đồng ý với Điều khoản dịch vụ", Location = new Point(150, 100), AutoSize = true };

            grpExtra.Controls.AddRange(new Control[] {
                new Label { Text = "Ngày sinh:", Location = new Point(20, 30), AutoSize = true }, dtpBirthDate,
                new Label { Text = "Giới tính:", Location = new Point(20, 65), AutoSize = true }, radMale, radFemale, chkTerms
            });

            // Nút bấm
            Button btnRegister = new Button { Text = "Đăng Ký", Location = new Point(110, 325), Size = new Size(105, 36) };
            btnRegister.Click += BtnRegister_Click;

            Button btnReset = new Button { Text = "Làm Mới", Location = new Point(230, 325), Size = new Size(105, 36) };
            btnReset.Click += BtnReset_Click;

            this.Controls.AddRange(new Control[] { grpPersonal, grpExtra, btnRegister, btnReset });
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            epCheck.Clear();
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                epCheck.SetError(txtUsername, "Tên đăng nhập không được để trống!");
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                epCheck.SetError(txtPassword, "Mật khẩu không được để trống!");
                isValid = false;
            }
            if (txtConfirmPassword.Text != txtPassword.Text || string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
            {
                epCheck.SetError(txtConfirmPassword, "Mật khẩu nhập lại không khớp!");
                isValid = false;
            }

            DateTime birthDate = dtpBirthDate.Value.Date;
            DateTime today = DateTime.Today;
            int age = today.Year - birthDate.Year;
            if (birthDate > today.AddYears(-age)) age--;

            if (age < 18)
            {
                epCheck.SetError(dtpBirthDate, "Độ tuổi phải từ 18 tuổi trở lên!");
                isValid = false;
            }
            if (!chkTerms.Checked)
            {
                epCheck.SetError(chkTerms, "Bạn phải đồng ý với Điều khoản dịch vụ!");
                isValid = false;
            }

            if (isValid)
            {
                string gender = radMale.Checked ? "Nam" : "Nữ";
                MessageBox.Show($"Đăng ký thành công!\n- Tài khoản: {txtUsername.Text}\n- Giới tính: {gender}\n- Tuổi: {age}",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            epCheck.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            txtConfirmPassword.Clear();
            dtpBirthDate.Value = DateTime.Today;
            radMale.Checked = true;
            chkTerms.Checked = false;
            txtUsername.Focus();
        }
    }
}