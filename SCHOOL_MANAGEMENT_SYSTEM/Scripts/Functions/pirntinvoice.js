
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
            url: "/api/invoice_v",
            dataSrc: ""

        },
        columns:
            [
                {
                    data: "id"
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
                        return "<button OnClick='PrintInvoice (" + data + ")' class='btn btn-primary btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-list-alt'></span> New Invoice</button>";
                    }
                },
                {
                    data: "id",
                    render: function (data) {
                        return "<button OnClick='Edit (" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-edit'></span> Edit</button>"
                        + "<button OnClick='CheckOut (" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-log-out'></span> Check Out</button>"

                        ;
                        //return "<div class='btn-group'><a href='#' class='btn btn-primary btn-xs'><span class='glyphicon glyphicon-cog'></span> Action</a><a href='#' class='btn btn-primary btn-xs dropdown-toggle' data-toggle='dropdown' aria-expanded='false'><span class='caret'></span></a>"
                        //       + "<ul class='dropdown-menu'>"
                        //         + "<li>"
                        //            + "<button OnClick='Edit (" + data + ")' class='btn btn-success btn-xs' style='margin-top:0px'><span class='glyphicon glyphicon-edit'></span> Edit</button>"
                        //            + "<button OnClick='History (" + data + ")' class='btn btn-info btn-xs' style='margin-top:5px'><span class='glyphicon glyphicon-list-alt'></span> History</button>"
                        //            + "<button OnClick='CheckOut (" + data + ")' class='btn btn-warning btn-xs' style='margin-top:5px'><span class='glyphicon glyphicon-trash'></span> Check Out</button>"
                        //         + "</li>"
                        //       + "</ul>"
                        //  + "</div>"
                        //;
                    }
                }
            ],
        destroy: true,
        //"order": [[0, "desc"]],
        //"info": false

    });
}

function PrintInvoice(id) {

    //alert(id);
    $.ajax({
        url: "/api/invoice_v/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
           alert(result.id);
            $('#name').val(result.guestname);
            $("#roomno").val(result.roomno);
            $("#roomprice").val(result.roomprice);
            $("#servicecharge").val(result.servicecharge);
            $("#oldrecordpower").val(result.pprerecord);
            $("#oldrecordwater").val(result.wprerecord);
            $("#powerid").val(result.pid);
            $("#waterid").val(result.wid);


            //getwater(id);
            //getpower(id);
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

