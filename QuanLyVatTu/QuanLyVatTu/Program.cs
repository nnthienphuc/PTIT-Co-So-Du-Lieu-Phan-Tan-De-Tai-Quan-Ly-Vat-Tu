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

namespace QuanLyVatTu
{
    static class Program
    {
        // Kết nối đến cơ sở dữ liệu gốc
        public static SqlConnection conn = new SqlConnection(); // biến để kết nối đến csdl gốc
        public static String connstr = "";                      
        public static String connstrPublisher = "Data Source=RUFFA; Initial Catalog=QLVT_DATHANG; Integrated Security=true"; // Bat mode dang nhap bang tai khoan
        public static SqlDataReader myReader;

        // Đăng nhập vào server phân mảnh
        public static String serverName = "";       // Tên Server phân mảnh sẽ kết nối tới
        public static String serverNameLeft = "";   // Tên phân mảnh còn lại
        public static String userName = "";

        public static String loginName = "";
        public static String loginPassword = "";

        // Remote
        public static String database = "QLVT_DATHANG"; // Tên CSDL cần làm việc
        // Remote login và remote password dùng để kết nối đến server còn lại
        public static String remoteLogin = "HOTROKETNOI";
        public static String remotePassword = "123456";
        // current Login và current Password để quay trở lại server hiện tại
        public static String currentLogin = "";
        public static String currentPassword = "";

        // Hiển thị thông tin nhân viên đang đăng nhập
        public static String role = "";     // CONGTY - CHINHANH - USER
        public static String staff = "";    // Họ tên nhân viên
        public static int brand = 0;        // Chi nhánh

        // Tạo mới đơn đặt hàng
        /**********************************************
         * maKhoDuocChon | maVatTuDuocChon biến lưu trữ mã kho được chọn phục vụ 
         * cho btnChonKhoHang trong phần tạo mới đơn đặt hàng
         * 
         * maSoDonDatHangDuocChon luu tru ma don hang duoc chon phuc vu
         * cho btnChonDonDatHang trong phan tao moi phieu nhap
         * soLuongVatTu bien luu tru so luong vat tu duoc chon
         * 
         **********************************************/
        public static string maKhoDuocChon = "";
        public static string maVatTuDuocChon = "";

        // Phục vụ cho việc IN BÁO CÁO
        public static int soLuongVatTu = 0;
        public static string maDonDatHangDuocChon = "";
        public static string maDonDatHangDuocChonChiTiet = "";
        public static int donGia = 0;

        //  Phục vụ cho tính năng HOẠT ĐỘNG NHÂN VIÊN
        public static string maNhanVienDuocChon = "";
        public static string hoTen = "";
        public static string soCMND = "";
        public static string diaChi = "";
        public static string ngaySinh = "";

        // BindingSource -> Liên kết dữ liệu từ bảng dữ liệu vào chương trình
        // bidingsource danh sách phân mảnh
        public static BindingSource bindingSource = new BindingSource();

        // Các form của toàn dự án -> chỉ là con trỏ chưa phải là đối tượng (về sau mới là đối tượng)
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

        public static frmDanhSachNhanVien frmDanhSachNhanVien;
        public static frmDanhSachVatTu frmDanhSachVatTu;
        public static frmDonHangKhongPhieuNhap frmDonHangKhongPhieuNhap;

        public static frmChiTietSoLuongTriGiaHangHoaNhapXuat frmChiTietSoLuongTriGiaHangHoaNhapXuat;
        public static frmHoatDongNhanVien frmHoatDongNhanVien;
        public static frmTongHopNhapXuat frmTongHopNhapXuat;

        // Kết nối tới server phân mảnh
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
                return 1;
            }

            catch (Exception e)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu.\nXem lại tài khoản và mật khẩu.\n " + e.Message, "", MessageBoxButtons.OK);
                //Console.WriteLine(e.Message);
                return 0;
            }
        }

        // Chỉ lấy dữ liệu từ View chứ không thao tác
        public static SqlDataReader ExecSqlDataReader(String strLenh)
        {
            SqlDataReader myreader;
            // Tạo SqlCommnad để gọi sp, view, func gồm cmd và conn
            SqlCommand sqlcmd = new SqlCommand(strLenh, Program.conn);
            sqlcmd.CommandType = CommandType.Text; // Dùng Text cho tiện đỡ phải đặt Sp, View, ...
            if (Program.conn.State == ConnectionState.Closed)
                Program.conn.Open();
            try
            {
                myreader = sqlcmd.ExecuteReader(); return myreader;

            }
            catch (SqlException ex)
            {
                Program.conn.Close();
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        /**********************************************
         * ExecSqlDataTable thực hiện câu lệnh mà dữ liệu trả về có thể
         * xem - thêm - xóa - sửa tùy ý
         **********************************************/
        public static DataTable ExecSqlDataTable(String cmd)
        {
            DataTable dt = new DataTable();
            if (Program.conn.State == ConnectionState.Closed) Program.conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd, conn);
            da.Fill(dt);
            conn.Close();
            return dt;
        }

        // Cập nhật trên một stored procedure và không trả về giá trị
        public static int ExecSqlNonQuery(String strlenh)
        {
            SqlCommand Sqlcmd = new SqlCommand(strlenh, conn);
            Sqlcmd.CommandType = CommandType.Text;
            // Làm tự động hàng loạt bên CSDL
            Sqlcmd.CommandTimeout = 600;// 10 phut
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
                return ex.State;    // Trạng thái lỗi gởi từ RAISEERROR trong SQL

            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
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
