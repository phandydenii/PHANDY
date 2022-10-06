
$(document).ready(function () {
    $('#FloorModal').on('show.bs.modal', function () {
        $(document).ajaxStart(function () {
            $('#loadingGif').addClass('show');
        }).ajaxStop(function () {
            $('#loadingGif').removeClass('show');
        });
        GetFloor();
    });
});

var tableFloor = [];
function GetFloor() {
    tableFloor = $('#dtFloor').dataTable({
        ajax: {
            url: "/api/Floors",
            dataSrc: ""
        },
        columns:
            [
                {
                    data: "id"
                },
                {
                    data: "floor_no"
                },
                {
                    data: "buildingname"
                },
                {
                    data: "id",
                    render: function (data) {
                        return "<button OnClick='FloorEdit (" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-edit'></span> Edit</button>" +
                               "<button OnClick='FloorDelete (" + data + ")' class='btn btn-danger btn-xs'><span class='glyphicon glyphicon-trash'></span> Delete</button>";
                    }
                }
            ],
        destroy: true,
        "order": [[0, "desc"]],
        "info": false

    });
}

//Save  
function FloorAction() {
    var action = '';
    action = document.getElementById('btnSaveFloor').innerText;

    if (action == "Add New") {
        document.getElementById('btnSaveFloor').innerText = 'Save';
        EnableControl();
        $('#floorno').focus();
    }
    else if (action === "Save") {
        var data = {
            floor_no: $('#floorno').val(),
            buildingid: $('#buildingid').val(),
            status: $('#status').val(),
        };

        $.ajax({
            url: "/api/Floors",
            data: JSON.stringify(data),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                toastr.success("New Floor has been Created", "Server Respond");
                $('#dtFloor').DataTable().ajax.reload();
                // $('#customerName').val('');
                //$("#FloorModal").modal('hide');
                document.getElementById('btnSaveFloor').innerText = "Add New";
                DisableControl();
                ClearControl();
            },
            error: function (errormesage) {
                $('#FloorName').focus();
                toastr.error("This Name is exist in Database", "Server Respond")
            }

        });

    } else if (action == "Update") {

        var data = {
            id: $('#Floorid').val(),

            floor_no: $('#floorno').val(),
            status: $('#status').val(),
        };
        $.ajax({
            url: "/api/Floors/" + data.id,
            data: JSON.stringify(data),
            type: "PUT",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                toastr.success("Floor has been Updated", "Server Respond");
                $('#dtFloor').DataTable().ajax.reload();
                //$("#FloorModal").modal('hide');
                document.getElementById('btnSaveFloor').innerText = "Add New";
                DisableControl();
                ClearControl();
            },
            error: function (errormesage) {
                toastr.error("Floor hasn't Updated in Database", "Server Respond")
            }
        });
    }
}

function FloorEdit(id) {
    
    //ClearControl();
    EnableControl();
    action = document.getElementById('btnSaveFloor').innerText = "Update";
    //alert('hi');
    $.ajax({
        url: "/api/Floors/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            $('#Floorid').val(result.id);
            $("#floorno").val(result.floor_no);
            $("#status").val(result.status);


            //$("#FloorModal").modal('show');
        },
        error: function (errormessage) {
            toastr.error("No Record Select!", "Service Response");
        }
    });

}

function FloorDelete(id) {
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
                    url: "/api/Floors/" + id,
                    type: "DELETE",
                    contentType: "application/json;charset=utf-8",
                    datatype: "json",
                    success: function (result) {
                        $('#dtFloor').DataTable().ajax.reload();
                        toastr.success("Floor Deleted successfully!", "Service Response");
                    },
                    error: function (errormessage) {
                        toastr.error("Floor Can't be Deleted", "Service Response");
                    }
                });
            }
        }
    });
}


function DisableControl() {
    document.getElementById('floorno').disabled = true;
    document.getElementById('buildingid').disabled = true;
    document.getElementById('status').disabled = true;


}

function EnableControl() {
    document.getElementById('floorno').disabled = false;
    document.getElementById('buildingid').disabled = false;
    document.getElementById('status').disabled = false;

}

function ClearControl() {
    $('#floorno').val('');
    $('#buildingid').val('');
    $('#status').val('');
}

function AddnewFloorAction() {
    document.getElementById('btnSaveFloor').innerText = "Add New";
    DisableControl();
    ClearControl();
}

function ValidationFormFloor() {
    var isValid = true;
    if ($('#Floor').val().trim() === "") {
        $('#Floor').css('border-color', 'red');
        $('#Floor').focus();
        alert('Please enter floor no');
        isValid = false;
    }
    else {
        $('#Floor').css('border-color', '#cccccc');
        if ($('#buildingid').val().trim() === "") {
            $('#buildingid').css('border-color', 'red');
            $('#buildingid').focus();
            alert('Please enter building');
            isValid = false;
        }
        else {
            $('#buildingid').css('border-color', '#cccccc');
        }
    }
    return isValid;
}

