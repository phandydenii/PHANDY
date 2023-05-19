$(document).ready(function () {
    GetBooking("All");
});
var tableBooking = [];
$('#status').on('change', function () {
    GetBooking(this.value);
});

/////On Change 
$('#frmbookroomid').on('change', function () {
    GetRoomPrice(this.value);
});
$('#changeroomid').on('change', function () {
    GetRoomPrice(this.value);

});
function GetRoomPrice(id) {
    $.ajax({
        url: "/api/rooms/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            $("#roompriceb").val(result.price);
        },
        error: function (errormessage) {
            toastr.error("No Record Select!", "Service Response");
        }
    });
}

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
                    data: "bookingdate",
                    render: function (data) {
                        return moment(new Date(data)).format('DD-MMM-YYYY');
                    }
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
                    data: "id",
                    render: function (data, type, row) {
                        //if (row.gueststatus == "CheckIn") {
                        //    return "<span class='label label-danger'><span class='glyphicon glyphicon-ok'></span> Active</span>";
                        //} else {
                        //    if (row.bookstatus == "Expire") {
                        //        return "<span class='label label-danger'><span class='glyphicon glyphicon-ban-circle'></span> Expire</span>";
                        //    } else if (row.bookstatus == "Book") {
                        //        return "<button OnClick='BookingEdit (" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-edit'></span> Edit</button>" +
                        //            "<button OnClick='CancelBooking (" + data + ")' class='btn btn-danger btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-minus-sign'></span> Cancel</button>" +
                        //            "<button OnClick='CheckIn (" + data + ")' class='btn btn-info btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-log-in'></span> Check In</button>";
                        //    } else if (row.bookstatus == "Cancel") {
                        //        return "<span class='label label-danger'><span class='glyphicon glyphicon-minus-sign'></span> Cancel</span>";
                        //    }
                        //}
                        if (row.bookstatus == "Book") {
                            return "<button OnClick='BookingEdit (" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-edit'></span> Edit</button>" +
                                "<button OnClick='CancelBooking (" + data + ")' class='btn btn-danger btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-minus-sign'></span> Cancel</button>" +
                                "<button OnClick='CheckIn (" + data + ")' class='btn btn-info btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-log-in'></span> Check In</button>";
                        } else if (row.bookstatus == "Expire") {
                            return "<span class='label label-danger'><span class='glyphicon glyphicon-ban-circle'></span> Expire</span>";
                        } else if (row.bookstatus == "Cancel") {
                            return "<span class='label label-warning'><span class='glyphicon glyphicon-minus-sign'></span> Cancel</span>";
                        } else if (row.bookstatus == "Active") {
                            return "";
                        }
                    }
                }
            ],
        destroy: true,
    });
}

function OnAddBooking() {
    $("#frmbookroomid").show();
    $('#rmid').hide();
    $('#changeroomid').hide();
    document.getElementById("AddBook").style.display = "block";
    $.get("/api/bookinginvoiceno", function (data) {
        $('#bookingno').val(data);
    });
    $.get("/api/ExchangeRates/1/2", function (data) {
        $("#exrate").val(data.rate);
    });
}

$('#RoomIdChange').on('change', function () {
    if (this.value == 0) {
        $('#roompriceb').val(0);
    } else {
        $.get("/api/rooms/" + this.value, function (data) {
            $('#roompriceb').val(data.price);
        });
    }
});

//On Check In
function CheckIn(id) {
    $("#CheckInModal").modal("show");
    $.ajax({
        url: "/api/booking_v/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            $("#bookid").val(result.id);
            $("#guestidchin").val(result.guestid);
            //$("#chroomid").val(result.roomid);
            $("#roompricech").val(result.roomprice);
            $("#payforroom").val(result.roomprice);
            $("#prepaid").val(result.roomprice / 2);
            $("#paydollar").val(result.roomprice + (result.roomprice / 2));
        },
        error: function (errormessage) {
            toastr.error("No Record Select!", "Service Response");
        }
    });

    $.get("/api/ExchangeRates/1/2", function (data) {
        $("#exrate").val(data.rate);
    });
}

function TotalBook() {
    var a = $('#totalbooking').val();
    $('#paydollarbooking').val(a);
}
function ChangePayBook() {
    var a = parseFloat($('#totalbooking').val());
    var b = parseFloat($('#paydollarbooking').val());
    var e = parseFloat($('#exchagerate').text());
    var result = (a - b) * e;
    $('#payreilbooking').val(result);

}

function OnBooking() {
    try {
        CreateGuest();
    }
    catch (e) {
        toastr.error(e.toString(), "Server Respond")
    }
}

///Insert Guest
function CreateGuest() {
    if ($("#guestname").val() == "") {
        $("#guestname").focus()
        return false;
    }
    var data = new FormData();
    data.append("name", $("#guestname").val());
    data.append("namekh", $("#gnamekh").val());
    data.append("sex", $("#gsex").val());
    data.append("dob", $("#dob").val());
    data.append("address", $("#address").val());
    data.append("nationality", $("#nationality").val());
    data.append("phone", $("#phonenumber").val());
    data.append("email", $("#email").val());
    data.append("ssn", $("#ssn").val());
    data.append("passport", $("#passport").val());
    data.append("status", 'BOOK');
    $.ajax({
        type: "POST",
        url: "/api/guests",
        contentType: false,
        processData: false,
        data: data,
        success: function (result) {
            InsertBooking(result);
            
        },
        error: function (error) {
            toastr.error("Please check all selected field!.", "Server Response");
        }
    });
}

//Insert Booking
function InsertBooking(result) {
    if ($('#totalbooking').val() == "") {
        $('#totalbooking').focus()
        return false;
    }

    var data = {
        bookingno: $('#bookingno').val(),
        guestid: result,
        roomid: $('#RoomIdChange').val(),
        total: $('#totalbooking').val(),
        paydollar: $('#paydollarbooking').val(),
        payriel: $('#payreilbooking').val(),
        checkindate: $('#checkindate').val(),
        expiredate: $('#expiredate').val(),
        note: $('#bookingnote').val(),
    };

    $.ajax({
        url: "/api/bookings",
        data: JSON.stringify(data),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            UpdateRoomBook();
            toastr.success("Book successfully!", "Server Respond");
            window.location.reload(true);
            window.location = "booking-rpt/" + result;
        },
        error: function (errormesage) {
            toastr.error("This Booking ID is exist in Database", "Server Respond");
            return false;
        }
    });
}

function UpdateRoomBook() {
    $.ajax({
        type: "PUT",
        url: "/api/updateroomstatus/" + $("#RoomIdChange").val() + "/BOOK",
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

function BookingEdit(id) {
    $("#BookModal").modal('show');
    $("#frmbookroomid").hide();
    document.getElementById("lblCheckRoom").style.display = "block";
    document.getElementById("UpdateBook").style.display = "block";


    $.ajax({
        url: "/api/bookings/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            var checkindate = moment(result.checkindate).format("YYYY-MM-DD");
            var expiredate = moment(result.expiredate).format("YYYY-MM-DD");
            var bookingdate = moment(result.bookingdate).format("YYYY-MM-DD");
            var bookingno = "RL"+("000000" + result.id).slice(-6);
            $('#bookid').val(result.id);
            $('#bookingno').val(bookingno);
            $('#bookingdate').val(bookingdate);
            $('#checkindate').val(checkindate);
            $('#expiredate').val(expiredate);
            $('#rmid').val(result.roomid);
            $('#roompriceb').val(result.room.price);
            $('#totalbooking').val(result.total);
            $('#paydollarbooking').val(result.paydollar);
            $('#payreilbooking').val(result.payriel);
            $('#note').val(result.note);
            $('#userid').val(result.userid);

            
            $('#guestid').val(result.guestid);
            $('#guestname').val(result.guest.name);
            $('#gnamekh').val(result.guest.namekh);
            $('#gsex').val(result.guest.sex);
            var dr = moment(result.guest.dob).format("YYYY-MM-DD");
            $("#dob").val(dr);
            $('#address').val(result.guest.address);
            $('#nationality').val(result.guest.nationality);
            $('#phonenumber').val(result.guest.phone);
            $('#email').val(result.guest.email);
            $('#ssn').val(result.guest.ssn);
            $('#passport').val(result.guest.passport);
            //$('#status').val(result.status);
        },
        error: function (errormessage) {
            toastr.error("Load Record Error", "Service Response");
        }
    });
}

function CancelBooking(id) {
    var gid;
    var rid;
    $.ajax({
        url: "/api/bookings/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            $('#roomid').val(result.roomid);
            rid = result.roomid;
            gid = result.guestid;
        },
        error: function (errormessage) {
            toastr.error("Load Record Error", "Service Response");
        }
    });

    bootbox.confirm({
        title: "",
        message: "Are you sure want to delete this?",
        button: {
            cancel: {
                label: "No",
                ClassName: "btn-default",
            },
            confirm: {
                label: "Ok",
                ClassName: "btn-danger"
            }
        },
        callback: function (result) {
            if (result) {
                $.ajax({
                    type: "PUT",
                    url: "/api/bookingstatus/" + id + "/Cancel",
                    contentType: false,
                    processData: false,
                    // data: status,
                    success: function (result) {
                        toastr.success("Booking has been cancel successfully.", "Server Response");
                    },
                    error: function (error) {
                        toastr.error("Unblock room fail!", "Server Response");
                    }
                });

                $.ajax({
                    type: "PUT",
                    url: "/api/updateroomstatus/" + rid + "/FREE",
                    contentType: false,
                    processData: false,
                    // data: status,
                    success: function (result) {
                        window.location.reload(true);
                    },
                    error: function (error) {
                        toastr.error("Unblock room fail!", "Server Response");
                    }
                });

                $.ajax({
                    type: "PUT",
                    url: "/api/updategueststatus/" + gid + "/Cancel",
                    contentType: false,
                    processData: false,
                    // data: status,
                    success: function (result) {
                    },
                    error: function (error) {
                        toastr.error("Update room status fail!", "Server Response");
                    }
                });
            }
        }
    })
}




///======///
//function UpdateBooking() {
//    if ($('#totalbooking').val() == "") {
//        $('#totalbooking').focus()
//        return false;
//    }

//    var data = {
//        id: $('#bookid').val(),
//        bookingdate: $('#bookingdate').val(),
//        guestid: $('#guestid').val(),
//        roomid: $('#roomid').val(),
//        total: $('#totalbooking').val(),
//        paydollar: $('#paydollarbooking').val(),
//        payriel: $('#payrielbooking').val(),
//        checkindate: $('#checkindate').val(),
//        expiredate: $('#expiredate').val(),
//        note: $('#note').val(),
//        status: $('#status').val(),
//    };

//    $.ajax({
//        url: "/api/bookings/"+data.id,
//        data: JSON.stringify(data),
//        type: "PUT",
//        contentType: "application/json;charset=utf-8",
//        dataType: "json",
//        success: function (result) {
//            toastr.success("Update record successfully!", "Service Response");
//            window.location = "booking-rpt/" + data.id;
//        },
//        error: function (errormesage) {
//            toastr.error("This Booking ID is exist in Database", "Server Respond");
//            return false;
//        }
//    });
//}

//function UpdateRoomStatus(id,status) { 
//    $.ajax({
//        type: "PUT",
//        url: "/api/updateroomstatus/" + id + "/"+status,
//        contentType: false,
//        processData: false,
//        success: function (result) {
//            //toastr.success("Room Status has been Update successfully.", "Server Response");
//        },
//        error: function (error) {
//            toastr.error("Update room status fail!", "Server Response");
//        }
//    });  
//}

//function UpdateRoomStatusBook(id) {
//    $.ajax({
//        type: "PUT",
//        url: "/api/updateroomstatus/" + id + "/FREE",
//        contentType: false,
//        processData: false,
//        // data: status,
//        success: function (result) {
//            //toastr.success("Room Status has been Update successfully.", "Server Response");
//        },
//        error: function (error) {
//            toastr.error("Update room status fail!", "Server Response");
//        }
//    });
//}

function CloseBook() {
    window.location.reload(true);
}