using QuanLyVatTu.SubForm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyVatTu
{
    public partial class frmNhanVien : Form
    {
        int viTri = 0; // vị trí con trỏ trên GridView
        bool dangThemMoi = false;
        String maCN = "";
        Stack undoList = new Stack();

        private Form CheckExists(Type ftype)
        {
            foreach (Form f in this.MdiChildren)
                if (f.GetType() == ftype)
                    return f;
            return null;
        }

        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            dataSet.EnforceConstraints = false;

            this.nhanVienTableAdapter.Connection.ConnectionString = Program.connstr;
            this.nhanVienTableAdapter.Fill(this.dataSet.NhanVien);

            this.phieuXuatTableAdapter.Connection.ConnectionString = Program.connstr;
            this.phieuXuatTableAdapter.Fill(this.dataSet.PhieuXuat);

            this.datHangTableAdapter.Connection.ConnectionString = Program.connstr;
            this.datHangTableAdapter.Fill(this.dataSet.DatHang);

            this.phieuNhapTableAdapter.Connection.ConnectionString = Program.connstr;
            this.phieuNhapTableAdapter.Fill(this.dataSet.PhieuNhap);

            maCN = ((DataRowView)bdsNhanVien[0])["MACN"].ToString(); // maCN của nhân viên đầu tiên
            cboChiNhanh.DataSource = Program.bindingSource; // Lấy bds từ frmDangNhap
            cboChiNhanh.DisplayMember = "TENCN";
            cboChiNhanh.ValueMember = "TENSERVER";
            cboChiNhanh.SelectedIndex = Program.brand;

            if (Program.role == "CONGTY")
            {
                cboChiNhanh.Enabled = true;

                this.btnThem.Enabled = false;
                this.btnXoa.Enabled = false;
                this.btnGhi.Enabled = false;

                this.btnHoanTac.Enabled = false;
                this.btnLamMoi.Enabled = true;
                this.btnChuyenChiNhanh.Enabled = false;
                this.btnThoat.Enabled = true;

                this.pnlNhapLieu.Enabled = false;
            }


            if (Program.role == "CHINHANH" || Program.role == "USER")
            {
                cboChiNhanh.Enabled = false;

                this.btnThem.Enabled = true;
                this.btnXoa.Enabled = true;
                this.btnGhi.Enabled = true;

                this.btnHoanTac.Enabled = false;
                this.btnLamMoi.Enabled = true;
                this.btnChuyenChiNhanh.Enabled = true;
                this.btnThoat.Enabled = true;

                this.pnlNhapLieu.Enabled = true;
                this.txtMaNV.Enabled = false;
            }

        }

        // Check xem nhân viên đủ 18 tuổi không
        private static int CalculateAge(DateTime dateOfBirth)
        {
            int age = 0;
            age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                age = age - 1;

            return age;
        }

        public frmNhanVien()
        {
            InitializeComponent();
        }

        private void btnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            viTri = bdsNhanVien.Position;       // Lấy vị trí hiện tại của con trỏ trong bds
            this.pnlNhapLieu.Enabled = true;
            dangThemMoi = true;

            bdsNhanVien.AddNew();
            txtMaCN.Text = maCN;
            dteNgaySinh.EditValue = "16-08-2003";
            txtLuong.Value = 4000000;

            this.txtMaNV.Enabled = true;
            this.btnThem.Enabled = false;
            this.btnXoa.Enabled = false;
            this.btnGhi.Enabled = true;
            this.btnHoanTac.Enabled = true;
            this.btnLamMoi.Enabled = false;
            this.btnChuyenChiNhanh.Enabled = false;
            this.btnThoat.Enabled = false;

            this.chkTrangThaiXoa.Checked = false;

            this.gclNhanVien.Enabled = false;
            this.pnlNhapLieu.Enabled = true;
        }

        private void btnHoanTac_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (dangThemMoi == true && this.btnThem.Enabled == false)
            {
                dangThemMoi = false;

                this.txtMaNV.Enabled = false;

                this.btnThem.Enabled = true;
                this.btnXoa.Enabled = true;
                this.btnGhi.Enabled = true;
                this.btnHoanTac.Enabled = false;
                this.btnLamMoi.Enabled = true;
                this.btnChuyenChiNhanh.Enabled = true;
                this.btnThoat.Enabled = true;

                this.chkTrangThaiXoa.Checked = false;

                this.gclNhanVien.Enabled = true;
                this.pnlNhapLieu.Enabled = true;

                bdsNhanVien.CancelEdit();       // Hủy bỏ dòng hiện tại
                bdsNhanVien.RemoveCurrent();    
                bdsNhanVien.Position = viTri;   // Trở về vị trí con trỏ trước đó
                return;
            }

            if (undoList.Count == 0)
            {
                MessageBox.Show("Không còn thao tác nào để hoàn tác", "Thông báo", MessageBoxButtons.OK);
                btnHoanTac.Enabled = false;
                return;
            }

            bdsNhanVien.CancelEdit();
            String cauTruyVanHoanTac = undoList.Pop().ToString();

            if (cauTruyVanHoanTac.Contains("sp_ChuyenChiNhanh"))
            {
                try
                {
                    String chiNhanhHienTai = Program.serverName;
                    String chiNhanhChuyenToi = Program.serverNameLeft;

                    Program.serverName = chiNhanhChuyenToi;
                    Program.loginName = Program.remoteLogin;
                    Program.loginPassword = Program.remotePassword;

                    if (Program.KetNoi() == 1)
                    {
                        return;
                    }

                    int n = Program.ExecSqlNonQuery(cauTruyVanHoanTac);

                    MessageBox.Show("Chuyển nhân viên trở lại thành công!", "Thông báo", MessageBoxButtons.OK);
                    Program.serverName = chiNhanhHienTai;
                    Program.loginName = Program.currentLogin;
                    Program.loginPassword = Program.currentPassword;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Chuyển nhân viên thất bại:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                if (Program.KetNoi() == 1)
                {
                    return;
                }
            }
            this.nhanVienTableAdapter.Fill(this.dataSet.NhanVien);
        }

        private void btnLamMoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                this.nhanVienTableAdapter.Fill(this.dataSet.NhanVien);
                this.gclNhanVien.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Làm mới:\n" + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            String maNV = ((DataRowView)bdsNhanVien[bdsNhanVien.Position])["MANV"].ToString();

            // Không cho xóa người đang đăng nhập - kể cả có hay không lập phiếu
            if (maNV == Program.userName)
            {
                MessageBox.Show("Không thể xóa chính tài khoản đang đăng nhập", "Thông báo", MessageBoxButtons.OK);
                return;
            }

            if (bdsNhanVien.Count == 0)
            {
                btnXoa.Enabled = false;
            }

            if (bdsDatHang.Count > 0)
            {
                MessageBox.Show("Không thể xóa nhân viên này vì đã lập đơn đặt hàng", "Thông báo", MessageBoxButtons.OK);
                return;
            }

            if (bdsPhieuNhap.Count > 0)
            {
                MessageBox.Show("Không thể xóa nhân viên này vì đã lập phiếu nhập", "Thông báo", MessageBoxButtons.OK);
                return;
            }

            if (bdsPhieuXuat.Count > 0)
            {
                MessageBox.Show("Không thể xóa nhân viên này vì đã lập phiếu xuất", "Thông báo", MessageBoxButtons.OK);
                return;
            }


            int trangThai = (chkTrangThaiXoa.Checked == true) ? 1 : 0;
            
            DateTime NGAYSINH = (DateTime)((DataRowView)bdsNhanVien[bdsNhanVien.Position])["NGAYSINH"];

            string cauTruyVanHoanTac =
                string.Format("INSERT INTO DBO.NHANVIEN(MANV,HO,TEN,SOCMND,DIACHI,NGAYSINH,LUONG,MACN,TrangThaiXoa)" +
                    "VALUES({0},'{1}','{2}','{3}','{4}',CAST({5} AS DATETIME),{6},'{7}', {8})",
                    txtMaNV.Text, txtHo.Text, txtTen.Text, txtCMND.Text, txtDiaChi.Text, NGAYSINH.ToString("yyyy-MM-dd"),
                    txtLuong.Value, txtMaCN.Text, trangThai);

            Console.WriteLine(cauTruyVanHoanTac);
            undoList.Push(cauTruyVanHoanTac);

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này không?", "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    viTri = bdsNhanVien.Position;
                    bdsNhanVien.RemoveCurrent();

                    this.nhanVienTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.nhanVienTableAdapter.Update(this.dataSet.NhanVien);

                    MessageBox.Show("Xóa nhân viên thành công!", "Thông báo", MessageBoxButtons.OK);
                    this.btnHoanTac.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa nhân viên: \n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    this.nhanVienTableAdapter.Fill(this.dataSet.NhanVien);

                    bdsNhanVien.Position = viTri;
                    return;
                }
            }
            else
            {
                undoList.Pop();
            }
        }

        private void cboChiNhanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboChiNhanh.SelectedValue.ToString() == "System.Data.DataRowView")
                return;

            Program.serverName = cboChiNhanh.SelectedValue.ToString();
            if (cboChiNhanh.SelectedIndex != Program.brand)
            {
                Program.loginName = Program.remoteLogin;
                Program.loginPassword = Program.remotePassword;
            }
            else
            {
                Program.loginName = Program.currentLogin;
                Program.loginPassword = Program.currentPassword;
            }

            if (Program.KetNoi() == 1)
            {
                MessageBox.Show("Không thể kết nối tới chi nhánh hiện tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                this.nhanVienTableAdapter.Connection.ConnectionString = Program.connstr;
                this.nhanVienTableAdapter.Fill(this.dataSet.NhanVien);

                this.datHangTableAdapter.Connection.ConnectionString = Program.connstr;
                this.datHangTableAdapter.Fill(this.dataSet.DatHang);

                this.phieuXuatTableAdapter.Connection.ConnectionString = Program.connstr;
                this.phieuNhapTableAdapter.Fill(this.dataSet.PhieuNhap);

                this.phieuXuatTableAdapter.Connection.ConnectionString = Program.connstr;
                this.phieuXuatTableAdapter.Fill(this.dataSet.PhieuXuat);
            }
        }

        private bool kiemTraDuLieuDauVao()
        {
            if (txtMaNV.Text == "")
            {
                MessageBox.Show("Không bỏ trống mã nhân viên", "Thông báo", MessageBoxButtons.OK);
                txtMaNV.Focus();
                return false;
            }
            if (Regex.IsMatch(txtMaNV.Text, @"^[0-9]+$") == false)
            {
                MessageBox.Show("Mã nhân viên chỉ chấp nhận số", "Thông báo", MessageBoxButtons.OK);
                txtMaNV.Focus();
                return false;
            }

            if (txtHo.Text == "")
            {
                MessageBox.Show("Không bỏ trống họ và tên", "Thông báo", MessageBoxButtons.OK);
                txtHo.Focus();
                return false;
            }
            if (Regex.IsMatch(txtHo.Text, @"^[A-Za-z ]+$") == false)
            {
                MessageBox.Show("Họ của người chỉ có chữ cái và khoảng trắng", "Thông báo", MessageBoxButtons.OK);
                txtHo.Focus();
                return false;
            }
            if (txtHo.Text.Length > 40)
            {
                MessageBox.Show("Họ không thể lớn hơn 40 kí tự", "Thông báo", MessageBoxButtons.OK);
                txtHo.Focus();
                return false;
            }

            if (txtTen.Text == "")
            {
                MessageBox.Show("Không bỏ trống Tên", "Thông báo", MessageBoxButtons.OK);
                txtTen.Focus();
                return false;
            }
            if (Regex.IsMatch(txtTen.Text, @"^[A-Za-z ]+$") == false)
            {
                MessageBox.Show("Tên người chỉ có chữ cái và khoảng trắng", "Thông báo", MessageBoxButtons.OK);
                txtTen.Focus();
                return false;
            }
            if (txtTen.Text.Length > 10)
            {
                MessageBox.Show("Tên không thể lớn hơn 10 kí tự", "Thông báo", MessageBoxButtons.OK);
                txtTen.Focus();
                return false;
            }

            if (txtCMND.Text == "")
            {
                MessageBox.Show("Không bỏ trống Số CMND", "Thông báo", MessageBoxButtons.OK);
                txtCMND.Focus();
                return false;
            }
            if (Regex.IsMatch(txtCMND.Text, @"^[0-9]+$") == false)
            {
                MessageBox.Show("Số CMND chỉ chỉ chấp nhận số", "Thông báo", MessageBoxButtons.OK);
                txtCMND.Focus();
                return false;
            }
            if (txtCMND.Text.Length > 20)
            {
                MessageBox.Show("Số CMND không thể lớn hơn 20 kí tự", "Thông báo", MessageBoxButtons.OK);
                txtCMND.Focus();
                return false;
            }
            if (txtCMND.Text.Length < 10)
            {
                MessageBox.Show("Số CMND không thể nhỏ hơn 10 kí tự", "Thông báo", MessageBoxButtons.OK);
                txtCMND.Focus();
                return false;
            }
            //foreach (char c in txtCMND.Text)
            //{
            //    if (!char.IsDigit(c))
            //    {
            //        MessageBox.Show("Số CMND chỉ được chứa các ký tự số từ 0 đến 9", "Thông báo", MessageBoxButtons.OK);
            //        txtCMND.Focus();
            //        return false;
            //    }
            //}

            if (txtDiaChi.Text == "")
            {
                MessageBox.Show("Không bỏ trống địa chỉ", "Thông báo", MessageBoxButtons.OK);
                txtDiaChi.Focus();
                return false;
            }
            if (Regex.IsMatch(txtDiaChi.Text, @"^[A-Za-z0-9, ]+$") == false)
            {
                MessageBox.Show("Địa chỉ chỉ chấp nhận chữ cái, số và khoảng trắng", "Thông báo", MessageBoxButtons.OK);
                txtDiaChi.Focus();
                return false;
            }
            if (txtDiaChi.Text.Length > 100)
            {
                MessageBox.Show("Không bỏ trống địa chỉ", "Thông báo", MessageBoxButtons.OK);
                txtDiaChi.Focus();
                return false;
            }

            if (CalculateAge(dteNgaySinh.DateTime) < 18)
            {
                MessageBox.Show("Nhân viên chưa đủ 18 tuổi", "Thông báo", MessageBoxButtons.OK);
                dteNgaySinh.Focus();
                return false;
            }

            if (txtLuong.Value < 4000000)
            {
                MessageBox.Show("Mức lương tối thiểu là 4,000,000 đồng", "Thông báo", MessageBoxButtons.OK);
                txtLuong.Focus();
                return false;
            }
            return true;
        }

        private void btnGhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool ketQua = kiemTraDuLieuDauVao();
            if (ketQua == false)
                return;

            String maNhanVien = txtMaNV.Text.Trim();
            DataRowView drv = ((DataRowView)bdsNhanVien[bdsNhanVien.Position]);
            String ho = txtHo.Text.ToString();
            String ten = txtTen.Text.ToString();
            String soCMND = txtCMND.Text.ToString();
            String diaChi = txtDiaChi.Text.ToString();

            DateTime ngaySinh = dteNgaySinh.DateTime;

            String luong = txtLuong.Value.ToString();
            int trangThai = (chkTrangThaiXoa.Checked == true) ? 1 : 0;

            String cauTruyVan =
                    "DECLARE	@result int " +
                    "EXEC @result = [dbo].[sp_TraCuu_KiemTraMaNhanVien] '" +
                    maNhanVien + "' " +
                    "SELECT 'Value' = @result"; ;
            SqlCommand sqlCommand = new SqlCommand(cauTruyVan, Program.conn);
            try
            {
                Program.myReader = Program.ExecSqlDataReader(cauTruyVan);
                if (Program.myReader == null)
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thực thi database thất bại!\n\n" + ex.Message, "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(ex.Message);
                return;
            }
            Program.myReader.Read();
            int result = int.Parse(Program.myReader.GetValue(0).ToString());
            Program.myReader.Close();

            int viTriConTro = bdsNhanVien.Position;
            int viTriMaNhanVien = bdsNhanVien.Find("MANV", txtMaNV.Text);

            if (result == 1 && viTriConTro != viTriMaNhanVien)
            {
                MessageBox.Show("Mã nhân viên này đã được sử dụng!", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            else
            {
                DialogResult dr = MessageBox.Show("Bạn có chắc muốn ghi dữ liệu vào cơ sở dữ liệu ?", "Thông báo",
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    try
                    {
                        btnThem.Enabled = true;
                        btnXoa.Enabled = true;
                        btnGhi.Enabled = true;
                        btnHoanTac.Enabled = true;

                        btnLamMoi.Enabled = true;
                        btnChuyenChiNhanh.Enabled = true;
                        btnThoat.Enabled = true;

                        this.txtMaNV.Enabled = false;
                        this.bdsNhanVien.EndEdit();
                        this.nhanVienTableAdapter.Update(this.dataSet.NhanVien);
                        this.gclNhanVien.Enabled = true;

                        String cauTruyVanHoanTac = "";
                        
                        // Trước khi ấn btnGhi là ấn btnThem
                        if (dangThemMoi == true)
                        {
                            cauTruyVanHoanTac = "" +
                                "DELETE DBO.NHANVIEN " +
                                "WHERE MANV = " + txtMaNV.Text.Trim();
                        }
                        // Trước khi ấn btnGhi là btnSua
                        else
                        {
                            cauTruyVanHoanTac =
                                "UPDATE DBO.NhanVien " +
                                "SET " +
                                "HO = '" + ho + "', " +
                                "TEN = '" + ten + "', " +
                                "SOCMND = '" + soCMND + "', " +
                                "DIACHI = '" + diaChi + "', " +
                                "NGAYSINH = CAST('" + ngaySinh.ToString("yyyy-MM-dd") + "' AS DATETIME)," +
                                "LUONG = '" + luong + "', " +
                                "MACN = '" + maCN.Trim() + "', " +
                                "TrangThaiXoa = '" + trangThai + "' " +
                                "WHERE MANV = '" + maNhanVien + "'";
                        }
                        Console.WriteLine(cauTruyVanHoanTac);

                        undoList.Push(cauTruyVanHoanTac);
                        dangThemMoi = false;
                        MessageBox.Show("Ghi thành công", "Thông báo", MessageBoxButtons.OK);
                    }
                    catch (Exception ex)
                    {

                        bdsNhanVien.RemoveCurrent();
                        MessageBox.Show("Không thể thực hiện hành động này\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        public void chuyenChiNhanh(String chiNhanh)
        {
            if (Program.serverName == chiNhanh)
            {
                MessageBox.Show("Hãy chọn chi nhánh khác chi nhánh bạn đang đăng nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            String maChiNhanhHienTai = "";
            String maChiNhanhMoi = "";
            int viTriHienTai = bdsNhanVien.Position;
            String maNhanVien = ((DataRowView)bdsNhanVien[viTriHienTai])["MANV"].ToString();

            if (chiNhanh.Contains("1"))
            {
                maChiNhanhHienTai = "CN2";
                maChiNhanhMoi = "CN1";
            }
            else if (chiNhanh.Contains("2"))
            {
                maChiNhanhHienTai = "CN1";
                maChiNhanhMoi = "CN2";
            }
            else
            {
                MessageBox.Show("Mã chi nhánh đã chọn không hợp lệ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Console.WriteLine("Mã CN hiện tại " + maChiNhanhHienTai);
            Console.WriteLine("Mã CN mới: " + maChiNhanhMoi);

            String cauTruyVanHoanTac = "EXEC sp_ChuyenChiNhanh " + maNhanVien + ",'" + maChiNhanhHienTai + "'";
            undoList.Push(cauTruyVanHoanTac);

            Program.serverNameLeft = chiNhanh; // Lấy tên chi nhánh chuyển tới để dùng hoàn tác khi cần

            String cauTruyVan = "EXEC sp_ChuyenChiNhanh " + maNhanVien + ",'" + maChiNhanhMoi + "'";

            SqlCommand sqlcommand = new SqlCommand(cauTruyVan, Program.conn);
            try
            {
                Program.myReader = Program.ExecSqlDataReader(cauTruyVan);
                MessageBox.Show("Chuyển chi nhánh thành công", "Thông báo", MessageBoxButtons.OK);

                if (Program.myReader == null)
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể thực hiện hành động này: \n" + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(ex.Message);
                return;
            }
            this.nhanVienTableAdapter.Update(this.dataSet.NhanVien);
            this.nhanVienTableAdapter.Fill(this.dataSet.NhanVien);
        }

        private void btnChuyenChiNhanh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int viTriHienTai = bdsNhanVien.Position;
            string maNhanVien = ((DataRowView)(bdsNhanVien[viTriHienTai]))["MANV"].ToString();

            if (maNhanVien == Program.userName)
            {
                MessageBox.Show("Không thể chuyển chính người đang đăng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (chkTrangThaiXoa.Checked)
            {
                MessageBox.Show("Nhân viên này không còn ở chi nhánh này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Form f = this.CheckExists(typeof(frmChuyenChiNhanh));
            if (f != null)
            {
                f.Activate();
            }
            frmChuyenChiNhanh form = new frmChuyenChiNhanh();
            form.Show();

            // Mang hàm chuyenChiNhanh từ frm này sang frmChuyenChiNhanh để thực hiện
            form.branchTransfer = new frmChuyenChiNhanh.MyDelegate(chuyenChiNhanh);

            this.btnHoanTac.Enabled = true;
        }
    }
}
