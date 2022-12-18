$(document).ready(function () {

    $(document).ajaxStart(function () {
        $('#loadingGif').addClass('show');
    }).ajaxStop(function () {
        $('#loadingGif').removeClass('show');
    });
    GetSalary();

})

var tableSalary = [];
toastr.optionsOverride = 'positionclass = "toast-bottom-right"';
toastr.options.positionClass = 'toast-bottom-right';

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
                    return "<button onclick='SalaryEdit(" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px;'>Edit</button>"
                        +  "<button onclick='SalaryDelete(" + data + ")' class='btn btn-danger btn-xs'>Delete</button>";
                }
            }
        ],
        destroy: true,
        "order": [0, "desc"],
        "info": false
    });
}

function SalaryEdit(id) {
    $("#StaffSalaryModal").modal('show');
    document.getElementById('btnSaveManageSalary').innerText = "Update";
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