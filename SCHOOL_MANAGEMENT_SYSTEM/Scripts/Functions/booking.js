
$(document).ready(function () {

    GetBooking();

    $('#BookingModal').on('show.bs.modal', function () {
        //document.getElementById('BookingTypeName').disable = true;
    });
});
var tableBooking = [];
function GetBooking() {
    tableBooking = $('#tableBooking').dataTable({
        ajax: {
            url: "/api/Bookings",
            dataSrc: ""
        },
        columns:
            [
                {
                    data: "id"
                },
                {
                    data: "booking_no"
                },
                {
                    data: "username"
                },
                {
                    data: "guestname"
                },
                {
                    data: "paydollar"
                },
                {
                    data: "payriel"
                },
                {
                    data: "bookingdate",
                    render: function (data) {
                        return moment(new Date(data)).format('DD-MMM-YYYY');
                    }
                },
                {
                    data: "note"
                },
                {
                    data: "id",
                    render: function (data) {
                        return "<button OnClick='BookingEdit (" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-edit'></span> Edit</button>" +
                            "<button OnClick='BookingDelete (" + data + ")' class='btn btn-danger btn-xs'><span class='glyphicon glyphicon-trash'></span> Delete</button>";
                    }
                }
            ],
        destroy: true,
        "order": [[0, "desc"]],
        "info": false

    });
}
//Save  
function BookingAction() {
    
    var action = '';
    action = document.getElementById('btnSaveBooking').innerText;

    if (action == "Add New") {
        document.getElementById('btnSaveBooking').innerText = 'Save';
        EnableControl();
        $('#bookingno').focus();
        $('#bookingdate').val(moment().format('YYYY-MM-DD'));
    }
    else if (action == "Save") {
        var data = {
            booking_no: $('#bookingno').val(),
            userid: $('#userid').val(),
            guestid: $('#guestid').val(),
            paydollar: $('#paydollar').val(),
            payriel: $('#payriel').val(),
            bookingdate: $('#bookingdate').val(),
            note: $('#note').val(),

        };


        $.ajax({
            url: "/api/Bookings",
            data: JSON.stringify(data),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                toastr.success("New Booking has been Created", "Server Respond");
                $('#tableBooking').DataTable().ajax.reload();
                // $('#customerName').val('');
                $("#BookingModal").modal('hide');
                document.getElementById('btnSaveBooking').innerText = "Add New";
                DisableControl();
                ClearControl();
            },
            error: function (errormesage) {
                $('#BookingName').focus();
                toastr.error("This Name is exist in Database", "Server Respond")
            }

        });

    } else if (action == "Update") {
        //alert('hi');
        //var res = ValidateCustomer();
        //if (res == false) {
        //    return false;
        //}
        var data = {
            id: $('#Bookingid').val(),

            booking_no: $('#bookingno').val(),
            userid: $('#userid').val(),
            guestid: $('#guestid').val(),
            paydollar: $('#paydollar').val(),
            payriel: $('#payriel').val(),
            bookingdate: $('#bookingdate').val(),
            note: $('#note').val(),

        };


        $.ajax({
            url: "/api/Bookings/" + data.id,
            data: JSON.stringify(data),
            type: "PUT",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                toastr.success("Booking has been Updated", "Server Respond");
                $('#tableBooking').DataTable().ajax.reload();
                $("#BookingModal").modal('hide');
                document.getElementById('btnSaveBooking').innerText = "Add New";
                DisableControl();
                ClearControl();
            },
            error: function (errormesage) {
                toastr.error("Booking hasn't Updated in Database", "Server Respond")
            }
        });
    }
}

function BookingEdit(id) {
    //alert('hi');
    //ClearControl();
    EnableControl();
    action = document.getElementById('btnSaveBooking').innerText = "Update";

    $.ajax({
        url: "/api/Bookings/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            $('#Bookingid').val(result.id);

            ///console.log(result.id);
            //var date = new Date(result.date);
            //var datetime = moment(date).format('YYYY-MM-DD');
            //$("#date").val(datetime);
            $("#bookingno").val(result.booking_no);
            $("#userid").val(result.userid);
            $("#guestid").val(result.guestid);
            var dr = moment(result.bookingdate).format("YYYY-MM-DD");
            $("#bookingdate").val(dr);
            $("#paydollar").val(result.paydollar);
            $("#payriel").val(result.payriel);
            $("#note").val(result.note);


            $("#BookingModal").modal('show');
        },
        error: function (errormessage) {
            toastr.error("No Record Select!", "Service Response");
        }
    });

}

function BookingDelete(id) {
    // alert('hi');
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
            //alert(id);
            if (result) {
                $.ajax({
                    url: "/api/Bookings/" + id,
                    type: "DELETE",
                    contentType: "application/json;charset=utf-8",
                    datatype: "json",
                    success: function (result) {
                        $('#tableBooking').DataTable().ajax.reload();
                        toastr.success("Booking Deleted successfully!", "Service Response");
                    },
                    error: function (errormessage) {
                        toastr.error("Booking Can't be Deleted", "Service Response");
                    }
                });
            }
        }
    });
}


function DisableControl() {
    document.getElementById('bookingno').disabled = true;
    document.getElementById('userid').disabled = true;
    document.getElementById('guestid').disabled = true;
    document.getElementById('paydollar').disabled = true;
    document.getElementById('payriel').disabled = true;
    document.getElementById('bookingdate').disabled = true;
    document.getElementById('note').disabled = true;


}

function EnableControl() {
    document.getElementById('bookingno').disabled = false;
    document.getElementById('userid').disabled = false;
    document.getElementById('guestid').disabled = false;
    document.getElementById('paydollar').disabled = false;
    document.getElementById('payriel').disabled = false;
    document.getElementById('bookingdate').disabled = false;
    document.getElementById('note').disabled = false;

}

function ClearControl() {
    $('#bookingno').val('');
    $('#userid').val('');
    $('#guestid').val('');
    $('#paydollar').val('');
    $('#payriel').val('');
    $('#bookingdate').val('');
    $('#note').val('');
}

function AddnewBookingAction() {
    document.getElementById('btnSaveBooking').innerText = "Add New";
    DisableControl();
    ClearControl();
}

/** function Validate() {
    var isValid = true;
    var formAddEdit = $("#formTrainingProgramAdd");
    if ($('#deliveryName').val().trim() === "") {
        $('#deliveryName').css('border-color', 'red');
        $('#deliveryName').focus();
        toastr.info("Please enter date", "Required");
    }
    else {
        $('#deliveryName').css('border-color', '#cccccc');
        //        if ($('#phone').val().trim() === "") {
        //            $('#phone').css('border-color', 'red');
        //            $('#phone').focus();
        //            toastr.info("Please enter your phone", "Required");
        //        }
        //        else {
        //            $('#phone').css('border-color', '#cccccc');
        //        }
        //    }
        //    return isValid;
        //}

        //  function ValidateCustomer() {
        //    var isValid = true;
        //    var formAddEdit = $("#formTrainingProgramAdd");
        //      if ($('#date').val().trim() == "") {
        //          $(' #date').css('border-color', 'red');
        //          $(' #date').focus();
        //        isValid = false;
        //    } else {
        //          $(' #date').css('border-color', '#cccccc');
        //          $(' #date').focus();
        //    }
        //      if ($(' #phone').val().trim() == "") {
        //          $(' #phone').css('border-color', 'red');
        //        isValid = false;
        //    } else {
        //          $(' #phone').css('border-color', '#cccccc');

        //    }
        //    if ($(' #currentlocation').val().trim() == "") {
        //        $(' #currentlocation').css('border-color', 'red');
        //        isValid = false;
        //    } else {
        //        $(' #currentlocation').css('border-color', '#cccccc');
        //    }
        //    if ($(' #customerName').val().trim() == "") {
        //        $(' #customerName').css('border-color', 'red');
        //        isValid = false;
        //    } else {
        //        $(' #customerName').css('border-color', '#cccccc');
        //    }
        //if ($(' #phone').val().trim() == "") {
        //    $(' #phone').css('border-color', 'red');
        //    isValid = false;
        //} else {
        //    $(' #phone').css('border-color', '#cccccc');
    }

    return isValid;
}
*/
