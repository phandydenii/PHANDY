$(document).ready(function () {
    $(document).ajaxStart(function () {
        $('#loadingGif').addClass('show');
    }).ajaxStop(function () {
        $('#loadingGif').removeClass('show');
    });
    GetOtherExpense('', '');
})

function PreviewOtherExpendList() {
    var todate = $('#todate').val();
    var fromdate = $('#fromdate').val();
    GetOtherExpense(fromdate, todate);
}

var tableEmployee = [];
toastr.optionsOverride = 'positionclass = "toast-bottom-right"';
toastr.options.positionClass = 'toast-bottom-right';
toastr.options.progressBar = true;

function GetOtherExpense(fromdate, todate) {
    if (fromdate == "" && todate == "") {
        tableEmployee = $('#tableOtherExpense').DataTable({
            ajax: {
                url: "/api/OtherExpense",
                dataSrc: ""
            },
            columns: [
                    {
                        data: "id"
                    },
                    {
                        data: "date",
                        render: function (data) {
                            return moment(new Date(data)).format('DD-MMM-YYYY');
                        }
                    },
                    {
                        data: "expenseTypes.typename"
                    },
                    {
                        data: "amount"
                    },

                    {
                        data: "note"
                    },
                    {
                        data: "id",
                        render: function (data) {
                            return "<button onclick='OtherExpenseEdit(" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px;'​><span class='glyphicon glyphicon-edit'></span></button>"
                                + "<button onclick='OtherExpenseDelete(" + data + ")' class='btn btn-danger btn-xs' ><span class='glyphicon glyphicon-trash'></span></button>";
                        }
                    },
            ],
            destroy: true,
            "order": [0, "desc"],
            "info": false
        });
    } else {
        tableEmployee = $('#tableOtherExpense').DataTable({
            ajax: {
                url: "/api/OtherExpense/" + fromdate + "/" + todate,
                dataSrc: ""
            },
            columns: [
                    {
                        data: "id"
                    },
                    {
                        data: "date",
                        render: function (data) {
                            return moment(new Date(data)).format('DD-MMM-YYYY');
                        }
                    },
                    {
                        data: "expenseTypes.typename"
                    },
                    {
                        data: "amount"
                    },

                    {
                        data: "note"
                    },
                    {
                        data: "id",
                        render: function (data) {
                            return "<button onclick='OtherExpenseEdit(" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px;'​>Edit</button>" + "<button onclick='OtherExpenseDelete(" + data + ")' class='btn btn-danger btn-xs' >Delete</button>";
                        }
                    },
            ],
            destroy: true,
            "order": [0, "desc"],
            "info": false
        });
    }
    
}


function OtherExpenseEdit(id) {
    document.getElementById('btnAddOtherExpense').style.display = "none";
    document.getElementById('btnSaveOtherExpense').style.display = "none";
    document.getElementById('btnUpdateOtherExpense').style.display = "block";

    document.getElementById('date').disabled = false;
    document.getElementById('expensetypeid').disabled = false;
    document.getElementById('amount').disabled = false;
    document.getElementById('note').disabled = false;

    $.ajax({
        url: "/api/OtherExpense/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            $('#id').val(result.id);
            var pd = moment(result.date).format("YYYY-MM-DD");
            $('#date').val(pd);
            $('#expensetypeid').val(result.expensetypeid);
            $("#amount").val(result.amount);
            $('#note').val(result.note);
            $('#file_old').val(result.image);
            if (result.image == "") {
                $('#image').attr('src', '../Images/invoice-icon.png');
            } else {
                $('#image').attr('src', '../Images/' + result.image);
            }
            $('#otherexpenseModel').modal('show');
        },
        error: function (errormessage) {
            toastr.error("No Record Select!", "Service Response");
        }
    });

}

function OtherExpenseDelete(id) {
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
                    url: "/api/OtherExpense/" + id,
                    type: "DELETE",
                    contentType: "application/json;charset=utf-8",
                    datatype: "json",
                    success: function (result) {
                        tableEmployee.ajax.reload();
                        toastr.success("OtherExpense has been Deleted successfully!", "Service Response");
                    },
                    error: function (errormessage) {
                        toastr.error("This OtherExpense is already exists in Database", "Service Response");
                    }
                });
            }
        }
    });
}

function ClickAddnewOtherExpense() {
    $('#otherexpenseModel').modal('show');
    document.getElementById('date').disabled = true;
    document.getElementById('expensetypeid').disabled = true;
    document.getElementById('amount').disabled = true;
    document.getElementById('note').disabled = true;
}