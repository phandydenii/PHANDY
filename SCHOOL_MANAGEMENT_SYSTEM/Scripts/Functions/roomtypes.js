
$(document).ready(function () {
    $('#RoomTypeModal').on('show.bs.modal', function () {
        GetRoomType();
    });
});
var tableRoomType = [];
function GetRoomType() {
    tableRoomType = $('#tableRoomType').dataTable({
        ajax: {
            url: "/api/RoomTypes",
            dataSrc: ""
        },
        columns:
            [
                {
                    data: "id"
                },
                {
                    data: "roomtypename"
                },
                {
                    data: "roomtypenamekh"
                },
                {
                    data: "id",
                    render: function (data) {
                        return "<button OnClick='OnEditRoomType (" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-edit'></span></button>" +
                            "<button OnClick='OnDeleteRoomType (" + data + ")' class='btn btn-danger btn-xs'><span class='glyphicon glyphicon-trash'></span></button>";
                    }
                }
            ],
        destroy: true,
        "order": [[0, "desc"]],
        "info": false

    });
}


function OnEditRoomType(id) {
    EnableControlRoomType();
    document.getElementById('btnAddRoomType').style.display = "block";
    document.getElementById('btnSaveRoomType').style.display = "none";
    document.getElementById('btnUpdateRoomType').style.display = "block";
    $.ajax({
        url: "/api/RoomTypes/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            $('#RoomTypeid').val(result.id);
            $("#roomtypename").val(result.roomtypename);
            $("#roomtypenamekh").val(result.roomtypenamekh);
            $("#note").val(result.note);
        },
        error: function (errormessage) {
            toastr.error("No Record Select!", "Service Response");
        }
    });

}

function OnDeleteRoomType(id) {
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
                    url: "/api/RoomTypes/" + id,
                    type: "DELETE",
                    contentType: "application/json;charset=utf-8",
                    datatype: "json",
                    success: function (result) {
                        $('#tableRoomType').DataTable().ajax.reload();
                        toastr.success("RoomType Deleted successfully!", "Service Response");
                    },
                    error: function (errormessage) {
                        toastr.error("RoomType Can't be Deleted", "Service Response");
                    }
                });
            }
        }
    });
}

function DisableControlRoomType() {
    document.getElementById('roomtypename').disabled = true;
    document.getElementById('roomtypenamekh').disabled = true;
    document.getElementById('note').disabled = true;
}

function EnableControlRoomType() {
    document.getElementById('roomtypename').disabled = false;
    document.getElementById('roomtypenamekh').disabled = false;
    document.getElementById('note').disabled = false;
}

function ClearControlRoomType() {
    $('#roomtypename').val('');
    $('#roomtypenamekh').val('');
    $('#note').val('');
}

function AddRoomTypeAction() {
    document.getElementById('btnSaveRoomType').innerText = "Add New";
    DisableControlRoomType();
    ClearControlRoomType();
}

function ValidationFormRoomType() {
    var isValid = true;
    if ($('#roomtypename').val().trim() === "") {
        $('#roomtypename').css('border-color', 'red');
        $('#roomtypename').focus();
        toastr.info("Please enter Room Type", "Required");
        isValid = false;
    }
    else {
        $('#roomtypename').css('border-color', '#cccccc');
        if ($('#roomtypenamekh').val().trim() === "") {
            $('#roomtypenamekh').css('border-color', 'red');
            $('#roomtypenamekh').focus();
            toastr.info("Please enter Room Type", "Required");
            isValid = false;
        }
        else {
            $('#roomtypenamekh').css('border-color', '#cccccc');

        }
    }
    return isValid;
}

