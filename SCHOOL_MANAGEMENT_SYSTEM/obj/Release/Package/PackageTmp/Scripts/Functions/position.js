$(document).ready(function () {

    $(document).ajaxStart(function () {
        $('#loadingGif').addClass('show');
    }).ajaxStop(function () {
        $('#loadingGif').removeClass('show');
    });

    $('#positionModel').on('show.bs.modal', function () {

        GetPosition();
        document.getElementById('positionname').disabled = true;
        document.getElementById('btnPosition').innerText = "Add New";

    })
})

var tablePosition = [];
toastr.optionsOverride = 'positionclass = "toast-bottom-right"';
toastr.options.positionClass = 'toast-bottom-right';

function GetPosition() {
    tablePosition = $('#positionTable').DataTable({
        ajax: {
            url: "/api/position",
            dataSrc: ""
        },
        columns: [
            //{
            //    data: "id"
            //},
            {
                data: "positionname"
            },
            {
                data: "id",
                render: function (data) {
                    return "<button onclick='PositionEdit(" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px;'>Edit</button>" + "<button onclick='PositionDelete(" + data + ")' class='btn btn-danger btn-xs'>Delete</button>";
                }
            }
        ],
        destroy: true,
        "order": [0, "desc"],
        "info": false
    });
}

function PositionAction() {
    var action = '';
    action = document.getElementById('btnPosition').innerText;
    if (action == "Add New") {
        document.getElementById('btnPosition').innerText = "Save";
        document.getElementById('positionname').disabled = false;
        $('#positionname').val('');
        $('#positionname').focus();

    } else if (action == "Save") {
        if ($('#positionname').val().trim() == "") {
            $('#positionname').css('border-color', 'red');
            $('#positionname').focus();
            toastr.info('Please enter Position Name.', "Server Response")
        } else {
            $('#positionname').css('border-color', '#cccccc');

            var dataSave = {
                positionname: $('#positionname').val(),
            };

            $.ajax({
                url: "/api/position",
                data: JSON.stringify(dataSave),
                type: "POST",
                contentType: "application/json;charset=utf-8",
                datatype: "json",
                success: function (result) {
                    toastr.success("Position has been created successfully.", "Server Response");
                    tablePosition.ajax.reload();
                    document.getElementById('btnPosition').innerText = "Add New";
                    $('#positionname').val('');
                    document.getElementById('positionname').disabled = true;
                },
                error: function (errormessage) {
                    toastr.error("This Position is already exists in Database", "Service Response");
                }
            })
        }
    } else if (action == "Update") {
        if ($('#positionname').val().trim() == "") {
            $('#positionname').css('border-color', 'red');
            $('#positionname').focus();
            toastr.info('Please enter position .', "Server Response")
        } else {
            $('#positionname').css('border-color', '#cccccc');

            var data = {
                id: $('#positionid').val(),
                positionname: $('#positionname').val(),
            };

            //console.log(data);

            $.ajax({
                url: "/api/position/" + data.id,
                data: JSON.stringify(data),
                type: "PUT",
                contentType: "application/json;charset=utf-8",
                datatype: "json",
                success: function (result) {
                    toastr.success("Position has been update successfully.", "Server Response");
                    tablePosition.ajax.reload();
                    document.getElementById('btnPosition').innerText = "Add New";
                    $('#positionname').val('');
                    document.getElementById('positionname').disabled = true;
                },
                error: function (errormessage) {
                    toastr.error("This Position is already exists in Database", "Service Response");
                }
            })
        }
    }

}

function PositionEdit(id) {
    $.ajax({
        url: "/api/position/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            $('#positionid').val(result.id);
            $('#positionname').val(result.positionname);
            
            document.getElementById('btnPosition').innerText = "Update";
            document.getElementById('positionname').disabled = false;
            $('#positionname').focus();
        },
        error: function (errormessage) {
            toastr.error("This Position is already exists in Database", "Service Response");
        }
    });

}

function PositionDelete(id) {
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