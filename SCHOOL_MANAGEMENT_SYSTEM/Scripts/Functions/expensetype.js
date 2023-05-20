$(document).ready(function () {
    $(document).ajaxStart(function () {
        $('#loadingGif').addClass('show');
    }).ajaxStop(function () {
        $('#loadingGif').removeClass('show');
    });
    $('#expensetypeModel').on('show.bs.modal', function () {
        GetExpenseType();
        document.getElementById('typename').disabled = true;
    })
})

var tableDepartment = [];
toastr.optionsOverride = 'positionclass = "toast-bottom-right"';
toastr.options.positionClass = 'toast-bottom-right';
toastr.options.progressBar = true;

function GetExpenseType() {
    tableDepartment = $('#expensetypeTable').DataTable({
        ajax: {
            url: "/api/ExpenseType",
            dataSrc: ""
        },
        columns: [
            {
                data:"id"
            },
            {
                data: "typename"
            },
            {
                data: "id",
                render: function (data) {
                    return "<button onclick='expensetypeEdit(" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px;'><span class='glyphicon glyphicon-edit'></span></button>"
                        + "<button onclick='expensetypeDelete(" + data + ")' class='btn btn-danger btn-xs'><span class='glyphicon glyphicon-trash'></span></button>";
                }
            }
        ],
        destroy: true,
        "order": [0, "desc"],
        "info": false
    });
}

function expensetypeEdit(id) {
    document.getElementById('btnAddExpenseType').style.display = "none";
    document.getElementById('btnSaveExpenseType').style.display = "none";
    document.getElementById('btnUpdateExpenseType').style.display = "block";
    document.getElementById('typename').disabled = false;
    $('#typename').focus();
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