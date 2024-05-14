using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyVatTu.ReportForm
{
    public partial class frmDanhSachVatTu : Form
    {
        public frmDanhSachVatTu()
        {
            InitializeComponent();
        }

        private void vattuBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.vattuBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dataSet);

        }

        private void frmDanhSachVatTu_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet.Vattu' table. You can move, or remove it, as needed.
            this.vattuTableAdapter.Fill(this.dataSet.Vattu);

        }

        private void btnXemTruoc_Click(object sender, EventArgs e)
        {
            rptDanhSachVatTu report = new rptDanhSachVatTu();
            ReportPrintTool printTool = new ReportPrintTool(report);
            printTool.ShowPreviewDialog();
        }

        private void btnXuatBan_Click(object sender, EventArgs e)
        {
            rptDanhSachVatTu report = new rptDanhSachVatTu();
        }
    }
}
