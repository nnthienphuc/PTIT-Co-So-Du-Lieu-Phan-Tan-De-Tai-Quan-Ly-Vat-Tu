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
        // vị trí của con trỏ trên grid view
        int viTri = 0;

        // false -> có thể là btnGhi hoặc btnXoa
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
            /*Step 1*/
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
            /*Step 2*/
            cboChiNhanh.DataSource = Program.bindingSource; // sao chép binding source từ login form
            cboChiNhanh.DisplayMember = "TENCN";
            cboChiNhanh.ValueMember = "TENSERVER";
            cboChiNhanh.SelectedIndex = Program.brand;

            /*Step 3*/
            // CONGTY chỉ xem dữ liệu
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

            // CHI NHANH & USER co the xem - xoa - sua du lieu nhung khong the 
            // chuyen sang chi nhanh khac
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

        private void nhanVienBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsNhanVien.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dataSet);

        }


        private void btnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }


        /*********************************************************************
         * bdsNhanVien.Position - vitri phuc vu cho btnHOANTAC. Gia su, co 5 nhan vien, con tro chuot
         * dang dung o vi tri nhan vien thu 2 thi chung ta an nut THEM
         * nhung neu chon btnHOANTAC, con tro chuot phai quay lai vi 
         * tri nhan vien thu 2, thay vi o vi tri duoi cung - tuc nhan vien so 5
         * 
         * neu nhap chu cho txtMANV thi se khong chuyen sang cac o khac duoc nua - bat buoc ghi so
         * 
         * Step 1: Kich hoat panel Nhap lieu & lay vi tri cua nhan vien hien tai
         * dat dangThemMoi = true
         * Step 2: gui lenh them moi toi bdsNHANVIEN - tu dong lay maChiNhanh - bo trong dteNGAYSINH
         * Step 3: vo hieu hoa cac nut chuc nang & gridControl - chi btnGHI & btnHOANTAC moi duoc hoat dong
         *********************************************************************/
        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            /*Step 1*/
            /*lấy vị trí hiện tại của con trỏ*/
            viTri = bdsNhanVien.Position;
            this.pnlNhapLieu.Enabled = true;
            dangThemMoi = true;

            /*Step 2*/
            /*AddNew tự động nhảy xuống cuối thêm 1 dòng mới*/
            bdsNhanVien.AddNew();
            txtMaCN.Text = maCN;
            dteNgaySinh.EditValue = "";
            txtLuong.Value = 4000000;

            /*Step 3*/
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

                bdsNhanVien.CancelEdit();
                /*xóa dòng hiện tại*/
                bdsNhanVien.RemoveCurrent();
                /* trở về lúc đầu con trỏ đang đứng*/
                bdsNhanVien.Position = viTri;
                return;
            }

            if (undoList.Count == 0)
            {
                MessageBox.Show("Không còn thao tác nào để khôi phục", "Thông báo", MessageBoxButtons.OK);
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

                    if (Program.KetNoi() == 0)
                    {
                        return;
                    }

                    int n = Program.ExecSqlNonQuery(cauTruyVanHoanTac);

                    MessageBox.Show("Chuyển nhân viên trở lại thành công", "Thông báo", MessageBoxButtons.OK);
                    Program.serverName = chiNhanhHienTai;
                    Program.loginName = Program.currentLogin;
                    Program.loginPassword = Program.currentPassword;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Chuyển nhân viên thất bại \n" + ex.Message, "Thông báo", MessageBoxButtons.OK);
                    return;
                }

            }
            else
            {
                if (Program.KetNoi() == 0)
                {
                    return;
                }
                int n = Program.ExecSqlNonQuery(cauTruyVanHoanTac);

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
                MessageBox.Show("Lỗi Làm mới" + ex.Message, "Thông báo", MessageBoxButtons.OK);
                return;
            }
        }

        /***************************************************************************
         * Step 1: tu biding source kiem tra xem nhan vien nay da lap don hang - 
         * phieu nhap - phieu xuat chua ?
         *          Neu co thi thong bao la khong the xoa va ket thuc
         *          Neu khong thi bat dau xoa
         * Step 2: Neu chon OK thi tien hanh xoa
         * Step 3: Lay ma nhan vien bi xoa roi luu lai trong manv
         * Step 4: Truong hop xoa nhan vien bi loi thi quay lai dung vi tri manv bi loi
         ***************************************************************************/
        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            String maNV = ((DataRowView)bdsNhanVien[bdsNhanVien.Position])["MANV"].ToString();

            // không cho xóa người đang đăng nhập - kể cả có hay không lập phiếu
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

            /* Phần này phục vụ tính năng hoàn tác
                    * Đưa câu truy vấn hoàn tác vào undoList */
            int trangThai = (chkTrangThaiXoa.Checked == true) ? 1 : 0;
            /*Lấy ngày sinh trong grid view*/
            DateTime NGAYSINH = (DateTime)((DataRowView)bdsNhanVien[bdsNhanVien.Position])["NGAYSINH"];

            string cauTruyVanHoanTac =
                string.Format("INSERT INTO DBO.NHANVIEN(MANV,HO,TEN,SOCMND,DIACHI,NGAYSINH,LUONG,MACN)" +
            "VALUES({0},'{1}','{2}','{3}','{4}',CAST({5} AS DATETIME), {6},'{7}')", txtMaNV.Text, txtHo.Text, txtTen.Text, txtCMND.Text, txtDiaChi.Text, NGAYSINH.ToString("yyyy-MM-dd"), txtLuong.Value, txtMaCN.Text.Trim());

            Console.WriteLine(cauTruyVanHoanTac);
            undoList.Push(cauTruyVanHoanTac);

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này không ?", "Thông báo",
                MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    viTri = bdsNhanVien.Position;
                    bdsNhanVien.RemoveCurrent();

                    this.nhanVienTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.nhanVienTableAdapter.Update(this.dataSet.NhanVien);

                    MessageBox.Show("Xóa thành công ", "Thông báo", MessageBoxButtons.OK);
                    this.btnHoanTac.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa nhân viên. Hãy thử lại\n" + ex.Message, "Thông báo", MessageBoxButtons.OK);
                    this.nhanVienTableAdapter.Fill(this.dataSet.NhanVien);
                    // trở về vị trí của nhân viên còn lỗi
                    bdsNhanVien.Position = viTri;
                    //bdsNhanVien.Position = bdsNhanVien.Find("MANV", manv);
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

            if (Program.KetNoi() == 0)
            {
                MessageBox.Show("Xảy ra lỗi kết nối với chi nhánh hiện tại", "Thông báo", MessageBoxButtons.OK);
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

        /**
         * viTriConTro: vi tri con tro chuot dang dung
         * viTriMaNhanVien: vi tri cua ma nhan vien voi btnTHEM or hanh dong sua du lieu
         * sp_TraCuu_KiemTraMaNhanVien tra ve 0 neu khong ton tai
         *                                    1 neu ton tai
         *                                    
         * Step 0 : Kiem tra du lieu dau vao
         * Step 1 : Dung stored procedure sp_TraCuu_KiemTraMaNhanVien de kiem tra txtMANV
         * Step 2 : Ket hop ket qua tu Step 1 & vi tri cua txtMANV co 2 truong hop xay ra
         * + TH0: ketQua = 1 && viTriConTro != viTriMaNhanVien -> them moi nhung MANV da ton tai
         * + TH1: ketQua = 1 && viTriConTro == viTriMaNhanVien -> sua nhan vien cu
         * + TH2: ketQua = 0 && viTriConTro == viTriMaNhanVien -> co the them moi nhan vien
         * + TH3: ketQua = 0 && viTriConTro != viTriMaNhanVien -> co the them moi nhan vien
         *          
         * Step 3 : Neu khong phai TH0 thi cac TH1 - TH2 - TH3 deu hop le 
         */
        private void btnGhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            /* Step 0 */
            bool ketQua = kiemTraDuLieuDauVao();
            if (ketQua == false)
                return;

            /*Step 1*/
            /*Lay du lieu truoc khi chon btnGHI - phuc vu btnHOANTAC - sau khi OK thi da la du lieu moi*/
            String maNhanVien = txtMaNV.Text.Trim();// Trim() de loai bo khoang trang thua
            DataRowView drv = ((DataRowView)bdsNhanVien[bdsNhanVien.Position]);
            String ho = drv["HO"].ToString();
            String ten = drv["TEN"].ToString();
            String soCMND = drv["SOCMND"].ToString();
            String diaChi = drv["DIACHI"].ToString();

            DateTime ngaySinh = ((DateTime)drv["NGAYSINH"]);
            Console.WriteLine(ngaySinh);

            int luong = int.Parse(drv["LUONG"].ToString());
            String maChiNhanh = drv["MACN"].ToString();
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
                /*khong co ket qua tra ve thi ket thuc luon*/
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

            /*Step 2*/
            int viTriConTro = bdsNhanVien.Position;
            int viTriMaNhanVien = bdsNhanVien.Find("MANV", txtMaNV.Text);

            if (result == 1 && viTriConTro != viTriMaNhanVien)
            {
                MessageBox.Show("Mã nhân viên này đã được sử dụng!", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            else/*them moi | sua nhan vien*/
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
                        /*trước khi ấn btnGHI là btnTHEM*/
                        if (dangThemMoi == true)
                        {
                            cauTruyVanHoanTac = "" +
                                "DELETE DBO.NHANVIEN " +
                                "WHERE MANV = " + txtMaNV.Text.Trim();
                        }
                        /*trước khi ấn btnGHI là sửa thông tin nhân viên*/
                        else
                        {
                            cauTruyVanHoanTac =
                                "UPDATE DBO.NhanVien " +
                                "SET " +
                                "HO = '" + ho + "'," +
                                "TEN = '" + ten + "'," +
                                "SOCMND = '" + soCMND + "'," +
                                "DIACHI = '" + diaChi + "'," +
                                "NGAYSINH = CAST('" + ngaySinh.ToString("yyyy-MM-dd") + "' AS DATETIME)," +
                                "LUONG = '" + luong + "'," +
                                "TrangThaiXoa = " + trangThai + " " +
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
                        MessageBox.Show("Thất bại. Vui lòng kiểm tra lại!\n" + ex.Message, "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /**************************************************************
         * Step 1: kiêm tra xem có nằm trên cùng chi nhánh không
         * Step 2: chuẩn bị các biến để lưu tên chi nhánh hiện tại và chi nhánh chuyển tới, tên nhân viên được chuyển
         * Step 3: trước khi thực hiện, lưu sẵn câu lệnh hoàn tác vào undoList + tên chi nhánh tới
         * Step 4: thực hiện chuyển chi nhánh với sp_ChuyenChiNhanh
         **************************************************************/
        public void chuyenChiNhanh(String chiNhanh)
        {
            /*Step 1*/
            if (Program.serverName == chiNhanh)
            {
                MessageBox.Show("Hãy chọn chi nhánh khác chi nhánh bạn đang đăng nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            /*Step 2*/
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
                MessageBox.Show("Mã chi nhánh không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Console.WriteLine("Ma chi nhanh hien tai: " + maChiNhanhHienTai);
            Console.WriteLine("Ma chi nhanh moi: " + maChiNhanhMoi);

            String cauTruyVanHoanTac = "EXEC sp_ChuyenChiNhanh " + maNhanVien + ",'" + maChiNhanhHienTai + "'";
            undoList.Push(cauTruyVanHoanTac);

            Program.serverNameLeft = chiNhanh; /*Lấy tên chi nhánh tới để làm tính năng hoàn tác*/
            Console.WriteLine("Ten server con lai: " + Program.serverNameLeft);

            String cauTruyVan = "EXEC sp_ChuyenChiNhanh " + maNhanVien + ",'" + maChiNhanhMoi + "'";
            Console.WriteLine("Cau Truy Van: " + cauTruyVan);
            Console.WriteLine("Cau Truy Van Hoan Tac: " + cauTruyVanHoanTac);

            SqlCommand sqlcommand = new SqlCommand(cauTruyVan, Program.conn);
            try
            {
                Program.myReader = Program.ExecSqlDataReader(cauTruyVan);
                MessageBox.Show("Chuyển chi nhánh thành công", "Thông báo", MessageBoxButtons.OK);

                if (Program.myReader == null)
                {
                    return;/*khong co ket qua tra ve thi ket thuc luon*/
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thực thi database thất bại!\n\n" + ex.Message, "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(ex.Message);
                return;
            }
            this.nhanVienTableAdapter.Update(this.dataSet.NhanVien);
        }

        private void btnChuyenChiNhanh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int viTriHienTai = bdsNhanVien.Position;
            //int trangThaiXoa = int.Parse(((DataRowView)(bdsNhanVien[viTriHienTai]))["TrangThaiXoa"].ToString());
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
            /*Kiem tra xem form da co trong bo nho chua*/
            Form f = this.CheckExists(typeof(frmChuyenChiNhanh));
            if (f != null)
            {
                f.Activate();
            }
            frmChuyenChiNhanh form = new frmChuyenChiNhanh();
            form.Show();

            /*đóng gói hàm chuyenChiNhanh từ formNHANVIEN đem về formChuyenChiNhanh để làm việc*/
            form.branchTransfer = new frmChuyenChiNhanh.MyDelegate(chuyenChiNhanh);

            /*Step 4*/
            this.btnHoanTac.Enabled = true;
        }
    }
}
