
$(document).ready(function () {
    $('#PrintNewInvoiceModal').on('show.bs.modal', function () {
        $('#odlrecordwater').focus();
    });
    $('#HistoryModal').on('show.bs.modal', function () {
        $('#rowhide').hide();
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
                    data: "roomtypename"
                },
                {
                    data: "payforroom",
                },
                {
                    data: "id",
                    render: function (data, type, row) {
                        return   "<button OnClick='CheckInEdit (" + data + ")' class='btn btn-warning btn-xs' style='margin-top:0px'><span class='glyphicon glyphicon-edit'></span> Edit</button>"
                            + "<button onclick='GuestHistory(" + row.guestid + ")' class='btn btn-success btn-xs' style='border-width: 0px;margin-left:5px'><span class='glyphicon glyphicon-list-alt'></span> History</button>"
                        ;    
                    }
                }
            ],
        
        destroy: true,
    });
}
var tableHistory = [];
function GuestHistory(id) {
    $('#HistoryModal').modal('show');
    $('#guestid').val(id);
    tableHistory = $('#tableHistory').dataTable({
        ajax: {
            url: "/api/invoice_v_by_guestid/" + id,
            dataSrc: ""
        },
        columns:
            [
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
                    data: "waterusage",
                    render: function (data) {
                        return data.toFixed(2);
                    }
                },
                {
                    data: "electricusage",
                    render: function (data) {
                        return data.toFixed(2);
                    }
                },
                {
                    data: "wtotal",
                    render: function (data) {
                        return data.toFixed(2);
                    }
                },
                {
                    data: "etotal",
                    render: function (data) {
                        return data.toFixed(2);
                    }
                },
                {
                    data: "grandtotal",
                    render: function (data) {
                        return data.toFixed(2);
                    }
                },
                {
                    data: "id",
                    render: function (data) {
                        return "<button OnClick='OnEditInvHistory (" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-edit'></span></button>"
                        + "<button OnClick='OnDeleteInvoiceHistory (" + data + ")' class='btn btn-danger btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-trash'></span></button>"
                        ;

                    }
                }
            ],
        destroy: true,
        "order": [[0, "asc"]],

    });
}
function OnEditInvHistory() {
    $('#rowhide').show();
}
function OnDeleteInvoiceHistory() {
    $('#rowhide').hide();
}

function CheckInEdit(id) {
    $("#CheckInModal").modal("show");
    $("#checkinid").val(id);
    $.ajax({
        url: "/api/checkin_v/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            //alert(result[0].wstartrecord);
            var checkindate = moment(result[0].checkindate).format("YYYY-MM-DD");
            var startdate = moment(result[0].checkindate).format("YYYY-MM-DD");
            var enddate = moment(result[0].checkindate).format("YYYY-MM-DD");
            $('#roomid').val(result[0].roomid);
            $('#checkindate').val(checkindate);
            $('#startdate').val(checkindate);
            $('#enddate').val(checkindate);
            if (result[0].active == 1) {
                $('#wrecord').attr('readonly', 'readonly');
                $('#precord').attr('readonly', 'readonly');
            }
            $('#guestid').val(result[0].guestid);
            $('#name').val(result[0].name);
            $('#namekh').val(result[0].namekh);
            $('#man').val(result[0].man);
            $('#women').val(result[0].women);
            $('#child').val(result[0].child);

            $('#weid').val(result[0].weid);
            $('#wrecord').val(result[0].wstartrecord);
            $('#precord').val(result[0].estartrecord);
            $('#prepaid').val(result[0].prepaid);
            $('#payforroom').val(result[0].payforroom);
        },
        error: function (errormessage) {
            toastr.error("No Record Select!", "Service Response");
        }
    });
}

function SaveCheckIn() {  
    var data = {
        id: $('#checkinid').val(),
        checkindate: $('#checkindate').val(),
        roomid: $('#roomid').val(),
        guestid: $('#guestid').val(),
        child: $('#child').val(),
        man: $('#man').val(),
        women: $('#women').val(),
        payforroom: $('#payforroom').val(),
        startdate: $('#startdate').val(),
        enddate: $('#enddate').val(),
        active: false,      
    };
    $.ajax({
        url: "/api/checkins/" + data.id,
        data: JSON.stringify(data),
        type: "PUT",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            UpdateWaterElectricUsage($("#weid").val())
            
        },
        error: function (error) {
            //console.log(error);
            toastr.error("Please check all selected field!.", "Server Response");
        }
    });
}
//Update WE
function UpdateWaterElectricUsage(id) {
    var data = new FormData();
    data.append("wstartrecord", $('#wrecord').val());
    data.append("estartrecord", $('#precord').val());
    $.ajax({
        type: "PUT",
        url: "/api/updatewestartrecord/" + id,
        contentType: false,
        processData: false,
        data: data,
        success: function (result) {
            toastr.success("Update check in successfully!", "Server Respond");
            window.location.reload(true);
        },
        error: function (errormesage) {
            toastr.error("Electric usage insert faild!", "Server Respond");
            return false;
        }
    });
}
function Close() {
    window.location.reload(true);
}
