using DevExpress.Skins;
using DevExpress.UserSkins;
using QuanLyVatTu.SubForm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using QuanLyVatTu.ReportForm;
using System.Diagnostics;

namespace QuanLyVatTu
{
    static class Program
    {
        // Kết nối đến publisher
        public static SqlConnection conn = new SqlConnection(); // Biến kết nối
        public static String connstr = "";                      
        public static String connstrPublisher = "Data Source = RUFFA; Initial Catalog = QLVT_DATHANG; Integrated Security = true"; // Kết nối bằng Windows Auth
        public static SqlDataReader myReader;

        // Đăng nhập vào server phân mảnh
        public static String serverName = "";       // Tên server phân mảnh kết nối
        public static String userName = "";         // userName trong phân mảnh (mặc định là mã  nhân viên)
        public static String loginName = "";        
        public static String loginPassword = "";   
        public static String database = "QLVT_DATHANG";
        public static String serverNameLeft = "";   // Tên server phân mảnh còn lại

        // Kết nối đến server còn lại thông qua tài khoản HOTROKETNOI
        public static String remoteLogin = "HOTROKETNOI";
        public static String remotePassword = "123456";

        // Lưu tài khoản login hiện tại để có thể trở về sau khi change index bên combobox
        public static String currentLogin = "";
        public static String currentPassword = "";

        // Hiển thị thông tin Nhân viên đăng nhập
        public static String role = "";     // 3 Role CongTy, ChiNhanh và User
        public static String staff = "";    // Họ và tên Nhân viên
        public static int brand = 0;        // Chi nhánh

        // Dùng trong frmDatHang
        public static string maKhoDuocChon = "";
        public static string maVatTuDuocChon = "";

        // Dùng trong frmPhieuNhap và frmDatHang
        public static int soLuongVatTu = 0;
        public static string maDonDatHangDuocChon = "";
        public static string maDonDatHangDuocChonChiTiet = "";
        public static int donGia = 0;

        //  Dùng trong RptHoatDongNhanVien, frmTaoTaiKhoan
        public static string maNhanVienDuocChon = "";
        public static string hoTen = "";
        public static string soCMND = "";
        public static string diaChi = "";
        public static string ngaySinh = "";

        // Biến để biết có đang tạo tài khoản hay là chỉ chọn nhân viên để in report (dùng subformChonNhanVien)
        public static bool dangTaoTaiKhoan = false;

        // Là nơi chỉ ra (Binding) để thao tác dữ liệu, là cầu nối giữa DataGridView và DataTable
        public static BindingSource bindingSource = new BindingSource();

        // Các form trong suốt quá trình làm đồ án
        // Nghiệp vụ
        public static frmDangNhap frmDangNhap;
        public static frmChinh frmChinh;
        public static frmNhanVien frmNhanVien;
        public static frmChuyenChiNhanh frmChuyenChiNhanh;
        public static frmVatTu frmVatTu;
        public static frmKho frmKho;
        public static frmDatHang frmDatHang;
        public static frmChonKhoHang frmChonKhoHang;
        public static gclChiTietPhieuNhap gclChiTietPhieuNhap;
        public static frmChonDonDatHang frmChonDonDatHang;
        public static frmChonChiTietDonDatHang frmChonChiTietDonDatHang;
        public static frmPhieuXuat frmPhieuXuat;

        // Report
        public static frmDanhSachNhanVien frmDanhSachNhanVien;
        public static frmDanhSachVatTu frmDanhSachVatTu;
        public static frmDonHangKhongPhieuNhap frmDonHangKhongPhieuNhap;
        public static frmChiTietSoLuongTriGiaHangHoaNhapXuat frmChiTietSoLuongTriGiaHangHoaNhapXuat;
        public static frmHoatDongNhanVien frmHoatDongNhanVien;
        public static frmTongHopNhapXuat frmTongHopNhapXuat;

        // Mở file pdf khi ấn xuất bản
        public static void OpenPdf(string filePath)
        {
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = filePath;
                process.StartInfo.UseShellExecute = true;
                process.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể mở file PDF: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Kết nối tới server phân mảnh bằng SQL Server Auth
        public static int KetNoi()
        {
            if (Program.conn != null && Program.conn.State == ConnectionState.Open)
                Program.conn.Close();
            try
            {
                Program.connstr = "Data Source=" + Program.serverName + ";Initial Catalog=" +
                       Program.database + ";User ID=" +
                       Program.loginName + ";password=" + Program.loginPassword;
                Program.conn.ConnectionString = Program.connstr;

                Program.conn.Open();
                return 0;
            }

            catch (Exception e)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu.\nXem lại tài khoản và mật khẩu.\n " + e.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 1;
            }
        }

        // Đọc dữ liệu chứ không được phép ghi
        public static SqlDataReader ExecSqlDataReader(String strLenh)
        {
            SqlDataReader myreader;
            // SQLCommand để gọi sp, view, function
            SqlCommand sqlcmd = new SqlCommand(strLenh, Program.conn);
            sqlcmd.CommandType = CommandType.Text; // Để thuận tiện khỏi phải đặt Sp, view hay function
            if (Program.conn.State == ConnectionState.Closed)
                Program.conn.Open();
            try
            {
                myreader = sqlcmd.ExecuteReader();
                return myreader;
            }
            catch (SqlException ex)
            {
                Program.conn.Close();
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        // Đọc ghi sửa dữ liệu (toàn quyền) nhưng chậm
        public static DataTable ExecSqlDataTable(String cmd)
        {
            DataTable dt = new DataTable();
            if (Program.conn.State == ConnectionState.Closed) Program.conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd, conn);
            da.Fill(dt);
            conn.Close();
            return dt;
        }

        // Cập nhật trên một sp, view và không trả về giá trị
        public static int ExecSqlNonQuery(String strlenh)
        {
            SqlCommand Sqlcmd = new SqlCommand(strlenh, conn);
            Sqlcmd.CommandType = CommandType.Text;
            // Làm tự động hàng loạt bên CSDL
            Sqlcmd.CommandTimeout = 600;// 10 phút
            if (conn.State == ConnectionState.Closed) conn.Open();
            try
            {
                Sqlcmd.ExecuteNonQuery(); conn.Close();
                return 0;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                conn.Close();
                return ex.State;    // Trạng thái lỗi từ RaiseError trong SQL Server

            }
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Program.frmChinh = new frmChinh();
            Application.Run(frmChinh);
        }
    }
}
