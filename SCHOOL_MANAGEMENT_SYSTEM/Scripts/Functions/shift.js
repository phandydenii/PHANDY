$(document).ready(function () {

    $(document).ajaxStart(function () {
        $('#loadingGif').addClass('show');
    }).ajaxStop(function () {
        $('#loadingGif').removeClass('show');
    });

    $('#shiftModel').on('show.bs.modal', function () {

        GetShift();
    })
})

var tableBranch = [];
toastr.optionsOverride = 'positionclass = "toast-bottom-right"';
toastr.options.positionClass = 'toast-bottom-right';

function GetShift() {
    tableBranch = $('#shiftTable').DataTable({
        ajax: {
            url: "/api/Shifts",
            dataSrc: ""
        },
        columns: [
            //{
            //    data: "shiftid"
            //},
            {
                data: "shiftname"
            },
            //{
            //    data: "shiftstatus"
            //},
            {
                data: "shiftid",
                render: function (data) {
                    return "<button onclick='shiftEdit(" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px;'>Edit</button>" + "<button onclick='shiftDelete(" + data + ")' class='btn btn-danger btn-xs'>Delete</button>";
                }
            }
        ],
        destroy: true,
        "order": [0, "desc"],
        "info": false
    });
}

function ShiftAction() {
    var action = '';
    action = document.getElementById('btnShift').innerText;
    if (action == "Add New") {
        document.getElementById('btnShift').innerText = "Save";
        document.getElementById('shiftname').disabled = false;
        $('#shiftname').focus();

    } else if (action == "Save") {
        if ($('#shiftname').val().trim() == "") {
            $('#shiftname').css('border-color', 'red');
            $('#shiftname').focus();
            toastr.info('Please enter Shift!', "Server Response")
        } else {
            $('#shiftname').css('border-color', '#cccccc');

            var dataSave = {
                shiftname: $('#shiftname').val(),
                shiftstatus: $('#shiftstatus').val()
            };

            $.ajax({
                url: "/api/Shifts",
                data: JSON.stringify(dataSave),
                type: "POST",
                contentType: "application/json;charset=utf-8",
                datatype: "json",
                success: function (result) {
                    toastr.success("Shift has been created successfully.", "Server Response");
                    tableBranch.ajax.reload();
                    document.getElementById('btnShift').innerText = "Add New";
                    $('#shiftname').val('');
                    document.getElementById('shiftname').disabled = true;
                },
                error: function (errormessage) {
                    toastr.error("This Shift is already exists in Database", "Service Response");
                }
            })
        }
    } else if (action == "Update") {
        if ($('#shiftname').val().trim() == "") {
            $('#shiftname').css('border-color', 'red');
            $('#shiftname').focus();
            toastr.info('Please enter Shift !', "Server Response")
        } else {
            $('#shiftname').css('border-color', '#cccccc');

            var data = {
                shiftid: $('#shiftid').val(),
                shiftname: $('#shiftname').val(),
                shiftstatus: $('shiftstatus').val()

            };

            //console.log(data);

            $.ajax({
                url: "/api/Shifts/" + data.shiftid,
                data: JSON.stringify(data),
                type: "PUT",
                contentType: "application/json;charset=utf-8",
                datatype: "json",
                success: function (result) {
                    toastr.success("Shift has been update successfully.", "Server Response");
                    tableBranch.ajax.reload();
                    document.getElementById('btnShift').innerText = "Add New";
                    $('#shiftname').val('');
                    $('#shiftstauts').val('');
                    document.getElementById('shiftname').disabled = true;
                    document.getElementById('shiftstatus').disabled = true;
                },
                error: function (errormessage) {
                    toastr.error("This Shift is already exists in Database", "Service Response");
                }
            })
        }
    }

}

function shiftEdit(id) {
    $.ajax({
        url: "/api/Shifts/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            $('#shiftid').val(result.shiftid);
            $('#shiftname').val(result.shiftname);
            $('#shiftstatus').val(result.shiftstatus);

            document.getElementById('btnShift').innerText = "Update";
            document.getElementById('shiftname').disabled = false;
            document.getElementById('shiftstatus').disabled = false;
            $('#shiftname').focus();
        },
        error: function (errormessage) {
            toastr.error("This Shift is already exists in Database", "Service Response");
        }
    });

}

function shiftDelete(id) {
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
                    url: "/api/Shifts/" + id,
                    type: "DELETE",
                    contentType: "application/json;charset=utf-8",
                    datatype: "json",
                    success: function (result) {
                        tableBranch.ajax.reload();
                        toastr.success("Shift has been Deleted successfully!", "Service Response");
                    },
                    error: function (errormessage) {
                        toastr.error("This Shift is already exists in Database", "Service Response");
                    }
                });
            }
        }
    });
}