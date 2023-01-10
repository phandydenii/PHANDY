
$(document).ready(function () {
    $(document).ajaxStart(function () {
        $('#loadingGif').addClass('show');
    }).ajaxStop(function () {
        $('#loadingGif').removeClass('show');
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
                    data: "guestid"
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
                //{
                //    data: "wstartrecord",
                //},
                //{
                //    data: "estartrecord",
                //},
                //{
                //    data: "startdate",
                //    render: function (data) {
                //        return moment(new Date(data)).format('DD-MMM-YYYY');
                //    }
                //},
                {
                    data: "enddate",
                    render: function (data) {
                        return moment(new Date(data)).format('DD-MMM-YYYY');
                    }
                },
                {
                    data: "checkinid",
                    render: function (data, type, row) {
                        
                        if (row.day <= 35 && row.day >= 28) {
                            return "<button OnClick='OnPrintInvoice (" + data + "," + row.guestid + ")' class='btn btn-warning btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-list-alt'></span> Print Invoice</button>"
                            ;
                            
                        } else {
                            return "<span class='text-primary'><span class='glyphicon glyphicon-ok'></span></span>";
                        }
                        
                    }
                }
            ],
        destroy: true,
    });
}

function OnPrintInvoice(checkinid, guestid) {
    //alert('Hello');
    $("#PrintNewInvoiceModal").modal("show");
    $("#checkinid").val(checkinid);
    $.ajax({
        url: "/api/invoice-v/newinvoie/" + guestid,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            $("#invid").val(result[0]["invoiceid"]);
            $('#name').val(result[0]["name"]);
            $('#roomid').val(result[0]["roomid"]);
            $("#roomno").val(result[0]["room_no"]);
            $("#rmprice").val(result[0]["price"]);
            $("#svprice").val(result[0]["servicecharge"]);
            $("#recordpowerold").val(result[0]["estartrecord"]);
            $("#recordwaterold").val(result[0]["wstartrecord"]);
            $("#weid").val(result[0]["weid"]);
            var checkindid = result[0]["checkinid"];
            var stdate = moment(result[0]["startdate"]).format("YYYY-MM-DD");
            var enddate = moment(result[0]["enddate"]).format("YYYY-MM-DD");
            $("#stdate").val(stdate);
            $("#eddate").val(enddate);
            $("#guestid").val(result[0]["guestid"]);
            $("#action").val(result[0]["action"]);

            //$("#ispaid").val(result[0]["paid"]);
            //$("#isprint").val(result[0]["printed"]);           
            //var invoiceno = "RL" + ("000000" + result[0]["invoiceid"]).slice(-6)

            //if (result[0]["action"] == false) {
            //    $('#invno').val(invoiceno);
            //} else {
            //    $.get("/api/invoicemaxid", function (data) {
            //        $('#invno').val(data);
            //    });
            //}
            $.get("/api/invoicemaxid", function (data) {
                var invoiceno = "RL" + ("000000" + data).slice(-6)
                $('#invno').val(invoiceno);
            });
            GetPayDemage(result[0]["guestid"], stdate, enddate);

            $.get("/api/ExchangeRates/1/2", function (data) {
                $('#exrate').val(data.rate);
            });

            $.get("/api/WEPrice/1/1", function (data) {
                $('#wprice').val(data.waterprice);
                $('#pprice').val(data.electricprice);
            });
        },
        error: function (errormessage) {
            toastr.error("No Record Select!", "Service Response");
        }
    });  
}
var tablepaydemage=[];
function GetPayDemage(id, fromdate, todate) {
    $.ajax({
        url: "/api/paydemages/" + id + "/" + fromdate + "/" + todate,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            var valsum = 0;
            $.each(result, function (key, value) {
                valsum += parseFloat(value.price);
                $('#PayDemageList').append("<li class='list-group-item'>" + value.item.itemname + "<span class='badge'>$ " + value.price + "</span></li>");
            });

            if (valsum != 0) {
                $('#paydemage').text("សម្ភារៈខូចខាត ​ ");
                $('#total').text("Total =");
                $('#itemprice').text(valsum);
            } else {
                $('#lblPayDemage').hidden = true;
            }
            
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
           // alert(result.id);
        },
        error: function (errormessage) {
            toastr.error("No Record Select!", "Service Response");
        }
    });
}


function InvoiceNewPrint() {
    if ($("#action").val() == "Update") {
        UpdateWaterElectricUsage();
    } else if ($("#action").val() == "Insert") {
        InsertWaterElectricUsage();
    }
}

//Update WE
function UpdateWaterElectricUsage() {
    var data = new FormData();
    data.append("checkinid", $("#checkinid").val());
    data.append("startdate", $('#stdate').val());
    data.append("enddate", $('#eddate').val());
    data.append("wstartrecord", $('#recordwaterold').val());
    data.append("wendrecord", $('#recordwaternew').val());
    data.append("estartrecord", $('#recordpowerold').val());
    data.append("eendrecord", $('#recordpowernew').val());
    $.ajax({
        type: "PUT",
        url: "/api/waterelectricusages/" + $("#weid").val(),
        contentType: false,
        processData: false,
        data: data,
        success: function (result) {
            InsertNewInvoice();
            
        },
        error: function (errormesage) {
            toastr.error("Electric usage insert faild!", "Server Respond");
            return false;
        }
    });
}

//Insert WE
function InsertWaterElectricUsage() {
    //var data = {
    //    checkindid: $("#checkinid").val(),
    //    startdate: $("#stdate").val(),
    //    enddate: $("#eddate").val(),
    //    wstartrecord: $("#recordwaterold").val(),
    //    wendrecord: $("#recordwaternew").val(),
    //    estartrecord: $("#recordpowerold").val(),
    //    eendrecord: $("#recordpowernew").val(),
    //}
    var data = new FormData();
    data.append("checkinid", $("#checkinid").val());
    data.append("startdate", $('#stdate').val());
    data.append("enddate", $('#eddate').val());
    data.append("wstartrecord", $('#recordwaterold').val());
    data.append("wendrecord", $('#recordwaternew').val());
    data.append("estartrecord", $('#recordpowerold').val());
    data.append("eendrecord", $('#recordpowernew').val());
    $.ajax({
        url: "/api/waterelectricusages",
        type: "POST",
        contentType: false,
        processData: false,
        data: data,
        success: function (result) {
            InsertNewInvoice();
        },
        error: function (errormesage) {
            toastr.error("Electric usage insert faild!", "Server Respond");
            return false;
        }
    });
}



//Insert Invoice
function InsertNewInvoice() {
    var data = {
        roomid: $('#roomid').val(),
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
            UpdateCheckInStatus();
            CreateInvoiceDetail(result);
            window.location = "invoice-report/" + result;
        },
        error: function (errormesage) {
            toastr.error("Create invoice faild...!" + errormesage, "Server Respond");
        }
    });
}

//INsert Invoice Detail
function CreateInvoiceDetail(id) {
    var data = new FormData();
    data.append("invoiceid", id);
    data.append("paydollar", '0');
    data.append("payriel", '0');

    data.append("guestid", $('#guestid').val());
    data.append("fromdate", $('#startdate').val());
    data.append("todate", $('#enddate').val());

    $.ajax({
        type: "POST",
        url: "/api/invoicedetails",
        contentType: false,
        processData: false,
        data: data,
        success: function (result) {
            toastr.success("Print invoice successfully.", "Server Response");
            window.location.reload(true);
        },
        error: function (errormesage) {
            toastr.error("Invoice Detail Faild!", "Server Respond");
            return false;
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
            UpdateWaterElectricUsage();

            CreateInvoiceDetail($("#invid").val());
        },
        error: function (errormesage) {
            toastr.error("Print invoice faild...!", "Server Respond");

        }
    });
}
function UpdateCheckInStatus(id) {
    $.ajax({
        type: "PUT",
        url: "/api/updatecheckind/" + $("#checkinid").val(),
        contentType: false,
        processData: false,
        // data: status,
        success: function (result) {
            //toastr.success("Room Status has been Update successfully.", "Server Response");
        },
        error: function (error) {
            toastr.error("Update room status fail!", "Server Response");
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
   // alert(totalwaterusage);

}
function TotalPayment() {
    var watertotal = document.getElementById('waterusagetotal').value;
    var powertotal = document.getElementById('powerusagetotal').value;
    var roomprice = document.getElementById('rmprice').value;
    var servicecharge = document.getElementById('svprice').value;
    var sumval = document.getElementById("itemprice").innerText;
    var paydemage = 0;
    if (sumval == "") {
        paydemage = 0;
    } else {
        paydemage = sumval;
    }
    
    var grandtotal = parseFloat(watertotal) + parseFloat(powertotal) + parseFloat(roomprice) + parseFloat(servicecharge) + parseFloat(paydemage);
    $('#grandtotal').val(grandtotal.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'));
    $('#grandtotalkh').val((grandtotal * parseFloat($('#exrate').val())).toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'));

    //alert(sumval);
   //alert("Water ="+watertotal+" Power ="+powertotal+" SV ="+servicecharge+" RP="+roomprice + " Item="+sumVal);
}
function OnCloseNewInv() {
    window.location.reload(true);
}









