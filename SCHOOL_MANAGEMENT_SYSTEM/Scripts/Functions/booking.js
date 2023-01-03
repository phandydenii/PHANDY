
$(document).ready(function () {
    GetBooking("Active");
});
var tableBooking = [];
$('#status').on('change', function () {
    GetBooking(this.value);
});
function GetBooking(status) {
    tableBooking = $('#tableBooking').dataTable({
        ajax: {
            url: "/api/booking-v/" + status,
            dataSrc: ""
        },
        columns:
            [
                {
                    data: "id"
                },
                {
                    data: "namekh"
                },
                {
                    data: "room_no"
                },
                {
                    data: "roomtypename"
                },
                {
                    data: "total"
                },
                {
                    data: "servicecharge"
                },
                {
                    data: "checkindate",
                    render: function (data) {
                    return moment(new Date(data)).format('DD-MMM-YYYY');
                    }
                },
                {
                    data: "expirecheckindate",
                    render: function (data) {
                        return moment(new Date(data)).format('DD-MMM-YYYY');
                    }
                },
                {
                    data: "bookingdate",
                    render: function (data) {
                        return moment(new Date(data)).format('DD-MMM-YYYY');
                    }
                },
                {
                    data: "id",
                    render: function (data, type, row) {
                        if (row.bookstatus == "Expire") {
                            return "<span class='label label-danger'><span class='glyphicon glyphicon-ok'></span> Expire</span>";
                        } else if (row.bookstatus == "Active") {
                            return "<button OnClick='BookingEdit (" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-edit'></span> Edit</button>" +
                                   "<button OnClick='CancelBooking (" + data + ")' class='btn btn-danger btn-xs'><span class='glyphicon glyphicon-trash'></span> Cancel</button>";
                        } else if (row.bookstatus == "Cancel") {
                            return "<span class='label label-danger'><span class='glyphicon glyphicon-ok'></span> Cancel</span>";
                        }
                    }
                }
            ],
        destroy: true,
    });
}
function BookingEdit(id) {
    $("#BookModal").modal('show');
    $.ajax({
        url: "/api/bookings/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            var checkindate = moment(result.checkindate).format("YYYY-MM-DD");
            var expiredate = moment(result.expiredate).format("YYYY-MM-DD");
            $('#id').val(result.id);
            $('#bookingno').val(result.bookingno);
            $('#bookingdate').val(result.bookingdate);
            $('#checkindate').val(checkindate);
            $('#expiredate').val(expiredate);
            $('#roomidb').val(result.roomid).change;
            $('#rmid').val(result.roomid);
            $('#guestid').val(result.guestid);
            $('#totalbooking').val(result.total);
            $('#paydollarbooking').val(result.paydollar);
            $('#payreilbooking').val(result.payriel);
            $('#note').val(result.note);
        },
        error: function (errormessage) {
            toastr.error("Load Record Error", "Service Response");
        }
    });
}
function UpdateBooking() {
    if ($('#totalbooking').val() == "") {
        $('#totalbooking').focus()
        return false;
    }

    var data = {
        id: $('#id').val(),
        bookingno: $('#bookingno').val(),
        bookingdate: $('#bookingdate').val(),
        guestid: $('#guestid').val(),
        roomid: $('#roomidb').val(),
        total: $('#totalbooking').val(),
        paydollar: $('#paydollarbooking').val(),
        payriel: $('#payrielbooking').val(),
        checkindate: $('#checkindate').val(),
        expiredate: $('#expiredate').val(),
        note: $('#note').val(),
    };

    $.ajax({
        url: "/api/bookings/"+data.id,
        data: JSON.stringify(data),
        type: "PUT",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if ($("#rmid").val() != $("#roomidb").val()) {
                UpdateRoomStatus($("#rmid").val(),'FREE');
                UpdateRoomStatus($("#roomidb").val(),'BOOK');
            }
            
            toastr.success("Update record successfully!", "Service Response");
        },
        error: function (errormesage) {
            toastr.error("This Booking ID is exist in Database", "Server Respond");
            return false;
        }
    });
}

function UpdateRoomStatus(id,status) { 
    $.ajax({
        type: "PUT",
        url: "/api/updateroomstatus/" + id + "/"+status,
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

function CancelBooking(id) {
    bootbox.confirm({
        title: "",
        message: "Are you sure want to delete this?",
        button: {
            cancel: {
                label: "Cancel",
                ClassName: "btn-default",
            },
            confirm: {
                label: "Delete",
                ClassName: "btn-danger"
            }
        },
        callback: function (result) {
            if (result) {
                $.ajax({
                    type: "PUT",
                    url: "/api/bookingstatus/" + id+"/Cancel",
                    contentType: false,
                    processData: false,
                    success: function (result) {
                        toastr.success("Booking hase been Cancel!", "Service Response");
                    },
                    error: function (error) {
                        toastr.error("Invoice Can't be Deleted", "Service Response");
                    }
                });

              
            }
        }
    })
    $.get("/api/bookings/"+id, function (data) {
        UpdateRoomStatusBook(data.roomid);

    });
    
}

function UpdateRoomStatusBook(id) {
    $.ajax({
        type: "PUT",
        url: "/api/updateroomstatus/" + id + "/FREE",
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