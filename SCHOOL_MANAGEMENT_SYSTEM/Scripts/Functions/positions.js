function ManagePosition() {
    $("#PositionModal").modal('show');
    document.getElementById('btnPosition').style.display = 'block';
    document.getElementById('btnUpdatePosition').style.display = 'none';
}

$(document).ready(function () {
    $('#PositionModal').on('show.bs.modal', function () {
        GetPosition();
        document.getElementById('positionname').disabled = true;
        document.getElementById('positionnamekh').disabled = true;
    });
})

var tablePosition = [];
function GetPosition() {
    tablePosition = $('#positionTable').DataTable({
        ajax: {
            url: "/api/position",
            dataSrc: ""
        },
        columns: [
            {
                data: "id"
            },
            {
                data: "positionname"
            },
            {
                data: "positionnamekh"
            },
            {
                data: "id",
                render: function (data) {
                    return "<button onclick='PositionEdit(" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px;'><span class='glyphicon glyphicon-edit'></span></button>"
                        + "<button onclick='PositionDelete(" + data + ")' class='btn btn-danger btn-xs'><span class='glyphicon glyphicon-trash'></span></button>";
                }
            }
        ],
        destroy: true,
        "order": [0, "desc"],
        "info": false
    });
}



function PositionEdit(id) {
    document.getElementById("btnAddPosition").style.display = "none";
    document.getElementById("btnSavePosition").style.display = "none";
    document.getElementById("btnUpdatePosition").style.display = "block";
    $.ajax({
        url: "/api/position/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            $('#positionid').val(result.id);
            $('#positionname').val(result.positionname);
            $('#positionnamekh').val(result.positionnamekh);
            $('#status').val(result.status);

            document.getElementById('btnPosition').innerText = "Update";
            document.getElementById('positionname').disabled = false;
            document.getElementById('positionnamekh').disabled = false;
            $('#positionname').focus();
        },
        error: function (errormessage) {
            toastr.error("This Position is already exists in Database", "Service Response");
        }
    });

}


function PositionDelete(id) {
    alert(id);
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
                    url: "/api/position/" + id,
                    type: "DELETE",
                    contentType: "application/json;charset=utf-8",
                    datatype: "json",
                    success: function (result) {
                        tablePosition.ajax.reload();
                        toastr.success("Position has been Deleted successfully!", "Service Response");
                        document.getElementById('positionname').disabled = true;
                        document.getElementById('positionnamekh').disabled = true;
                        document.getElementById('btnPosition').innerText = "Add New";
                    },
                    error: function (errormessage) {
                        toastr.error("This Position cannot delete it already use in Database", "Service Response");
                    }
                });
            }
        }
    });
}

