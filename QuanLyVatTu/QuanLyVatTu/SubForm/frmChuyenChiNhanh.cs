using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyVatTu.SubForm
{
    public partial class frmChuyenChiNhanh : DevExpress.XtraEditors.XtraForm
    {
        public frmChuyenChiNhanh()
        {
            InitializeComponent();
        }

        private void frmChuyenChiNhanh_Load(object sender, EventArgs e)
        {
            cboChiNhanh.DataSource = Program.bindingSource.DataSource;
            cboChiNhanh.DisplayMember = "TENCN";
            cboChiNhanh.ValueMember = "TENSERVER";
            cboChiNhanh.SelectedIndex = Program.brand;
        }

        /************************************************************
         * tạo delegate - một cái biến mà khi được gọi, nó sẽ thực hiện 1 hàm(function) khác
         * Ví dụ: ở class formNhanVien, ta có hàm chuyển chi nhánh, hàm này cần 1 tham số, chính
         * là tên server được chọn ở frmChuyenChiNhanh này. Để gọi được hàm chuyển chi nhánh ở frmNhanVien
         * Chúng ta khai báo 1 delete là branchTransfer để gọi hàm chuyển chi nhánh về form này
         *************************************************************/
        public delegate void MyDelegate(string chiNhanh);
        public MyDelegate branchTransfer;

        private void cboChiNhanh_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            if (cboChiNhanh.Text.Trim().Equals(""))
            {
                MessageBox.Show("Vui lòng chọn chi nhánh", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn chuyển nhân viên này đi?", "Thông báo",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.OK)
            {
                branchTransfer(cboChiNhanh.SelectedValue.ToString());
            }

            this.Dispose();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
