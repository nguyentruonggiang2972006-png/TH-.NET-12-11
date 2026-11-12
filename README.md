# BÁO CÁO THỰC HÀNH C# WINFORMS

## THÔNG TIN SINH VIÊN
- **Họ và tên:** Nguyễn Trường Giang
- **Mã số sinh viên:** 24810310200
- **Lớp:** D19CNPM3
- **Tên môn học:** Lập trình .NET
- **Nội dung:** bài tập thực hành 12-11

---

## KẾT QUẢ THỰC HÀNH

### BÀI 5.1: FORM ĐĂNG KÝ TÀI KHOẢN VÀ BẮT LỖI GIAO DIỆN  (ERRORPROVIDER) 
**1. Khi nhấn nút ĐĂNG KÝ**
![Tên đăng nhập và mk k để trống](/screenshots/511.png)
![Mật khẩu nhập lại phải khớp với Mật khẩu ban đầu.](/screenshots/512.png)
![Độ tuổi tính từ Ngày sinh phải ≥ 18 tuổi. ](/screenshots/513.png)
![Phải tích chọn đồng ý Điều khoản dịch vụ.](/screenshots/514.png)

**2. Hợp lệ, hiển thị thông báo thành công**
![ hiển thị MessageBox  thông báo thành công. ](./screenshots/515.png)

---

### BÀI 5.2: BẢNG TÍNH TIỀN DỊCH VỤ VÀ CHIẾT KHẤU ĐƠN HÀNG  (LISTBOX & COMBOBOX) 

**1. Yêu cầu chức năng**
![Khi thay đổi mục chọn trên ComboBox, load danh sách món/dịch vụ tương ứng vào lstAvailableServices. ](./screenshots/521.png)
![Khi nhấn nút > hoặc Double Click vào một item ở 
lstAvailableServices, chuyển item đó sang lstSelectedServices.
](./screenshots/522.png)
![3. Tự động tính lại tổng chi phí dựa trên đơn giá của các dịch vụ nằm  trong lstSelectedServices mỗi khi có sự thay đổi danh sách](./screenshots/523.png)


---

### BÀI 5.3: FORM QUẢN LÝ DANH SÁCH SẢN PHẨM TRONG BỘ NHỚ (DATAGRIDVIEW CƠ BẢN) 
**1.Yêu cầu chức năng**
![ Khởi tạo một lớp Product gồm các thuộc tính: ProductId,  
ProductName, UnitPrice, Quantity, Category. ](/screenshots/531.png)
![ Nút Thêm.](/screenshots/532.png)
![Khi Click vào một dòng trong DataGridView, đẩy ngược dữ liệu dòng  đó lên các ô nhập liệu tương ứng.  ](/screenshots/533.png)
![Nút Xóa](/screenshots/534.png)
---

### BÀI 5.4: TRÌNH QUẢN LÝ TẬP TIN DẠNG CHUYÊN NGHIỆP  (TREEVIEW & LISTVIEW) 

**1. Yêu cầu chức năng**
![ Xây dựng cấu trúc cây dữ liệu mẫu cho TreeView khi khởi chạy ứng  dụng. ](/screenshots/541.png)
![Khi người dùng click chọn một Node trên TreeView, lọc danh sách  nhân viên tương ứng thuộc Node đó và nạp vào ListView. ](/screenshots/542.png)
![ListView ở chế độ Details phải hiển thị rõ các cột: Mã NV, Họ Tên,  Chức vụ, Ngày vào làm ](/screenshots/543.png)