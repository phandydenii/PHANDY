﻿$(document).ready(function () {
    
    $('#RoomModal').on('show.bs.modal', function () {
        
        GetRoom();
        DisableControlRoom();

    });

});
var tableRoom = [];
function GetRoom() {
    tableRoom = $('#tableRoom').dataTable({
        ajax: {
            url: "/api/Rooms",
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
                    data: "price"
                },
                {
                    data: "roomtypename"
                },
                {
                    data: "floorno"
                },               
                {
                    data: "status", 
                    
                },
                {
                    data: "id",
                    render: function (data) {
                        return "<button OnClick='OnEditRoom (" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-edit'></span> Edit</button>"
                            + "<button OnClick='OnDeleteRoom (" + data + ")' class='btn btn-danger btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-trash'></span> Delete</button>"                           
                            ;
                    }
                }
            ],
        destroy: true,
        "info": false

    });
}


//Save  
function RoomAction() {
    var action = '';
    action = document.getElementById('btnSaveRoom').innerText;

    if (action == "Add New") {
        document.getElementById('btnSaveRoom').innerText = 'Save';
        EnableControlRoom();
        $('#roomno').focus();
        //$('#Roomdate').val(moment().format('YYYY-MM-DD'));
    }
    else if (action == "Save") {
        var isValid = ValidationForm();
        if (isValid == false) {
            return false;
        }

        
        var data = {
            room_no: $('#roomno').val(),
            roomtypeid: $('#roomtypeid').val(),
            floorid: $('#floorid').val(),
            servicecharge: $('#servicecharge').val(),
            price: $('#price').val(),
            roomkey: $('#roomkey').val(),
            status: $('#statuss').val(),
          

        };


        $.ajax({
            url: "/api/Rooms",
            data: JSON.stringify(data),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                toastr.success("New Room has been Created", "Server Respond");
                $('#tableRoom').DataTable().ajax.reload();
                $("#RoomModal").modal('hide');
                window.location.reload(true);
            },
            error: function (errormesage) {
                $('#RoomName').focus();
                toastr.error("This Name is exist in Database", "Server Respond")
            }

        });

    } else if (action == "Update") {

        var data = {
            id: $('#Roomid').val(),

            room_no: $('#roomno').val(),
            roomtypeid: $('#roomtypeid').val(),
            floorid: $('#floorid').val(),
            servicecharge: $('#servicecharge').val(),
            price: $('#price').val(),
            roomkey: $('#roomkey').val(),
            status: $('#statusv').val(),

        };


        $.ajax({
            url: "/api/Rooms/" + data.id,
            data: JSON.stringify(data),
            type: "PUT",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                toastr.success("Room has been Updated", "Server Respond");
                $('#tableRoom').DataTable().ajax.reload();
                $("#RoomModal").modal('hide');
                document.getElementById('btnSaveRoom').innerText = "Add New";
                DisableControlRoom();
                ClearControlRoom();
            },
            error: function (errormesage) {
                toastr.error("Room hasn't Updated in Database", "Server Respond")
            }
        });
    }
}

function OnEditRoom(id) {
    EnableControlRoom();
    document.getElementById('btnSaveRoom').innerText = "Update";

    $.ajax({
        url: "/api/Rooms/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            $('#Roomid').val(result.id);

            $("#roomno").val(result.room_no);
            $("#roomtypeid").val(result.roomtypeid);
            $("#floorid").val(result.floorid);
            $("#servicecharge").val(result.servicecharge);
            $("#price").val(result.price);
            $("#roomkey").val(result.roomkey);
            $("#status").val(result.status);
        },
        error: function (errormessage) {
            toastr.error("No Record Select!", "Service Response");
        }
    });
}

//function OnDeleteRoom(id) {
//    bootbox.confirm({
//        title: "",
//        message: "Are you sure want to delete this?",
//        button: {
//            cancel: {
//                label: "Cancel",
//                ClassName: "btn-default",
//            },
//            confirm: {
//                label: "Delete",
//                ClassName: "btn-danger"
//            }
//        },
//        callback: function (result) {
//            if (result) {
//                $.ajax({
//                    url: "/api/Rooms/" + id,
//                    type: "DELETE",
//                    contentType: "application/json;charset=utf-8",
//                    datatype: "json",
//                    success: function (result) {
//                        $('#tableRoom').DataTable().ajax.reload();
//                        toastr.success("Room Deleted successfully!", "Service Response");
//                    },
//                    error: function (errormessage) {
//                        toastr.error("Room Can't be Deleted", "Service Response");
//                    }
//                });
//            }
//        }
//    });
//}


function DisableControlRoom() {
    document.getElementById('roomno').disabled = true;
    document.getElementById('roomtypeid').disabled = true;
    document.getElementById('floorid').disabled = true;
    document.getElementById('price').disabled = true;
    document.getElementById('servicecharge').disabled = true;
    document.getElementById('roomkey').disabled = true;
    document.getElementById('status').disabled = true;
}

function EnableControlRoom() {
    document.getElementById('roomno').disabled = false;
    document.getElementById('roomtypeid').disabled = false;
    document.getElementById('floorid').disabled = false;
    document.getElementById('price').disabled = false;
    document.getElementById('servicecharge').disabled = false;
    document.getElementById('roomkey').disabled = false;
    document.getElementById('status').disabled = false;

}

//function ClearControlRoom() {
//    $('#roomno').val('');
//    $('#price').val('');
//    $('#servicecharge').val('');
//    $('#roomkey').val('');

//}

//function AddnewRoomAction() {
//    document.getElementById('btnSaveRoom').innerText = "Save";
//    //DisableControlRoom();
//    ClearControlRoom();
//}

//function ValidationForm() {
//    var isValid = true;
//    if ($('#roomno').val().trim() === "") {
//        $('#roomno').css('border-color', 'red');
//        $('#roomno').focus();
//        alert('Please enter room no');
//        //toastr.info("Please enter room no", "Required");
//        isValid = false;
//    }
//    else {
//        $('#roomno').css('border-color', '#cccccc');
//        if ($('#price').val().trim() === "") {
//            $('#price').css('border-color', 'red');
//            $('#price').focus();
//            alert('Please enter your price');
//            //toastr.info("Please enter your price", "Required");
//            isValid = false;
//        }
//        else {
//            $('#price').css('border-color', '#cccccc');
//            if ($('#roomkey').val().trim() === "") {
//                $('#roomkey').css('border-color', 'red');
//                $('#roomkey').focus();
//                alert('Please enter your room key');
//                //toastr.info("Please enter your room key", "Required");
//                isValid = false;
//            }
//            else {
//                $('#roomkey').css('border-color', '#cccccc');
//            }
//        }
           
//    }
//    return isValid;
//}


