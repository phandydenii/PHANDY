
function AddSalary() {
    $("#StaffSalaryModal").modal('show');
    $('#btnSaveManageSalary').show();
    $('#btnUpdateSalary').hide();
}

$(document).ready(function () {
    $(document).ajaxStart(function () {
        $('#loadingGif').addClass('show');
    }).ajaxStop(function () {
        $('#loadingGif').removeClass('show');
    });
    GetSalary();
})

var tableSalary = [];
//toastr.options.positionClass = 'toast-top-center';
toastr.options.progressBar = true;
function GetSalary() {
    //alert("Hello");
    tableSalary = $('#tableSalary').DataTable({
        ajax: {
            url: "/api/salaries",
            dataSrc: ""
        },
        columns: [

            {
                data: "id"
            },
            {
                data: "staff.name"
            },
            {
                data: "staff.namekh"
            },
            {
                data: "salary"
            },
            {
                data: "id",
                render: function (data) {
                    return "<button onclick='SalaryEdit(" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px;'><span class='glyphicon glyphicon-edit'></span> Edit</button>"
                        + "<button onclick='SalaryDelete(" + data + ")' class='btn btn-danger btn-xs'><span class='glyphicon glyphicon-trash'></span> Delete</button>";
                }
            }
        ],
        destroy: true,
        "order": [0, "desc"],
        "info": false
    });
}

function SaveManageSalary() {
    if ($('#staffid').val() == "") {
        $('#staffid').focus();
        return false;
    }
    var data = {
        staffid: $('#staffid').val(),
        salary: $('#salary').val(),
        note: $('#note').val(),
    };
    $.ajax({
        url: "/api/salaries",
        data: JSON.stringify(data),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            toastr.success("Insert record successfully!", "Server Respond");
            window.location.reload(true);

        },
        error: function (errormesage) {
            $('#itemcharge').focus();
            toastr.error("Inser record faild!", "Server Respond")
        }
    });
}

function UpdateSalaryd() {
    if($('#staffid').val() == ""){
        $('#staffid').focus();
        return false;
    }

    var data = new FormData();
    data.append("staffid", $("#staffid").val());
    data.append("salary", $("#salary").val());
    data.append("note", $("#note").val());
    $.ajax({
        type: "PUT",
        url: "/api/salaries/" + $('#salaryid').val(),
        contentType: false,
        processData: false,
        data: data,
        success: function (result) {
            toastr.success("Insert record successfully.", "Server Response");
            window.location.reload(true);
        },
        error: function (error) {
            console.log(error);
            toastr.error("Record Already Exists!.", "Server Response");
        }
    });
}

function SalaryEdit(id) {
    $("#StaffSalaryModal").modal('show');
    $('#btnSaveManageSalary').hide();
    $('#btnUpdateSalary').show();
    $.ajax({
        url: "/api/salaries/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
           // alert(result.id);
            $('#salaryid').val(result.id);
            $('#staffid').val(result.staffid);
            $("#salary").val(result.salary);
            $("#note").val(result.note);
        },
        error: function (errormessage) {
            toastr.error("No Record Select!", "Service Response");
        }
    });
}

function SalaryDelete(id) {
    bootbox.confirm({
        title: "",
        message: "<h3>Are you sure want to delete record " + id + " ?</h3>",
        buttons: {
            confirm: {
                label: 'Yes',
                className: 'btn-success btn-sm'
            },
            cancel: {
                label: 'No',
                className: 'btn-danger btn-sm'
            }
        },
        callback: function (result) {
            if (result) {
                $.ajax({
                    url: "/api/salaries/" + id,
                    type: "DELETE",
                    contentType: "application/json;charset=utf-8",
                    datatype: "json",
                    success: function (result) {
                        toastr.success("Record delete successfully!", "Service Response");
                        window.location.reload(true);
                    },
                    error: function (errormessage) {
                        toastr.error("Record delte faild!", "Service Response");
                    }
                });
            }
        }
    });
}

function CloseStaffSalary() {
    window.location.reload(true);
}
