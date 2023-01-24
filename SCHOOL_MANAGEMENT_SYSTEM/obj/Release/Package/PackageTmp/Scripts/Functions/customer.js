$(document).ready(function () {

    $(document).ajaxStart(function () {
        $('#loadingGif').addClass('show');
    }).ajaxStop(function () {
        $('#loadingGif').removeClass('show');
    });

    GetCustomer("all");
    $('#displayshowroom').on('change', function () {
        var departmentid = this.value;
        if (departmentid == "---Select Showroom----") {
            GetCustomer("all");
        } else {
            //alert(departmentid);
            GetCustomer(departmentid);
        }
    })
    //GetBankAccount();
    //GetCustomer();

    //$('#customerModel').on('show.bs.modal', function () {
    //})
})

var tableEmployee = [];
toastr.optionsOverride = 'positionclass = "toast-bottom-right"';
toastr.options.positionClass = 'toast-bottom-right';

function GetCustomer(showroomId) {
    //alert(departmentId);
    tableEmployee = $('#tableCustomer').DataTable({
        ajax: {
            url: (showroomId == "all") ? "/api/Customer?showroomId=all" : "/api/Customer?showroomId=" + showroomId,
            dataSrc: ""
        },
        columns: [
                //{
                //    data: "id"
                //},
                 {
                     data: "name"
                 },
                 {
                     data: "sex"
                 },
                  {
                      data: "phone"
                  },
               {
                   data: "address"
               },
                {
                    data: "identityno"
                },
                //{
                //    data: "photo"
                //},

                {
                    data: "customertype"
                },
                //{
                //    data: "status"
                //},

                //{
                //    data: "showroomid"
                //},
            {
                data: "id",
                render: function (data) {
                    return "<button onclick='FeedBack(" + data + ")' class='btn btn-info btn-xs' style='margin-right:5px;'​>FeedBack</button>" +
                    "<button onclick='GetBankAccount(" + data + ")' class='btn btn-primary btn-xs' style='margin-right:5px;'​>BankAccount</button>" +
                    "<button onclick='CustomerEdit(" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px;'​>Edit</button>" +
                        "<button onclick='CustomerDelete(" + data + ")' class='btn btn-danger btn-xs' >Delete</button>";
                }
            },
        ],
        destroy: true,
        "order": [0, "desc"],
        "info": false
    });
}

function FeedBack(id) {
    $('#feedbackidtemp').val(id);
    GetFeedback();
    $('#FeedbackModel').modal('show');
}
function GetBankAccount(id) {
    $('#customeridtemp').val(id);
    BankAccount();
    $('#BankAccountModel').modal('show');
}

function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#photo').attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}

function CustomerAction() {
    var action = '';
    action = document.getElementById('btnCus').innerText;
    if (action == "Add New") {
        //$('#dob').val(moment().format('YYYY-MM-DD'));
        document.getElementById('btnCus').innerText = "Save";
        document.getElementById('name').disabled = false;
        document.getElementById('sex').disabled = false;
        document.getElementById('phone').disabled = false;
        document.getElementById('address').disabled = false;
        document.getElementById('identityno').disabled = false;
        document.getElementById('customertype').disabled = false;
        document.getElementById('showroomid').disabled = false;
        $('#photo').attr('src', '../Images/no_image.png');
        $("#name").focus();
    } else if (action == "Save") {
        var response = Validate();
        if (response == false) {
            return false;
        }
        var data = new FormData();
        var files = $("#file").get(0).files;
        if (files.length > 0) {
            data.append("photo", files[0]);
        }

        data.append("name", $("#name").val());
        data.append("sex", $("#sex").val());
        data.append("phone", $("#phone").val());
        data.append("address", $("#address").val());
        data.append("showroomid", $("#showroomid").val());
        data.append("identityno", $("#identityno").val());
        data.append("customertype", $("#customertype").val());
        
        //console.log(files[0]);

        $.ajax({
            type: "POST",
            url: "/api/Customer",
            contentType: false,
            processData: false,
            data: data,
            success: function (result) {

                //console.log(result);

                toastr.success("Customer has been created successfully.", "Server Response");
                tableEmployee.ajax.reload();

                $('#id').val(result.id);
                $('#customerModel').modal('hide');

                document.getElementById('btnCus').innerText = "Add New";
                DisableControl();
                ClearData();

            },
            error: function (error) {
                //console.log(error);
                toastr.error("Customer Already Exists!.", "Server Response");
            }
        });

        //maritalstatus

    } else if (action == "Update") {
        var response = Validate();
        if (response == false) {
            return false;
        }
        var data = new FormData();
        var files = $("#file").get(0).files;
        if (files.length > 0) {
            data.append("photo", files[0]);
        }
        data.append("id", $('#id'));
        data.append("name", $("#name").val());
        data.append("sex", $("#sex").val());
        data.append("phone", $("#phone").val());
        data.append("address", $("#address").val());
        data.append("identityno", $("#identityno").val());
        data.append("showroomid", $("#showroomid").val());
        data.append("customertype", $("#customertype").val());
        data.append("file_old", $("#file_old").val());

        console.log(data);

        $.ajax({
            type: "PUT",
            url: "/api/Customer/" + $('#id').val(),
            contentType: false,
            processData: false,
            data: data,
            success: function (result) {

                //console.log(result);

                toastr.success("Customer has been updated successfully.", "Server Response");
                tableEmployee.ajax.reload();

                //$('#employeeId').val(result.id);
                $('#customerModel').modal('hide');

                document.getElementById('btnCus').innerText = "Add New";
                DisableControl();
                ClearData();

            },
            error: function (error) {
                //console.log(error);
                toastr.error("Customer Already Exists!.", "Server Response");
            }
        });

    }
}

function CustomerEdit(id) {
    ClearData();
    EnableControl();
    document.getElementById('btnCus').innerText = "Update";

    $.ajax({
        url: "/api/Customer/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            $('#id').val(result.id);
            $('#name').val(result.name);
            $('#sex').val(result.sex);
            $("#phone").val(result.phone);
            $("#address").val(result.address);
            $('#identityno').val(result.identityno);
            $('#customertype').val(result.customertype).change();
            $('#showroomid').val(result.showroomid);
            $('#file_old').val(result.photo);
            //console.log(result);
            //alert(result.photo);
            if (result.photo == "") {
                $('#photo').attr('src', '../Images/no_image.png');
            } else {
                //alert(result.img);
                $('#photo').attr('src', '../Images/' + result.photo);
            }
            EnableControl();
            $('#customerModel').modal('show');
        },
        error: function (errormessage) {
            toastr.error("No Record Select!", "Service Response");
        }
    });

}

function CustomerDelete(id) {
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
                    url: "/api/Customer/" + id,
                    type: "DELETE",
                    contentType: "application/json;charset=utf-8",
                    datatype: "json",
                    success: function (result) {
                        tableEmployee.ajax.reload();
                        toastr.success("Customer has been Deleted successfully!", "Service Response");
                    },
                    error: function (errormessage) {
                        toastr.error("This Customer is already exists in Database", "Service Response");
                    }
                });
            }
        }
    });
}

function Validate() {
    var isValid = true;
    if ($('#name').val().trim() == "") {
        $('#name').css('border-color', 'red');
        $('#name').focus();
    } else {
        $('#name').css('border-color', '#cccccc');
        $('#empname').focus();
    }
    return isValid;
}

function ClickAddnewCustomer() {
    document.getElementById('name').disabled = true;
    document.getElementById('sex').disabled = true;
    document.getElementById('phone').disabled = true;
    document.getElementById('address').disabled = true;
    document.getElementById('identityno').disabled = true;
    document.getElementById('customertype').disabled = true;
    document.getElementById('showroomid').disabled = true;
    $('#name').val('');
    $('#sex').val('');
    $('#phone').val('');
    $('#address').val('');
    $('#identityno').val('');
    $('#customertype').val('');
    $('#showroomid').val('');
    $('#name').focus();
    $('#photo').attr('src', '../Images/no_image.png');
    document.getElementById('btnCus').innerText = "Add New";
}


//var tableBankAccounts = [];
//function GetBankAccount() {
    
//    tableBankAccount = $('#tableBankAccount').dataTable({
//        ajax: {
//            url: "/api/BankAccounts",
//            dataSrc: ""
//        },
//        columns:
//            [
//                {
//                    data: "date",
//                    render: function (data) {
//                        return moment(data).format("DD-MMM-YYYY");
//                    }
//                },
//                {
//                    data: "customer.name",
//                },
//                {
//                    data: "abaname",
//                },
//                {
//                    data: "abanumber",
//                },
//                {
//                    data: "id",
//                    render: function (data) {
//                        return "<button onclick='BankAccountEdit(" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px;'>Edit</button>" +
//                            "<button onclick='BankAccountDelete(" + data + ")' class='btn btn-danger btn-xs'>Delete</button>";
//                    }
//                }
//            ],
//        destroy: true,

//    });
   
//}
//function BankAccountAction() {
    
//    var action = '';
//    action = document.getElementById('btnSaveBankAccount').innerText;
//    //DisableControl();
//    if (action == "Add New") {
//        document.getElementById('btnSaveBankAccount').innerText = 'Save';

//        EnableControl();
//        ClearControl();
//        $('#bdate').focus().val(moment().format('YYYY-MM-DD'));
//    }

//    if (action === "Save") {
//        //Validate();
//        var data = {
//            date: $('#bdate').val(),
//            customerid: $('#customerid').val(),
//            abaname: $('#abaname').val(),
//            abanumber: $('#abanumber').val(),
//        };
//        $.ajax({
//            url: "/api/BankAccounts",
//            data: JSON.stringify(data),
//            type: "POST",
//            contentType: "application/json;charset=utf-8",
//            dataType: "json",
//            success: function (result) {
//                toastr.success("Save to database successfully.", "Server Response");
//                $('#tableBankAccount').DataTable().ajax.reload();
//                ClearControl();
//                document.getElementById('btnSaveBankAccount').innerText = "Add New";
//                //$('#BankAccountModel').modal('hide');
//                //document.getElementById('btnSaveBankAccount').innerHTML = "<span class='glyphicon glyphicon-plus-sign'></span> <span class='kh'>បង្កើតថ្មី</span> / Add New";

//            },
//            error: function (errormessage) {
//                toastr.error("This BankAccount is already exists.", "Server Response");
//                //document.getElementById('btnSaveBankAccount').innerHTML = "<span class='glyphicon glyphicon-plus-sign'></span> <span class='kh'>បង្កើតថ្មី</span> / Add New";

//            }
//        });
//    }
//    else if (action === "Update") {
//        //alert('hi');
//        var data = {
//            id: $('#id').val(),
//            date: $('#bdate').val(),
//            customerid: $('#customerid').val(),
//            abaname: $('#abaname').val(),
//            abanumber: $('#abanumber').val(),
//        };
//        $.ajax({
//            url: "/api/BankAccounts/" + data.id,
//            data: JSON.stringify(data),
//            type: "PUT",
//            contentType: "application/json;charset=utf-8",
//            dataType: "json",
//            success: function (result) {
//                toastr.success("Updated successfully.", "Server Response");
//                $('#tableBankAccount').DataTable().ajax.reload();
//                ClearControl();
//                document.getElementById('btnSaveBankAccount').innerText = "Add New";
//                //$('#BankAccountModel').modal('hide');
//            },
//            error: function (errormessage) {
//                toastr.error("This BankAccount can't Update.", "Server Response");

//            }
//        });
//    }
//}


//function BankAccountEdit(id) {
//    ClearControl();
//    EnableControl();

//    //$('#status').val('');
//    action = document.getElementById('btnSaveBankAccount').innerText = "Update";

//    $.ajax({
//        url: "/api/BankAccounts/" + id,
//        type: "GET",
//        contentType: "application/json;charset=UTF-8",
//        dataType: "json",
//        success: function (result) {

//            $('#id').val(result.id);

//            var date = new Date(result.date);
//            var datetime = moment(date).format('YYYY-MM-DD');
//            $('#bdate').val(datetime);
//            $('#customerid').val(result.customerid);
//            $('#abaname').val(result.abaname);
//            $('#abanumber').val(result.abanumber);
//        },
//        error: function (errormessage) {
//            toastr.error("Something unexpected happen.", "Server Response");
//        }
//    });
//    return false;
//}


//function BankAccountDelete(id) {
//    //alert('hi');
//    bootbox.confirm({
//        title: "",
//        message: "Are you sure want to delete this?",
//        button: {
//            cancel: {
//                label: "Cancel",
//                ClassName: "btn-default",
//            },
//            confirm: {
//                label: "Delete",
//                ClassName: "btn-danger"
//            }
//        },
//        callback: function (result) {
//            //alert(id);
//            if (result) {
//                $.ajax({
//                    url: "/api/BankAccounts/" + id,
//                    type: "DELETE",
//                    contentType: "application/json;charset=utf-8",
//                    datatype: "json",
//                    success: function (result) {
//                        $('#tableBankAccount').DataTable().ajax.reload();

//                        toastr.success("BankAccount Deleted successfully!", "Service Response");
//                    },
//                    error: function (errormessage) {
//                        toastr.error("BankAccount Can't be Deleted", "Service Response");
//                    }
//                });
//            }
//        }
//    });
//}


//function DisableControl() {
//    document.getElementById('bdate').disabled = true;
//    document.getElementById('customerid').disabled = true;
//    document.getElementById('abaname').disabled = true;
//    document.getElementById('abanumber').disabled = true;
//}

//function EnableControl() {

//    document.getElementById('bdate').disabled = false;
//    document.getElementById('customerid').disabled = false;
//    document.getElementById('abaname').disabled = false;
//    document.getElementById('abanumber').disabled = false;
//}

//function ClearControl() {
//    $('#abaname').val('');
//    $('#abanumber').val('');
//    $('#customerid').val('');
//    $('#bdate').val('');

//}

//function ClickAddnewBankAccount() {
//    document.getElementById('btnSaveBankAccount').innerText = "Add New";
//    DisableControl();
//    ClearControl();
//}

//function Validate() {
//    var isValid = true;
//    var formAddEdit = $("#formTrainingProgramAdd");
//    if ($('#BankAccountTittle').val().trim() === "") {
//        $('#BankAccountTittle').css('border-color', 'red');
//        $('#BankAccountTittle').focus();
//        toastr.info("Please enter BankAccount title", "Required");
//    }
//    else {
//        $('#BankAccountTittle').css('border-color', '#cccccc');
//        if ($('#createBy').val().trim() === "") {
//            $('#createBy').css('border-color', 'red');
//            $('#createBy').focus();
//            toastr.info("Please enter your phone", "Required");
//        }
//        else {
//            $('#createBy').css('border-color', '#cccccc');
//        }
//    }
//    return isValid;
//}


