using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace QuanLyVatTu.ReportForm
{
    public partial class rptDonHangKhongPhieuNhap : DevExpress.XtraReports.UI.XtraReport
    {
        public rptDonHangKhongPhieuNhap()
        {
            InitializeComponent();
            this.sqlDataSource1.Connection.ConnectionString = Program.connstr;
            this.sqlDataSource1.Fill();
        }

    }
}
