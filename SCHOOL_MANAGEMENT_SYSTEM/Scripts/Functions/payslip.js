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
                    data: "salaryamount"
                },
                {
                    data: "note"
                },
                {
                    data: "id",
                    render: function (data) {
                        return "<button OnClick='EditPaySlip (" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-edit'></span> Edit</button>" +
                            "<button OnClick='DeletePaySlip (" + data + ")' class='btn btn-danger btn-xs'><span class='glyphicon glyphicon-trash'></span> Delete</button>";
                    }
                }
            ],
        destroy: true,
        "order": [[0, "desc"]],
        "info": false

    });
}

function SaveStaffAction() {
    var action = document.getElementById('btnSaveStaff').innerText;
    if(action=="Save"){
        var data = new FormData();
        data.append("staffid", $("#staffid").val());
        data.append("salary", $("#salary").val());
        data.append("vat", $("#vat").val());
        data.append("penanty", $("#penanty").val());
        data.append("bonus", $("#bonus").val());
        data.append("note", $("#note").val());
        data.append("salaryamount", $("#salaryamount").val());
        $.ajax({
            type: "POST",
            url: "/api/payslips",
            contentType: false,
            processData: false,
            data: data,
            success: function (result) {
                toastr.success("Insert record successfully.", "Server Response");
                $('#PaySlipModal').modal('hide');
                //window.open("/payslip-rpt?id=" + result, "");
                
            },
            error: function (error) {
                console.log(error);
                toastr.error("Record Already Exists!.", "Server Response");
            }
        });
    } else if (action == "Update") {
        var data = new FormData();
        data.append("staffid", $("#staffid").val());
        data.append("salary", $("#salary").val());
        data.append("vat", $("#vat").val());
        data.append("penanty", $("#penanty").val());
        data.append("bonus", $("#bonus").val());
        data.append("note", $("#note").val());
        data.append("salaryamount", $("#salaryamount").val());
        $.ajax({
            type: "PUT",
            url: "/api/payslips/" + $('#payslipid').val(),
            contentType: false,
            processData: false,
            data: data,
            success: function (result) {
                toastr.success("Insert record successfully.", "Server Response");
                $('#PaySlipModal').modal('hide');
                window.open("/payslip-rpt/" + result, "");
            },
            error: function (error) {
                console.log(error);
                toastr.error("Record Already Exists!.", "Server Response");
            }
        });
    }
    
}

function EditPaySlip(id) {
    $('#PaySlipModal').modal('show');
    document.getElementById('btnSaveStaff').innerText = "Update";
    $.ajax({
        url: "/api/payslips/"+id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            $('#payslipid').val(result.id);
            $('#staffid').val(result.staffid);
            $('#salary').val(result.salary);
            $('#vat').val(result.vat);
            $('#penanty').val(result.penanty);
            $('#bonus').val(result.bonus);
            $('#salaryamount').val(result.salaryamount);
            
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
