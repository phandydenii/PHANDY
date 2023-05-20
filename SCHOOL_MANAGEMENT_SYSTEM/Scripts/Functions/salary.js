
$(document).ready(function () {
    $(document).ajaxStart(function () {
        $('#loadingGif').addClass('show');
    }).ajaxStop(function () {
        $('#loadingGif').removeClass('show');
    });
    GetSalary();
    $('#StaffSalaryModal').on('show.bs.modal', function () {
        document.getElementById('staffid').disabled = true;
        document.getElementById('salary').disabled = true;
        document.getElementById('note').disabled = true;
    });
})

var tableSalary = [];
toastr.options.positionClass = 'toast-top-center';
toastr.options.progressBar = true;
function GetSalary() {
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
                    return "<button onclick='SalaryEdit(" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px;'><span class='glyphicon glyphicon-edit'></span></button>"
                        + "<button onclick='SalaryDelete(" + data + ")' class='btn btn-danger btn-xs'><span class='glyphicon glyphicon-trash'></span></button>";
                }
            }
        ],
        destroy: true,
        "order": [0, "desc"],
        "info": false
    });
}

function SalaryEdit(id) {
    document.getElementById('btnAddManageSalary').style.display = "none";
    document.getElementById('btnSaveManageSalary').style.display = "none";
    document.getElementById('btnUpdateSalary').style.display = "block";

    document.getElementById('staffid').disabled = false;
    document.getElementById('salary').disabled = false;
    document.getElementById('note').disabled = false;
    $.ajax({
        url: "/api/salaries/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            $('#salaryid').val(result.id);
            $('#staffid').val(result.staffid);
            $("#salary").val(result.salary);
            $("#note").val(result.note);
            console.log(result.salary);
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
            cancel: {
                label: 'No',
                className: 'btn-danger btn-sm'
            },
            confirm: {
                label: 'Yes',
                className: 'btn-success btn-sm'
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