using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers
{
    public class ReportsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Route("student-invoice/{invoiceid}")]        [System.Web.Mvc.HttpGet]        public ActionResult GetInvoiceReport(string invoiceid)        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;            SqlConnection con = new SqlConnection(connectionString);            SqlDataAdapter adp = new SqlDataAdapter("Select * From payment_v where id=" + invoiceid + "", con);            adp.Fill(ds);            ReportViewer reportViewer = new ReportViewer();            reportViewer.ProcessingMode = ProcessingMode.Local;            reportViewer.SizeToReportContent = true;            reportViewer.Width = Unit.Percentage(100);            reportViewer.Height = Unit.Percentage(100);
            //ReportParameter qrCODE = new ReportParameter("qrCODE", base64String);
            //reportViewer.LocalReport.EnableExternalImages = true;
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\PaymentRpt.rdlc";
            //reportViewer.LocalReport.SetParameters(new ReportParameter[] { staffname, from, to, khmerDate, khmerYear, qrCODE });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));            ViewBag.ReportViewer = reportViewer;            return View("_paymentrpt");
        }

        [Route("department-list")]        [System.Web.Mvc.HttpGet]        public ActionResult GetDepartment()        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;            SqlConnection con = new SqlConnection(connectionString);            SqlDataAdapter adp = new SqlDataAdapter("Select * From Departments", con);            adp.Fill(ds);            ReportViewer reportViewer = new ReportViewer();            reportViewer.ProcessingMode = ProcessingMode.Local;            reportViewer.SizeToReportContent = true;            reportViewer.Width = Unit.Percentage(100);            reportViewer.Height = Unit.Percentage(100);
            //ReportParameter qrCODE = new ReportParameter("qrCODE", base64String);
            //reportViewer.LocalReport.EnableExternalImages = true;
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\DepartmentRpt.rdlc";
            //reportViewer.LocalReport.SetParameters(new ReportParameter[] { staffname, from, to, khmerDate, khmerYear, qrCODE });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));            ViewBag.ReportViewer = reportViewer;            return View("_departmentrpt");
        }

        [Route("branch-list")]        [System.Web.Mvc.HttpGet]        public ActionResult GetBranch()        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;            SqlConnection con = new SqlConnection(connectionString);            SqlDataAdapter adp = new SqlDataAdapter("Select * From Branches", con);            adp.Fill(ds);            ReportViewer reportViewer = new ReportViewer();            reportViewer.ProcessingMode = ProcessingMode.Local;            reportViewer.SizeToReportContent = true;            reportViewer.Width = Unit.Percentage(100);            reportViewer.Height = Unit.Percentage(100);
            //ReportParameter qrCODE = new ReportParameter("qrCODE", base64String);
            //reportViewer.LocalReport.EnableExternalImages = true;
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\BranchRpt.rdlc";
            //reportViewer.LocalReport.SetParameters(new ReportParameter[] { staffname, from, to, khmerDate, khmerYear, qrCODE });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));            ViewBag.ReportViewer = reportViewer;            return View("_branchrpt");
        }


        [Route("user-list")]        [System.Web.Mvc.HttpGet]        public ActionResult GetUser()        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;            SqlConnection con = new SqlConnection(connectionString);            SqlDataAdapter adp = new SqlDataAdapter("Select * From AspNetUsers", con);            adp.Fill(ds);            ReportViewer reportViewer = new ReportViewer();            reportViewer.ProcessingMode = ProcessingMode.Local;            reportViewer.SizeToReportContent = true;            reportViewer.Width = Unit.Percentage(100);            reportViewer.Height = Unit.Percentage(100);
            //ReportParameter qrCODE = new ReportParameter("qrCODE", base64String);
            //reportViewer.LocalReport.EnableExternalImages = true;
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\UserRpt.rdlc";
            //reportViewer.LocalReport.SetParameters(new ReportParameter[] { staffname, from, to, khmerDate, khmerYear, qrCODE });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));            ViewBag.ReportViewer = reportViewer;            return View("_userrpt");
        }

        [Route("employee-list")]        [System.Web.Mvc.HttpGet]        public ActionResult GetEmployee()        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;            SqlConnection con = new SqlConnection(connectionString);            SqlDataAdapter adp = new SqlDataAdapter("Select * From Employees", con);            adp.Fill(ds);            ReportViewer reportViewer = new ReportViewer();            reportViewer.ProcessingMode = ProcessingMode.Local;            reportViewer.SizeToReportContent = true;            reportViewer.Width = Unit.Percentage(100);            reportViewer.Height = Unit.Percentage(100);
            //ReportParameter qrCODE = new ReportParameter("qrCODE", base64String);
            //reportViewer.LocalReport.EnableExternalImages = true;
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\EmployeeRpt.rdlc";
            //reportViewer.LocalReport.SetParameters(new ReportParameter[] { staffname, from, to, khmerDate, khmerYear, qrCODE });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));            ViewBag.ReportViewer = reportViewer;            return View("_employeerpt");
        }


        [Route("shift-list")]        [System.Web.Mvc.HttpGet]        public ActionResult GetShift()        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;            SqlConnection con = new SqlConnection(connectionString);            SqlDataAdapter adp = new SqlDataAdapter("Select * From Shifts", con);            adp.Fill(ds);            ReportViewer reportViewer = new ReportViewer();            reportViewer.ProcessingMode = ProcessingMode.Local;            reportViewer.SizeToReportContent = true;            reportViewer.Width = Unit.Percentage(100);            reportViewer.Height = Unit.Percentage(100);
            //ReportParameter qrCODE = new ReportParameter("qrCODE", base64String);
            //reportViewer.LocalReport.EnableExternalImages = true;
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\ShiftRpt.rdlc";
            //reportViewer.LocalReport.SetParameters(new ReportParameter[] { staffname, from, to, khmerDate, khmerYear, qrCODE });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));            ViewBag.ReportViewer = reportViewer;            return View("_shiftsrpt");
        }

        [Route("grade-list")]        [System.Web.Mvc.HttpGet]        public ActionResult GetGrade()        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;            SqlConnection con = new SqlConnection(connectionString);            SqlDataAdapter adp = new SqlDataAdapter("Select * From Grades", con);            adp.Fill(ds);            ReportViewer reportViewer = new ReportViewer();            reportViewer.ProcessingMode = ProcessingMode.Local;            reportViewer.SizeToReportContent = true;            reportViewer.Width = Unit.Percentage(100);            reportViewer.Height = Unit.Percentage(100);
            //ReportParameter qrCODE = new ReportParameter("qrCODE", base64String);
            //reportViewer.LocalReport.EnableExternalImages = true;
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\GradeRpt.rdlc";
            //reportViewer.LocalReport.SetParameters(new ReportParameter[] { staffname, from, to, khmerDate, khmerYear, qrCODE });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));            ViewBag.ReportViewer = reportViewer;            return View("_gradesrpt");
        }


        [Route("course-list")]        [System.Web.Mvc.HttpGet]        public ActionResult GetCourse()        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;            SqlConnection con = new SqlConnection(connectionString);            SqlDataAdapter adp = new SqlDataAdapter("Select * From Courses", con);            adp.Fill(ds);            ReportViewer reportViewer = new ReportViewer();            reportViewer.ProcessingMode = ProcessingMode.Local;            reportViewer.SizeToReportContent = true;            reportViewer.Width = Unit.Percentage(100);            reportViewer.Height = Unit.Percentage(100);
            //ReportParameter qrCODE = new ReportParameter("qrCODE", base64String);
            //reportViewer.LocalReport.EnableExternalImages = true;
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\CourseRpt.rdlc";
            //reportViewer.LocalReport.SetParameters(new ReportParameter[] { staffname, from, to, khmerDate, khmerYear, qrCODE });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));            ViewBag.ReportViewer = reportViewer;            return View("_coursesrpt");
        }

        [Route("studylanguage-list")]        [System.Web.Mvc.HttpGet]        public ActionResult GetStudyLanguage()        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;            SqlConnection con = new SqlConnection(connectionString);            SqlDataAdapter adp = new SqlDataAdapter("Select * From Studylanguages", con);            adp.Fill(ds);            ReportViewer reportViewer = new ReportViewer();            reportViewer.ProcessingMode = ProcessingMode.Local;            reportViewer.SizeToReportContent = true;            reportViewer.Width = Unit.Percentage(100);            reportViewer.Height = Unit.Percentage(100);
            //ReportParameter qrCODE = new ReportParameter("qrCODE", base64String);
            //reportViewer.LocalReport.EnableExternalImages = true;
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\StudyLanguageRpt.rdlc";
            //reportViewer.LocalReport.SetParameters(new ReportParameter[] { staffname, from, to, khmerDate, khmerYear, qrCODE });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));            ViewBag.ReportViewer = reportViewer;            return View("_studylanguagesrpt");
        }

        [Route("period-list")]        [System.Web.Mvc.HttpGet]        public ActionResult GetStudyPeriod()        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;            SqlConnection con = new SqlConnection(connectionString);            SqlDataAdapter adp = new SqlDataAdapter("Select * From Studyperiods", con);            adp.Fill(ds);            ReportViewer reportViewer = new ReportViewer();            reportViewer.ProcessingMode = ProcessingMode.Local;            reportViewer.SizeToReportContent = true;            reportViewer.Width = Unit.Percentage(100);            reportViewer.Height = Unit.Percentage(100);
            //ReportParameter qrCODE = new ReportParameter("qrCODE", base64String);
            //reportViewer.LocalReport.EnableExternalImages = true;
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\StudyPeriodRpt.rdlc";
            //reportViewer.LocalReport.SetParameters(new ReportParameter[] { staffname, from, to, khmerDate, khmerYear, qrCODE });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));            ViewBag.ReportViewer = reportViewer;            return View("_studyperiodsrpt");
        }

        [Route("exchange-list")]        [System.Web.Mvc.HttpGet]        public ActionResult GetExchange()        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;            SqlConnection con = new SqlConnection(connectionString);            SqlDataAdapter adp = new SqlDataAdapter("Select * From ExchangeRates", con);            adp.Fill(ds);            ReportViewer reportViewer = new ReportViewer();            reportViewer.ProcessingMode = ProcessingMode.Local;            reportViewer.SizeToReportContent = true;            reportViewer.Width = Unit.Percentage(100);            reportViewer.Height = Unit.Percentage(100);
            //ReportParameter qrCODE = new ReportParameter("qrCODE", base64String);
            //reportViewer.LocalReport.EnableExternalImages = true;
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\ExchangeRateRpt.rdlc";
            //reportViewer.LocalReport.SetParameters(new ReportParameter[] { staffname, from, to, khmerDate, khmerYear, qrCODE });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));            ViewBag.ReportViewer = reportViewer;            return View("_exchangeratesrpt");
        }

        [Route("student-list")]        [System.Web.Mvc.HttpGet]        public ActionResult GetStudent()        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;            SqlConnection con = new SqlConnection(connectionString);            SqlDataAdapter adp = new SqlDataAdapter("select *, 'SH' + RIGHT('000000' + CONVERT(NVARCHAR, studentid), 6) AS stuID from Students", con);            adp.Fill(ds);            ReportViewer reportViewer = new ReportViewer();            reportViewer.ProcessingMode = ProcessingMode.Local;            reportViewer.SizeToReportContent = true;            reportViewer.Width = Unit.Percentage(100);            reportViewer.Height = Unit.Percentage(100);
            //ReportParameter qrCODE = new ReportParameter("qrCODE", base64String);
            //reportViewer.LocalReport.EnableExternalImages = true;
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\StudentRpt.rdlc";
            //reportViewer.LocalReport.SetParameters(new ReportParameter[] { staffname, from, to, khmerDate, khmerYear, qrCODE });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));            ViewBag.ReportViewer = reportViewer;            return View("_studentsrpt");
        }


        [Route("appropriate-list")]        [System.Web.Mvc.HttpGet]        public ActionResult GetAppropriate()        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;            SqlConnection con = new SqlConnection(connectionString);            SqlDataAdapter adp = new SqlDataAdapter("Select * From StudentAppropriate_V", con);            adp.Fill(ds);            ReportViewer reportViewer = new ReportViewer();            reportViewer.ProcessingMode = ProcessingMode.Local;            reportViewer.SizeToReportContent = true;            reportViewer.Width = Unit.Percentage(100);            reportViewer.Height = Unit.Percentage(100);
            //ReportParameter qrCODE = new ReportParameter("qrCODE", base64String);
            //reportViewer.LocalReport.EnableExternalImages = true;
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\AppropriateRpt.rdlc";
            //reportViewer.LocalReport.SetParameters(new ReportParameter[] { staffname, from, to, khmerDate, khmerYear, qrCODE });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));            ViewBag.ReportViewer = reportViewer;            return View("_appropriaterpt");
        }

        [Route("emergency-list")]        [System.Web.Mvc.HttpGet]        public ActionResult GetEmergency()        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;            SqlConnection con = new SqlConnection(connectionString);            SqlDataAdapter adp = new SqlDataAdapter("Select * From StudentEmergency_V", con);            adp.Fill(ds);            ReportViewer reportViewer = new ReportViewer();            reportViewer.ProcessingMode = ProcessingMode.Local;            reportViewer.SizeToReportContent = true;            reportViewer.Width = Unit.Percentage(100);            reportViewer.Height = Unit.Percentage(100);
            //ReportParameter qrCODE = new ReportParameter("qrCODE", base64String);
            //reportViewer.LocalReport.EnableExternalImages = true;
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\EmergencyRpt.rdlc";
            //reportViewer.LocalReport.SetParameters(new ReportParameter[] { staffname, from, to, khmerDate, khmerYear, qrCODE });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));            ViewBag.ReportViewer = reportViewer;            return View("_emergencyrpt");
        }

        [Route("student-parrent-list")]        [System.Web.Mvc.HttpGet]        public ActionResult GetStuentParrent()        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;            SqlConnection con = new SqlConnection(connectionString);            SqlDataAdapter adp = new SqlDataAdapter("Select * From StudentParrent_V", con);            adp.Fill(ds);            ReportViewer reportViewer = new ReportViewer();            reportViewer.ProcessingMode = ProcessingMode.Local;            reportViewer.SizeToReportContent = true;            reportViewer.Width = Unit.Percentage(100);            reportViewer.Height = Unit.Percentage(100);
            //ReportParameter qrCODE = new ReportParameter("qrCODE", base64String);
            //reportViewer.LocalReport.EnableExternalImages = true;
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\ParrentStudentsRpt.rdlc";
            //reportViewer.LocalReport.SetParameters(new ReportParameter[] { staffname, from, to, khmerDate, khmerYear, qrCODE });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));            ViewBag.ReportViewer = reportViewer;            return View("_parrentstudentrpt");
        }

        [Route("register-list")]        [System.Web.Mvc.HttpGet]        public ActionResult GetRegister()        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;            SqlConnection con = new SqlConnection(connectionString);            SqlDataAdapter adp = new SqlDataAdapter("Select * From Register_V", con);            adp.Fill(ds);            ReportViewer reportViewer = new ReportViewer();            reportViewer.ProcessingMode = ProcessingMode.Local;            reportViewer.SizeToReportContent = true;            reportViewer.Width = Unit.Percentage(100);            reportViewer.Height = Unit.Percentage(100);
            //ReportParameter qrCODE = new ReportParameter("qrCODE", base64String);
            //reportViewer.LocalReport.EnableExternalImages = true;
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\RegisterVRpt.rdlc";
            //reportViewer.LocalReport.SetParameters(new ReportParameter[] { staffname, from, to, khmerDate, khmerYear, qrCODE });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));            ViewBag.ReportViewer = reportViewer;            return View("_register_vrpt");
        }

        [Route("payment-summary-list")]        public ActionResult GetOrderSummary()        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;            SqlConnection conx = new SqlConnection(connectionString);            SqlDataAdapter adp = new SqlDataAdapter("select * from PaymentRpt where paymentstatus = 'ACTIVE' AND cast(paymentdate as date)>= cast(getdate() as date)", conx);

            adp.Fill(ds);            ReportViewer reportViewer = new ReportViewer();            reportViewer.ProcessingMode = ProcessingMode.Local;            reportViewer.SizeToReportContent = true;            reportViewer.ShowPrintButton = true;            reportViewer.Width = Unit.Percentage(100);            reportViewer.Height = Unit.Percentage(100);
            //ReportParameter qrCODE = new ReportParameter("qrCODE", base64String);
            //reportViewer.LocalReport.EnableExternalImages = true;
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\PaymentReportRpt.rdlc";
            //reportViewer.LocalReport.SetParameters(new ReportParameter[] { staffname, from, to, khmerDate, khmerYear, qrCODE });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));            ViewBag.ReportViewer = reportViewer;            return View("_payment_v_reportrpt");

        }

        [Route("payment-list-by-date/{fromdate}/{todate}")]        public ActionResult GetPaymentSummaryByDate(string fromdate, string todate)        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;            SqlConnection conx = new SqlConnection(connectionString);            SqlDataAdapter adp = new SqlDataAdapter("select * from PaymentRpt where paymentstatus = 'ACTIVE' AND cast(paymentdate as date)>= cast(@from as Date) and paymentdate<=cast(@to as Date) ", conx);
            adp.SelectCommand.Parameters.Add("@from",SqlDbType.Date).Value= fromdate;
            adp.SelectCommand.Parameters.Add("@to", SqlDbType.Date).Value= todate;
            adp.Fill(ds);            ReportViewer reportViewer = new ReportViewer();            reportViewer.ProcessingMode = ProcessingMode.Local;            reportViewer.SizeToReportContent = true;            reportViewer.ShowPrintButton = true;            reportViewer.Width = Unit.Percentage(100);            reportViewer.Height = Unit.Percentage(100);
            ReportParameter staff = new ReportParameter("user", User.Identity.Name);
            ReportParameter fromd = new ReportParameter("fromdate", fromdate);
            ReportParameter tod = new ReportParameter("todate", todate);
            //reportViewer.LocalReport.EnableExternalImages = true;
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\PaymentReportRpt.rdlc";
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { staff, fromd, tod });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));            ViewBag.ReportViewer = reportViewer;            return View("_payment_v_reportrpt");

        }

        [Route("paymentdetail-list")]        public ActionResult GetPaymentDetail(string fromdate, string todate)        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;            SqlConnection conx = new SqlConnection(connectionString);            SqlDataAdapter adp = new SqlDataAdapter("select * from PaymentDetail_V where paymentstatus = 'ACTIVE' AND cast(paymentdate as date)>= cast(getdate() as date) ", conx);
            //adp.SelectCommand.Parameters.AddWithValue("@from", fromdate);
            //adp.SelectCommand.Parameters.AddWithValue("@to", todate);
            adp.Fill(ds);            ReportViewer reportViewer = new ReportViewer();            reportViewer.ProcessingMode = ProcessingMode.Local;            reportViewer.SizeToReportContent = true;            reportViewer.ShowPrintButton = true;            reportViewer.Width = Unit.Percentage(100);            reportViewer.Height = Unit.Percentage(100);
            ReportParameter staff = new ReportParameter("user", User.Identity.Name);
            //ReportParameter fromd = new ReportParameter("fromdate", fromdate);
            //ReportParameter tod = new ReportParameter("todate", todate);
            //reportViewer.LocalReport.EnableExternalImages = true;
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\PaymentDetailRpt.rdlc";
            //reportViewer.LocalReport.SetParameters(new ReportParameter[] { staff, fromd, tod });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));            ViewBag.ReportViewer = reportViewer;            return View("_paymentdetail_vrpt");

        }

        [Route("paymentdetail-list-by-date/{fromdate}/{todate}")]        public ActionResult GetPaymentDetailByDate(string fromdate, string todate)        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;            SqlConnection conx = new SqlConnection(connectionString);            SqlDataAdapter adp = new SqlDataAdapter("select * from PaymentDetail_V where paymentstatus = 'ACTIVE' AND cast(paymentdate as date)>= cast(@from as Date) and paymentdate<=cast(@to as Date) ", conx);
            adp.SelectCommand.Parameters.AddWithValue("@from", fromdate);
            adp.SelectCommand.Parameters.AddWithValue("@to", todate);
            adp.Fill(ds);            ReportViewer reportViewer = new ReportViewer();            reportViewer.ProcessingMode = ProcessingMode.Local;            reportViewer.SizeToReportContent = true;            reportViewer.ShowPrintButton = true;            reportViewer.Width = Unit.Percentage(100);            reportViewer.Height = Unit.Percentage(100);
            ReportParameter staff = new ReportParameter("user", User.Identity.Name);
            ReportParameter fromd = new ReportParameter("fromdate", fromdate);
            ReportParameter tod = new ReportParameter("todate", todate);
            //reportViewer.LocalReport.EnableExternalImages = true;
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\PaymentDetailRpt.rdlc";
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { staff, fromd, tod });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));            ViewBag.ReportViewer = reportViewer;            return View("_paymentdetail_vrpt");

        }









        //Report New
        [Route("customer-invoice/{invoiceid}")]        [System.Web.Mvc.HttpGet]        public ActionResult GetInvoice(string invoiceid)        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;            SqlConnection con = new SqlConnection(connectionString);            SqlDataAdapter adp = new SqlDataAdapter("Select * From invoice_v where id=" + invoiceid + " and deliverytype=N'ដឹករួច'", con);            adp.Fill(ds);

            DataTable ds2 = new DataTable();            SqlDataAdapter adp2 = new SqlDataAdapter("Select * From invoice_v where id=" + invoiceid + " and deliverytype in(N'កំពុងដឹក',N'ដឹកបន្ត')", con);            adp2.Fill(ds2);            ReportViewer reportViewer = new ReportViewer();            reportViewer.ProcessingMode = ProcessingMode.Local;            reportViewer.SizeToReportContent = true;            reportViewer.Width = Unit.Percentage(100);            reportViewer.Height = Unit.Percentage(100);
            ReportParameter staff = new ReportParameter("staff", User.Identity.Name);
            //reportViewer.LocalReport.EnableExternalImages = true;
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\InvoiceRpt.rdlc";
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { staff });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", ds2));            ViewBag.ReportViewer = reportViewer;            return View("_paymentrpt");
        }

        [Route("customer-invoice-return/{invoiceid}")]        [System.Web.Mvc.HttpGet]        public ActionResult GetInvoiceReturn(string invoiceid)        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;            SqlConnection con = new SqlConnection(connectionString);            SqlDataAdapter adp = new SqlDataAdapter("Select * From invoice_v where id=" + invoiceid + " and deliverytype=N'ត្រឡប់'", con);            adp.Fill(ds);

                        ReportViewer reportViewer = new ReportViewer();            reportViewer.ProcessingMode = ProcessingMode.Local;            reportViewer.SizeToReportContent = true;            reportViewer.Width = Unit.Percentage(100);            reportViewer.Height = Unit.Percentage(100);
            ReportParameter staff = new ReportParameter("staff", User.Identity.Name);
            //reportViewer.LocalReport.EnableExternalImages = true;
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\InvoiceReturnRpt.rdlc";
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { staff });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));            ViewBag.ReportViewer = reportViewer;            return View("_paymentreturnrpt");
        }

        [Route("customer-list")]        [System.Web.Mvc.HttpGet]        public ActionResult GetCustomerList()        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;            SqlConnection con = new SqlConnection(connectionString);            SqlDataAdapter adp = new SqlDataAdapter("Select * From customer_tbl", con);            adp.Fill(ds);
                        ReportViewer reportViewer = new ReportViewer();            reportViewer.ProcessingMode = ProcessingMode.Local;            reportViewer.SizeToReportContent = true;            reportViewer.Width = Unit.Percentage(100);            reportViewer.Height = Unit.Percentage(100);
            //ReportParameter staff = new ReportParameter("staff", User.Identity.Name);
            //reportViewer.LocalReport.EnableExternalImages = true;
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\CustomerRpt.rdlc";
            //reportViewer.LocalReport.SetParameters(new ReportParameter[] { staff });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));            ViewBag.ReportViewer = reportViewer;            return View("_customerrpt");
        }

        [Route("category-list")]        [System.Web.Mvc.HttpGet]        public ActionResult GetCategoryList()        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;            SqlConnection con = new SqlConnection(connectionString);            SqlDataAdapter adp = new SqlDataAdapter("Select * From category_tbl", con);            adp.Fill(ds);

            ReportViewer reportViewer = new ReportViewer();            reportViewer.ProcessingMode = ProcessingMode.Local;            reportViewer.SizeToReportContent = true;            reportViewer.Width = Unit.Percentage(100);            reportViewer.Height = Unit.Percentage(100);
            //ReportParameter staff = new ReportParameter("staff", User.Identity.Name);
            //reportViewer.LocalReport.EnableExternalImages = true;
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\CategoryRpt.rdlc";
            //reportViewer.LocalReport.SetParameters(new ReportParameter[] { staff });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));            ViewBag.ReportViewer = reportViewer;            return View("_categoryrpt");
        }

        [Route("product-list")]        [System.Web.Mvc.HttpGet]        public ActionResult GetProductList()        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;            SqlConnection con = new SqlConnection(connectionString);            SqlDataAdapter adp = new SqlDataAdapter("Select * From Product_V", con);            adp.Fill(ds);

            ReportViewer reportViewer = new ReportViewer();            reportViewer.ProcessingMode = ProcessingMode.Local;            reportViewer.SizeToReportContent = true;            reportViewer.Width = Unit.Percentage(100);            reportViewer.Height = Unit.Percentage(100);
            //ReportParameter staff = new ReportParameter("staff", User.Identity.Name);
            //reportViewer.LocalReport.EnableExternalImages = true;
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\ProductRpt.rdlc";
            //reportViewer.LocalReport.SetParameters(new ReportParameter[] { staff });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));            ViewBag.ReportViewer = reportViewer;            return View("_productrpt");
        }

        [Route("position-list")]        [System.Web.Mvc.HttpGet]        public ActionResult GetPositionList()        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;            SqlConnection con = new SqlConnection(connectionString);            SqlDataAdapter adp = new SqlDataAdapter("Select * From position_tbl", con);            adp.Fill(ds);

            ReportViewer reportViewer = new ReportViewer();            reportViewer.ProcessingMode = ProcessingMode.Local;            reportViewer.SizeToReportContent = true;            reportViewer.Width = Unit.Percentage(100);            reportViewer.Height = Unit.Percentage(100);
            //ReportParameter staff = new ReportParameter("staff", User.Identity.Name);
            //reportViewer.LocalReport.EnableExternalImages = true;
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\PositionRpt.rdlc";
            //reportViewer.LocalReport.SetParameters(new ReportParameter[] { staff });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));            ViewBag.ReportViewer = reportViewer;            return View("_positionrpt");
        }

        [Route("showroom-list")]        [System.Web.Mvc.HttpGet]        public ActionResult GetShowroomList()        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;            SqlConnection con = new SqlConnection(connectionString);            SqlDataAdapter adp = new SqlDataAdapter("Select * From showroom_tbl", con);            adp.Fill(ds);

            ReportViewer reportViewer = new ReportViewer();            reportViewer.ProcessingMode = ProcessingMode.Local;            reportViewer.SizeToReportContent = true;            reportViewer.Width = Unit.Percentage(100);            reportViewer.Height = Unit.Percentage(100);
            //ReportParameter staff = new ReportParameter("staff", User.Identity.Name);
            //reportViewer.LocalReport.EnableExternalImages = true;
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\ShowroomRpt.rdlc";
            //reportViewer.LocalReport.SetParameters(new ReportParameter[] { staff });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));            ViewBag.ReportViewer = reportViewer;            return View("_showroomrpt");
        }

        [Route("location-list")]        [System.Web.Mvc.HttpGet]        public ActionResult GetLocationList()        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;            SqlConnection con = new SqlConnection(connectionString);            SqlDataAdapter adp = new SqlDataAdapter("Select * From location_tbl", con);            adp.Fill(ds);

            ReportViewer reportViewer = new ReportViewer();            reportViewer.ProcessingMode = ProcessingMode.Local;            reportViewer.SizeToReportContent = true;            reportViewer.Width = Unit.Percentage(100);            reportViewer.Height = Unit.Percentage(100);
            //ReportParameter staff = new ReportParameter("staff", User.Identity.Name);
            //reportViewer.LocalReport.EnableExternalImages = true;
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\LocationRpt.rdlc";
            //reportViewer.LocalReport.SetParameters(new ReportParameter[] { staff });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));            ViewBag.ReportViewer = reportViewer;            return View("_locationrpt");
        }

        [Route("invoice-summary-list")]        public ActionResult GetInvoiceSummary()        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;            SqlConnection conx = new SqlConnection(connectionString);            SqlDataAdapter adp = new SqlDataAdapter("select * from invoice_summary_v where cast(date as date)>= cast(getdate() as date)", conx);
            adp.Fill(ds);

            DataTable ds2 = new DataTable();            SqlDataAdapter adp2 = new SqlDataAdapter("select SUM(AmountDollar) as AmountDollar,SUM(AmountReal) as AmountReal,SUM(CarPriceDollar) as CarPriceDollar,SUM(CarpriceReal) as CarpriceReal,SUM(ShippriceDollar) as ShippriceDollar,SUM(ShippriceReal) as ShippriceReal,SUM(AlreadypaidDollar) as AlreadypaidDollar,SUM(AlreadypaidReal) as AlreadypaidReal from invoice_summary_v_total where cast(date as date)>= cast(getdate() as date)", conx);            adp2.Fill(ds2);            ReportViewer reportViewer = new ReportViewer();            reportViewer.ProcessingMode = ProcessingMode.Local;            reportViewer.SizeToReportContent = true;            reportViewer.ShowPrintButton = true;            reportViewer.Width = Unit.Percentage(100);            reportViewer.Height = Unit.Percentage(100);
            //ReportParameter qrCODE = new ReportParameter("qrCODE", base64String);
            //reportViewer.LocalReport.EnableExternalImages = true;
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\Invoice_summaryRpt.rdlc";
            //reportViewer.LocalReport.SetParameters(new ReportParameter[] { staffname, from, to, khmerDate, khmerYear, qrCODE });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", ds2));            ViewBag.ReportViewer = reportViewer;            return View("_invoice_summaryrpt");

        }


        [Route("invoice-summary-by-date/{fromdate}/{todate}")]        public ActionResult GetInvoiceSummaryByDate(string fromdate, string todate)        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;            SqlConnection conx = new SqlConnection(connectionString);            SqlDataAdapter adp = new SqlDataAdapter("select * from invoice_summary_v where  cast(date as date)>= cast(@from as Date) and cast(date as date)<=cast(@to as Date)", conx);
            adp.SelectCommand.Parameters.AddWithValue("@from", fromdate);
            adp.SelectCommand.Parameters.AddWithValue("@to", todate);
            adp.Fill(ds);            DataTable ds2 = new DataTable();            SqlDataAdapter adp2 = new SqlDataAdapter("select SUM(AmountDollar) as AmountDollar,SUM(AmountReal) as AmountReal,SUM(CarPriceDollar) as CarPriceDollar,SUM(CarpriceReal) as CarpriceReal,SUM(ShippriceDollar) as ShippriceDollar,SUM(ShippriceReal) as ShippriceReal,SUM(AlreadypaidDollar) as AlreadypaidDollar,SUM(AlreadypaidReal) as AlreadypaidReal from invoice_summary_v_total where  cast(date as date)>= cast(@from as Date) and cast(date as date)<=cast(@to as Date)", conx);
            adp2.SelectCommand.Parameters.AddWithValue("@from", fromdate);
            adp2.SelectCommand.Parameters.AddWithValue("@to", todate);
            adp2.Fill(ds2);            ReportViewer reportViewer = new ReportViewer();            reportViewer.ProcessingMode = ProcessingMode.Local;            reportViewer.SizeToReportContent = true;            reportViewer.ShowPrintButton = true;            reportViewer.Width = Unit.Percentage(100);            reportViewer.Height = Unit.Percentage(100);
            ReportParameter staff = new ReportParameter("user", User.Identity.Name);
            ReportParameter fromd = new ReportParameter("fromdate", fromdate);
            ReportParameter tod = new ReportParameter("todate", todate);
            //reportViewer.LocalReport.EnableExternalImages = true;
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\Invoice_summaryRpt.rdlc";
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { staff, fromd, tod });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", ds2));            ViewBag.ReportViewer = reportViewer;            return View("_invoice_summaryrpt");

        }

        [Route("invoice-detail-list")]        public ActionResult GetInvoiceDetail()        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;            SqlConnection conx = new SqlConnection(connectionString);            SqlDataAdapter adp = new SqlDataAdapter("select * from invoice_v where status = 1 AND cast(date as date)>= cast(getdate() as date)", conx);

            adp.Fill(ds);            ReportViewer reportViewer = new ReportViewer();            reportViewer.ProcessingMode = ProcessingMode.Local;            reportViewer.SizeToReportContent = true;            reportViewer.ShowPrintButton = true;            reportViewer.Width = Unit.Percentage(100);            reportViewer.Height = Unit.Percentage(100);
            //ReportParameter qrCODE = new ReportParameter("qrCODE", base64String);
            //reportViewer.LocalReport.EnableExternalImages = true;
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\Invoice_detailRpt.rdlc";
            //reportViewer.LocalReport.SetParameters(new ReportParameter[] { staffname, from, to, khmerDate, khmerYear, qrCODE });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));            ViewBag.ReportViewer = reportViewer;            return View("_invoice_detailrpt");

        }


        [Route("invoice-detail-by-date/{fromdate}/{todate}")]        public ActionResult GetInvoiceDetailByDate(string fromdate, string todate)        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;            SqlConnection conx = new SqlConnection(connectionString);            SqlDataAdapter adp = new SqlDataAdapter("select * from invoice_v where status = 1 AND cast(date as date)>= cast(@from as Date) and cast(date as date)<=cast(@to as Date) ", conx);
            adp.SelectCommand.Parameters.AddWithValue("@from", fromdate);
            adp.SelectCommand.Parameters.AddWithValue("@to", todate);
            adp.Fill(ds);            ReportViewer reportViewer = new ReportViewer();            reportViewer.ProcessingMode = ProcessingMode.Local;            reportViewer.SizeToReportContent = true;            reportViewer.ShowPrintButton = true;            reportViewer.Width = Unit.Percentage(100);            reportViewer.Height = Unit.Percentage(100);
            ReportParameter staff = new ReportParameter("user", User.Identity.Name);
            ReportParameter fromd = new ReportParameter("fromdate", fromdate);
            ReportParameter tod = new ReportParameter("todate", todate);
            //reportViewer.LocalReport.EnableExternalImages = true;
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\Invoice_detailRpt.rdlc";
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { staff, fromd, tod });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));            ViewBag.ReportViewer = reportViewer;            return View("_invoice_detailrpt");

        }

        [Route("invoice-return-detail-list")]        public ActionResult GetInvoiceDetailReturn()        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;            SqlConnection conx = new SqlConnection(connectionString);            SqlDataAdapter adp = new SqlDataAdapter("select * from invoice_v where status = 1 AND cast(date as date)>= cast(getdate() as date) AND deliverytype=N'ត្រឡប់'", conx);

            adp.Fill(ds);            ReportViewer reportViewer = new ReportViewer();            reportViewer.ProcessingMode = ProcessingMode.Local;            reportViewer.SizeToReportContent = true;            reportViewer.ShowPrintButton = true;            reportViewer.Width = Unit.Percentage(100);            reportViewer.Height = Unit.Percentage(100);
            //ReportParameter qrCODE = new ReportParameter("qrCODE", base64String);
            //reportViewer.LocalReport.EnableExternalImages = true;
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\Invoice_return_detailRpt.rdlc";
            //reportViewer.LocalReport.SetParameters(new ReportParameter[] { staffname, from, to, khmerDate, khmerYear, qrCODE });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));            ViewBag.ReportViewer = reportViewer;            return View("_invoice_return_detailrpt");

        }


        [Route("invoice-return-detail-by-date/{fromdate}/{todate}")]        public ActionResult GetInvoiceReturnDetailByDate(string fromdate, string todate)        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;            SqlConnection conx = new SqlConnection(connectionString);            SqlDataAdapter adp = new SqlDataAdapter("select * from invoice_v where status = 1 AND cast(date as date)>= cast(@from as Date) and cast(date as date)<=cast(@to as Date)  AND deliverytype=N'ត្រឡប់'", conx);
            adp.SelectCommand.Parameters.AddWithValue("@from", fromdate);
            adp.SelectCommand.Parameters.AddWithValue("@to", todate);
            adp.Fill(ds);            ReportViewer reportViewer = new ReportViewer();            reportViewer.ProcessingMode = ProcessingMode.Local;            reportViewer.SizeToReportContent = true;            reportViewer.ShowPrintButton = true;            reportViewer.Width = Unit.Percentage(100);            reportViewer.Height = Unit.Percentage(100);
            ReportParameter staff = new ReportParameter("user", User.Identity.Name);
            ReportParameter fromd = new ReportParameter("fromdate", fromdate);
            ReportParameter tod = new ReportParameter("todate", todate);
            //reportViewer.LocalReport.EnableExternalImages = true;
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\Invoice_return_detailRpt.rdlc";
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { staff, fromd, tod });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));            ViewBag.ReportViewer = reportViewer;            return View("_invoice_return_detailrpt");

        }


        [Route("collectmoney-invoice/{invoiceid}")]        [System.Web.Mvc.HttpGet]        public ActionResult GetCollectMoneyInvoice(string invoiceid)        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;            SqlConnection con = new SqlConnection(connectionString);            SqlDataAdapter adp = new SqlDataAdapter("Select * From CollectMoney_V where id=" + invoiceid + " and status=1", con);            adp.Fill(ds);
                        ReportViewer reportViewer = new ReportViewer();            reportViewer.ProcessingMode = ProcessingMode.Local;            reportViewer.SizeToReportContent = true;            reportViewer.Width = Unit.Percentage(100);            reportViewer.Height = Unit.Percentage(100);
            //ReportParameter staff = new ReportParameter("staff", User.Identity.Name);
            //reportViewer.LocalReport.EnableExternalImages = true;
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\CollectMoneyRpt.rdlc";
            //reportViewer.LocalReport.SetParameters(new ReportParameter[] { staff });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));            ViewBag.ReportViewer = reportViewer;            return View("_collectinvoicerpt");
        }

        [Route("invoice-money-list")]        public ActionResult GetCollectMoney()        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;            SqlConnection conx = new SqlConnection(connectionString);            SqlDataAdapter adp = new SqlDataAdapter("select * from CollectMoney_V where status = 1 AND cast(date as date)>= cast(getdate() as date)", conx);

            adp.Fill(ds);            ReportViewer reportViewer = new ReportViewer();            reportViewer.ProcessingMode = ProcessingMode.Local;            reportViewer.SizeToReportContent = true;            reportViewer.ShowPrintButton = true;            reportViewer.Width = Unit.Percentage(100);            reportViewer.Height = Unit.Percentage(100);
            //ReportParameter qrCODE = new ReportParameter("qrCODE", base64String);
            //reportViewer.LocalReport.EnableExternalImages = true;
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\Collect_Money_summaryRpt.rdlc";
            //reportViewer.LocalReport.SetParameters(new ReportParameter[] { staffname, from, to, khmerDate, khmerYear, qrCODE });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));            ViewBag.ReportViewer = reportViewer;            return View("_collect_money_rpt");

        }

        [Route("money-detail-by-date/{fromdate}/{todate}")]        public ActionResult GetCollectMoneyByDate(string fromdate, string todate)        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;            SqlConnection conx = new SqlConnection(connectionString);            SqlDataAdapter adp = new SqlDataAdapter("select * from CollectMoney_V where status = 1 AND cast(date as date)>= cast(@from as Date) and cast(date as date)<=cast(@to as Date) ", conx);
            adp.SelectCommand.Parameters.AddWithValue("@from", fromdate);
            adp.SelectCommand.Parameters.AddWithValue("@to", todate);
            adp.Fill(ds);            ReportViewer reportViewer = new ReportViewer();            reportViewer.ProcessingMode = ProcessingMode.Local;            reportViewer.SizeToReportContent = true;            reportViewer.ShowPrintButton = true;            reportViewer.Width = Unit.Percentage(100);            reportViewer.Height = Unit.Percentage(100);
            ReportParameter staff = new ReportParameter("user", User.Identity.Name);
            ReportParameter fromd = new ReportParameter("fromdate", fromdate);
            ReportParameter tod = new ReportParameter("todate", todate);
            //reportViewer.LocalReport.EnableExternalImages = true;
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\Collect_Money_summaryRpt.rdlc";
            reportViewer.LocalReport.SetParameters(new ReportParameter[] { staff, fromd, tod });
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds));            ViewBag.ReportViewer = reportViewer;            return View("_collect_money_rpt");

        }
    }
}
