using iTextSharp.text.pdf;
using Microsoft.AspNet.Identity;
using Microsoft.Reporting.WebForms;
using SCHOOL_MANAGEMENT_SYSTEM.Models;
using SCHOOL_MANAGEMENT_SYSTEM.ViewModels;
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

        [Route("check-out-report/{id}")]
        [System.Web.Mvc.HttpGet]
        public ActionResult GetCheckOutReport(string id)
        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("Select * From CHECK_OUT_V where id=" + id, con);
            adp.Fill(ds);

            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\CHECKOUT_REPORT.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));
            ViewBag.ReportViewer = reportViewer;
            return View("_checkout");
        }

        [Route("invoice-report/{invoiceid}")]
        [System.Web.Mvc.HttpGet]
        public ActionResult GetInvoiceReport(string invoiceid)
        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("Select * From InvoiceV where id=" + invoiceid, con);
            adp.Fill(ds);

            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);
            //ReportParameter qrCODE = new ReportParameter("qrCODE", base64String);
            //reportViewer.LocalReport.EnableExternalImages = true;
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\INVOICE_REPORT.rdlc";
            //reportViewer.LocalReport.SetParameters(new ReportParameter[] { staffname, from, to, khmerDate, khmerYear, qrCODE });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));
            ViewBag.ReportViewer = reportViewer;
            return View("_invoicerpt");
        }

        [Route("checkin-rpt/{id}")]
        [System.Web.Mvc.HttpGet]
        public ActionResult GetCheckInReport(int id)
        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("Select * From CHECK_IN_V where id=" + id, con);
            adp.Fill(ds);

            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\INVOICE_REPORT.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));
            ViewBag.ReportViewer = reportViewer;
            return View("_checkinrpt");
        }

        [Route("invoice-rpt/{id}")]
        [System.Web.Mvc.HttpGet]
        public ActionResult GetInvoiceReport(int id)
        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("Select * From InvoiceV where id="+id, con);
            adp.Fill(ds);

            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);
            ReportParameter pfromdate = new ReportParameter("fromdate", DateTime.Today.ToString("yyyy-MM-dd"));
            ReportParameter ptodate = new ReportParameter("todate", DateTime.Today.ToString("yyyy-MM-dd"));
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\INVOICE_REPORT.rdlc";
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { pfromdate, ptodate });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));
            ViewBag.ReportViewer = reportViewer;
            return View("_invoicerpt");
        }
        [Route("invoicelist-rpt")]
        [System.Web.Mvc.HttpGet]
        public ActionResult GetInvoiceReport()
        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("Select * From InvoiceV where invoicedate >= getdate()", con);
            adp.Fill(ds);

            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);
            ReportParameter pfromdate = new ReportParameter("fromdate", DateTime.Today.ToString("yyyy-MM-dd"));
            ReportParameter ptodate = new ReportParameter("todate", DateTime.Today.ToString("yyyy-MM-dd"));
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\INVOICE_LIST.rdlc";
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { pfromdate, ptodate });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));
            ViewBag.ReportViewer = reportViewer;
            return View("_invoicelist");
        }
        [Route("invoicelist-rpt/{fromdate}/{todate}/paid")]
        [System.Web.Mvc.HttpGet]
        public ActionResult GetInvoiceReport(string fromdate,string todate, string paid)
        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("Select * From InvoiceV where invoicedate between '" + fromdate + "' and '"+todate+"' and paid=1", con);
            adp.Fill(ds);

            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);
            ReportParameter pfromdate = new ReportParameter("fromdate", fromdate);
            ReportParameter ptodate = new ReportParameter("todate", todate);
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\INVOICE_LIST.rdlc";
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { pfromdate, ptodate });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));
            ViewBag.ReportViewer = reportViewer;
            return View("_invoicelist");
        }
        [Route("invoicelist-rpt/{fromdate}/{todate}/notpaid")]
        [System.Web.Mvc.HttpGet]
        public ActionResult GetInvoiceReport(string fromdate, string todate)
        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("Select * From InvoiceV where invoicedate between '" + fromdate + "' and '" + todate + "' and paid=0", con);
            adp.Fill(ds);

            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);
            ReportParameter pfromdate = new ReportParameter("fromdate", fromdate);
            ReportParameter ptodate = new ReportParameter("todate", todate);
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\INVOICE_LIST_NOT_PAID.rdlc";
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { pfromdate, ptodate });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));
            ViewBag.ReportViewer = reportViewer;
            return View("_invoicelist");
        }
        [Route("staff-list")]
        [System.Web.Mvc.HttpGet]
        public ActionResult GetDepartment()
        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("Select * From STAFF_V", con);
            adp.Fill(ds);

            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\STAFF_REPORT.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));
            ViewBag.ReportViewer = reportViewer;
            return View("_stafflistrpt");
        }

        [Route("guest-list")]
        [System.Web.Mvc.HttpGet]
        public ActionResult GetBranch()
        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("Select * From guest_tbl", con);
            adp.Fill(ds);

            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);
            //ReportParameter qrCODE = new ReportParameter("qrCODE", base64String);
            //reportViewer.LocalReport.EnableExternalImages = true;
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\GUEST_REPORT.rdlc";
            //reportViewer.LocalReport.SetParameters(new ReportParameter[] { staffname, from, to, khmerDate, khmerYear, qrCODE });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));
            ViewBag.ReportViewer = reportViewer;
            return View("_guestlistrpt");
        }

        [Route("item-list")]
        [System.Web.Mvc.HttpGet]
        public ActionResult GetItem()
        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("Select * From item_tbl", con);
            adp.Fill(ds);

            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);
            //ReportParameter qrCODE = new ReportParameter("qrCODE", base64String);
            //reportViewer.LocalReport.EnableExternalImages = true;
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\ITEM_REPORT.rdlc";
            //reportViewer.LocalReport.SetParameters(new ReportParameter[] { staffname, from, to, khmerDate, khmerYear, qrCODE });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));
            ViewBag.ReportViewer = reportViewer;
            return View("_itemlistrpt");
        }

        [Route("user-list")]
        [System.Web.Mvc.HttpGet]
        public ActionResult GetUser()
        {

            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("Select * From AspNetUsers", con);
            adp.Fill(ds);

            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);
            //ReportParameter qrCODE = new ReportParameter("qrCODE", base64String);
            //reportViewer.LocalReport.EnableExternalImages = true;
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\USER_REPORT.rdlc";
            //reportViewer.LocalReport.SetParameters(new ReportParameter[] { staffname, from, to, khmerDate, khmerYear, qrCODE });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));
            ViewBag.ReportViewer = reportViewer;
            return View("_userrpt");
        }

        [Route("payslip-rpt/{id}")]
        [System.Web.Mvc.HttpGet]
        public ActionResult GetEmployee(int id)
        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("Select * From PaySlip_V where id=" + id, con);
            adp.Fill(ds);
            string amount = ds.Rows[0]["totalsalary"].ToString();
            string a = ConvertAmount(double.Parse(amount));
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);
            ReportParameter pamountinword = new ReportParameter("amountinword", a);
            reportViewer.LocalReport.EnableExternalImages = true;
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\PAYSLIP_REPORT.rdlc";
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { pamountinword});
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));
            ViewBag.ReportViewer = reportViewer;
            return View("_payslip");
        }

        [Route("booking-rpt/{id}")]
        [System.Web.Mvc.HttpGet]
        public ActionResult GetBooking(int id)
        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("Select * From Booking_V where id=" + id, con);
            adp.Fill(ds);

            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);
            //ReportParameter qrCODE = new ReportParameter("qrCODE", base64String);
            //reportViewer.LocalReport.EnableExternalImages = true;
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\BOOK_REPORT.rdlc";
            //reportViewer.LocalReport.SetParameters(new ReportParameter[] { staffname, from, to, khmerDate, khmerYear, qrCODE });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));
            ViewBag.ReportViewer = reportViewer;
            return View("_bookingrpt");
        }

        [Route("bookinglist-rpt")]
        [System.Web.Mvc.HttpGet]
        public ActionResult GetBookingList()
        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("select * from Booking_V where bookingdate>=GETDATE()", con);
            adp.Fill(ds);

            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\BOOKING_LIST.rdlc";
           
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));
            ViewBag.ReportViewer = reportViewer;
            return View("_bookinglist");
        }
        [Route("bookinglist-rpt/{fromdate}/{todate}/{status}")]
        [System.Web.Mvc.HttpGet]
        public ActionResult GetBookingList(string fromdate,string todate,string status)
        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("Select * From Booking_V where bookingdate between '"+fromdate+"' and '"+todate+"' and status='" + status+"'", con);
            adp.Fill(ds);

            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);
            ReportParameter pfromdate = new ReportParameter("fromdate", fromdate);
            ReportParameter ptodate = new ReportParameter("todate", todate);
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\BOOKING_LIST.rdlc";
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { pfromdate, ptodate });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));
            ViewBag.ReportViewer = reportViewer;
            return View("_bookinglist");
        }

        [Route("checkinlist-rpt")]
        [System.Web.Mvc.HttpGet]
        public ActionResult GetCheckInList()
        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("Select * From CHECK_IN_V where active=1 and checkindate>=GETDATE()  ", con);
            adp.Fill(ds);

            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);
            ReportParameter pfromdate = new ReportParameter("fromdate", DateTime.Today.ToString("yyyy-MM-dd"));
            ReportParameter ptodate = new ReportParameter("todate", DateTime.Today.ToString("yyyy-MM-dd"));
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\CHECK_IN_LIST.rdlc";
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { pfromdate, ptodate });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));
            ViewBag.ReportViewer = reportViewer;
            return View("_checkinlist");
        }
        [Route("checkinlist-rpt/{fromdate}/{todate}")]
        [System.Web.Mvc.HttpGet]
        public ActionResult GetCheckInList(string fromdate, string todate)
        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("Select * From CHECK_IN_V where checkindate between '" + fromdate + "' and '" + todate + "'", con);
            adp.Fill(ds);

            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);
            ReportParameter pfromdate = new ReportParameter("fromdate", fromdate);
            ReportParameter ptodate = new ReportParameter("todate", todate);
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\CHECK_IN_LIST.rdlc";
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { pfromdate, ptodate });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));
            ViewBag.ReportViewer = reportViewer;
            return View("_checkinlist");
        }

        [Route("checkoutlist-rpt")]
        [System.Web.Mvc.HttpGet]
        public ActionResult GetChecOutList()
        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("select * from CHECK_OUT_V where date >=GETDATE() ", con);
            adp.Fill(ds);

            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);
            ReportParameter pfromdate = new ReportParameter("fromdate", DateTime.Today.ToString("yyyy-MM-dd"));
            ReportParameter ptodate = new ReportParameter("todate", DateTime.Today.ToString("yyyy-MM-dd"));
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\CHECK_OUT_LIST.rdlc";
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { pfromdate, ptodate });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));
            ViewBag.ReportViewer = reportViewer;
            return View("_checkoutlist");
        }
        [Route("checkoutlist-rpt/{fromdate}/{todate}")]
        [System.Web.Mvc.HttpGet]
        public ActionResult GetCheckOutList(string fromdate, string todate)
        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("select * from CHECK_OUT_V where date between '" + fromdate + "' and '" + todate + "'", con);
            adp.Fill(ds);

            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);
            ReportParameter pfromdate = new ReportParameter("fromdate", fromdate);
            ReportParameter ptodate = new ReportParameter("todate", todate);
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\CHECK_OUT_LIST.rdlc";
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { pfromdate, ptodate });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));
            ViewBag.ReportViewer = reportViewer;
            return View("_checkoutlist");
        }

        [Route("paysliplist-rpt")]
        [System.Web.Mvc.HttpGet]
        public ActionResult GetPaySlipList()
        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("select * from PaySlip_V where date >= getdate()", con);
            adp.Fill(ds);

            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);
            ReportParameter pfromdate = new ReportParameter("fromdate", DateTime.Today.ToString("yyyy-MM-dd"));
            ReportParameter ptodate = new ReportParameter("todate", DateTime.Today.ToString("yyyy-MM-dd"));
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\PAYSLIP_LIST.rdlc";
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { pfromdate, ptodate });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));
            ViewBag.ReportViewer = reportViewer;
            return View("_paysliplist");
        }
        [Route("paysliplist-rpt/{fromdate}/{todate}")]
        [System.Web.Mvc.HttpGet]
        public ActionResult GetPaySlipList(string fromdate, string todate)
        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("select * from PaySlip_V where date between '" + fromdate + "' and '" + todate + "'", con);
            adp.Fill(ds);

            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);
            ReportParameter pfromdate = new ReportParameter("fromdate", fromdate);
            ReportParameter ptodate = new ReportParameter("todate", todate);
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\PAYSLIP_LIST.rdlc";
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { pfromdate, ptodate });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));
            ViewBag.ReportViewer = reportViewer;
            return View("_paysliplist");
        }

        [Route("otherexpenselist-rpt")]
        [System.Web.Mvc.HttpGet]
        public ActionResult _otherexpenselist()
        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("select * from OTHER_EXPENSE_V where date >= getdate()", con);
            adp.Fill(ds);
            var reportView = new ReportViewModel()
            {
                ExpenseType = _context.ExpenseTypes.ToList(),
            };
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);
            ReportParameter pfromdate = new ReportParameter("fromdate", DateTime.Today.ToString("yyyy-MM-dd"));
            ReportParameter ptodate = new ReportParameter("todate", DateTime.Today.ToString("yyyy-MM-dd"));
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\OTHER_EXPENSE.rdlc";
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { pfromdate, ptodate });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));
            ViewBag.ReportViewer = reportViewer;
            return View("_otherexpenselist", reportView);
        }
        [Route("otherexpenselist-rpt/{fromdate}/{todate}/{typename}")]
        [System.Web.Mvc.HttpGet]
        public ActionResult _otherexpenselist(string fromdate, string todate,string typename)
        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            string cmd = ""; 
            if (typename == "All")
            {
                cmd = "select * from OTHER_EXPENSE_V where date between '" + fromdate + "' and '" + todate + "'";
            }else
            {
                cmd = "select * from OTHER_EXPENSE_V where date between '" + fromdate + "' and '" + todate + "' and expensetypeid=" + typename;
            }
            SqlDataAdapter adp = new SqlDataAdapter(cmd, con);
            adp.Fill(ds);
            var reportView = new ReportViewModel()
            {
                ExpenseType = _context.ExpenseTypes.ToList(),
            };
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);
            ReportParameter pfromdate = new ReportParameter("fromdate", fromdate);
            ReportParameter ptodate = new ReportParameter("todate", todate);
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\OTHER_EXPENSE.rdlc";
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { pfromdate, ptodate });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));
            ViewBag.ReportViewer = reportViewer;
            reportViewer.ServerReport.Refresh();
            return View("_otherexpenselist",reportView);
        }

        [Route("incomelist-rpt")]
        [System.Web.Mvc.HttpGet]
        public ActionResult _incomelist()
        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("select * from INCOME_V where date >= getdate()", con);
            adp.Fill(ds);
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);
            ReportParameter pfromdate = new ReportParameter("fromdate", DateTime.Today.ToString("yyyy-MM-dd"));
            ReportParameter ptodate = new ReportParameter("todate", DateTime.Today.ToString("yyyy-MM-dd"));
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\INCOME_LIST.rdlc";
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { pfromdate, ptodate });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));
            ViewBag.ReportViewer = reportViewer;
            return View("_incomelist");
        }

        [Route("incomelist-rpt/{fromdate}/{todate}/{type}")]
        [System.Web.Mvc.HttpGet]
        public ActionResult _incomelist(string fromdate, string todate, string type)
        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            string cmd = "";
            if (type == "All")
            {
                cmd = "select * from INCOME_V where date between '" + fromdate + "' and '" + todate + "'";
            }
            else
            {
                cmd = "select * from INCOME_V where date between '" + fromdate + "' and '" + todate + "' and incometype='"+ type + "'";
            }
            SqlDataAdapter adp = new SqlDataAdapter(cmd, con);
            adp.Fill(ds);
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);
            ReportParameter pfromdate = new ReportParameter("fromdate", fromdate);
            ReportParameter ptodate = new ReportParameter("todate", todate);
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\INCOME_LIST.rdlc";
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { pfromdate, ptodate });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));
            ViewBag.ReportViewer = reportViewer;
            reportViewer.ServerReport.Refresh();
            return View("_incomelist");
        }


        [Route("room-list")]
        [System.Web.Mvc.HttpGet]
        public ActionResult _roomlist()
        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("select * from ROOM_LIST", con);
            adp.Fill(ds);
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\ROOM_REPORT.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));
            ViewBag.ReportViewer = reportViewer;
            return View("_roomlistrpt");
        }

        [Route("room-list/{status}")]
        [System.Web.Mvc.HttpGet]
        public ActionResult _roomlist(string status)
        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            SqlDataAdapter adp;
            if (status == "All")
            {
                 adp = new SqlDataAdapter("select * from ROOM_LIST ", con);
            }
            else
            {
                 adp = new SqlDataAdapter("select * from ROOM_LIST where status='" + status + "'", con);
            }
            adp.Fill(ds);
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\ROOM_REPORT.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));
            ViewBag.ReportViewer = reportViewer;
            reportViewer.ServerReport.Refresh();
            return View("_roomlistrpt");
        }

        public static String ConvertAmount(double amount)
        {
            try
            {
                Int64 amount_int = (Int64)amount;
                Int64 amount_dec = (Int64)Math.Round((amount - (double)(amount_int)) * 100);
                if (amount_dec == 0)
                {
                    return NumberToWords(amount_int) + "ដុល្លាគត់";
                }
                else
                {
                    return NumberToWords(amount_int) + "ដុល្លា និង" + NumberToWords(amount_dec) + "សេន";
                }
            }
            catch (Exception )
            {
                // TODO: handle exception  
            }
            return "";
        }
        public static string NumberToWords(Int64 number)
        {
            if (number == 0)
                return "សូន្យ";

            if (number < 0)
                return "ដក" + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + "លាន";
                number %= 1000000;
            }

            if ((number / 100000) > 0)
            {
                words += NumberToWords(number / 100000) + "សែន";
                number %= 100000;
            }

            if ((number / 10000) > 0)
            {
                words += NumberToWords(number / 10000) + "ម៉ឺន";
                number %= 10000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + "ពាន់";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + "រយ";
                number %= 100;
            }

            if (number > 0)
            {
                var unitsMap = new[] { "សួន្យ", "មួយ", "ពីរ", "បី", "បួន", "ប្រាំ", "ប្រាំមួយ", "ប្រាំពីរ", "ប្រាំបី", "ប្រាំបួន" };
                var tensMap = new[] { "សូន្យ", "ដប់", "ម្ភៃ", "សាមសិប", "សែសិប", "ហាសិប", "ហុកសិប", "ចិតសិប", "ប៉ែតសិប", "កៅសិប" };

                if (number < 10)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += unitsMap[number % 10];
                }
            }

            return words;
        }


        public static string GetFullDateKhmer(DateTime strInput)
        {

            string[] arrEnglish = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"};
            string[] arrKhmer =  { "០", "១", "២", "៣", "៤", "៥", "៦", "៧", "៨", "៩"};
            string[] arrMonth = { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12"};
            string[] arrMonthKh = { "មករា", "កុម្ភៈ", "មីនា", "មេសា", "ឧសភា", "មិថុនា", "កក្កដា", "សីហា", "កញ្ញា", "តុលា", "វិច្ឆិកា", "ធ្នូ"};

            string TextShow = strInput.ToString("dd-MMM-yyyy").Replace("-", " ");

            for (int j=0; j== arrKhmer.Length - 1; j++)
            {
                TextShow = TextShow.Replace(arrEnglish[j], arrKhmer[j]);
            }

            for (int i = 0; i == arrMonth.Length - 1; i++)
            {
                TextShow = TextShow.Replace(arrMonth[i], arrMonthKh[i]);
            }

            return TextShow;
        }
    }
}
