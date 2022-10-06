$(document).ready(function () {

    $(document).ajaxStart(function () {
        $('#loadingGif').addClass('show');
    }).ajaxStop(function () {
        $('#loadingGif').removeClass('show');
    });

    $('#salaryModel').on('show.bs.modal', function () {

        //GetParrent(id);
    })
})

var tableSalary = [];
toastr.optionsOverride = 'positionclass = "toast-bottom-right"';
toastr.options.positionClass = 'toast-bottom-right';

function GetSalary(id) {
    //alert("Hello");
    tableSalary = $('#salaryTable').DataTable({
        ajax: {
            url: "/api/SalaryByEmployee/" + id,
            dataSrc: ""
        },
        columns: [

            //{
            //    data: "salaryId"
            //},
            //{
            //    data: "salaryEmpid"
            //},
            {
                data: "salaryDate",
                render: function (data) {
                    return moment(new Date(data)).format('DD-MMM-YYYY');
                }
            },
            {
                data: "salaryAmount"
            },
            {
                data: "salaryNote"
            },
            {
                data: "salaryId",
                render: function (data) {
                    return "<button onclick='SalaryEdit(" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px;'>Edit</button>" + "<button onclick='SalaryDelete(" + data + ")' class='btn btn-danger btn-xs'>Delete</button>";
                }
            }
        ],
        destroy: true,
        "order": [0, "desc"],
        "info": false
    });
    document.getElementById('btnSalary').innerText = "Add New";
    $('#salaryAmount').val("0.00");
    $('#salaryNote').val("")
    DisableControl();
}

function SalaryAction() {
    var action = '';
    action = document.getElementById('btnSalary').innerText;
    if (action == "Add New") {
        $('#salaryDate').val(moment().format('YYYY-MM-DD'));
        document.getElementById('btnSalary').innerText = "Save";
        EnableControl();
        $('#salaryDate').focus();

    } else if (action == "Save") {
        if ($('#salaryDate').val().trim() == "") {
            $('#salaryDate').css('border-color', 'red');
            $('#salaryDate').focus();
            toastr.info('Please enter salary date', "Server Response")
        } else if ($('#salaryAmount').val().trim() == "") {
            $('#salaryAmount').css('border-color', 'red');
            $('#salaryAmount').focus();
            toastr.info('Please enter salary amount', "Server Response")
        } else {
            $('#salaryAmount').css('border-color', '#cccccc');

            var dataSave = {
                salaryAmount: $('#salaryAmount').val(),
                employeeid: $('#empid').val(),
                salaryDate: $('#salaryDate').val(),
                salaryNote: $('#salaryNote').val(),
               
            };

            console.log(dataSave);

            $.ajax({
                url: "/api/Salarys",
                data: JSON.stringify(dataSave),
                type: "POST",
                contentType: "application/json;charset=utf-8",
                datatype: "json",
                success: function (result) {
                    toastr.success("Salary has been created successfully.", "Server Response");
                    tableSalary.ajax.reload();
                    DisableControl();
                    ClearData();

                    //GetParrent(id);
                },
                error: function (errormessage) {
                    toastr.error("This Salary is already exists in Database", "Service Response");
                }
            })
        }
    } else if (action == "Update") {
        if ($('#salaryDate').val().trim() == "") {
            $('#salaryDate').css('border-color', 'red');
            $('#salaryDate').focus();
            toastr.info('Please enter salary date', "Server Response")
        } else if ($('#salaryAmount').val().trim() == "") {
            $('#salaryAmount').css('border-color', 'red');
            $('#salaryAmount').focus();
            toastr.info('Please enter salary amount', "Server Response")
        }else {
            $('#salaryAmount').css('border-color', '#cccccc');

            var data = {
                salaryId: $('#salaryId').val(),
                salaryAmount: $('#salaryAmount').val(),
                employeeid: $('#empid').val(),
                salaryDate: $('#salaryDate').val(),
                salaryNote: $('#salaryNote').val(),

            };

            //console.log(data);

            $.ajax({
                url: "/api/Salarys/" + data.salaryId,
                data: JSON.stringify(data),
                type: "PUT",
                contentType: "application/json;charset=utf-8",
                datatype: "json",
                success: function (result) {
                    toastr.success("Salary has been update successfully.", "Server Response");
                    tableSalary.ajax.reload();
                    DisableControl();
                    ClearData();

                },
                error: function (errormessage) {
                    toastr.error("This Salary Save Not Complete!", "Service Response");
                }
            })
        }
    }

}

function SalaryEdit(salaryId) {
    //alert(id);
    $.ajax({
        url: "/api/Salarys/" + salaryId,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            $('#salaryId').val(result.salaryId);
            $('#salaryAmount').val(result.salaryAmount);
            var dr = moment(result.salaryDate).format("YYYY-MM-DD");
            $('#salaryDate').val(dr);
            $('#salaryNote').val(result.salaryNote);

            document.getElementById('btnSalary').innerText = "Update";
            //alert(result.Id);
            EnableControl();
            $('#salaryAmount').focus();
        },
        error: function (errormessage) {
            toastr.error("This Salary have no in Database", "Service Response");
        }
    });

}

function SalaryDelete(id) {
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
                    url: "/api/Salarys/" + id,
                    type: "DELETE",
                    contentType: "application/json;charset=utf-8",
                    datatype: "json",
                    success: function (result) {
                        tableSalary.ajax.reload();
                        toastr.success("Salary has been Deleted successfully!", "Service Response");
                        DisableControl();
                    },
                    error: function (errormessage) {
                        toastr.error("This Salary Cannot Delete from Database!", "Service Response");

                    }
                });
            }
        }
    });

}

function DisableControl() {
    document.getElementById('salaryAmount').disabled = true;
    document.getElementById('salaryDate').disabled = true;
    document.getElementById('salaryNote').disabled = true;
    document.getElementById('btnSalary').innerText = "Add New";
}

function EnableControl() {
    document.getElementById('salaryAmount').disabled = false;
    document.getElementById('salaryDate').disabled = false;
    document.getElementById('salaryNote').disabled = false;
}

function ClearData() {
    $('#salaryAmount').val('');
    $('#salaryDate').val('');
    $('#salaryNote').val('');

}