$(document).ready(function () {

    $(document).ajaxStart(function () {
        $('#loadingGif').addClass('show');
    }).ajaxStop(function () {
        $('#loadingGif').removeClass('show');
    });

    $('#expensetypeModel').on('show.bs.modal', function () {

        GetExpenseType();
        document.getElementById('typename').disabled = true;
        document.getElementById('btnExpenseType').innerText = "Add New";

    })
})

var tableDepartment = [];
toastr.optionsOverride = 'positionclass = "toast-bottom-right"';
toastr.options.positionClass = 'toast-bottom-right';

function GetExpenseType() {
    //alert('Hello');
    tableDepartment = $('#expensetypeTable').DataTable({
        ajax: {
            url: "/api/ExpenseType",
            dataSrc: ""
        },
        columns: [
            //{
            //    data:"id"
            //},
            {
                data: "typename"
            },
            {
                data: "id",
                render: function (data) {
                    return "<button onclick='expensetypeEdit(" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px;'>Edit</button>" + "<button onclick='expensetypeDelete(" + data + ")' class='btn btn-danger btn-xs'>Delete</button>";
                }
            }
        ],
        destroy: true,
        "order": [0, "desc"],
        "info": false
    });
}
function CloseExpenseType() {
    window.location.reload(true);
}
function ExpenseTypeAction() {
    var action = '';
    action = document.getElementById('btnExpenseType').innerText;
    if (action == "Add New") {
        document.getElementById('btnExpenseType').innerText = "Save";
        document.getElementById('typename').disabled = false;
        $('#typename').focus();

    } else if (action == "Save") {
        if ($('#typename').val().trim() == "") {
            $('#typename').css('border-color', 'red');
            $('#typename').focus();
            toastr.info('Please enter Department Name.', "Server Response")
        } else {
            $('#typename').css('border-color', '#cccccc');

            var dataSave = {
                typename: $('#typename').val()
            };

            $.ajax({
                url: "/api/ExpenseType",
                data: JSON.stringify(dataSave),
                type: "POST",
                contentType: "application/json;charset=utf-8",
                datatype: "json",
                success: function (result) {
                    toastr.success("ExpenseType has been created successfully.", "Server Response");
                    tableDepartment.ajax.reload();
                    document.getElementById('btnExpenseType').innerText = "Add New";
                    $('#typename').val('');
                    document.getElementById('typename').disabled = true;
                },
                error: function (errormessage) {
                    toastr.error("This ExpenseType is already exists in Database", "Service Response");
                }
            })
        }
    } else if (action == "Update") {
        if ($('#typename').val().trim() == "") {
            $('#typename').css('border-color', 'red');
            $('#typename').focus();
            toastr.info('Please enter Category Name.', "Server Response")
        } else {
            $('#typename').css('border-color', '#cccccc');

            var data = {
                id: $('#idtype').val(),
                typename: $('#typename').val()
            };

            //console.log(data);

            $.ajax({
                url: "/api/ExpenseType/" + data.id,
                data: JSON.stringify(data),
                type: "PUT",
                contentType: "application/json;charset=utf-8",
                datatype: "json",
                success: function (result) {
                    toastr.success("ExpenseType has been update successfully.", "Server Response");
                    tableDepartment.ajax.reload();
                    document.getElementById('btnExpenseType').innerText = "Add New";
                    $('#typename').val('');
                    document.getElementById('typename').disabled = true;
                },
                error: function (errormessage) {
                    toastr.error("This ExpenseType is already exists in Database", "Service Response");
                }
            })
        }
    }

}

function expensetypeEdit(id) {
    $.ajax({
        url: "/api/ExpenseType/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            $('#idtype').val(result.id);
            $('#typename').val(result.typename);
            document.getElementById('btnExpenseType').innerText = "Update";
            document.getElementById('typename').disabled = false;
            $('#typename').focus();
        },
        error: function (errormessage) {
            toastr.error("This ExpenseType is already exists in Database", "Service Response");
        }
    });

}

function expensetypeDelete(idtype) {
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
                    url: "/api/ExpenseType/" + idtype,
                    type: "DELETE",
                    contentType: "application/json;charset=utf-8",
                    datatype: "json",
                    success: function (result) {
                        tableDepartment.ajax.reload();
                        toastr.success("ExpenseType has been Deleted successfully!", "Service Response");
                        document.getElementById('typename').disabled = true;
                        document.getElementById('btnExpenseType').innerText = "Add New";
                    },
                    error: function (errormessage) {
                        toastr.error("This ExpenseType cannot delete it already use in Database", "Service Response");
                    }
                });
            }
        }
    });
}