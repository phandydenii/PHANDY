$(document).ready(function () {

    $(document).ajaxStart(function () {
        $('#loadingGif').addClass('show');
    }).ajaxStop(function () {
        $('#loadingGif').removeClass('show');
    });

    GetOtherExpense('', '');

    //$('#fromdate').on('change', function () {
    //    var fromdate = this.value;
    //    var todate = $('#todate').val();
    //    GetOtherExpense(fromdate, todate);

    //});
    //$('#todate').on('change', function () {
    //    var todate = this.value;
    //    var fromdate = $('#fromdate').val();
    //    GetOtherExpense(fromdate, todate);

    //});
})
function PreviewOtherExpendList() {
    var todate = $('#todate').val();
    var fromdate = $('#fromdate').val();
    GetOtherExpense(fromdate, todate);
}

var tableEmployee = [];
toastr.optionsOverride = 'positionclass = "toast-bottom-right"';
toastr.options.positionClass = 'toast-bottom-right';

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
                            return "<button onclick='OtherExpenseEdit(" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px;'​>Edit</button>" + "<button onclick='OtherExpenseDelete(" + data + ")' class='btn btn-danger btn-xs' >Delete</button>";
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
function CloseFrm() {
    window.location.reload(true);
}
function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#image').attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}

function OtherExpenseAction() {
    var action = '';
    action = document.getElementById('btnOtherExpense').innerText;
    if (action == "Add New") {
        $('#date').val(moment().format('YYYY-MM-DD'));
        document.getElementById('btnOtherExpense').innerText = "Save";
        document.getElementById('date').disabled = false;
        document.getElementById('expensetypeid').disabled = false;
        document.getElementById('amount').disabled = false;
        document.getElementById('note').disabled = false;
        $("#expensetypeid").focus();
        $("#amount").val(0);
    } else if (action == "Save") {
        var response = Validate();
        if (response == false) {
            return false;
        }
        var data = new FormData();
        data.append("date", $("#date").val());
        data.append("expensetypeid", $("#expensetypeid").val());
        data.append("amount", $("#amount").val());
        data.append("note", $("#note").val());
        var files = $("#file").get(0).files;
        if (files.length > 0) {
            data.append("image", files[0]);
        }
        $.ajax({
            type: "POST",
            url: "/api/OtherExpense",
            contentType: false,
            processData: false,
            data: data,
            success: function (result) {
                toastr.success("OtherExpense has been created successfully.", "Server Response");
                tableEmployee.ajax.reload();
                window.location.reload(true);
                $('#id').val(result.id);
                $('#otherexpenseModel').modal('hide');

                document.getElementById('btnOtherExpense').innerText = "Add New";
                $('#date').val('');
                $('#expensetypeid').val('');
                $('#amount').val('0.00');
                $('#note').val('');
                
            },
            error: function (error) {
                toastr.error("OtherExpense Already Exists!.", "Server Response");
            }
        });
    } else if (action == "Update") {
        var response = Validate();
        if (response == false) {
            return false;
        }
        var data = new FormData();
        data.append("id", $('#id'));
        data.append("date", $("#date").val());
        data.append("expensetypeid", $("#expensetypeid").val());
        data.append("amount", $("#amount").val());
        data.append("note", $("#note").val());
        var files = $("#file").get(0).files;
        if (files.length > 0) {
            data.append("image", files[0]);
        }
        $.ajax({
            type: "PUT",
            url: "/api/OtherExpense/" + $('#id').val(),
            contentType: false,
            processData: false,
            data: data,
            success: function (result) {
                toastr.success("OtherExpense has been updated successfully.", "Server Response");
                tableEmployee.ajax.reload();
                $('#otherexpenseModel').modal('hide');
                window.location.reload(true);
                document.getElementById('btnOtherExpense').innerText = "Add New";
                $('#date').val('');
                $('#expensetypeid').val('');
                $('#amount').val('0.00');
                $('#note').val('');

            },
            error: function (error) {
                toastr.error("OtherExpense Already Exists!.", "Server Response");
            }
        });

    }
}

function OtherExpenseEdit(id) {
    document.getElementById('btnOtherExpense').innerText = "Update";
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

            document.getElementById('date').disabled = false;
            document.getElementById('expensetypeid').disabled = false;
            document.getElementById('amount').disabled = false;
            document.getElementById('note').disabled = false;

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

function Validate() {
    var isValid = true;
    if ($('#amount').val().trim() == "") {
        $('#amount').css('border-color', 'red');
        $('#amount').focus();
        isValid = false;
    } else {
        $('#amount').css('border-color', '#cccccc');
        $('#amount').focus();
    }
    return isValid;
}

function ClickAddnewOtherExpense() {
    document.getElementById('date').disabled = true;
    document.getElementById('expensetypeid').disabled = true;
    document.getElementById('amount').disabled = true;
    document.getElementById('note').disabled = true;

    $('#amount').val('');
    $('#note').val('');
    $('#date').val('');
    $('#amount').focus();
    document.getElementById('btnOtherExpense').innerText = "Add New";
}
