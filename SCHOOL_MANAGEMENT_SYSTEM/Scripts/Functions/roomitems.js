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
                        return "<button OnClick='OnRoomItemEdit (" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-edit'></span> Edit</button>" +
                               "<button OnClick='OnDeletRoomItem (" + data + ")' class='btn btn-danger btn-xs'><span class='glyphicon glyphicon-trash'></span> Delete</button>";
                    }
                }
            ],
        destroy: true,
        "order": [[0, "desc"]],
        "info": false

    });
}


//Save  
function RoomItmAction() {
    
    var action = '';
    action = document.getElementById('btnSaveRoomItem').innerText;

    if (action == "Add New") {
        document.getElementById('btnSaveRoomItem').innerText = 'Save';
        EnableControlRoomItem();
        $('#rmid').focus();

    }
    else if (action === "Save") {
        if($("#rprice").val().trim() ==="") {
            toastr.error("Please input price", "Server Respond");
            return false
        }
        var data = {
            roomid: $('#rmid').val(),
            itemid: $('#itemid').val(),
            price: $('#rprice').val(),
        };

        $.ajax({
            url: "/api/roomdetails",
            data: JSON.stringify(data),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                toastr.success("New RoomType has been Created", "Server Respond");
                $('#tblRoomItem').DataTable().ajax.reload();
                document.getElementById('btnSaveRoomItem').innerText = "Add New";
                DisableControlRoomItem();
                ClearControlRoomItem();
            },
            error: function (errormesage) {
                $('#roomid').focus();
                toastr.error("This Name is exist in Database", "Server Respond")
            }

        });

    }
    else if (action === "Update") {
        var data = {
            id: $('#roomitemid').val(),
            roomid: $('#rmid').val(),
            itemid: $('#itemid').val(),
            price: $('#rprice').val(),
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
                document.getElementById('btnSaveRoomItem').innerText = "Add New";
                DisableControlRoomItem()
                ClearControlRoomItem();
            },
            error: function (errormesage) {
                toastr.error("RoomType hasn't Updated in Database", "Server Respond")
            }
        });
    }
}

function OnRoomItemEdit(id) {
    EnableControlRoomItem();
    action = document.getElementById('btnSaveRoomItem').innerText = "Update";

    $.ajax({
        url: "/api/roomdetails/"+id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            $('#roomitemid').val(result.id);
            $("#rmid").val(result.roomid);
            $("#itemid").val(result.itemid);
            $("#rprice").val(result.price);
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
    document.getElementById('rmid').disabled = true;
    document.getElementById('itemid').disabled = true;
    document.getElementById('rprice').disabled = true;
}

function EnableControlRoomItem() {
    document.getElementById('rmid').disabled = false;
    document.getElementById('itemid').disabled = false;
    document.getElementById('rprice').disabled = false;
}

function ClearControlRoomItem() {
    $('#rmid').val('');
    $('#itemid').val('');
    $('#rprice').val('');
}

function OnCloseRoomItem() {
    window.location.reload(true);
}

