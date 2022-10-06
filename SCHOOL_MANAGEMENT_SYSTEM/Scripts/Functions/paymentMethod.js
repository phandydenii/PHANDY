$(document).ready(function () {
    GetPaymentMethod();
    //alert('hi');
});
var tablePaymentMethods = [];
function GetPaymentMethod() {
    tablePaymentMethod = $('#tablePaymentMethod').dataTable({
        ajax: {
            url: "/api/PaymentMethods",
            dataSrc: ""
        },
        columns:
            [
                {
                    data: "id",
                },                
                {
                    data: "methodname",
                },
                {
                    data: "accountno",
                },
                {
                    data: "accountname",
                },
                {
                    data: "id",
                    render: function (data) {
                        return "<button onclick='PaymentMethodEdit(" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px;'>Edit</button>" +
                            "<button OnClick='PaymentMethodDelete (" + data + ")' class='btn btn-default btn-xs' ><span class='glyphicon glyphicon-remove'></span> Delete</button>";
                    }
                }
            ],
        destroy: true,

    });
}
function PaymentMethodAction() {
    var action = '';
    action = document.getElementById('btnSavePaymentMethod').innerText;
    DisableControl();
    if (action == "Add New") {
        document.getElementById('btnSavePaymentMethod').innerText = 'Save';
        EnableControl();

        $('#methodName').focus();
    }

    if (action === "Save") {
        //Validate();
        var data = {
            methodname: $('#methodName').val(),
            accountno: $('#accountNo').val(),
            accountname: $('#accountName').val(),
        };
        $.ajax({
            url: "/api/PaymentMethods",
            data: JSON.stringify(data),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                toastr.success("Save to database successfully.", "Server Response");
                $('#tablePaymentMethod').DataTable().ajax.reload();

                $('#PaymentMethodModel').modal('hide');
                //document.getElementById('btnSavePaymentMethod').innerHTML = "<span class='glyphicon glyphicon-plus-sign'></span> <span class='kh'>បង្កើតថ្មី</span> / Add New";

            },
            error: function (errormessage) {
                toastr.error("This PaymentMethod is already exists.", "Server Response");
                //document.getElementById('btnSavePaymentMethod').innerHTML = "<span class='glyphicon glyphicon-plus-sign'></span> <span class='kh'>បង្កើតថ្មី</span> / Add New";

            }
        });
    }
    else if (action === "Update") {
        //alert('hi');
        var data = {
            id: $('#id').val(),
            methodname: $('#methodName').val(),
            accountno: $('#accountNo').val(),
            accountname: $('#accountName').val(),
        };
        $.ajax({
            url: "/api/PaymentMethods/" + data.id,
            data: JSON.stringify(data),
            type: "PUT",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                toastr.success("Updated successfully.", "Server Response");
                $('#tablePaymentMethod').DataTable().ajax.reload();

                $('#PaymentMethodModel').modal('hide');
                //tablePaymentMethods.ajax.reload();
                //$('#registerModal').modal('hide');

            },
            error: function (errormessage) {
                toastr.error("This PaymentMethod can't Update.", "Server Response");

            }
        });
    }
}


function PaymentMethodEdit(id) {
    ClearControl();
    EnableControl();

    //$('#status').val('');
    action = document.getElementById('btnSavePaymentMethod').innerText = "Update";
    
    $.ajax({
        url: "/api/PaymentMethods/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {

            $('#id').val(result.id);

            $('#methodName').val(result.methodname);
            $('#accountNo').val(result.accountno);
            $('#accountName').val(result.accountname);

            $('#PaymentMethodModel').modal('show');

        },
        error: function (errormessage) {
            toastr.error("Something unexpected happen.", "Server Response");
        }
    });
    return false;
}


function PaymentMethodDelete(id) {
    //alert('hi');
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
                    url: "/api/PaymentMethods/" + id,
                    type: "DELETE",
                    contentType: "application/json;charset=utf-8",
                    datatype: "json",
                    success: function (result) {
                        $('#tablePaymentMethod').DataTable().ajax.reload();

                        toastr.success("PaymentMethod Deleted successfully!", "Service Response");
                    },
                    error: function (errormessage) {
                        toastr.error("PaymentMethod Can't be Deleted", "Service Response");
                    }
                });
            }
        }
    });
}

function DisableControl() {
    document.getElementById('methodName').disabled = true;
    document.getElementById('accountNo').disabled = true;
    document.getElementById('accountName').disabled = true;
}

function EnableControl() {

    document.getElementById('methodName').disabled = false;
    document.getElementById('accountNo').disabled = false;
    document.getElementById('accountName').disabled = false;
}

function ClearControl() {
    $('#methodName').val('');
    $('#accountNo').val('');
    $('#accountName').val('');

}

function ClickAddnewPaymentMethod() {
    document.getElementById('btnSavePaymentMethod').innerText = "Add New";
    DisableControl();
    ClearControl();
}

function Validate() {
    var isValid = true;
    var formAddEdit = $("#formTrainingProgramAdd");
    if ($('#PaymentMethodTittle').val().trim() === "") {
        $('#PaymentMethodTittle').css('border-color', 'red');
        $('#PaymentMethodTittle').focus();
        toastr.info("Please enter PaymentMethod title", "Required");
    }
    else {
        $('#PaymentMethodTittle').css('border-color', '#cccccc');
        if ($('#createBy').val().trim() === "") {
            $('#createBy').css('border-color', 'red');
            $('#createBy').focus();
            toastr.info("Please enter your phone", "Required");
        }
        else {
            $('#createBy').css('border-color', '#cccccc');
        }
    }
    return isValid;
}