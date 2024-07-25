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
    public partial class gclChiTietPhieuNhap : Form
    {
        int viTri = 0;

        bool dangThemMoi = false;
        public string makho = "";
        string maChiNhanh = "";
 
        Stack undoList = new Stack();

        BindingSource bds = null;
        GridControl gc = null;
        string type = "";       // Phiếu xuất hay chi tiết phiếu xuất

        public gclChiTietPhieuNhap()
        {
            InitializeComponent();
        }

        private void frmPhieuNhap_Load(object sender, EventArgs e)
        {
            dataSet.EnforceConstraints = false;

            this.ChiTietPhieuNhapTableAdapter.Connection.ConnectionString = Program.connstr;
            this.ChiTietPhieuNhapTableAdapter.Fill(this.dataSet.CTPN);

            this.phieuNhapTableAdapter.Connection.ConnectionString = Program.connstr;
            this.phieuNhapTableAdapter.Fill(this.dataSet.PhieuNhap);

            cboChiNhanh.DataSource = Program.bindingSource;
            cboChiNhanh.DisplayMember = "TENCN";
            cboChiNhanh.ValueMember = "TENSERVER";
            cboChiNhanh.SelectedIndex = Program.brand;
        }

        private void btnCheDoPhieuNhap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // Xử lý Phiếu nhập
            btnMeNuChonCheDo.Links[0].Caption = "Phiếu Nhập";
            bds = bdsPhieuNhap;
            gc = gclPhieuNhap;

            txtMaPhieuNhap.Enabled = false;
            dteNgay.Enabled = false;

            txtMaDonDatHang.Enabled = false;
            btnChonDonHang.Enabled = false;

            txtMaNhanVien.Enabled = false;
            txtMaKho.Enabled = false;

            btnChonChiTietDonHang.Enabled = false;

            gclPhieuNhap.Enabled = true;
            gcChiTietPhieuNhap.Enabled = true;

            // Tắt chức năng của gcl ChiTietPhieuNhap
            txtMaVatTu.Enabled = false;
            txtSoLuong.Enabled = false;
            txtDonGia.Enabled = false;

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

                this.gbxPhieuNhap.Enabled = false;
            }
            if (Program.role == "CHINHANH" || Program.role == "USER")
            {
                cboChiNhanh.Enabled = false;

                this.btnThem .Enabled = true;
                bool turnOn = (bdsPhieuNhap.Count > 0) ? true : false;  // kiểm tra xem bds đang có data không
                this.btnXoa.Enabled = turnOn;
                this.btnGhi.Enabled = true;
                this.btnHoanTac.Enabled = false;
                this.btnLamMoi.Enabled = true;
                this.btnMeNuChonCheDo.Enabled = true;
                this.btnThoat.Enabled = true;
            }

        }

        private void btnCheDoChiTietPhieuNhap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnMeNuChonCheDo.Links[0].Caption = "Chi Tiết Phiếu Nhập";

            bds = bdsChiTietPhieuNhap;
            gc = gclPhieuNhap;

            gclPhieuNhap.Enabled = true;
            gcChiTietPhieuNhap.Enabled = true;

            txtMaPhieuNhap.Enabled = false;
            dteNgay.Enabled = false;

            txtMaDonDatHang.Enabled = false;
            btnChonDonHang.Enabled = false;

            txtMaNhanVien.Enabled = false;
            txtMaKho.Enabled = false;

            txtMaVatTu.Enabled = false;
            txtSoLuong.Enabled = false;
            txtDonGia.Enabled = false;

            btnChonChiTietDonHang.Enabled = false;

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
            }

            if (Program.role == "CHINHANH" || Program.role == "USER")
            {
                cboChiNhanh.Enabled = false;

                this.btnThem.Enabled = true;
                this.btnXoa.Enabled = false;
                this.btnGhi.Enabled = true;

                this.btnHoanTac.Enabled = false;
                this.btnLamMoi.Enabled = true;
                this.btnMeNuChonCheDo.Enabled = true;
                this.btnThoat.Enabled = true;
            }
        }

        private void btnChonDonHang_Click(object sender, EventArgs e)
        {
            frmChonDonDatHang form = new frmChonDonDatHang();
            form.ShowDialog();

            this.txtMaDonDatHang.Text = Program.maDonDatHangDuocChon;
            this.txtMaKho.Text = Program.maKhoDuocChon;
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            viTri = bds.Position;
            dangThemMoi = true;

            bds.AddNew();       // Nhảy xuống cuối table và thêm 1 dòng mới
            if (btnMeNuChonCheDo.Links[0].Caption == "Phiếu Nhập")
            {   
                this.gbxPhieuNhap.Enabled = true;
                this.txtMaPhieuNhap.Enabled = true;

                this.dteNgay.EditValue = DateTime.Now;
                this.dteNgay.Enabled = false;

                this.txtMaDonDatHang.Enabled = false;
                this.btnChonDonHang.Enabled = true;

                this.txtMaNhanVien.Text = Program.userName;

                this.txtMaKho.Text = Program.maKhoDuocChon;

                ((DataRowView)(bdsPhieuNhap.Current))["NGAY"] = DateTime.Now;
                ((DataRowView)(bdsPhieuNhap.Current))["MasoDDH"] = Program.maDonDatHangDuocChon;
                ((DataRowView)(bdsPhieuNhap.Current))["MANV"] = Program.userName;
                ((DataRowView)(bdsPhieuNhap.Current))["MAKHO"] = Program.maKhoDuocChon;
            }

            if (btnMeNuChonCheDo.Links[0].Caption == "Chi Tiết Phiếu Nhập")
            {
                this.gbxPhieuNhap.Enabled = true;
                DataRowView drv = ((DataRowView)bdsPhieuNhap[bdsPhieuNhap.Position]);
                String maNhanVien = drv["MANV"].ToString();
                if (Program.userName != maNhanVien)
                {
                    MessageBox.Show("Bạn không thêm chi tiết phiếu nhập trên phiếu không phải do mình tạo", "Thông báo", MessageBoxButtons.OK);
                    bdsChiTietPhieuNhap.RemoveCurrent();
                    return;
                }

                ((DataRowView)(bdsChiTietPhieuNhap.Current))["MAPN"] = ((DataRowView)(bdsPhieuNhap.Current))["MAPN"];
                ((DataRowView)(bdsChiTietPhieuNhap.Current))["MAVT"] = Program.maVatTuDuocChon;
                ((DataRowView)(bdsChiTietPhieuNhap.Current))["SOLUONG"] = Program.soLuongVatTu;
                ((DataRowView)(bdsChiTietPhieuNhap.Current))["DONGIA"] = Program.donGia;

                this.txtMaVatTu.Enabled = false;
                this.btnChonChiTietDonHang.Enabled = true;

                this.txtSoLuong.Enabled = true;
                this.txtSoLuong.EditValue = 1;

                this.txtDonGia.Enabled = true;
                this.txtDonGia.EditValue = 100;

                this.txtSoLuong.Enabled = true;
                this.txtDonGia.Enabled = true;
            }

            this.btnThem.Enabled = false;
            this.btnXoa.Enabled = false;
            this.btnGhi.Enabled = true;
            this.btnHoanTac.Enabled = true;
            this.btnLamMoi.Enabled = false;
            this.btnMeNuChonCheDo.Enabled = false;
            this.btnThoat.Enabled = false;

            gclPhieuNhap.Enabled = false;
            gcChiTietPhieuNhap.Enabled = false;
        }

        private void btnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
        }

        private void btnLamMoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                this.phieuNhapTableAdapter.Fill(this.dataSet.PhieuNhap);
                this.ChiTietPhieuNhapTableAdapter.Fill(this.dataSet.CTPN);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi làm mới dữ liệu\n\n" + ex.Message, "Thông Báo", MessageBoxButtons.OK);
                Console.WriteLine(ex.Message);
                return;
            }
        }

        private void btnChonChiTietDonHang_Click(object sender, EventArgs e)
        {
            // Lấy mã số DDH hiện tại của gclPhieuNhap
            Program.maDonDatHangDuocChon = ((DataRowView)(bdsPhieuNhap.Current))["MasoDDH"].ToString().Trim();

            frmChonChiTietDonDatHang form = new frmChonChiTietDonDatHang();
            form.ShowDialog();

            this.txtMaVatTu.Text = Program.maVatTuDuocChon;
            this.txtSoLuong.Value = Program.soLuongVatTu;
            this.txtDonGia.Value = Program.donGia;
        }

        private void btnHoanTac_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (dangThemMoi == true && this.btnThem.Enabled == false)
            {
                dangThemMoi = false;

                if (btnMeNuChonCheDo.Links[0].Caption == "Phiếu Nhập")
                {
                    this.txtMaDonDatHang.Enabled = false;
                    dteNgay.Enabled = false;

                    txtMaDonDatHang.Enabled = false;
                    txtMaKho.Enabled = false;

                    btnChonDonHang.Enabled = false;
                    txtMaDonDatHang.Enabled = false;
                }
                if (btnMeNuChonCheDo.Links[0].Caption == "Chi Tiết Phiếu Nhập")
                {
                    this.txtMaDonDatHang.Enabled = false;
                    this.btnChonChiTietDonHang.Enabled = false;

                    this.txtMaVatTu.Enabled = false;
                    this.txtSoLuong.Enabled = false;
                    this.txtDonGia.Enabled = false;

                    this.btnXoa.Enabled = false;
                }

                this.btnThem.Enabled = true;
                this.btnXoa.Enabled = true;
                this.btnGhi.Enabled = true;
                this.btnLamMoi.Enabled = true;
                this.btnMeNuChonCheDo.Enabled = true;
                this.btnThoat.Enabled = true;

                this.gclPhieuNhap.Enabled = true;
                this.gcChiTietPhieuNhap.Enabled = true;

                bds.CancelEdit();
                bds.Position = viTri;
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

            this.phieuNhapTableAdapter.Fill(this.dataSet.PhieuNhap);
            this.ChiTietPhieuNhapTableAdapter.Fill(this.dataSet.CTPN);

            bdsPhieuNhap.Position = viTri;
        }
        private void capNhatSoLuongVatTu(string maVatTu, int soLuong)
        {
            string cauTruyVan = "EXEC sp_CapNhatSoLuongVatTu 'IMPORT','" + maVatTu + "', " + soLuong;

            Console.WriteLine(cauTruyVan);
        }
        private String taoCauTruyVanHoanTac(String cheDo)
        {
            String cauTruyVan = "";
            DataRowView drv;

            if (cheDo == "Chi Tiết Phiếu Nhập" && dangThemMoi == false)
            {
                drv = ((DataRowView)(bdsChiTietPhieuNhap.Current));
                int soLuong = int.Parse(drv["SOLUONG"].ToString().Trim());
                float donGia = float.Parse(drv["DONGIA"].ToString().Trim());
                String maPhieuNhap = drv["MAPN"].ToString().Trim();
                String maVatTu = drv["MAVT"].ToString().Trim();

                cauTruyVan = "UPDATE DBO.CTPN " +
                    "SET " +
                    "SOLUONG = " + soLuong + ", " +
                    "DONGIA = " + donGia + " " +
                    "WHERE MAPN = '" + maPhieuNhap + "' " +
                    "AND MAVT = '" + maVatTu + "' ";
            }
            return cauTruyVan;
        }

        private bool kiemTraDuLieuDauVao(String cheDo)
        {
            if (cheDo == "Phiếu Nhập")
            {
                if (txtMaPhieuNhap.Text == "")
                {
                    MessageBox.Show("Không bỏ trống mã phiếu nhập !", "Thông báo", MessageBoxButtons.OK);
                    txtMaPhieuNhap.Focus();
                    return false;
                }
                if (txtMaPhieuNhap.Text == "")
                {
                    MessageBox.Show("Không bỏ trống mã phiếu nhập !", "Thông báo", MessageBoxButtons.OK);
                    txtMaPhieuNhap.Focus();
                    return false;
                }

                if (Regex.IsMatch(txtMaPhieuNhap.Text, @"^[A-Za-z 0-9 ]+$") == false)
                {
                    MessageBox.Show("mã phiếu nhập chỉ có chữ cái và khoảng trắng !", "Thông báo", MessageBoxButtons.OK);
                    return false;
                }

                if (txtMaKho.Text == "")
                {
                    MessageBox.Show("Không bỏ trống mã kho !", "Thông báo", MessageBoxButtons.OK);
                    return false;
                }

                if (Regex.IsMatch(txtMaKho.Text, @"^[A-Za-z 0-9 ]+$") == false)
                {
                    MessageBox.Show("mã kho chỉ có chữ cái và khoảng trắng !", "Thông báo", MessageBoxButtons.OK);
                    return false;
                }
                if (txtMaDonDatHang.Text == "")
                {
                    MessageBox.Show("Không bỏ trống mã đơn đặt hàng !", "Thông báo", MessageBoxButtons.OK);
                    return false;
                }
            }

            if (cheDo == "Chi Tiết Phiếu Nhập")
            {
                if (txtMaVatTu.Text == "")
                {
                    MessageBox.Show("Không bỏ trống mã vật tư !", "Thông báo", MessageBoxButtons.OK);
                    return false;
                }

                if (txtSoLuong.Value < 0 ||
                    txtSoLuong.Value > Program.soLuongVatTu)
                {
                    MessageBox.Show("Số lượng vật tư không thể lớn hơn số lượng vật tư trong chi tiết đơn hàng !", "Thông báo", MessageBoxButtons.OK);
                    txtSoLuong.Focus();
                    return false;
                }

                if (txtDonGia.Value < 1)
                {
                    MessageBox.Show("Đơn giá không thể nhỏ hơn 1 VND", "Thông báo", MessageBoxButtons.OK);
                    txtDonGia.Focus();
                    return false;
                }
            }

            return true;
        }

        private void btnGhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            String cheDo = (btnMeNuChonCheDo.Links[0].Caption == "Phiếu Nhập") ? "Phiếu Nhập" : "Chi Tiết Phiếu Nhập";

            bool ketQua = kiemTraDuLieuDauVao(cheDo);
            if (ketQua == false) return;

            string cauTruyVanHoanTac = taoCauTruyVanHoanTac(cheDo);

            String maPhieuNhap = txtMaPhieuNhap.Text.Trim();
            String cauTruyVan =
                    "DECLARE	@result int " +
                    "EXEC @result = sp_KiemTraMaPhieuNhap '" +
                    maPhieuNhap + "' " +
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
                MessageBox.Show("Không thể thực hiện hành động này:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(ex.Message);
                return;
            }
            Program.myReader.Read();
            int result = int.Parse(Program.myReader.GetValue(0).ToString());
            Program.myReader.Close();

            int viTriConTro = bdsPhieuNhap.Position;
            int viTriMaPhieuNhap = bdsPhieuNhap.Find("MAPN", maPhieuNhap);

            if (result == 1 && cheDo == "Phiếu Nhập" && viTriMaPhieuNhap != viTriConTro)
            {
                MessageBox.Show("Mã phiếu nhập đã được sử dụng!", "Thông báo", MessageBoxButtons.OK);
                txtMaPhieuNhap.Focus();
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
                        if (cheDo == "Phiếu Nhập" && dangThemMoi == true)
                        {
                            cauTruyVanHoanTac =
                                "DELETE FROM DBO.PHIEUNHAP " +
                                "WHERE MAPN = '" + maPhieuNhap + "'";
                        }

                        if (cheDo == "Chi Tiết Phiếu Nhập" && dangThemMoi == true)
                        {
                            cauTruyVanHoanTac =
                                "DELETE FROM DBO.CTPN " +
                                "WHERE MAPN = '" + maPhieuNhap + "' " +
                                "AND MAVT = '" + Program.maVatTuDuocChon + "'";

                            string maVatTu = txtMaVatTu.Text.Trim();
                            int soLuong = (int)txtSoLuong.Value;

                            capNhatSoLuongVatTu(maVatTu, soLuong);
                        }

                        undoList.Push(cauTruyVanHoanTac);
                        Console.WriteLine("cau truy van hoan tac");
                        Console.WriteLine(cauTruyVanHoanTac);

                        this.bdsPhieuNhap.EndEdit();
                        this.bdsChiTietPhieuNhap.EndEdit();
                        this.phieuNhapTableAdapter.Update(this.dataSet.PhieuNhap);
                        this.ChiTietPhieuNhapTableAdapter.Update(this.dataSet.CTPN);

                        this.btnThem.Enabled = true;
                        this.btnXoa.Enabled = true;
                        this.btnGhi.Enabled = true;
                        this.btnHoanTac.Enabled = true;
                        this.btnLamMoi.Enabled = true;
                        this.btnMeNuChonCheDo.Enabled = true;
                        this.btnThoat.Enabled = true;

                        this.gclPhieuNhap.Enabled = true;
                        this.gcChiTietPhieuNhap.Enabled = true;

                        this.txtSoLuong.Enabled = false;
                        this.txtDonGia.Enabled = false;

                        dangThemMoi = false;
                        MessageBox.Show("Ghi thành công", "Thông báo", MessageBoxButtons.OK);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        bds.RemoveCurrent();
                        MessageBox.Show("Đã xảy ra lỗi không mong muốn:\n" + ex.Message, "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataRowView drv;
            string cauTruyVanHoanTac = "";
            string cheDo = (btnMeNuChonCheDo.Links[0].Caption == "Phiếu Nhập") ? "Phiếu Nhập" : "Chi Tiết Phiếu Nhập";

            if (cheDo == "Phiếu Nhập")
            {
                drv = ((DataRowView)bdsPhieuNhap[bdsPhieuNhap.Position]);
                String maNhanVien = drv["MANV"].ToString();
                if (Program.userName != maNhanVien)
                {
                    MessageBox.Show("Không xóa chi tiết phiếu xuất không phải do mình tạo", "Thông báo", MessageBoxButtons.OK);
                    return;
                }

                if (bdsChiTietPhieuNhap.Count > 0)
                {
                    MessageBox.Show("Không thể xóa phiếu nhập vì có chi tiết phiếu nhập", "Thông báo", MessageBoxButtons.OK);
                    return;
                }

                drv = ((DataRowView)bdsPhieuNhap[bdsPhieuNhap.Position]);
                DateTime ngay = ((DateTime)drv["NGAY"]);

                cauTruyVanHoanTac = "INSERT INTO DBO.PHIEUNHAP(MAPN, NGAY, MasoDDH, MANV, MAKHO) " +
                    "VALUES( '" + drv["MAPN"].ToString().Trim() + "', '" +
                    ngay.ToString("yyyy-MM-dd") + "', '" +
                    drv["MasoDDH"].ToString() + "', '" +
                    drv["MANV"].ToString() + "', '" +
                    drv["MAKHO"].ToString() + "')";
            }

            if (cheDo == "Chi Tiết Phiếu Nhập")
            {
                drv = ((DataRowView)bdsPhieuNhap[bdsPhieuNhap.Position]);
                String maNhanVien = drv["MANV"].ToString();
                if (Program.userName != maNhanVien)
                {
                    MessageBox.Show("Bạn không xóa chi tiết phiếu nhập không phải do mình tạo", "Thông báo", MessageBoxButtons.OK);
                    return;
                }

                drv = ((DataRowView)bdsChiTietPhieuNhap[bdsChiTietPhieuNhap.Position]);
                cauTruyVanHoanTac = "INSERT INTO DBO.CTPN(MAPN, MAVT, SOLUONG, DONGIA) " +
                    "VALUES('" + drv["MAPN"].ToString().Trim() + "', '" +
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
                    if (cheDo == "Phiếu Nhập")
                    {
                        bdsPhieuNhap.RemoveCurrent();
                    }
                    if (cheDo == "Chi Tiết Phiếu Nhập")
                    {
                        bdsChiTietPhieuNhap.RemoveCurrent();
                    }


                    this.phieuNhapTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.phieuNhapTableAdapter.Update(this.dataSet.PhieuNhap);

                    this.ChiTietPhieuNhapTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.ChiTietPhieuNhapTableAdapter.Update(this.dataSet.CTPN);

                    dangThemMoi = false;
                    MessageBox.Show("Xóa thành công ", "Thông báo", MessageBoxButtons.OK);
                    this.btnHoanTac.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa Phiếu nhập:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.phieuNhapTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.phieuNhapTableAdapter.Update(this.dataSet.PhieuNhap);

                    this.ChiTietPhieuNhapTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.ChiTietPhieuNhapTableAdapter.Update(this.dataSet.CTPN);
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
                MessageBox.Show("Không thể kết nối tới chi nhánh hiện tại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                this.ChiTietPhieuNhapTableAdapter.Connection.ConnectionString = Program.connstr;
                this.ChiTietPhieuNhapTableAdapter.Fill(this.dataSet.CTPN);

                this.phieuNhapTableAdapter.Connection.ConnectionString = Program.connstr;
                this.phieuNhapTableAdapter.Fill(this.dataSet.PhieuNhap);
            }
        }

        private void gcChiTietPhieuNhap_Click(object sender, EventArgs e)
        {

        }
    }
}
