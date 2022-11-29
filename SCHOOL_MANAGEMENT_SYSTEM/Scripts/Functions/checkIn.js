
$(document).ready(function () {
    $('#PrintNewInvoiceModal').on('show.bs.modal', function () {
        $('#odlrecordwater').focus();
    });
    GetCheckInDetail();
})

var tbCheckInDetail = [];
function GetCheckInDetail() {
    tbCheckInDetail = $('#tableCheckInDetail').dataTable({
        ajax: {
            url: "/api/checkin_v",
            dataSrc: ""   
        },
        columns:
            [
                {
                    data: "id"
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
                    data: "floorno"
                },
                {
                    data: "building",
                   
                },
                //{
                //    data: "id",
                //    render: function (data) {
                //            return "<button OnClick='OnPrintInvoice (" + data + ")' class='btn btn-primary btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-list-alt'></span> New Invoice</button>";                         
                //    }
                //},
                {
                    data: "id",
                    render: function (data) {
                        return "<button OnClick='CheckOut (" + data + ")' class='btn btn-success btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-log-out'></span> Check Out</button>"
                             + "<button OnClick='CheckInEdit (" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-edit'></span> Edit</button>"
                        ;
                    }
                }
            ],
        destroy: true,
    });
}

function CheckInEdit() {
    $.ajax({
        url: "/api/checkin_v/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            $('#guestname').val(result.name);
            $("#roomno").val(result.room_no);
            $("#roomprice").val(result.price);
            $("#roomid").val(result.roomid);
            $("#guestid").val(result.guestid);
            $("#servicecharge").val(result.servicecharge);
            $("#checkinid").val(result.id);

            $.get("/api/powerusagerecord/" + id, function (data) {
                $('#oldrecordpower').val(data);
            });
            $.get("/api/waterusagerecord/" + id, function (data) {
                $('#oldrecordwater').val(data);
            });
        },
        error: function (errormessage) {
            toastr.error("No Record Select!", "Service Response");
        }
    });
}

function OnPrintInvoice(id) {
    //alert(id);
    $.ajax({
        url: "/api/checkin_v/"+id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            $('#guestname').val(result.name);
            $("#roomno").val(result.room_no);
            $("#roomprice").val(result.price);
            $("#roomid").val(result.roomid); 
            $("#guestid").val(result.guestid);
            $("#servicecharge").val(result.servicecharge);
            $("#checkinid").val(result.id);

            $.get("/api/powerusagerecord/"+id, function (data) {
                $('#oldrecordpower').val(data);
            });
            $.get("/api/waterusagerecord/" + id, function (data) {
                $('#oldrecordwater').val(data);
            });
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


