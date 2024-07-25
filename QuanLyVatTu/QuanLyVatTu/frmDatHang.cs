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
    public partial class frmDatHang : Form
    {
        int viTri = 0;

        bool dangThemMoi = false;

        public string makho = "";
        string maChiNhanh = "";

        Stack undoList = new Stack();

        BindingSource bds = null;
        GridControl gc = null;
        string type = "";

        public frmDatHang()
        {
            InitializeComponent();
        }

        private void frmDatHang_Load(object sender, EventArgs e)
        {
            this.txtMaNhanVien.Enabled = false;
            dataSet.EnforceConstraints = false;

            this.ChiTietDonDatHangTableAdapter.Connection.ConnectionString = Program.connstr;
            this.ChiTietDonDatHangTableAdapter.Fill(this.dataSet.CTDDH);

            this.datHangTableAdapter.Connection.ConnectionString = Program.connstr;
            this.datHangTableAdapter.Fill(this.dataSet.DatHang);


            this.PhieuNhapTableAdapter.Connection.ConnectionString = Program.connstr;
            this.PhieuNhapTableAdapter.Fill(this.dataSet.PhieuNhap);

            /*Step 2*/
            cboChiNhanh.DataSource = Program.bindingSource;
            cboChiNhanh.DisplayMember = "TENCN";
            cboChiNhanh.ValueMember = "TENSERVER";
            cboChiNhanh.SelectedIndex = Program.brand;
            bds = bdsDatHang;
            gc = gcDatHang;
        }

        private void btnCheDoDonDatHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnMeNuChonCheDo.Links[0].Caption = "Đơn Đặt Hàng";

            bds = bdsDatHang;
            gc = gcDatHang;

            txtMaDonDatHang.Enabled = true;
            dteNgay.Enabled = false;
            
            txtNhaCungCap.Enabled = true;
            txtMaNhanVien.Enabled = false;

            txtMaKho.Enabled = false;
            btnChonKhoHang.Enabled = true;

            txtMaVatTu.Enabled = false;
            btnChonVatTu.Enabled = false;
            txtSoLuong.Enabled = false;
            txtDonGia.Enabled = false;

            gcDatHang.Enabled = true;
            gbxDonDatHang.Enabled = true;
            gcChiTietDonDatHang.Enabled = true;
            
            if (Program.role == "CONGTY")
            {
                this.cboChiNhanh.Enabled = true;
                this.btnThem.Enabled = false;
                this.btnXoa.Enabled = false;
                this.btnGhi.Enabled = false;

                this.btnHoanTac.Enabled = false;
                this.btnLamMoi.Enabled = true;
                this.btnMeNuChonCheDo.Enabled = false;
                this.btnThoat.Enabled = true;
                this.gbxDonDatHang.Enabled = false;
            }

            if (Program.role == "CHINHANH" || Program.role == "USER")
            {
                this.cboChiNhanh.Enabled = false;

                this.btnThem.Enabled = true;
                bool turnOn = (bdsDatHang.Count > 0) ? true : false;
                this.btnXoa.Enabled = turnOn;
                this.btnGhi.Enabled = true;
                this.btnHoanTac.Enabled = false;
                this.btnLamMoi.Enabled = true;
                this.btnMeNuChonCheDo.Enabled = true;
                this.btnThoat.Enabled = true;

                this.txtMaDonDatHang.Enabled = false;
            }
        }

        private void btnCheDoChiTietDonDatHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            btnMeNuChonCheDo.Links[0].Caption = "Chi Tiết Đơn Đặt Hàng";

            bds = bdsCTDH;
            gc = gcChiTietDonDatHang;

            txtMaDonDatHang.Enabled = false;
            dteNgay.Enabled = false;

            txtNhaCungCap.Enabled = false;
            txtMaNhanVien.Enabled = false;

            txtMaKho.Enabled = false;
            btnChonKhoHang.Enabled = false;

            txtMaVatTu.Enabled = false;
            btnChonVatTu.Enabled = true;

            txtSoLuong.Enabled = true;
            txtDonGia.Enabled = true;

            gcDatHang.Enabled = true;
            gcChiTietDonDatHang.Enabled = true;

            if (Program.role == "CONGTY")
            {
                cboChiNhanh.Enabled = true;

                this.btnThem.Enabled = false;
                this.btnXoa.Enabled = false;
                this.btnGhi.Enabled = false;
                this.btnHoanTac.Enabled = false;
                this.btnLamMoi.Enabled = true;
                this.btnMeNuChonCheDo.Enabled = false;
                this.btnThoat.Enabled = true;

                this.gbxDonDatHang.Enabled = false;
            }

            if (Program.role == "CHINHANH" || Program.role == "USER")
            {
                cboChiNhanh.Enabled = false;

                this.btnThem.Enabled = true;
                bool turnOn = (bdsCTDH.Count > 0) ? true : false;
                this.btnXoa.Enabled = turnOn;
                this.btnGhi.Enabled = true;
                this.btnHoanTac.Enabled = false;
                this.btnLamMoi.Enabled = true;
                this.btnMeNuChonCheDo.Enabled = true;
                this.btnThoat.Enabled = true;

                this.txtMaDonDatHang.Enabled = false;

            }
        }
        private void btnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            viTri = bds.Position;
            dangThemMoi = true;
            bds.AddNew();

            if (btnMeNuChonCheDo.Links[0].Caption == "Đơn Đặt Hàng")
            {
                this.gbxDonDatHang.Enabled = true;

                this.txtMaDonDatHang.Enabled = true;
                this.dteNgay.EditValue = DateTime.Now;
                this.dteNgay.Enabled = false;
                this.txtNhaCungCap.Enabled = true;
                this.txtMaNhanVien.Text = Program.userName;
                this.btnChonKhoHang.Enabled = true;

                ((DataRowView)(bdsDatHang.Current))["MANV"] = Program.userName;
                ((DataRowView)(bdsDatHang.Current))["NGAY"] = DateTime.Now;
            }
            if (btnMeNuChonCheDo.Links[0].Caption == "Chi Tiết Đơn Đặt Hàng")
            {
                this.gbxDonDatHang.Enabled = true;
                DataRowView drv = ((DataRowView)bdsDatHang[bdsDatHang.Position]);
                String maNhanVien = drv["MANV"].ToString();
                if (Program.userName != maNhanVien)
                {
                    MessageBox.Show("Bạn không thêm chi tiết đơn hàng trên phiếu không phải do mình tạo", "Thông báo", MessageBoxButtons.OK);
                    bdsCTDH.RemoveCurrent();
                    return;
                }
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
        }

        private bool kiemTraDuLieuDauVao(String cheDo)
        {
            if (cheDo == "Đơn Đặt Hàng")
            {
                if (txtMaDonDatHang.Text == "")
                {
                    MessageBox.Show("Không thể bỏ trống mã đơn hàng", "Thông báo", MessageBoxButtons.OK);
                    return false;
                }
                if (txtMaDonDatHang.Text.Length > 8)
                {
                    MessageBox.Show("Mã đơn đặt hàng không quá 8 kí tự ", "Thông báo", MessageBoxButtons.OK);
                    return false;
                }
                if (Regex.IsMatch(txtMaDonDatHang.Text, @"^[A-Za-z 0-9]+$") == false)
                {
                    MessageBox.Show("Mã đơn đặt hàng chỉ có chữ cái và khoảng trắng", "Thông báo", MessageBoxButtons.OK);
                    return false;
                }
                
                if (txtMaNhanVien.Text == "")
                {
                    MessageBox.Show("Không thể bỏ trống mã nhân viên", "Thông báo", MessageBoxButtons.OK);
                    return false;
                }
                if (txtNhaCungCap.Text == "")
                {
                    MessageBox.Show("Không thể bỏ trống nhà cung cấp", "Thông báo", MessageBoxButtons.OK);
                    return false;
                }
                if (Regex.IsMatch(txtNhaCungCap.Text, @"^[A-Za-z ]+$") == false)
                {
                    MessageBox.Show("Mã đơn đặt hàng chỉ có chữ cái và khoảng trắng ", "Thông báo", MessageBoxButtons.OK);
                    return false;
                }
                if (txtNhaCungCap.Text.Length > 100)
                {
                    MessageBox.Show("Tên nhà cung cấp không quá 100 kí tự", "Thông báo", MessageBoxButtons.OK);
                    return false;
                }
                if (txtMaKho.Text == "")
                {
                    MessageBox.Show("Không thể bỏ trống mã kho", "Thông báo", MessageBoxButtons.OK);
                    return false;
                }
            }

            if (cheDo == "Chi Tiết Đơn Đặt Hàng")
            {
                if (txtMaVatTu.Text == "")
                {
                    MessageBox.Show("Không thể bỏ trống mã vật tư", "Thông báo", MessageBoxButtons.OK);
                    return false;
                }
                if (txtSoLuong.Value < 0 || txtDonGia.Value < 0)
                {
                    MessageBox.Show("Không thể nhỏ hơn 1", "Thông báo", MessageBoxButtons.OK);
                    return false;
                }
            }
            return true;
        }

        private String taoCauTruyVanHoanTac(String cheDo)
        {
            String cauTruyVan = "";
            DataRowView drv;

            if (cheDo == "Đơn Đặt Hàng" && dangThemMoi == false)
            {
                drv = ((DataRowView)bdsDatHang[bdsDatHang.Position]);
                DateTime ngay = ((DateTime)drv["NGAY"]);

                cauTruyVan = "UPDATE DBO.DATHANG " +
                    "SET " +
                    "NGAY = CAST('" + ngay.ToString("yyyy-MM-dd") + "' AS DATETIME), " +
                    "NhaCC = '" + drv["NhaCC"].ToString().Trim() + "', " +
                    "MANV = '" + drv["MANV"].ToString().Trim() + "', " +
                    "MAKHO = '" + drv["MAKHO"].ToString().Trim() + "' " +
                    "WHERE MasoDDH = '" + drv["MasoDDH"].ToString().Trim() + "'";
            }

            if (cheDo == "Đơn Đặt Hàng" && dangThemMoi == true)
            {
                drv = ((DataRowView)bdsDatHang[bdsDatHang.Position]);
                DateTime ngay = ((DateTime)drv["NGAY"]);
                cauTruyVan = "INSERT INTO DBO.DATHANG(MasoDDH, NGAY, NhaCC, MaNV, MaKho) " +
                    "VALUES('" + drv["MasoDDH"] + "', '" +
                    ngay.ToString("yyyy-MM-dd") + "', '" +
                    drv["NhaCC"].ToString() + "', '" +
                    drv["MaNV"].ToString() + "', '" +
                    drv["MaKho"].ToString() + "' )";

            }

            if (cheDo == "Chi Tiết Đơn Đặt Hàng" && dangThemMoi == false)
            {
                drv = ((DataRowView)bdsCTDH[bdsCTDH.Position]);

                cauTruyVan = "UPDATE DBO.CTDDH " +
                    "SET " +
                    "SOLUONG = " + drv["SOLUONG"].ToString() + " , " +
                    "DONGIA = " + drv["DONGIA"].ToString() + " " +
                    "WHERE MasoDDH = '" + drv["MasoDDH"].ToString().Trim() + "'" +
                    " AND MAVT = '" + drv["MAVT"].ToString().Trim() + "'";

            }

            if (cheDo == "Chi Tiết Đơn Đặt Hàng" && dangThemMoi == true)
            {
                drv = ((DataRowView)bdsCTDH[bdsCTDH.Position]);
                cauTruyVan = "INSERT INTO DBO.CTDDH(MasoDDH, SOLUONG, DONGIA, MAVT) " +
                    "VALUES('" + drv["MasoDDH"].ToString().Trim() + "', '" +
                    drv["SOLUONG"].ToString() + "', '" +
                    drv["DONGIA"].ToString() + "', '" +
                    drv["MAVT"].ToString().Trim() + "')";
            }
            return cauTruyVan;
        }

        private void btnGhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            viTri = bdsDatHang.Position;
            DataRowView drv = ((DataRowView)bdsDatHang[bdsDatHang.Position]);

            String maNhanVien = drv["MANV"].ToString();
            String maDonDatHang = drv["MasoDDH"].ToString().Trim();
            if (Program.userName != maNhanVien && dangThemMoi == false)
            {
                MessageBox.Show("Bạn không thể sửa phiếu do người khác lập", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            String cheDo = (btnMeNuChonCheDo.Links[0].Caption == "Đơn Đặt Hàng") ? "Đơn Đặt Hàng" : "Chi Tiết Đơn Đặt Hàng";
            bool ketQua = kiemTraDuLieuDauVao(cheDo);
            if (ketQua == false) return;
            String cauTruyVanHoanTac = taoCauTruyVanHoanTac(cheDo);

            String maDonDatHangMoi = txtMaDonDatHang.Text;
            String cauTruyVan =
                    "DECLARE	@result int " +
                    "EXEC @result = sp_KiemTraMaDonDatHang '" +
                    maDonDatHangMoi + "' " +
                    "SELECT 'Value' = @result";
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

            int viTriHienTai = bds.Position;
            int viTriMaDonDatHang = bdsDatHang.Find("MasoDDH", txtMaDonDatHang.Text);

            if (result == 1 && cheDo == "Đơn Đặt Hàng" && viTriHienTai != viTriMaDonDatHang)
            {
                MessageBox.Show("Mã đơn hàng này đã được sử dụng !\n\n", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        if (cheDo == "Đơn Đặt Hàng" && dangThemMoi == true)
                        {
                            cauTruyVanHoanTac =
                                "DELETE FROM DBO.DATHANG " +
                                "WHERE MasoDDH = '" + maDonDatHang + "'";
                        }

                        if (cheDo == "Chi Tiết Đơn Đặt Hàng" && dangThemMoi == true)
                        {
                            ((DataRowView)(bdsCTDH.Current))["MasoDDH"] = ((DataRowView)(bdsDatHang.Current))["MasoDDH"];
                            ((DataRowView)(bdsCTDH.Current))["MAVT"] = Program.maVatTuDuocChon;
                            ((DataRowView)(bdsCTDH.Current))["SOLUONG"] =
                                txtSoLuong.Value;
                            ((DataRowView)(bdsCTDH.Current))["DONGIA"] =
                                (int)txtDonGia.Value;

                            cauTruyVanHoanTac =
                                "DELETE FROM DBO.CTDDH " +
                                "WHERE MasoDDH = '" + maDonDatHang + "' " +
                                "AND MAVT = '" + txtMaVatTu.Text.Trim() + "'";
                        }

                        undoList.Push(cauTruyVanHoanTac);

                        this.bdsDatHang.EndEdit();
                        this.bdsCTDH.EndEdit();
                        this.datHangTableAdapter.Update(this.dataSet.DatHang);
                        this.ChiTietDonDatHangTableAdapter.Update(this.dataSet.CTDDH);

                        this.btnThem.Enabled = true;
                        this.btnXoa.Enabled = true;
                        this.btnGhi.Enabled = true;
                        this.btnHoanTac.Enabled = true;
                        this.btnLamMoi.Enabled = true;
                        this.btnMeNuChonCheDo.Enabled = true;
                        this.btnThoat.Enabled = true;

                        dangThemMoi = false;
                        MessageBox.Show("Ghi thành công", "Thông báo", MessageBoxButtons.OK);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        bds.RemoveCurrent();
                        MessageBox.Show("Đã xảy ra lỗi khi ghi dữ liệu:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

            }
        }
        private void btnLamMoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                this.datHangTableAdapter.Fill(this.dataSet.DatHang);
                this.ChiTietDonDatHangTableAdapter.Fill(this.dataSet.CTDDH);

                this.gcDatHang.Enabled = true;
                this.gcChiTietDonDatHang.Enabled = true;
                bdsDatHang.Position = viTri;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Làm mới" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
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
                MessageBox.Show("Xảy ra lỗi kết nối với chi nhánh hiện tại", "Thông báo", MessageBoxButtons.OK);
            }
            else
            {
                this.ChiTietDonDatHangTableAdapter.Connection.ConnectionString = Program.connstr;
                this.ChiTietDonDatHangTableAdapter.Fill(this.dataSet.CTDDH);

                this.datHangTableAdapter.Connection.ConnectionString = Program.connstr;
                this.datHangTableAdapter.Fill(this.dataSet.DatHang);

                this.PhieuNhapTableAdapter.Connection.ConnectionString = Program.connstr;
                this.PhieuNhapTableAdapter.Fill(this.dataSet.PhieuNhap);
            }
        }

        private void btnHoanTac_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (dangThemMoi == true && this.btnThem.Enabled == false)
            {
                dangThemMoi = false;

                if (btnMeNuChonCheDo.Links[0].Caption == "Đơn Đặt Hàng")
                {
                    this.txtMaDonDatHang.Enabled = false;
                    this.dteNgay.Enabled = false;
                    this.txtNhaCungCap.Enabled = true;
                    this.btnChonKhoHang.Enabled = true;
                }
                if (btnMeNuChonCheDo.Links[0].Caption == "Chi Tiết Đơn Đặt Hàng")
                {
                    this.txtMaVatTu.Enabled = false;
                    this.btnChonVatTu.Enabled = true;

                    this.txtSoLuong.Enabled = true;
                    this.txtSoLuong.EditValue = 1;

                    this.txtDonGia.Enabled = true;
                    this.txtDonGia.EditValue = 1;
                }

                this.btnThem.Enabled = true;
                this.btnXoa.Enabled = true;
                this.btnGhi.Enabled = true;
                this.btnLamMoi.Enabled = true;
                this.btnMeNuChonCheDo.Enabled = true;
                this.btnThoat.Enabled = true;

                bds.CancelEdit();
                bds.Position = viTri;
                return;
            }

            if (undoList.Count == 0)
            {
                MessageBox.Show("Không còn thao tác nào để hoàn tác", "Thông báo", MessageBoxButtons.OK);
                btnHoanTac.Enabled = false;
                return;
            }

            bds.CancelEdit();
            String cauTruyVanHoanTac = undoList.Pop().ToString();

            Console.WriteLine(cauTruyVanHoanTac);

            this.datHangTableAdapter.Fill(this.dataSet.DatHang);
            this.ChiTietDonDatHangTableAdapter.Fill(this.dataSet.CTDDH);

            bdsDatHang.Position = viTri;
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string cauTruyVan = "";
            string cheDo = (btnMeNuChonCheDo.Links[0].Caption == "Đơn Đặt Hàng") ? "Đơn Đặt Hàng" : "Chi Tiết Đơn Đặt Hàng";

            dangThemMoi = true;

            if (cheDo == "Đơn Đặt Hàng")
            {
                if (bdsCTDH.Count > 0)
                {
                    MessageBox.Show("Không thể xóa đơn đặt hàng này vì có chi tiết đơn đặt hàng", "Thông báo", MessageBoxButtons.OK);
                    return;
                }
                if (bdsPhieuNhap.Count > 0)
                {
                    MessageBox.Show("Không thể xóa đơn đặt hàng này vì có phiếu nhập", "Thông báo", MessageBoxButtons.OK);
                    return;
                }
            }
            if (cheDo == "Chi Tiết Đơn Đặt Hàng")
            {
                DataRowView drv = ((DataRowView)bdsDatHang[bdsDatHang.Position]);
                String maNhanVien = drv["MANV"].ToString();
                if (Program.userName != maNhanVien)
                {
                    MessageBox.Show("Bạn không xóa chi tiết đơn hàng trên phiếu không phải do mình tạo", "Thông báo", MessageBoxButtons.OK);
                    return;
                }
            }

            cauTruyVan = taoCauTruyVanHoanTac(cheDo);
            undoList.Push(cauTruyVan);

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không ?", "Thông báo",
                MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    viTri = bds.Position;
                    if (cheDo == "Đơn Đặt Hàng")
                    {
                        bdsDatHang.RemoveCurrent();
                    }
                    if (cheDo == "Chi Tiết Đơn Đặt Hàng")
                    {
                        bdsCTDH.RemoveCurrent();
                    }


                    this.datHangTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.datHangTableAdapter.Update(this.dataSet.DatHang);

                    this.ChiTietDonDatHangTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.ChiTietDonDatHangTableAdapter.Update(this.dataSet.CTDDH);

                    dangThemMoi = false;
                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK);
                    this.btnHoanTac.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể xóa Đơn đặt hàng:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    this.datHangTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.datHangTableAdapter.Update(this.dataSet.DatHang);

                    this.ChiTietDonDatHangTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.ChiTietDonDatHangTableAdapter.Update(this.dataSet.CTDDH);

                    bds.Position = viTri;
                    return;
                }
            }
            else
            {
                undoList.Pop();
            }
        }
    }
}
