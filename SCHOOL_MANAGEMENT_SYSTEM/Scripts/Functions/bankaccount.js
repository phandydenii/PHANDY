
$(document).ready(function () {

    $(document).ajaxStart(function () {
        $('#loadingGif').addClass('show');
    }).ajaxStop(function () {
        $('#loadingGif').removeClass('show');
    });
    
   
    $('#BankAccountModel').on('show.bs.modal', function () {
    });
})

var tblBankAccounts = [];
function BankAccount() {
    var cusidtemp = $('#customeridtemp').val();
    tblBankAccount = $('#tblBankAccount').dataTable({
        ajax: {
            url: "/api/BankAccounts/" + cusidtemp,
            dataSrc: ""
        },
        columns:
            [
                {
                    data: "date",
                    render: function (data) {
                        return moment(data).format("DD-MMM-YYYY");
                    }
                },
                {
                    data: "customer.name",
                },
                {
                    data: "abaname",
                },
                {
                    data: "abanumber",
                },
                {
                    data: "id",
                    render: function (data) {
                        return "<button onclick='BankAccountEdit(" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px;'>Edit</button>" +
                            "<button onclick='BankAccountDelete(" + data + ")' class='btn btn-danger btn-xs'>Delete</button>";
                    }
                }
            ],
        destroy: true,

    });

}
function BankAccountAction() {
    
    var action = '';
    action = document.getElementById('btnSaveBankAccount').innerText;
    //DisableControl();
    if (action == "Add New") {
        document.getElementById('btnSaveBankAccount').innerText = 'Save';

        EnableControl();
        ClearControl();
        $('#bdate').focus().val(moment().format('YYYY-MM-DD'));
    }

    if (action === "Save") {
        //Validate();
        var data = {
            date: $('#bdate').val(),
            customerid: $('#customerid').val(),
            abaname: $('#abaname').val(),
            abanumber: $('#abanumber').val(),
        };
        $.ajax({
            url: "/api/BankAccounts",
            data: JSON.stringify(data),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                toastr.success("Save to database successfully.", "Server Response");
                $('#tblBankAccount').DataTable().ajax.reload();
                ClearControl();
                document.getElementById('btnSaveBankAccount').innerText = "Add New";
                //$('#BankAccountModel').modal('hide');
                //document.getElementById('btnSaveBankAccount').innerHTML = "<span class='glyphicon glyphicon-plus-sign'></span> <span class='kh'>បង្កើតថ្មី</span> / Add New";

            },
            error: function (errormessage) {
                toastr.error("This BankAccount is already exists.", "Server Response");
                //document.getElementById('btnSaveBankAccount').innerHTML = "<span class='glyphicon glyphicon-plus-sign'></span> <span class='kh'>បង្កើតថ្មី</span> / Add New";

            }
        });
    }
    else if (action === "Update") {
        //alert('hi');
        var data = {
            id: $('#id').val(),
            date: $('#bdate').val(),
            customerid: $('#customerid').val(),
            abaname: $('#abaname').val(),
            abanumber: $('#abanumber').val(),
        };
        $.ajax({
            url: "/api/BankAccounts/" + data.id,
            data: JSON.stringify(data),
            type: "PUT",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                toastr.success("Updated successfully.", "Server Response");
                $('#tblBankAccount').DataTable().ajax.reload();
                ClearControl();
                document.getElementById('btnSaveBankAccount').innerText = "Add New";
                //$('#BankAccountModel').modal('hide');
            },
            error: function (errormessage) {
                toastr.error("This BankAccount can't Update.", "Server Response");

            }
        });
    }
}


function BankAccountEdit(id) {
    ClearControl();
    EnableControl();

    //$('#status').val('');
    action = document.getElementById('btnSaveBankAccount').innerText = "Update";

    $.ajax({
        url: "/api/BankAccounts/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {

            $('#id').val(result.id);

            var date = new Date(result.date);
            var datetime = moment(date).format('YYYY-MM-DD');
            $('#bdate').val(datetime);
            $('#customerid').val(result.customerid);
            $('#abaname').val(result.abaname);
            $('#abanumber').val(result.abanumber);
        },
        error: function (errormessage) {
            toastr.error("Something unexpected happen.", "Server Response");
        }
    });
    return false;
}


function BankAccountDelete(id) {
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
                    url: "/api/BankAccounts/" + id,
                    type: "DELETE",
                    contentType: "application/json;charset=utf-8",
                    datatype: "json",
                    success: function (result) {
                        $('#tblBankAccount').DataTable().ajax.reload();

                        toastr.success("BankAccount Deleted successfully!", "Service Response");
                    },
                    error: function (errormessage) {
                        toastr.error("BankAccount Can't be Deleted", "Service Response");
                    }
                });
            }
        }
    });
}


function DisableControl() {
    document.getElementById('bdate').disabled = true;
    document.getElementById('customerid').disabled = true;
    document.getElementById('abaname').disabled = true;
    document.getElementById('abanumber').disabled = true;
}

function EnableControl() {

    document.getElementById('bdate').disabled = false;
    document.getElementById('customerid').disabled = false;
    document.getElementById('abaname').disabled = false;
    document.getElementById('abanumber').disabled = false;
}

function ClearControl() {
    $('#abaname').val('');
    $('#abanumber').val('');
    $('#customerid').val('');
    $('#bdate').val('');

}

function ClickAddnewBankAccount() {
    document.getElementById('btnSaveBankAccount').innerText = "Add New";
    DisableControl();
    ClearControl();
}

function Validate() {
    var isValid = true;
    var formAddEdit = $("#formTrainingProgramAdd");
    if ($('#BankAccountTittle').val().trim() === "") {
        $('#BankAccountTittle').css('border-color', 'red');
        $('#BankAccountTittle').focus();
        toastr.info("Please enter BankAccount title", "Required");
    }
    else {
        $('#BankAccountTittle').css('border-color', '#cccccc');
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
