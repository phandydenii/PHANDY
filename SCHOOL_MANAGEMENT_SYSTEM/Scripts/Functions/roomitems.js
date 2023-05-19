$(document).ready(function () {
    $('#RoomItemModal').on('show.bs.modal', function () {
        GetRoomItem();
        DisableControlRoomItem();
    });
});

var RoomItemTable = [];
function GetRoomItem() {
    RoomItemTable = $('#tblRoomItem').dataTable({
        ajax: {
            url: "/api/roomdetails",
            dataSrc: ""
        },
        columns:
            [
                {
                    data: "id"
                },
                {
                    data: "room_no"
                },
                {
                    data: "itemname"
                },
                {
                    data: "price"
                },
                {
                    data: "id",
                    render: function (data) {
                        return "<button OnClick='OnRoomItemEdit (" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-edit'></span></button>" +
                               "<button OnClick='OnDeletRoomItem (" + data + ")' class='btn btn-danger btn-xs'><span class='glyphicon glyphicon-trash'></span></button>";
                    }
                }
            ],
        destroy: true,
        "order": [[0, "desc"]],
        "info": false

    });
}

function AddNewRoomItem() {
    document.getElementById('btnAddNewRoomItem').style.display = "none";
    document.getElementById('btnSaveRoomItem').style.display = "block";
    document.getElementById('btnUpdateRoomItem').style.display = "none";
    EnableControlRoomItem();
}

//Save  
function SaveRoomItem() {
    if ($("#iroomid").val() == 0) {
        $("#iroomid").focus();
        return false;
    }
    if ($("#itemid").val() == 0) {
        $("#itemid").focus();
        return false;
    }

    var data = {
        roomid: $('#iroomid').val(),
        itemid: $('#itemid').val(),
        price: $('#iprice').val(),
    };
    $.ajax({
        url: "/api/roomdetails",
        data: JSON.stringify(data),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            toastr.success("Insert data successfully.", "Server Respond");
            $('#tblRoomItem').DataTable().ajax.reload();
            DisableControlRoomItem();
            ClearControlRoomItem();
            $("#itemid").val() == 0;
            $("#iroomid").val() == 0
        },
        error: function (errormesage) {
            $('#roomid').focus();
            toastr.error("Inser data faild.", "Server Respond")
        }
    });    
}

function UpdateRoomItem() {
    var data = {
        id: $('#roomitemid').val(),
        roomid: $('#iroomid').val(),
        itemid: $('#itemid').val(),
        price: $('#iprice').val(),
    };
    $.ajax({
        url: "/api/roomdetails/" + data.id,
        data: JSON.stringify(data),
        type: "PUT",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            toastr.success("RoomType has been Updated", "Server Respond");
            $('#tblRoomItem').DataTable().ajax.reload();
            DisableControlRoomItem()
            ClearControlRoomItem();

            document.getElementById('btnAddNewRoomItem').style.display = "block";
            document.getElementById('btnSaveRoomItem').style.display = "none";
            document.getElementById('btnUpdateRoomItem').style.display = "none";
        },
        error: function (errormesage) {
            toastr.error("RoomType hasn't Updated in Database", "Server Respond")
        }
    });
}

function OnRoomItemEdit(id) {
    document.getElementById('btnAddNewRoomItem').style.display = "none";
    document.getElementById('btnSaveRoomItem').style.display = "none";
    document.getElementById('btnUpdateRoomItem').style.display = "block";
    EnableControlRoomItem();
    $.ajax({
        url: "/api/roomdetails/"+id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            $('#roomitemid').val(result.id);
            $("#iroomid").val(result.roomid);
            $("#itemid").val(result.itemid);
            $("#iprice").val(result.price);
            //alert(result.room_no);
        },
        error: function (errormessage) {
            toastr.error("No Record Select!", "Service Response");
        }
    });

}

function OnDeletRoomItem(id) {
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
                    url: "/api/roomdetails/" + id,
                    type: "DELETE",
                    contentType: "application/json;charset=utf-8",
                    datatype: "json",
                    success: function (result) {
                        $('#tblRoomItem').DataTable().ajax.reload();
                        toastr.success("Item hase been Deleted from room successfully!", "Service Response");
                    },
                    error: function (errormessage) {
                        toastr.error("Delete item from room faild!", "Service Response");
                    }
                });
            }
        }
    });
}

function DisableControlRoomItem() {
    document.getElementById('iroomid').disabled = true;
    document.getElementById('itemid').disabled = true;
    document.getElementById('iprice').disabled = true;
}

function EnableControlRoomItem() {
    document.getElementById('iroomid').disabled = false;
    document.getElementById('itemid').disabled = false;
    document.getElementById('iprice').disabled = false;
}

function ClearControlRoomItem() {
    $('#iroomid').val('');
    $('#itemid').val('');
    $('#iprice').val('');
}

function OnCloseRoomItem() {
    window.location.reload(true);
}
