
$(document).ready(function () {
    $('#PrintNewInvoiceModal').on('show.bs.modal', function () {
        $('#odlrecordwater').focus();
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
                    render: function (data) {
                        return "<button OnClick='PrintInvoice (" + data + ")' class='btn btn-danger btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-list-alt'></span> Print Invoice</button>"

                        ;
                    }
                }
            ],
        destroy: true,
    });
}

function PrintInvoice(id) {
    $("#PrintNewInvoiceModal").modal("show");
    $("#invid").val(id);
    $("#checkinid").val(id);
    $.ajax({
        url: "/api/invoice-v/newinvoie/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            
            $('#name').val(result[0]["name"]);
            $("#roomno").val(result[0]["room_no"]);
            $("#rmprice").val(result[0]["price"]);
            $("#svprice").val(result[0]["servicecharge"]);
            $("#recordpowerold").val(result[0]["woldrecord"]);
            $("#recordwaterold").val(result[0]["poldrecord"]);
            
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

