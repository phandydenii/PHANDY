
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
            url: "/api/invoice_v?a=0",
            dataSrc: ""

        },
        columns:
            [
                
                {
                    data: "invoiceno"
                },
                {
                    data: "guestname"
                },
                {
                    data: "checkindate",
                    
                    render: function (data) {
                        return moment(new Date(data)).format('DD-MMM-YYYY');
                    }
                },
                {
                    data: "roomno"
                },
                {
                    data: "roomtypename",
                },
                {
                    data: "servicecharge",
                },
                {
                    data: "roomprice",
                },
                {
                    data: "invoicedate",
                    render: function (data) {
                        //var result1 = data.addMonths(1);
                        //var newDate = new Date(date.setMonth(date.getMonth() + 1));
                        return moment(new Date(data)).format('DD-MMM-YYYY');
                    }
                },
                {
                    data: "id",
                    render: function (data) {
                        return "<button OnClick='PrintInvoice (" + data + ")' class='btn btn-info btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-list-alt'></span> New Invoice</button>"
                            + "<button OnClick='Edit (" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-edit'></span> Edit</button>"
                            + "<button OnClick='CheckOut (" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-log-out'></span> Check Out</button>"

                        ;
                    }
                }
            ],
        destroy: true,
        //"order": [[0, "desc"]],
        //"info": false

    });
}

function PrintInvoice(id) {
    $.ajax({
        url: "/api/invoice_v/0/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            $('#ivnno').val(result.invoiceno);
            $('#name').val(result.guestname);
            $("#roomno").val(result.roomno);
            $("#rmprice").val(result.roomprice);
            $("#svprice").val(result.servicecharge);
            if (result.pcurrentrecord == 0) {
                $("#recordpowerold").val(result.pprerecord);
            } else {
                $("#recordpowerold").val(result.pcurrentrecord);
            }
            if (result.wcurrentrecord == 0) {
                $("#recordwaterold").val(result.wprerecord);
            } else {
                $("#recordwaterold").val(result.wcurrentrecord);
            }
            
            
            $("#powerid").val(result.pid);
            $("#waterid").val(result.wid);
            $("#invid").val(result.id);

        },
        error: function (errormessage) {
            toastr.error("No Record Select!", "Service Response");
        }
    });

    $("#PrintNewInvoiceModal").modal("show");
    $.get("/api/invoicemaxid", function (data) {
        $('#invoiceno').val(data);
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

