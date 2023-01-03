using iTextSharp.text.pdf;
using Microsoft.Reporting.WebForms;
using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers
{
    public class ReportController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private ApplicationDbContext _context;

        public ReportController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ExportToPDF()
        {
            //Report  
            ReportViewer reportViewer = new ReportViewer();

            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.LocalReport.ReportPath = Server.MapPath(@"~\Report1.rdlc");

            //Byte  
            Warning[] warnings;
            string[] streamids;
            string mimeType, encoding, filenameExtension;

            byte[] bytes = reportViewer.LocalReport.Render("Pdf", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);

            //File  
            string FileName = "Test_" + DateTime.Now.Ticks.ToString() + ".pdf";
            string FilePath = HttpContext.Server.MapPath(@"~\TempFiles\") + FileName;

            //create and set PdfReader  
            PdfReader reader = new PdfReader(bytes);
            FileStream output = new FileStream(FilePath, FileMode.Create);

            string Agent = HttpContext.Request.Headers["User-Agent"].ToString();

            //create and set PdfStamper  
            PdfStamper pdfStamper = new PdfStamper(reader, output, '0', true);

            if (Agent.Contains("Firefox"))
                pdfStamper.JavaScript = "var res = app.loaded('var pp = this.getPrintParams();pp.interactive = pp.constants.interactionLevel.full;this.print(pp);');";
            else
                pdfStamper.JavaScript = "var res = app.setTimeOut('var pp = this.getPrintParams();pp.interactive = pp.constants.interactionLevel.full;this.print(pp);', 200);";

            pdfStamper.FormFlattening = false;
            pdfStamper.Close();
            reader.Close();

            //return file path  
            string FilePathReturn = @"TempFiles/" + FileName;
            return Content(FilePathReturn);
        }


        [Route("invoice-report/{invoiceid}")]        [System.Web.Mvc.HttpGet]        public ActionResult GetInvoiceReport(string invoiceid)        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;            SqlConnection con = new SqlConnection(connectionString);            SqlDataAdapter adp = new SqlDataAdapter("Select * From InvoiceV where id=" + invoiceid, con);            adp.Fill(ds);            ReportViewer reportViewer = new ReportViewer();            reportViewer.ProcessingMode = ProcessingMode.Local;            reportViewer.SizeToReportContent = true;            reportViewer.Width = Unit.Percentage(100);            reportViewer.Height = Unit.Percentage(100);
            //ReportParameter qrCODE = new ReportParameter("qrCODE", base64String);
            //reportViewer.LocalReport.EnableExternalImages = true;
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\INVOICE_REPORT.rdlc";
            //reportViewer.LocalReport.SetParameters(new ReportParameter[] { staffname, from, to, khmerDate, khmerYear, qrCODE });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));            ViewBag.ReportViewer = reportViewer;            return View("_invoicerpt");
        }

        [Route("invoice-rpt")]        [System.Web.Mvc.HttpGet]        public ActionResult GetInvoiceReport()        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;            SqlConnection con = new SqlConnection(connectionString);            SqlDataAdapter adp = new SqlDataAdapter("Select * From InvoiceV where id=1", con);            adp.Fill(ds);            ReportViewer reportViewer = new ReportViewer();            reportViewer.ProcessingMode = ProcessingMode.Local;            reportViewer.SizeToReportContent = true;            reportViewer.Width = Unit.Percentage(100);            reportViewer.Height = Unit.Percentage(100);
            //ReportParameter qrCODE = new ReportParameter("qrCODE", base64String);
            //reportViewer.LocalReport.EnableExternalImages = true;
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\Report1.rdlc";
            //reportViewer.LocalReport.SetParameters(new ReportParameter[] { staffname, from, to, khmerDate, khmerYear, qrCODE });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));            ViewBag.ReportViewer = reportViewer;            return View("_invoicerpt");
        }

        [Route("staff-list")]        [System.Web.Mvc.HttpGet]        public ActionResult GetDepartment()        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;            SqlConnection con = new SqlConnection(connectionString);            SqlDataAdapter adp = new SqlDataAdapter("Select * From STAFF_V", con);            adp.Fill(ds);            ReportViewer reportViewer = new ReportViewer();            reportViewer.ProcessingMode = ProcessingMode.Local;            reportViewer.SizeToReportContent = true;            reportViewer.Width = Unit.Percentage(100);            reportViewer.Height = Unit.Percentage(100);
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\STAFF_REPORT.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));            ViewBag.ReportViewer = reportViewer;            return View("_stafflistrpt");
        }

        [Route("guest-list")]        [System.Web.Mvc.HttpGet]        public ActionResult GetBranch()        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;            SqlConnection con = new SqlConnection(connectionString);            SqlDataAdapter adp = new SqlDataAdapter("Select * From guest_tbl", con);            adp.Fill(ds);            ReportViewer reportViewer = new ReportViewer();            reportViewer.ProcessingMode = ProcessingMode.Local;            reportViewer.SizeToReportContent = true;            reportViewer.Width = Unit.Percentage(100);            reportViewer.Height = Unit.Percentage(100);
            //ReportParameter qrCODE = new ReportParameter("qrCODE", base64String);
            //reportViewer.LocalReport.EnableExternalImages = true;
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\GUEST_REPORT.rdlc";
            //reportViewer.LocalReport.SetParameters(new ReportParameter[] { staffname, from, to, khmerDate, khmerYear, qrCODE });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));            ViewBag.ReportViewer = reportViewer;            return View("_guestlistrpt");
        }

        [Route("item-list")]        [System.Web.Mvc.HttpGet]        public ActionResult GetItem()        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;            SqlConnection con = new SqlConnection(connectionString);            SqlDataAdapter adp = new SqlDataAdapter("Select * From item_tbl", con);            adp.Fill(ds);            ReportViewer reportViewer = new ReportViewer();            reportViewer.ProcessingMode = ProcessingMode.Local;            reportViewer.SizeToReportContent = true;            reportViewer.Width = Unit.Percentage(100);            reportViewer.Height = Unit.Percentage(100);
            //ReportParameter qrCODE = new ReportParameter("qrCODE", base64String);
            //reportViewer.LocalReport.EnableExternalImages = true;
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\ITEM_REPORT.rdlc";
            //reportViewer.LocalReport.SetParameters(new ReportParameter[] { staffname, from, to, khmerDate, khmerYear, qrCODE });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));            ViewBag.ReportViewer = reportViewer;            return View("_itemlistrpt");
        }


        [Route("user-list")]        [System.Web.Mvc.HttpGet]        public ActionResult GetUser()        {

            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;            SqlConnection con = new SqlConnection(connectionString);            SqlDataAdapter adp = new SqlDataAdapter("Select * From AspNetUsers", con);            adp.Fill(ds);            ReportViewer reportViewer = new ReportViewer();            reportViewer.ProcessingMode = ProcessingMode.Local;            reportViewer.SizeToReportContent = true;            reportViewer.Width = Unit.Percentage(100);            reportViewer.Height = Unit.Percentage(100);
            //ReportParameter qrCODE = new ReportParameter("qrCODE", base64String);
            //reportViewer.LocalReport.EnableExternalImages = true;
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\USER_REPORT.rdlc";
            //reportViewer.LocalReport.SetParameters(new ReportParameter[] { staffname, from, to, khmerDate, khmerYear, qrCODE });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));            ViewBag.ReportViewer = reportViewer;            return View("_userrpt");
        }

        [Route("payslip-rpt")]        [System.Web.Mvc.HttpGet]        public ActionResult GetEmployee(int id)        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;            SqlConnection con = new SqlConnection(connectionString);            SqlDataAdapter adp = new SqlDataAdapter("Select * From PaySlip_V where id=" + id, con);            adp.Fill(ds);            ReportViewer reportViewer = new ReportViewer();            reportViewer.ProcessingMode = ProcessingMode.Local;            reportViewer.SizeToReportContent = true;            reportViewer.Width = Unit.Percentage(100);            reportViewer.Height = Unit.Percentage(100);
            //ReportParameter qrCODE = new ReportParameter("qrCODE", base64String);
            //reportViewer.LocalReport.EnableExternalImages = true;
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\PAYSLIP_REPORT.rdlc";
            //reportViewer.LocalReport.SetParameters(new ReportParameter[] { staffname, from, to, khmerDate, khmerYear, qrCODE });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));            ViewBag.ReportViewer = reportViewer;            return View("_payslip");
        }

        [Route("booking-rpt/{id}")]        [System.Web.Mvc.HttpGet]        public ActionResult GetBooking(int id)        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;            SqlConnection con = new SqlConnection(connectionString);            SqlDataAdapter adp = new SqlDataAdapter("Select * From Booking_V where id=" + id, con);            adp.Fill(ds);            ReportViewer reportViewer = new ReportViewer();            reportViewer.ProcessingMode = ProcessingMode.Local;            reportViewer.SizeToReportContent = true;            reportViewer.Width = Unit.Percentage(100);            reportViewer.Height = Unit.Percentage(100);
            //ReportParameter qrCODE = new ReportParameter("qrCODE", base64String);
            //reportViewer.LocalReport.EnableExternalImages = true;
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\BOOK_REPORT.rdlc";
            //reportViewer.LocalReport.SetParameters(new ReportParameter[] { staffname, from, to, khmerDate, khmerYear, qrCODE });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));            ViewBag.ReportViewer = reportViewer;            return View("_bookingrpt");
        }

    }
}
