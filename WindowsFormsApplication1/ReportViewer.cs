using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication1.Interfaces;
using WindowsFormsApplication1.Services;

namespace WindowsFormsApplication1
{
    public partial class ReportViewer : Form
    {
        private readonly IShopping _shopping;
        ReportDocument rprt = new ReportDocument();  
        string reportLoction=@"C:\Users\Ahsan\documents\visual studio 2012\Projects\WindowsFormsApplication1\WindowsFormsApplication1\CrystalReport1.rpt";
        public ReportViewer()
        {
            InitializeComponent();

            _shopping = new ShoppingService();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

            loadReport();
           

        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadReport();
        }
        private void loadReport()
        {
            DateTime from;
            DateTime to;
            DateTime.TryParse(txt_fron.Text, out from);
            DateTime.TryParse(txt_to.Text, out to);
            rprt.Load(reportLoction);
            var sda = _shopping.LoadAllSale(from.ToString(), to.ToString());
            DataSet ds = new DataSet();
            sda.Fill(ds, "DataTable1");
            rprt.SetDataSource(ds);
            crystalReportViewer1.ReportSource = rprt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Main m = new Main();
            m.Show();
        }
    }
}
