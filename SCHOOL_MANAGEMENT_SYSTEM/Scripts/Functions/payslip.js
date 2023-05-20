$(document).ready(function () {
    GetPaySlipList();
});

var tablePaySlip = [];
function GetPaySlipList() {
    tablePaySlip = $('#TablePaySlip').dataTable({
        ajax: {
            url: "/api/payslips",
            dataSrc: ""
        },
        columns:
            [
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
                    data: "staff.name"
                },
                {
                    data: "staff.namekh"
                },
                {
                    data: "salary"
                },
                {
                    data: "vat"
                },
                {
                    data: "penanty"
                },
                {
                    data: "bonus"
                },
                {
                    data: "totalsalary"
                },
                {
                    data: "note"
                },
                {
                    data: "id",
                    render: function (data) {
                        return "<button OnClick='EditPaySlip (" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-edit'></span></button>" +
                            "<button OnClick='DeletePaySlip (" + data + ")' class='btn btn-danger btn-xs'><span class='glyphicon glyphicon-trash'></span></button>";
                    }
                }
            ],
        destroy: true,
        "order": [[0, "desc"]],
        "info": false

    });
}

function CreatePaySlip() {
    $('#PaySlipModal').modal('show');
    document.getElementById('btnSaveStaff').style.display = "block";
    document.getElementById('btnUpdateStaff').style.display = "none";
}

function EditPaySlip(id) {
    $.ajax({
        url: "/api/payslips/"+id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            
            $('#staffid').val(result.staffid);
            $('#salary').val(result.salary);
            $('#vat').val(result.vat);
            $('#penanty').val(result.penanty);
            $('#bonus').val(result.bonus);
            $('#totalsalary').val(result.totalsalary);
            $('#note').val(result.note);

            document.getElementById('btnSaveStaff').style.display = "none";
            document.getElementById('btnUpdateStaff').style.display = "block";
            $('#PaySlipModal').modal('show');
            $('#payslipid').val(id);
        },
        error: function (errormessage) {
            toastr.error("No Record Select!", "Service Response");
        }
    });
}

function DeletePaySlip(id) {
    bootbox.confirm({
        title: "",
        message: "<h3>Are you sure want to delete record "+id+" ?</h3>",
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
                    url: "/api/payslips/" + id,
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