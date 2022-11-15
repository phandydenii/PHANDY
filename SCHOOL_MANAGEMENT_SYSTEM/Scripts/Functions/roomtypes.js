
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
                        return "<button OnClick='OnEditRoomType (" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-edit'></span> Edit</button>" +
                            "<button OnClick='OnDeleteRoomType (" + data + ")' class='btn btn-danger btn-xs'><span class='glyphicon glyphicon-trash'></span> Delete</button>";
                    }
                }
            ],
        destroy: true,
        "order": [[0, "desc"]],
        "info": false

    });
}

//Save  
function RoomTypeAction() {
    var action = '';
    action = document.getElementById('btnSaveRoomType').innerText;

    if (action == "Add New") {
        document.getElementById('btnSaveRoomType').innerText = 'Save';
        EnableControlRoomType();
        $('#roomtypename').focus();

    }
    else if (action === "Save") {
        var res = ValidationFormRoomType();
        if (res == false) {
            return false;
        }

        var data = {
            roomtypename: $('#roomtypename').val(),
            roomtypenamekh: $('#roomtypenamekh').val(),
            note: $('#note').val(),
        };

        $.ajax({
            url: "/api/RoomTypes",
            data: JSON.stringify(data),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                toastr.success("New RoomType has been Created", "Server Respond");
                $('#tableRoomType').DataTable().ajax.reload();
                document.getElementById('btnSaveRoomType').innerText = "Add New";
                DisableControlRoomType();
                ClearControlRoomType();
            },
            error: function (errormesage) {
                $('#RoomTypeName').focus();
                toastr.error("This Name is exist in Database", "Server Respond")
            }

        });

    } else if (action == "Update") {
        var data = {
            id: $('#RoomTypeid').val(),
            roomtypename: $('#roomtypename').val(),
            roomtypenamekh: $('#roomtypenamekh').val(),
            note: $('#note').val(),
        };
        $.ajax({
            url: "/api/RoomTypes/" + data.id,
            data: JSON.stringify(data),
            type: "PUT",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                toastr.success("RoomType has been Updated", "Server Respond");
                $('#tableRoomType').DataTable().ajax.reload();
                document.getElementById('btnSaveRoomType').innerText = "Add New";
                DisableControlRoomType();
                ClearControlRoomType();
            },
            error: function (errormesage) {
                toastr.error("RoomType hasn't Updated in Database", "Server Respond")
            }
        });
    }
}

function OnEditRoomType(id) {
    EnableControlRoomType();
    action = document.getElementById('btnSaveRoomType').innerText = "Update";

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

