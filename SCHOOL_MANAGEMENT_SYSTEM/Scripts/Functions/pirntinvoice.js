
$(document).ready(function () {
    $('#PrintNewInvoiceModal').on('show.bs.modal', function () {
        //$('#odlrecordwater').focus();
        TotalPayment();
    });
    GetCheckInDetail();

})

var tbCheckInDetail = [];
function GetCheckInDetail() {
    tbCheckInDetail = $('#tablePrintInvoice').dataTable({
        ajax: {
            url: "/api/invoice-v/newinvoie",
            dataSrc: ""

        },
        columns:
            [
                {
                    data: "checkinid"
                },
                {
                    data: "name"
                },
                {
                    data: "checkindate",
                    render: function (data) {
                        return moment(new Date(data)).format('DD-MMM-YYYY');
                    }
                },
                {
                    data: "room_no"
                },
                {
                    data: "roomtypename",
                },
                {
                    data: "servicecharge",
                },
                {
                    data: "price",
                },
                {
                    data: "woldrecord",
                },
                {
                    data: "poldrecord",
                },
                {
                    data: "startdate",
                    render: function (data) {
                        return moment(new Date(data)).format('DD-MMM-YYYY');
                    }
                },
                {
                    data: "enddate",
                    render: function (data) {
                        return moment(new Date(data)).format('DD-MMM-YYYY');
                    }
                },
                {
                    data: "checkinid",
                    render: function (data, type, row) {
                        var d = row.day - 30;
                        var lateday;
                        if(d==0){
                            lateday = "";
                        } else {
                            lateday = "Late "+d+" days";
                        }
                        return "<span class='label label-danger' style='margin-right:5px'>" + lateday + "</span>"
                              + "<button OnClick='OnPrintInvoice (" + data +"," + data +")' class='btn btn-warning btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-list-alt'></span> Print Invoice</button>"
                        ;
                    }
                }
            ],
        destroy: true,
    });
}

function OnPrintInvoice(checkinid,guestid) {
    //alert(id+" "+checkinid)
    $("#PrintNewInvoiceModal").modal("show");
    //$("#invid").val(id);
    $("#checkinid").val(checkinid);
    $.ajax({
        url: "/api/invoice-v/newinvoie/" + guestid,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            $('#name').val(result[0]["name"]);
            $("#roomno").val(result[0]["room_no"]);
            $("#roomprice").val(result[0]["price"]);
            $("#svprice").val(result[0]["servicecharge"]);
            $("#recordpowerold").val(result[0]["woldrecord"]);
            $("#recordwaterold").val(result[0]["poldrecord"]);
            var checkindid = result[0]["checkinid"];
            var stdate = moment(result[0]["startdate"]).format("YYYY-MM-DD");
            var enddate = moment(result[0]["enddate"]).format("YYYY-MM-DD");
            $("#startdate").val(stdate);
            $("#enddate").val(enddate);

            $("#guestid").val(result[0]["guestid"]);
            $("#ispaid").val(result[0]["paid"]);
            $("#isprint").val(result[0]["printed"]);

            if (result[0]["printed"] == false) {
                $('#invno').val(result[0]["invoiceno"]);
            } else {
                $.get("/api/invoicemaxid", function (data) {
                    $('#invno').val(data);
                });
            }
            GetPayDemage(checkindid, stdate, enddate);


        },
        error: function (errormessage) {
            toastr.error("No Record Select!", "Service Response");
        }
    });

    $.get("/api/ExchangeRates/1/2", function (data) {
        $('#exrate').val(data.rate);
    });

    $.get("/api/WaterPoserPrice/1/1", function (data) {
        $('#wprice').val(data.waterprice);
        $('#pprice').val(data.powerprice);
    });
}
var tablepaydemage=[];
function GetPayDemage(id, fromdate, todate) {
    //alert(fromdate + " " + todate);
    tablepaydemage=$('#tblPayDemage').DataTable({
        ajax: {
            url: "/api/paydemages/" + id + "/" + fromdate + "/" + todate,
            dataSrc: ""
        },
        columns: [
            {
                data: "item.itemname",
            },
            {
                data: "item.price",
            }
        ],

        destroy: true,
        "paging": false,
        "ordering": false,
        "info": false,
        "searching": false
    });
}


function getwater(id) {
    $.ajax({
        url: "/api/waterusagerecord/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            $('#oldrecordwater').val(result.prerecord);
            $('#waterid').val(result.id);
        },
        error: function (errormessage) {
            toastr.error("No Record Select!", "Service Response");
        }
    });
}

function getpower(id) {
    $.ajax({
        url: "/api/powerusagerecord/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            $('#oldrecordpower').val(result.prerecord);
            $('#powerid').val(result.id);
        },
        error: function (errormessage) {
            toastr.error("No Record Select!", "Service Response");
        }
    });
}

function CheckInEdit(id) {
    //alert(id);
    $.ajax({
        url: "/api/checkin_v/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            alert(result.id);
        },
        error: function (errormessage) {
            toastr.error("No Record Select!", "Service Response");
        }
    });
}


function InvoiceNewPrint() {
    $.get("/api/invoice_v/" + $("#invid").val(), function (data) {
        if (data.isprint == false) {
            //alert('false');
            PrintUpdateInvoice();
        } else {
            //alert('true');
            InsertNewInvoice();
        }
    });

}
function PrintUpdateInvoice() {
    var data = new FormData();
    data.append("grandtotal", $("#grandtotal").val());
    data.append("totalriel", $("#grandtotalkh").val());
    data.append("totaldollar", $("#grandtotal").val());
    data.append("totalother", $("#svprice").val());
    data.append("note", $("#note").val());
    data.append("payriel", 0);
    data.append("paydollar", 0);
    data.append("paid", false);
    $.ajax({
        type: "PUT",
        url: "/api/invoices/" + $("#invid").val(),
        contentType: false,
        processData: false,
        data: data,
        success: function (result) {
            UpdateWaterPrint();
            UpdatePowerPrint();
            toastr.success("Print Invoice successfully!.", "Server Response");
            window.location.reload(true);
        },
        error: function (errormesage) {
            toastr.error("Print invoice faild...!", "Server Respond");

        }
    });
}
function UpdateWaterPrint() {
    $.ajax({
        type: "PUT",
        url: "/api/updatewaterusage/" + $("#checkinid").val() + "/" + $('#recordwaternew').val(),
        contentType: false,
        processData: false,
        success: function (result) {
            //toastr.success("Update water payment has been Update successfully.", "Server Response");
        },
        error: function (error) {
            toastr.error("Update water fail!", "Server Response");
        }
    });
}
function UpdatePowerPrint() {
    $.ajax({
        type: "PUT",
        url: "/api/updateelectrics/" + $("#checkinid").val() + "/" + $('#recordpowernew').val(),
        contentType: false,
        processData: false,
        success: function (result) {
            //toastr.success("Room Status has been Update successfully.", "Server Response");
        },
        error: function (error) {
            toastr.error("Update room status fail!", "Server Response");
        }
    });
}

function InsertNewInvoice() {
    var data = {
        invoiceno: $('#invno').val(),
        guestid: $('#guestid').val(),
        checkinid: $('#checkinid').val(),
        grandtotal: $('#grandtotal').val(),
        totalriel: $('#grandtotalkh').val(),
        totaldollar: $('#grandtotal').val(),
        totalother: $('#svprice').val(),
        note: $('#note').val(),
        printed: true,
    };
    $.ajax({
        url: "/api/invoices",
        data: JSON.stringify(data),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $("#PrintNewInvoiceModal").modal("hide");
            InsertPowerUsage(result);
            InsertWaterUsage(result);
            CreateInvoiceDetail(result);
            alert('Print Invoice successfully!');
            window.location.reload(true);

        },
        error: function (errormesage) {
            toastr.error("Create invoice faild...!" + errormesage, "Server Respond");
        }
    });
}

function OnTest() {
    CreateInvoiceDetail(1);
}
function CreateInvoiceDetail(id) {
    var data = new FormData();
    data.append("invoiceid", id);
    data.append("paydollar", '0');
    data.append("payriel", '0');

    data.append("checkinid", $('#checkinid').val());
    data.append("fromdate", $('#startdate').val());
    data.append("todate", $('#enddate').val());

    $.ajax({
        type: "POST",
        url: "/api/invoicedetails",
        contentType: false,
        processData: false,
        data: data,
        success: function (result) {
            window.location.reload(true);
        },
        error: function (errormesage) {
            toastr.error("Water usage insert faild!", "Server Respond");
            return false;
        }

    });
}






function InsertPowerUsage(id) {
    var data = {
        predate: $('#startdate').val(),
        prerecord: $('#recordpowerold').val(),
        currentdate: $('#enddate').val(),
        currentrecord: $('#recordpowernew').val(),
        invoiceid: id,
    };
    $.ajax({
        url: "/api/powerusage",
        data: JSON.stringify(data),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            //window.location.reload(true);
        },
        error: function (errormesage) {
            toastr.error("Power usage insert faild!", "Server Respond");
            return false;
        }
    });
}
function InsertWaterUsage(id) {
    var data = {
        predate: $('#startdate').val(),
        prerecord: $('#recordwaterold').val(),
        currentdate: $('#enddate').val(),
        currentrecord: $('#recordwaternew').val(),
        invoiceid: id,
    };
    $.ajax({
        url: "/api/waterusage",
        data: JSON.stringify(data),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            //window.location.reload(true);
        },
        error: function (errormesage) {
            toastr.error("Water usage insert faild!", "Server Respond");
            return false;
        }

    });
}  
function RecordWaterChange() {
    var waternewrecord = document.getElementById('recordwaternew').value;
    var wateroldrecord = document.getElementById('recordwaterold').value;
    var totalwaterusage = ((parseFloat(waternewrecord) - parseFloat(wateroldrecord)) * parseFloat($('#wprice').val())) / parseFloat($('#exrate').val());
    if (totalwaterusage <= 0) {
        alert('Faild!');
        $('#recordwaternew').focus();
    } else {
        $('#waterusagetotal').val(totalwaterusage.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'));
        TotalPayment();
    }

}
function RecordPowerChange() {
    var powernewrecord = document.getElementById('recordpowernew').value;
    var powerroldrecord = document.getElementById('recordpowerold').value;
    var totalwaterusage = ((parseInt(powernewrecord) - parseInt(powerroldrecord)) * parseFloat($('#pprice').val())) / parseFloat($('#exrate').val());
    if (totalwaterusage <= 0) {
        alert('Faild!');
        $('#recordwaternew').focus();
    } else {
        $('#powerusagetotal').val(totalwaterusage.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'));
        TotalPayment();
    }


}
function TotalPayment() {
    var watertotal = document.getElementById('waterusagetotal').value;
    var powertotal = document.getElementById('powerusagetotal').value;
    var roomprice = document.getElementById('roomprice').value;
    var servicecharge = document.getElementById('svprice').value;
    var table = document.getElementById("tblPayDemage"),
    sumVal = 0;
    for (var i = 1; i < table.rows.length; i++) {
        sumVal = sumVal + parseFloat(table.rows[i].cells[1].innerHTML);
    }
    var grandtotal = parseFloat(watertotal) + parseFloat(powertotal) + parseFloat(roomprice) + parseFloat(servicecharge) + parseFloat(sumVal);
    $('#grandtotal').val(grandtotal.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'));
    $('#grandtotalkh').val((grandtotal * parseFloat($('#exrate').val())).toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'));


   // alert("Water ="+watertotal+" Power ="+powertotal+" SV ="+servicecharge+" RP="+roomprice + " Item="+sumVal);
}
function OnCloseNewInv() {
    window.location.reload(true);
}




























function ChangeItem() {
    
    var id = $('select[name=item]').val()
    var name = $("#item option:selected").text();
    var tBody = $("#tabletest > TBODY")[0];
    var row = tBody.insertRow(-1);
    var cell = $(row.insertCell(-1));

    cell.html(name);
    cell = $(row.insertCell(-1));

    cell.html(id);
    cell = $(row.insertCell(-1));

    var btnRemove = $("<input />");
    btnRemove.attr("type", "button");
    btnRemove.attr("class", "btn btn-danger btn-xs");
    btnRemove.attr("onclick", "Remove(this);");
    btnRemove.val("X");
    cell.append(btnRemove);
    
    const itemList=[];
    var table = document.getElementById("tabletest"),
    sumVal = 0;
    for (var i = 1; i < table.rows.length; i++) {
        sumVal = sumVal + parseFloat(table.rows[i].cells[1].innerHTML);
    }

    
    $('#itemcharge').val(sumVal);
    TotalPayment();
};


function Remove(button) {
    //Determine the reference of the Row using the Button.
    var row = $(button).closest("TR");
    var name = $("TD", row).eq(0).html();
    //Get the reference of the Table.
    var table = $("#tabletest")[0];

    //Delete the Table row using it's Index.
    table.deleteRow(row[0].rowIndex);

    var table = document.getElementById("tabletest"),
    sumVal = 0;
    for (var i = 1; i < table.rows.length; i++) {
        sumVal = sumVal + parseFloat(table.rows[i].cells[1].innerHTML);
    }
    $('#itemcharge').val(sumVal)
    TotalPayment();
};

//=========


//[HttpPost]
//public IHttpActionResult CreateShipperHistory(ShipHistoryReq shipHistoryReq)
//{
//    var newtod = new ShipperHistoryDto()
//    {
//        shipperid = shipHistoryReq.shipperid,
//        totalbox = shipHistoryReq.totalbox,
//        totalservice = shipHistoryReq.totalservice,
//        totalprice = shipHistoryReq.totalprice,
//    };

//    var department = Mapper.Map<ShipperHistoryDto, ShipHistory>(newtod);
//    department.date = DateTime.Today;
//    _context.ShipHistories.Add(department);
//    _context.SaveChanges();

//    DataTable ds = new DataTable();
//    var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
//    SqlConnection conx = new SqlConnection(connectionString);
//    SqlDataAdapter adp = new SqlDataAdapter("select CASE WHEN max(id) IS NULL THEN '1' ELSE max(id) END from shiphistory_tbl ", conx);
//    adp.Fill(ds);
//    string InvNo = ds.Rows[0][0].ToString();

//    var resp = new HttpResponseMessage();
//    foreach (var i in shipHistoryReq.list_id)
//    {
//        var resul1 = _context.InvoiceDetail.FirstOrDefault(c => c.id == i);
//        var newzhid = new HistoryDetail()
//        {
//            invoicedetailid = resul1.id,                    
//            shiphistoryid = int.Parse(InvNo)
//        };
//        _context.HistoryDetails.Add(newzhid);
//        _context.SaveChanges();


//    }            

//return Ok(InvNo);

//}