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
                    return "<button onclick='SalaryEdit(" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px;'>Edit</button>" + "<button onclick='SalaryDelete(" + data + ")' class='btn btn-danger btn-xs'>Delete</button>";
                }
            }
        ],
        destroy: true,
        "order": [0, "desc"],
        "info": false
    });
}
