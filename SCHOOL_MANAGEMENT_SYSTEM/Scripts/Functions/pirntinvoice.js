
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
                        } else if (parseFloat(row.action) > 0) {
                            return "<button OnClick='OnPaymentNow (" + row.action + ")' class='btn btn-info btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-list-alt'></span> Payment Now</button>"
                                 + "<button OnClick='OnEdtInvoice (" + row.action + ")' class='btn btn-primary btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-edit'></span> Edit</button>"
                            ;                            
                        } 
                        else{
                            return "<span class='text-primary'><span class='glyphicon glyphicon-ok'></span></span>";
                        }                       
                    }
                }
            ],
        destroy: true,
    });
}

/////============OnPrintInvoice Button==========
function OnPrintInvoice(checkinid, guestid) {
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

            $.get("/api/invoicemaxid", function (data) {
                var invoiceno = "RL" + ("000000" + data).slice(-6)
                $('#invno').val(invoiceno);
            });
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


//==============Payent Now Button =========
function OnPaymentNow(id) {
    $("#PaymentModal").modal('show');
    $('#wendrecord').attr('readonly', 'readonly');
    $('#eendrecord').attr('readonly', 'readonly');
    $.ajax({
        url: "/api/invoice_v/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {

            $('#id').val(id);
            var invoiceno = "RL" + ("000000" + result.id).slice(-6)
            $('#invoiceno').val(invoiceno);
            $('#guestid').val(result.guestid);
            $("#guestname").val(result.guestname);
            $("#room").val(result.roomno);
            $('#roomprice').val(result.roomprice);
            var stdate = moment(result.startdate).format("YYYY-MM-DD");
            var enddate = moment(result.enddate).format("YYYY-MM-DD");
            $("#startdate").val(stdate);
            $("#enddate").val(enddate);
            $('#wstartrecord').val(result.wstartrecord);
            $('#wendrecord').val(result.wendrecord);
            var totalwater = result.wtotal;
            $('#totalwater').val(totalwater.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'));
            $('#estartrecord').val(result.estartrecord);
            $('#eendrecord').val(result.eendrecord);
            var totalelectric = result.etotal;
            $('#totalelectric').val(totalelectric.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'));

            $('#servicecharge').val(result.servicecharge);
            $('#totalpay').val(result.grandtotal.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'));
            $('#totalpaykh').val(result.totalriel.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'));
            $('#note').val(result.note);

            $('#checkinid').val(result.checkinid);

        },
        error: function (errormessage) {
            toastr.error("Load Record Error", "Service Response");
        }
    });

    $.get("/api/WEPrice/1/2", function (data) {
        $("#wpid").val(data.id);
        $("#wprice").text(data.waterprice);
        $("#pprice").text(data.powerprice);
    });


    $.get("/api/ExchangeRates/1/2", function (data) {
        $("#exrate").val(data.rate);
        $("#lblidroom").text(data.rate);
    });
}

//==============Edit Invoice Button ======
function OnEdtInvoice(id) {
    $("#PaymentModal").modal('show');
    document.getElementById('SavePaymentbtn').innerText = 'Update';
    $.ajax({
        url: "/api/invoice_v/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            $('#id').val(id);
            $('#weid').val(result.weid);
            var invoiceno = "RL" + ("000000" + result.id).slice(-6)
            $('#invoiceno').val(invoiceno);
            $('#guestid').val(result.guestid);
            $("#guestname").val(result.guestname);
            $("#room").val(result.roomno);
            $('#roomprice').val(result.roomprice);
            var stdate = moment(result.startdate).format("YYYY-MM-DD");
            var enddate = moment(result.enddate).format("YYYY-MM-DD");
            $("#startdate").val(stdate);
            $("#enddate").val(enddate);
            $('#wstartrecord').val(result.wstartrecord);
            $('#wendrecord').val(result.wendrecord);
            var totalwater = result.wtotal;
            $('#totalwater').val(totalwater.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'));
            $('#estartrecord').val(result.estartrecord);
            $('#eendrecord').val(result.eendrecord);
            var totalelectric = result.etotal;
            $('#totalelectric').val(totalelectric.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'));
            $('#servicecharge').val(result.servicecharge);
            $('#totalpay').val(result.grandtotal.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'));
            $('#totalpaykh').val(result.totalriel.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'));
            $('#note').val(result.note);
            $('#checkinid').val(result.checkinid);
        },
        error: function (errormessage) {
            toastr.error("Load Record Error", "Service Response");
        }
    });

    $.get("/api/WEPrice/1/2", function (data) {
        $("#wpid").val(data.id);
        $("#wprice").text(data.waterprice);
        $("#pprice").text(data.electricprice);
    });


    $.get("/api/ExchangeRates/1/2", function (data) {
        $("#exrate").val(data.rate);
        $("#lblidroom").text(data.rate);
    });
}


//==============Print Invoice Button In _PrintNewInvoice ====
function InvoiceNewPrint() {
    if ($("#action").val() == "Update") {
        UpdateWaterElectricUsage();
    } else if ($("#action").val() == "Insert") {
        InsertWaterElectricUsage();
    }
}

//===========Update WE Function======
function UpdateWaterElectricUsage() {
    var data = new FormData();
    data.append("startdate", $('#stdate').val());
    data.append("enddate", $('#eddate').val());
    data.append("wendrecord", $('#recordwaternew').val());
    data.append("eendrecord", $('#recordpowernew').val());
    $.ajax({
        type: "PUT",
        url: "/api/updateweendrecord/" + $("#weid").val(),
        contentType: false,
        processData: false,
        data: data,
        success: function (result) {
            InsertNewInvoice($("#weid").val());
        },
        error: function (errormesage) {
            toastr.error("Electric usage insert faild!", "Server Respond");
            return false;
        }
    });
}

//===========Insert WE FUnction========
function InsertWaterElectricUsage() {
    var data = new FormData();
    data.append("guestid", $("#guestid").val());
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
            InsertNewInvoice(result);
        },
        error: function (errormesage) {
            toastr.error("Electric usage insert faild!", "Server Respond");
            return false;
        }
    });
}

//Insert Invoice Function=======
function InsertNewInvoice(id) {
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
        weusageid: id,
    };
    $.ajax({
        url: "/api/invoices",
        data: JSON.stringify(data),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            UpdateCheckInStatus();
            window.location = "invoice-report/" + result;
        },
        error: function (errormesage) {
            toastr.error("Create invoice faild...!" + errormesage, "Server Respond");
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

///////For Print Invoice
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
    var grandtotal = parseFloat(watertotal) + parseFloat(powertotal) + parseFloat(roomprice) + parseFloat(servicecharge);
    $('#grandtotal').val(grandtotal.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'));
    $('#grandtotalkh').val((grandtotal * parseFloat($('#exrate').val())).toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'));

    //alert(sumval);
   //alert("Water ="+watertotal+" Power ="+powertotal+" SV ="+servicecharge+" RP="+roomprice + " Item="+sumVal);
}
function OnCloseNewInv() {
    window.location.reload(true);
}



