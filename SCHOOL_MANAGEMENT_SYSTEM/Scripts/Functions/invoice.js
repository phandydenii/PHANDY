
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
                    data: "enddate",
                    render: function (data) {
                        const EndDate = new Date();
                        const StartDate = new Date(data);
                        const oneDay = 1000 * 60 * 60 * 24;
                        const start = Date.UTC(EndDate.getFullYear(), EndDate.getMonth(), EndDate.getDate());
                        const end = Date.UTC(StartDate.getFullYear(), StartDate.getMonth(), StartDate.getDate());
                        var result = (start - end) / oneDay;
                        if (result > 0) {
                            return moment(new Date(data)).format('DD-MMM-YYYY') + " <span class='label label-danger'>Late " + result + " day</span>"
                        } else {
                            return moment(new Date(data)).format('DD-MMM-YYYY');
                        }
                        
                    }
                },
                {
                    data: "checkinid",
                    render: function (data, type, row) {
                        const now = new Date(Date.now());
                        const now1 = new Date(data.enddate);
                        const counday = parseInt(now.getDate()) - parseInt(now1.getDate());

                        

                        if (parseFloat(row.action) > 0) {
                                return "<button OnClick='OnPaymentNow (" + row.action + ")' class='btn btn-info btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-list-alt'></span> Payment Now</button>"
                                    + "<button OnClick='OnEdtInvoice (" + row.action + ")' class='btn btn-primary btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-edit'></span> Edit</button>"
                                    + "<button onclick='GuestHistory(" + row.guestid + ")' class='btn btn-success btn-xs' style='border-width: 0px;margin-left:5px'><span class='glyphicon glyphicon-list-alt'></span> History</button>"
                                    ;
                            
                        } else if (row.action == '') {
                            return "<button onclick='GuestHistory(" + row.guestid + ")' class='btn btn-success btn-xs' style='border-width: 0px;margin-left:5px'><span class='glyphicon glyphicon-list-alt'></span> History</button>"
                                ;
                        } else {
                                return "<button OnClick='OnPrintInvoice (" + data + "," + row.guestid + ")' class='btn btn-warning btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-list-alt'></span> Print Invoice</button>"
                                    + "<button onclick='GuestHistory(" + row.guestid + ")' class='btn btn-success btn-xs' style='border-width: 0px;margin-left:5px'><span class='glyphicon glyphicon-list-alt'></span> History</button>"
                                    ;
                            
                        }
                    }
                }
            ],
        destroy: true,
    });
}

function GuestHistory(id) {
    $('#guestid').val(id);
    $('#HistoryModal').modal('show');
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

    $('#SavePaymentbtn').show();
    $('#UpdatePaymentbtn').hide();
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

            $('#paydollar').val(result.grandtotal.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'));

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
    $('#SavePaymentbtn').hide();
    $('#UpdatePaymentbtn').show();
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


