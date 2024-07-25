using DevExpress.XtraGrid;
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
    public partial class frmPhieuXuat : Form
    {
        int viTri = 0;              // vị trí con trỏ trên GridView
        
        bool dangThemMoi = false;

        public string makho = "";
        string maChiNhanh = "";
        
        Stack undoList = new Stack();

        BindingSource bds = null;
        GridControl gc = null;
        string type = "";       // phieuxuat hay chitietphieuxuat

        public frmPhieuXuat()
        {
            InitializeComponent();
        }

        private void phieuXuatBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsPhieuXuat.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dataSet);

        }
        private void frmPhieuXuat_Load(object sender, EventArgs e)
        {
            dataSet.EnforceConstraints = false;

            this.phieuXuatTableAdapter.Connection.ConnectionString = Program.connstr;
            this.phieuXuatTableAdapter.Fill(this.dataSet.PhieuXuat);

            this.chiTietPhieuXuatTableAdapter.Connection.ConnectionString = Program.connstr;
            this.chiTietPhieuXuatTableAdapter.Fill(this.dataSet.CTPX);

            cboChiNhanh.DataSource = Program.bindingSource; // lấy bds từ frmDangNhap
            cboChiNhanh.DisplayMember = "TENCN";
            cboChiNhanh.ValueMember = "TENSERVER";
            cboChiNhanh.SelectedIndex = Program.brand;

        }

        private void btnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
        }

        private void btnCheDoDoPhieuXuat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnMeNuChonCheDo.Links[0].Caption = "Phiếu Xuất";

            bds = bdsPhieuXuat;
            gc = gclChiTietPhieuXuat;

            txtMaPhieuXuat.Enabled = false;
            txtNgay.Enabled = false;
            txtTenKhachHang.Enabled = true;
            txtMaNhanVien.Enabled = false;
            btnChonKhoHang.Enabled = true;
            txtMaKho.Enabled = false;

            txtMaVatTu.Enabled = false;
            btnChonVatTu.Enabled = false;
            txtSoLuong.Enabled = false;
            txtDonGia.Enabled = false;

            gcPhieuXuat.Enabled = true;
            gclChiTietPhieuXuat.Enabled = true;

            if (Program.role == "CONGTY")
            {
                cboChiNhanh.Enabled = true;

                this.btnThem.Enabled = false;
                this.btnXoa.Enabled = false;
                this.btnGhi.Enabled = false;

                this.btnHoanTac.Enabled = false;
                this.btnLamMoi.Enabled = true;
                this.btnMeNuChonCheDo.Enabled = true;
                this.btnThoat.Enabled = true;

                this.gbxPhieuXuat.Enabled = false;
            }
            if (Program.role == "CHINHANH" || Program.role == "USER")
            {
                cboChiNhanh.Enabled = false;

                this.btnThem.Enabled = true;
                bool turnOn = (bdsPhieuXuat.Count > 0) ? true : false;
                this.btnXoa.Enabled = turnOn;
                this.btnGhi.Enabled = true;

                this.btnHoanTac.Enabled = false;
                this.btnLamMoi.Enabled = true;
                this.btnMeNuChonCheDo.Enabled = true;
                this.btnThoat.Enabled = true;
            }
        }
        private void btnCheDoChiTietPhieuXuat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnMeNuChonCheDo.Links[0].Caption = "Chi Tiết Phiếu Xuất";
            bds = bdsChiTietPhieuXuat;

            if (Program.role == "CONGTY")
            {
                cboChiNhanh.Enabled = true;
                this.txtTenKhachHang.Enabled = false;
                this.btnChonKhoHang.Enabled = false;
                this.btnThem.Enabled = false;
                this.btnXoa.Enabled = false;
                this.btnGhi.Enabled = false;

                this.btnHoanTac.Enabled = false;
                this.btnLamMoi.Enabled = true;
                this.btnMeNuChonCheDo.Enabled = true;
                this.btnThoat.Enabled = true;

                this.gbxPhieuXuat.Enabled = false;
            }
            
            if (Program.role == "CHINHANH" || Program.role == "USER")
            {
                cboChiNhanh.Enabled = false;
                this.txtTenKhachHang.Enabled = false;
                this.btnChonKhoHang.Enabled = false;
                this.btnThem.Enabled = true;
                bool turnOn = (bdsPhieuXuat.Count > 0) ? true : false;
                this.btnXoa.Enabled = turnOn;
                this.btnGhi.Enabled = true;

                this.btnHoanTac.Enabled = false;
                this.btnLamMoi.Enabled = true;
                this.btnMeNuChonCheDo.Enabled = true;
                this.btnThoat.Enabled = true;
            }
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            viTri = bds.Position;       // Vị trí hiện tại của con trỏ
            dangThemMoi = true;

            bds.AddNew();               // Thêm 1 dòng mới ở cuối table
            if (btnMeNuChonCheDo.Links[0].Caption == "Phiếu Xuất")
            {
                this.gbxPhieuXuat.Enabled =true;
                this.txtMaPhieuXuat.Enabled = true;

                this.txtNgay.EditValue = DateTime.Now;
                this.txtNgay.Enabled = false;

                this.txtTenKhachHang.Enabled = true;
                this.txtMaNhanVien.Text = Program.userName;

                this.btnChonKhoHang.Enabled = true;
                this.txtMaKho.Text = Program.maKhoDuocChon;

                this.txtMaVatTu.Enabled = false;
                this.btnChonVatTu.Enabled = false;
                this.txtSoLuong.Enabled = false;
                this.txtDonGia.Enabled = false;

                ((DataRowView)(bdsPhieuXuat.Current))["NGAY"] = DateTime.Now;
                ((DataRowView)(bdsPhieuXuat.Current))["MANV"] = Program.userName;
                ((DataRowView)(bdsPhieuXuat.Current))["MAKHO"] =
                Program.maKhoDuocChon;

            }

            if (btnMeNuChonCheDo.Links[0].Caption == "Chi Tiết Phiếu Xuất")
            {

                DataRowView drv = ((DataRowView)bdsPhieuXuat[bdsPhieuXuat.Position]);
                String maNhanVien = drv["MANV"].ToString();
                if (Program.userName != maNhanVien)
                {
                    MessageBox.Show("Không thể thêm chi tiết phiếu xuất trên phiếu  không phải do mình tạo", "Thông báo", MessageBoxButtons.OK);
                    return;
                }

                ((DataRowView)(bdsChiTietPhieuXuat.Current))["MAPX"] = ((DataRowView)(bdsPhieuXuat.Current))["MAPX"];
                ((DataRowView)(bdsChiTietPhieuXuat.Current))["MAVT"] = Program.maVatTuDuocChon;

                this.txtMaVatTu.Enabled = false;
                this.btnChonVatTu.Enabled = true;

                this.txtSoLuong.Enabled = true;
                this.txtSoLuong.EditValue = 1;

                this.txtDonGia.Enabled = true;
                this.txtDonGia.EditValue = 1;
            }
            this.btnThem.Enabled = false;
            this.btnXoa.Enabled = false;
            this.btnGhi.Enabled = true;

            this.btnHoanTac.Enabled = true;
            this.btnLamMoi.Enabled = false;
            this.btnMeNuChonCheDo.Enabled = false;
            this.btnThoat.Enabled = false;

            gcPhieuXuat.Enabled = false;
            gclChiTietPhieuXuat.Enabled = false;
        }

        private void btnChonKhoHang_Click(object sender, EventArgs e)
        {
            frmChonKhoHang form = new frmChonKhoHang();
            form.ShowDialog();

            this.txtMaKho.Text = Program.maKhoDuocChon;
        }

        private void btnChonVatTu_Click(object sender, EventArgs e)
        {
            frmChonVatTu form = new frmChonVatTu();
            form.ShowDialog();
            this.txtMaVatTu.Text = Program.maVatTuDuocChon;
        }

        private bool kiemTraDuLieuDauVao(string cheDo)
        {
            if (cheDo == "Phiếu Xuất")
            {
                DataRowView drv = ((DataRowView)bdsPhieuXuat[bdsPhieuXuat.Position]);
                String maNhanVien = drv["MANV"].ToString();
                if (Program.userName != maNhanVien)
                {
                    MessageBox.Show("Không thể sửa phiếu xuất do người khác tạo", "Thông báo", MessageBoxButtons.OK);
                    return false;
                }

                if (txtMaPhieuXuat.Text == "")
                {
                    MessageBox.Show("Không bỏ trống mã phiếu nhập !", "Thông báo", MessageBoxButtons.OK);
                    txtMaPhieuXuat.Focus();
                    return false;
                }

                if (Regex.IsMatch(txtMaPhieuXuat.Text, @"^[A-Za-z 0-9 ]+$") == false)
                {
                    MessageBox.Show("Mã phiếu nhập chỉ có chữ cái và khoảng trắng", "Thông báo", MessageBoxButtons.OK);
                    return false;
                }

                if (txtMaPhieuXuat.Text.Length > 8)
                {
                    MessageBox.Show("Mã phiếu xuất không thể quá 8 kí tự !", "Thông báo", MessageBoxButtons.OK);
                    txtMaPhieuXuat.Focus();
                    return false;
                }

                if (txtTenKhachHang.Text == "")
                {
                    MessageBox.Show("Không bỏ trống tên khách hàng !", "Thông báo", MessageBoxButtons.OK);
                    txtTenKhachHang.Focus();
                    return false;
                }

                if (Regex.IsMatch(txtTenKhachHang.Text, @"^[A-Za-z ]+$") == false)
                {
                    MessageBox.Show("Tên khách hàng chỉ có chữ cái và khoảng trắng", "Thông báo", MessageBoxButtons.OK);
                    return false;
                }


                if (txtTenKhachHang.Text.Length > 100)
                {
                    MessageBox.Show("Tên khách hàng không quá 100 kí tự !", "Thông báo", MessageBoxButtons.OK);
                    txtTenKhachHang.Focus();
                    return false;
                }

                if (txtMaKho.Text == "")
                {
                    MessageBox.Show("Không bỏ trống mã kho !", "Thông báo", MessageBoxButtons.OK);
                    return false;
                }

                if (Regex.IsMatch(txtMaKho.Text, @"^[A-Za-z 0-9 ]+$") == false)
                {
                    MessageBox.Show("Mã kho chỉ có chữ cái và khoảng trắng", "Thông báo", MessageBoxButtons.OK);
                    return false;
                }


            }

            if (cheDo == "Chi Tiết Phiếu Xuất")
            {
                DataRowView drv = ((DataRowView)bdsPhieuXuat[bdsPhieuXuat.Position]);
                String maNhanVien = drv["MANV"].ToString();
                if (Program.userName != maNhanVien)
                {
                    MessageBox.Show("Không thể thêm chi tiết phiếu xuất với phiếu xuất do người khác tạo !", "Thông báo", MessageBoxButtons.OK);
                    bdsChiTietPhieuXuat.RemoveCurrent();
                    return false;
                }

                if (txtMaPhieuXuat.Text == "")
                {
                    MessageBox.Show("Không bỏ trống mã phiếu nhập !", "Thông báo", MessageBoxButtons.OK);
                    txtMaPhieuXuat.Focus();
                    return false;
                }

                if (txtMaPhieuXuat.Text.Length > 8)
                {
                    MessageBox.Show("Mã phiếu xuất không thể quá 8 kí tự !", "Thông báo", MessageBoxButtons.OK);
                    txtMaPhieuXuat.Focus();
                    return false;
                }

                if (txtMaVatTu.Text == "")
                {
                    MessageBox.Show("Thiếu mã vật tư !", "Thông báo", MessageBoxButtons.OK);
                    txtMaVatTu.Focus();
                    return false;
                }

                if (txtMaVatTu.Text.Length > 4)
                {
                    MessageBox.Show("Mã vật tư không quá 4 kí tự !", "Thông báo", MessageBoxButtons.OK);
                    txtMaVatTu.Focus();
                    return false;
                }

                if (txtSoLuong.Value < 0 || txtSoLuong.Value > Program.soLuongVatTu)
                {
                    MessageBox.Show("Số lượng vật tư không thể bé hơn 0 & lớn hơn số lượng vật tư đang có trong kho hàng !", "Thông báo", MessageBoxButtons.OK);
                    txtSoLuong.Focus();
                    return false;
                }

                if (txtDonGia.Value < 0)
                {
                    MessageBox.Show("Đơn giá không thể bé hơn 0 VND !", "Thông báo", MessageBoxButtons.OK);
                    txtDonGia.Focus();
                    return false;
                }
            }

            return true;
        }
        private string taoCauTruyVanHoanTac(string cheDo)
        {
            String cauTruyVan = "";
            DataRowView drv;

            // Đang sửa Phiếu xuất đã có sẵn
            if (cheDo == "Phiếu Xuất" && dangThemMoi == false)
            {
                drv = ((DataRowView)(bdsPhieuXuat.Current));
                DateTime ngay = (DateTime)drv["NGAY"];


                cauTruyVan = "UPDATE DBO.PHIEUXUAT " +
                    "SET " +
                    "NGAY = CAST('" + ngay.ToString("yyyy-MM-dd") + "' AS DATETIME), " +
                    "HOTENKH = '" + drv["HOTENKH"].ToString().Trim() + "', " +
                    "MANV = '" + drv["MANV"].ToString().Trim() + "', " +
                    "MAKHO = '" + drv["MAKHO"].ToString().Trim() + "' " +
                    "WHERE MAPX = '" + drv["MAPX"].ToString().Trim() + "' ";
            }
            // Sửa chi tiết phiếu xuất đã có sẵn
            if (cheDo == "Chi Tiết Phiếu Xuất" && dangThemMoi == false)
            {
                drv = ((DataRowView)(bdsChiTietPhieuXuat.Current));
                int soLuong = int.Parse(drv["SOLUONG"].ToString().Trim());
                float donGia = float.Parse(drv["DONGIA"].ToString().Trim());
                String maPhieuXuat = drv["MAPX"].ToString().Trim();
                String maVatTu = drv["MAVT"].ToString().Trim();

                cauTruyVan = "UPDATE DBO.CTPX " +
                    "SET " +
                    "SOLUONG = " + soLuong + " " +
                    "DONGIA = " + donGia + " " +
                    "WHERE MAPX = '" + maPhieuXuat + "' " +
                    "AND MAVT = '" + maVatTu + "' ";
            }

            return cauTruyVan;
        }

        private void capNhatSoLuongVatTu(string maVatTu, string soLuong)
        {
            string cauTruyVan = "EXEC sp_CapNhatSoLuongVatTu 'EXPORT','" + maVatTu + "', " + soLuong;


            int n = Program.ExecSqlNonQuery(cauTruyVan);
            //Console.WriteLine("Line 411");
            //Console.WriteLine(cauTruyVan);
        }

        private void btnHoanTac_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (dangThemMoi == true && this.btnThem.Enabled == false)
            {
                dangThemMoi = false;

                if (btnMeNuChonCheDo.Links[0].Caption == "Phiếu Xuất")
                {
                    this.txtMaPhieuXuat.Enabled = false;
                    this.txtNgay.Enabled = false;
                    this.txtMaNhanVien.Enabled = false;
                    this.txtTenKhachHang.Enabled = true;

                    this.txtMaKho.Enabled = false;
                    this.btnChonKhoHang.Enabled = true;
                }
                if (btnMeNuChonCheDo.Links[0].Caption == "Chi Tiết Phiếu Nhập")
                {
                    this.txtMaPhieuXuat.Enabled = false;

                    this.txtMaVatTu.Enabled = false;
                    this.btnChonVatTu.Enabled = false;

                    this.txtSoLuong.Enabled = true;
                    this.txtDonGia.Enabled = true;
                }

                this.btnThem.Enabled = true;
                this.btnXoa.Enabled = true;
                this.btnGhi.Enabled = true;
                this.btnLamMoi.Enabled = true;
                this.btnMeNuChonCheDo.Enabled = true;
                this.btnThoat.Enabled = true;

                this.gcPhieuXuat.Enabled = true;
                this.gclChiTietPhieuXuat.Enabled = true;
                    
                bds.CancelEdit();       // Hủy bỏ dòng hiện tại
                bds.Position = viTri;   // Trở về vị trí ban đầu của con trỏ
                return;
            }

            if (undoList.Count == 0)
            {
                MessageBox.Show("Không còn thao tác nào để khôi phục", "Thông báo", MessageBoxButtons.OK);
                btnHoanTac.Enabled = false;
                return;
            }

            bds.CancelEdit();
            String cauTruyVanHoanTac = undoList.Pop().ToString();

            Console.WriteLine(cauTruyVanHoanTac);

            this.phieuXuatTableAdapter.Fill(this.dataSet.PhieuXuat);
            this.chiTietPhieuXuatTableAdapter.Fill(this.dataSet.CTPX);

            bdsPhieuXuat.Position = viTri;
        }

       
        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataRowView drv;
            string cauTruyVanHoanTac = "";
            string cheDo = (btnMeNuChonCheDo.Links[0].Caption == "Phiếu Xuất") ? "Phiếu Xuất" : "Chi Tiết Phiếu Xuất";

            if (cheDo == "Phiếu Xuất")
            {
                drv = ((DataRowView)bdsPhieuXuat[bdsPhieuXuat.Position]);
                String maNhanVien = drv["MANV"].ToString();
                if (Program.userName != maNhanVien)
                {
                    MessageBox.Show("Không xóa chi tiết phiếu xuất không phải do mình tạo", "Thông báo", MessageBoxButtons.OK);
                    return;
                }

                if (bdsChiTietPhieuXuat.Count > 0)
                {
                    MessageBox.Show("Không thể xóa vì có chi tiết phiếu xuất", "Thông báo", MessageBoxButtons.OK);
                    return;
                }


                DateTime ngay = ((DateTime)drv["NGAY"]);

                cauTruyVanHoanTac = "INSERT INTO DBO.PHIEUXUAT(MAPX, NGAY, HOTENKH, MANV, MAKHO) " +
                    "VALUES( '" + drv["MAPX"].ToString().Trim() + "', '" +
                    ngay.ToString("yyyy-MM-dd") + "', '" +
                    drv["HOTENKH"].ToString() + "', '" +
                    drv["MANV"].ToString() + "', '" +
                    drv["MAKHO"].ToString() + "')";
            }

            if (cheDo == "Chi Tiết Phiếu Xuất")
            {
                drv = ((DataRowView)bdsPhieuXuat[bdsPhieuXuat.Position]);
                String maNhanVien = drv["MANV"].ToString();
                if (Program.userName != maNhanVien)
                {
                    MessageBox.Show("Bạn không xóa chi tiết phiếu xuất không phải do mình tạo", "Thông báo", MessageBoxButtons.OK);
                    return;
                }

                drv = ((DataRowView)bdsChiTietPhieuXuat[bdsChiTietPhieuXuat.Position]);
                cauTruyVanHoanTac = "INSERT INTO DBO.CTPX(MAPX, MAVT, SOLUONG, DONGIA) " +
                    "VALUES('" + drv["MAPX"].ToString().Trim() + "', '" +
                    drv["MAVT"].ToString().Trim() + "', " +
                    drv["SOLUONG"].ToString().Trim() + ", " +
                    drv["DONGIA"].ToString().Trim() + ")";
            }
           
            undoList.Push(cauTruyVanHoanTac);


            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không ?", "Thông báo",
                MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    viTri = bds.Position;
                    if (cheDo == "Phiếu Xuất")
                    {
                        bdsPhieuXuat.RemoveCurrent();
                    }
                    if (cheDo == "Chi Tiết Phiếu Xuất")
                    {
                        bdsChiTietPhieuXuat.RemoveCurrent();
                    }


                    this.phieuXuatTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.phieuXuatTableAdapter.Update(this.dataSet.PhieuXuat);

                    this.chiTietPhieuXuatTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.chiTietPhieuXuatTableAdapter.Update(this.dataSet.CTPX);

                    dangThemMoi = false;
                    MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK);
                    this.btnHoanTac.Enabled = true;
                }
                catch (Exception ex)    
                {
                    MessageBox.Show("Lỗi xóa nhân viên: \n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.phieuXuatTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.phieuXuatTableAdapter.Update(this.dataSet.PhieuXuat);

                    this.chiTietPhieuXuatTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.chiTietPhieuXuatTableAdapter.Update(this.dataSet.CTPX);
                    
                    bds.Position = viTri;
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

            // Kiểm tra xem index đang chọn là của chi nhánh nào để phục vụ cho việc đổi login
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
                MessageBox.Show("Lỗi không thể kết nối tới chi nhánh hiện tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                this.phieuXuatTableAdapter.Connection.ConnectionString = Program.connstr;
                this.phieuXuatTableAdapter.Fill(this.dataSet.PhieuXuat);

                this.chiTietPhieuXuatTableAdapter.Connection.ConnectionString = Program.connstr;
                this.chiTietPhieuXuatTableAdapter.Fill(this.dataSet.CTPX);
            }
        }

        private void gclChiTietPhieuXuat_Click(object sender, EventArgs e)
        {

        }

        private void btnGhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            String cheDo = (btnMeNuChonCheDo.Links[0].Caption == "Phiếu Xuất") ? "Phiếu Xuất" : "Chi Tiết Phiếu Xuất";

            bool ketQua = kiemTraDuLieuDauVao(cheDo);
            if (ketQua == false) return;

            string cauTruyVanHoanTac = taoCauTruyVanHoanTac(cheDo);

            String maPhieuXuat = txtMaPhieuXuat.Text.Trim();
            String cauTruyVan =
                    "DECLARE	@result int " +
                    "EXEC @result = sp_KiemTraMaPhieuXuat '" +
                    maPhieuXuat + "' " +
                    "SELECT 'Value' = @result";
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
                MessageBox.Show("Không thể thực hiện hành động này: \n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(ex.Message);
                return;
            }
            Program.myReader.Read();
            int result = int.Parse(Program.myReader.GetValue(0).ToString());
            Program.myReader.Close();

            int viTriConTro = bdsPhieuXuat.Position;
            int viTriMaPhieuXuat = bdsPhieuXuat.Find("MAPX", maPhieuXuat);

            if (result == 1 && cheDo == "Phiếu Xuất" && viTriMaPhieuXuat != viTriConTro)
            {
                MessageBox.Show("Mã phiếu xuất đã được sử dụng!", "Thông báo", MessageBoxButtons.OK);
                txtMaPhieuXuat.Focus();
                return;
            }
            else
            {
                DialogResult dr = MessageBox.Show("Bạn có chắc muốn ghi dữ liệu vào cơ sở dữ liệu?", "Thông báo",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    try
                    {
                        if (cheDo == "Phiếu Xuất" && dangThemMoi == true)
                        {
                            cauTruyVanHoanTac =
                                "DELETE FROM DBO.PHIEUXUAT " +
                                "WHERE MAPX = '" + maPhieuXuat + "'";
                        }

                        if (cheDo == "Chi Tiết Phiếu Xuất" && dangThemMoi == true)
                        {
                            cauTruyVanHoanTac =
                                "DELETE FROM DBO.CTPN " +
                                "WHERE MAPN = '" + maPhieuXuat + "' " +
                                "AND MAVT = '" + Program.maVatTuDuocChon + "'";

                            string maVatTu = txtMaVatTu.Text.Trim();
                            string soLuong = txtSoLuong.Text.Trim();

                            capNhatSoLuongVatTu(maVatTu, soLuong);
                        }

                        undoList.Push(cauTruyVanHoanTac);
                        Console.WriteLine(cauTruyVanHoanTac);

                        this.bdsPhieuXuat.EndEdit();
                        this.bdsChiTietPhieuXuat.EndEdit();
                        this.phieuXuatTableAdapter.Update(this.dataSet.PhieuXuat);
                        this.chiTietPhieuXuatTableAdapter.Update(this.dataSet.CTPX);

                        this.txtMaPhieuXuat.Enabled = false;

                        this.btnThem.Enabled = true;
                        this.btnXoa.Enabled = true;
                        this.btnGhi.Enabled = true;

                        this.btnHoanTac.Enabled = true;
                        this.btnLamMoi.Enabled = true;
                        this.btnMeNuChonCheDo.Enabled = true;
                        this.btnThoat.Enabled = true;

                        this.gcPhieuXuat.Enabled = true;
                        this.gclChiTietPhieuXuat.Enabled = true;
                        dangThemMoi = false;
                        MessageBox.Show("Ghi thành công", "Thông báo", MessageBoxButtons.OK);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        bds.RemoveCurrent();
                        MessageBox.Show("Không thể thực hiện hành động này: \n" + ex.Message, "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
        }

        private void btnLamMoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                this.phieuXuatTableAdapter.Fill(this.dataSet.PhieuXuat);
                this.chiTietPhieuXuatTableAdapter.Fill(this.dataSet.CTPX);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể làm mới:\n" + ex.Message, "Thông báo", MessageBoxButtons.OK);
            }
        }
    }


}
