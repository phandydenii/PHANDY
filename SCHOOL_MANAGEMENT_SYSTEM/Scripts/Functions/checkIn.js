
$(document).ready(function () {

    GetCheckIn();

    $('#CheckInModal').on('show.bs.modal', function () {
        //document.getElementById('CheckInTypeName').disable = true;
    });
});
var tableCheckIn = [];
function GetCheckIn() {
    tableCheckIn = $('#tableCheckIn').dataTable({
        ajax: {
            url: "/api/CheckIns",
            dataSrc: ""
        },
        columns:
            [
                {
                    data: "id"
                },
                {
                    data: "checkindate",
                    render: function (data) {
                        return moment(new Date(data)).format('DD-MMM-YYYY');
                    }
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
                    data: "username"
                },
                {
                    data: "guestname"
                },
                {
                    data: "child"
                },
                {
                    data: "man"
                },
                {
                    data: "women"
                },
                {
                    data: "id",
                    render: function (data) {
                        return "<button OnClick='CheckInEdit (" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-edit'></span> Edit</button>" +
                            "<button OnClick='CheckInDelete (" + data + ")' class='btn btn-danger btn-xs'><span class='glyphicon glyphicon-trash'></span> Delete</button>";
                    }
                }
            ],
        destroy: true,
        "order": [[0, "desc"]],
        "info": false

    });
}
//Save  
function CheckInAction() {
    var action = '';
    action = document.getElementById('btnSaveCheckIn').innerText;

    if (action == "Add New") {
        document.getElementById('btnSaveCheckIn').innerText = 'Save';
        EnableControl();
        $('#checkindate').focus().val(moment().format('YYYY-MM-DD'));
        $('#enddate').val(moment().format('YYYY-MM-DD'));
        $('#startdate').val(moment().format('YYYY-MM-DD'));
    }
    else if (action == "Save") {
        var data = {
            checkindate: $('#checkindate').val(),
            startdate: $('#startdate').val(),
            enddate: $('#enddate').val(),
            userid: $('#userid').val(),
            guestid: $('#guestid').val(),
            child: $('#child').val(),
            man: $('#man').val(),
            women: $('#women').val(),

        };


        $.ajax({
            url: "/api/CheckIns",
            data: JSON.stringify(data),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                toastr.success("New CheckIn has been Created", "Server Respond");
                $('#tableCheckIn').DataTable().ajax.reload();
                // $('#customerName').val('');
                $("#CheckInModal").modal('hide');
                document.getElementById('btnSaveCheckIn').innerText = "Add New";
                DisableControl();
                ClearControl();
            },
            error: function (errormesage) {
                $('#CheckInName').focus();
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
            id: $('#CheckInid').val(),

            checkindate: $('#checkindate').val(),
            startdate: $('#startdate').val(),
            enddate: $('#enddate').val(),
            userid: $('#userid').val(),
            guestid: $('#guestid').val(),
            child: $('#child').val(),
            man: $('#man').val(),
            women: $('#women').val(),

        };


        $.ajax({
            url: "/api/CheckIns/" + data.id,
            data: JSON.stringify(data),
            type: "PUT",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                toastr.success("CheckIn has been Updated", "Server Respond");
                $('#tableCheckIn').DataTable().ajax.reload();
                $("#CheckInModal").modal('hide');
                document.getElementById('btnSaveCheckIn').innerText = "Add New";
                DisableControl();
                ClearControl();
            },
            error: function (errormesage) {
                toastr.error("CheckIn hasn't Updated in Database", "Server Respond")
            }
        });
    }
}

function CheckInEdit(id) {
    //alert('hi');
    //ClearControl();
    EnableControl();
    action = document.getElementById('btnSaveCheckIn').innerText = "Update";

    $.ajax({
        url: "/api/CheckIns/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            $('#CheckInid').val(result.id);

            var dr = moment(result.checkindate).format("YYYY-MM-DD");
            $("#checkindate").val(dr);
            var dr = moment(result.startdate).format("YYYY-MM-DD");
            $("#startdate").val(dr);
            var dr = moment(result.enddate).format("YYYY-MM-DD");
            $("#enddate").val(dr);
            $("#userid").val(result.userid);
            $("#guestid").val(result.guestid);
            $("#child").val(result.child);
            $("#man").val(result.man);
            $("#women").val(result.women);


            $("#CheckInModal").modal('show');
        },
        error: function (errormessage) {
            toastr.error("No Record Select!", "Service Response");
        }
    });

}

function CheckInDelete(id) {
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
                    url: "/api/CheckIns/" + id,
                    type: "DELETE",
                    contentType: "application/json;charset=utf-8",
                    datatype: "json",
                    success: function (result) {
                        $('#tableCheckIn').DataTable().ajax.reload();
                        toastr.success("CheckIn Deleted successfully!", "Service Response");
                    },
                    error: function (errormessage) {
                        toastr.error("CheckIn Can't be Deleted", "Service Response");
                    }
                });
            }
        }
    });
}


function DisableControl() {
    document.getElementById('checkindate').disabled = true;
    document.getElementById('startdate').disabled = true;
    document.getElementById('enddate').disabled = true;
    document.getElementById('userid').disabled = true;
    document.getElementById('guestid').disabled = true;
    document.getElementById('child').disabled = true;
    document.getElementById('man').disabled = true;
    document.getElementById('women').disabled = true;


}

function EnableControl() {
    document.getElementById('checkindate').disabled = false;
    document.getElementById('startdate').disabled = false;
    document.getElementById('enddate').disabled = false;
    document.getElementById('userid').disabled = false;
    document.getElementById('guestid').disabled = false;
    document.getElementById('child').disabled = false;
    document.getElementById('man').disabled = false;
    document.getElementById('women').disabled = false;

}

function ClearControl() {
    $('#checkindate').val('');
    $('#startdate').val('');
    $('#enddate').val('');
    $('#userid').val('');
    $('#guestid').val('');
    $('#child').val('');
    $('#man').val('');
    $('#women').val('');
}

function AddnewCheckInAction() {
    document.getElementById('btnSaveCheckIn').innerText = "Add New";
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
