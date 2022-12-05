
$(document).ready(function () {
    $('#FloorModal').on('show.bs.modal', function () {
        $(document).ajaxStart(function () {
            $('#loadingGif').addClass('show');
        }).ajaxStop(function () {
            $('#loadingGif').removeClass('show');
        });
        GetFloor();
        DisableFloorControl();
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
        EnableFloorControl();
        $('#floorno').focus();
    }
    else if (action === "Save") {
        var res = ValidationFormFloor();
        if (res == false) {
            return false;
        }


        var data = {
            floor_no: $('#floorno').val(),
            buildingid: $('#buildingid').val(),
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
                DisableFloorControl();
                ClearFloorControl();
            },
            error: function (errormesage) {
                $('#FloorName').focus();
                toastr.error("This Name is exist in Database", "Server Respond")
            }

        });

    } else if (action == "Update") {
        var res = ValidationFormFloor();
        if (res == false) {
            return false;
        }

        var data = {
            id: $('#Floorid').val(),
            floor_no: $('#floorno').val(),
            buildingid: $('#buildingid').val(),
        };
        $.ajax({
            url: "/api/Floors/" + data.id,
            data: JSON.stringify(data),
            type: "PUT",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                toastr.success("Update record successully!", "Server Respond");
                $('#dtFloor').DataTable().ajax.reload();
                document.getElementById('btnSaveFloor').innerText = "Add New";
                DisableFloorControl();
                ClearFloorControl();
            },
            error: function (errormesage) {
                toastr.error("Update record faild!", "Server Respond")
            }
        });
    }
}

function FloorEdit(id) {
    EnableFloorControl();
    action = document.getElementById('btnSaveFloor').innerText = "Update";
    $.ajax({
        url: "/api/floors/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            $('#Floorid').val(result.id);
            $("#floorno").val(result.floor_no);
            $("#buildingid").val(result.buildingid);
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
        message: "<h4>Do you want to delete record '" + id + "' ?</h4>",
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
                });s
            }
        }
    });
}


function DisableFloorControl() {
    document.getElementById('floorno').disabled = true;
    document.getElementById('buildingid').disabled = true;
}

function EnableFloorControl() {
    document.getElementById('floorno').disabled = false;
    document.getElementById('buildingid').disabled = false;
}

function ClearFloorControl() {
    $('#floorno').val('');
    $('#buildingid').val('');
    $('#status').val('');
}

function AddnewFloorAction() {
    document.getElementById('btnSaveFloor').innerText = "Add New";
    DisableFloorControl();
    ClearFloorControl();
}

function ValidationFormFloor() {
    var isValid = true;
    if ($('#floorno').val().trim() === "") {
        $('#floorno').css('border-color', 'red');
        $('#floorno').focus();
        isValid = false;
    }
    else {
        $('#floorno').css('border-color', '#cccccc');
    }
    return isValid;
}


function OnCloseFloor() {
    window.location.reload(true);
}


