$(document).ready(function () {

    $(document).ajaxStart(function () {
        $('#loadingGif').addClass('show');
    }).ajaxStop(function () {
        $('#loadingGif').removeClass('show');
    });

    GetEmployee("all");
    $('#displaydepartment').on('change', function () {
        var departmentid = this.value;
        if (departmentid == "---Select Department----") {
            GetEmployee("all");
        } else {
            //alert(departmentid);
            GetEmployee(departmentid);
        }
    })
    //GetEmployee();
    
        //$('#employeeModel').on('show.bs.modal', function () {
        //})
})

var tableEmployee = [];
toastr.optionsOverride = 'positionclass = "toast-bottom-right"';
toastr.options.positionClass = 'toast-bottom-right';

function GetEmployee(departmentId) {
    tableEmployee = $('#tableEmployee').DataTable({
        ajax: {
            url: (departmentId == "all") ? "/api/Employees?departmentid=all" : "/api/Employees?departmentid=" + departmentId,
            dataSrc: ""
        },
        columns: [
                {
                    data: "id"
                },
                 {
                     data: "name"
                 },
                 {
                     data: "name_kh"
                 },
                  {
                      data: "gender"
                  },
               {
                   data: "phone"
               },

                //{
                //    data: "branch.name"
                //},
                //{
                //    data: "department.name"
                //},
           
                {
                    data: "emp_address"
                },
                {
                    data: "dob",
                    render: function (data) {
                        return moment(new Date(data)).format('DD-MMM-YYYY');
                    }
                },
                //{
                //    data: "pob"
                //},


            {
                data: "id",
                render: function (data) {
                    return "<button onclick='employeeEdit(" + data + ")' class='btn btn-warning btn-xs' style='margin-right:0px;'>Edit</button>" + "<button onclick='employeeDelete(" + data + ")' class='btn btn-danger btn-xs' >Delete</button>";
                }
            },
            {
                data: "id",
                render: function (data) {
                    return "<button onclick='employeeSalary(" + data + ")' class='btn-success btn-xs pull-right' style='margin-right:0px;'>Salary</button>" + "<button onclick='employeeParrent(" + data + ")' class='btn-success btn-xs pull-right'>Parrent</button>" + "<button onclick='employeeEducation(" + data + ")' class='btn-success btn-xs pull-right'>Education</button>";
                }
            },
        ],
        destroy: true,
        "order": [0, "desc"],
        "info": false
    });
}

function readURL(input){
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#employeeimg').attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}

function EmployeeAction() {
    var action = '';
    action = document.getElementById('btnEmployee').innerText;
    if (action == "Add New") {
        document.getElementById('btnEmployee').innerText = "Save";
        EnableControl();
        $('#employeename').focus();
        $('#dob').val(moment().format('YYYY-MM-DD'));
        
    } else if (action == "Save") {
        var response = Validate();
        if (response == false) {
            return false;
        }
        var data = new FormData();
        var files = $("#file").get(0).files;
        if (files.length > 0) {
            data.append("employeeImg", files[0]);
        }
        
        data.append("employeename", $("#employeename").val());
        data.append("employeenamekh", $("#employeenamekh").val());
        data.append("gender", $("#gender").val());
        data.append("phone", $("#phone").val());
        data.append("marintalstatus", $("#marintalstatus").val());
        data.append("branchId", $("#employeeBranchId").val());
        data.append("departmentId", $("#employeeDepartmentId").val());
        data.append("dob", $("#dob").val());
        data.append("pob", $("#pob").val());
        data.append("address", $("#address").val());
        data.append("email", $("#email").val());
      
        //console.log(files[0]);

        $.ajax({
            type: "POST",
            url: "/api/employees",
            contentType: false,
            processData: false,
            data: data,
            success: function (result) {

                //console.log(result);

                toastr.success("Employeee has been created successfully.", "Server Response");
                tableEmployee.ajax.reload();

                $('#employeeId').val(result.id);
                $('#employeeModel').modal('hide');
                
                document.getElementById('btnEmployee').innerText = "Add New";
                DisableControl();
                ClearData();

            },
            error: function (error) {
                //console.log(error);
                toastr.error("Employeee Already Exists!.", "Server Response");
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
            data.append("employeeImg", files[0]);
        } 
        data.append("Id", $('#employeeId'));
        data.append("employeename", $("#employeename").val());
        data.append("employeenamekh", $("#employeenamekh").val());
        data.append("gender", $("#gender").val());
        data.append("phone", $("#phone").val());
        data.append("marintalstatus", $("#maritalstatus").val());
        data.append("branchId", $("#employeeBranchId").val());
        data.append("departmentId", $("#employeeDepartmentId").val());
        data.append("dob", $("#dob").val());
        data.append("pob", $("#pob").val());
        data.append("address", $("#address").val());
        data.append("email", $("#email").val());
        data.append("file_old", $("#file_old").val());

        console.log(data);

        $.ajax({
            type: "PUT",
            url: "/api/Employees/" + $('#employeeId').val(),
            contentType: false,
            processData: false,
            data: data,
            success: function (result) {

                //console.log(result);

                toastr.success("Employeee has been updated successfully.", "Server Response");
                tableEmployee.ajax.reload();

                //$('#employeeId').val(result.id);
                $('#employeeModel').modal('hide');

                document.getElementById('btnEmployee').innerText = "Add New";
                DisableControl();
                ClearData();

            },
            error: function (error) {
                //console.log(error);
                toastr.error("Employeee Already Exists!.", "Server Response");
            }
        });

    }
}

function employeeEdit(id) {
    ClearData();
    EnableControl();
    document.getElementById('btnEmployee').innerText = "Update";

    $.ajax({
        url: "/api/Employees/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            $('#employeeId').val(result.id);
            $('#employeename').val(result.name);
            $('#employeenamekh').val(result.name_kh);
            $("#employeeBranchId").val(result.branchId);
            $("#employeeDepartmentId").val(result.departmentId);
            $('#gender').val(result.gender).change();
            $('#phone').val(result.phone);
            //$('#dob').val(result.dob);
            var dr = moment(result.dob).format("YYYY-MM-DD");
            $("#dob").val(dr);
            $('#pob').val(result.pob);
            $('#email').val(result.email);
            $('#address').val(result.emp_address);
            $('#marintalstatus').val(result.marital_Status).change();
            $('#file_old').val(result.img);
           
            console.log(result);

            if (result.img == "") {
                $('#employeeimg').attr('src', '../Images/no_image.png');
               
            } else {
                //alert(result.img);
                $('#employeeimg').attr('src', '../Images/' + result.img);

               
            }
            $('#employeeModel').modal('show');
        },
        error: function (errormessage) {
            toastr.error("No Record Select!", "Service Response");
        }
    });

}

function employeeDelete(id) {
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
                    url: "/api/Employees/" + id,
                    type: "DELETE",
                    contentType: "application/json;charset=utf-8",
                    datatype: "json",
                    success: function (result) {
                        tableEmployee.ajax.reload();
                        toastr.success("Employee has been Deleted successfully!", "Service Response");
                    },
                    error: function (errormessage) {
                        toastr.error("This Employee is already exists in Database", "Service Response");
                    }
                });
            }
        }
    });
}

function DisableControl() {
    document.getElementById('employeename').disabled = true;
    document.getElementById('employeenamekh').disabled = true;
    document.getElementById('gender').disabled = true;
    document.getElementById('phone').disabled = true;
    document.getElementById('maritalstatus').disabled = true;
    document.getElementById('employeeDepartmentId').disabled = true;
    document.getElementById('employeeBranchId').disabled = true;
    document.getElementById('dob').disabled = true;
    document.getElementById('pob').disabled = true;
    document.getElementById('email').disabled = true;
    document.getElementById('address').disabled = true;
    document.getElementById('btnEmployee').innerText = "Add New";

}

function EnableControl() {
    document.getElementById('employeename').disabled = false;
    document.getElementById('employeenamekh').disabled = false;
    document.getElementById('gender').disabled = false;
    document.getElementById('phone').disabled = false;
    document.getElementById('maritalstatus').disabled = false;
    document.getElementById('employeeDepartmentId').disabled = false;
    document.getElementById('employeeBranchId').disabled = false;
    document.getElementById('dob').disabled = false;
    document.getElementById('pob').disabled = false;
    document.getElementById('email').disabled = false;
    document.getElementById('address').disabled = false;
}

function Validate() {
    var isValid = true;
    if ($('#employeename').val().trim() == "") {
        $('#employeename').css('border-color', 'red');
        $('#employeename').focus();
        isValid = false;
    } else if ($('#employeenamekh').val().trim() == "") {
        $('#employeenamekh').css('border-color', 'red');
        $('#employeenamekh').focus();
        isValid = false;
    } else if ($('#dob').val().trim() == "") {
        $('#dob').css('border-color', 'red');
        $('#dob').focus();
        isValid = false;
    } else {
        $('#employeename').css('border-color', '#cccccc');
        $('#employeenamekh').css('border-color', '#cccccc');
        $('#employeename').focus();
    }
    return isValid;
}

function ClearData() {
    $('#employeename').val('');
    $('#employeenamekh').val('');
    $('#gender').val('');
    $('#phone').val('');
    //$('#dob').val('');
    $('#pob').val('');
    $('#email').val('');
    $('#address').val('');
    $('#employeename').focus();
}

function ClickAddnewEmployee() {
        DisableControl();
        $('#employeename').val('');
        $('#employeenamekh').val('');
        //$('#gender').val('');
        $('#phone').val('');
        $('#dob').val('');
        $('#pob').val('');
        $('#email').val('');
        $('#address').val('');
        $('#employeename').focus();
        $('#employeeimg').attr('src', '../Images/no_image.png');
        document.getElementById('btnEmployee').innerText = "Add New";
}

function employeeParrent(id) {
    //alert(id);
    GetParrent(id);   // parrent.js
    $("#parrentEmpId").val(id);
    $("#parrentModel").modal('show');
}

function employeeEducation(id) {
    //alert(id);
    GetEducation(id);   // education.js
    $("#educationEmpid").val(id);
    $("#educationModel").modal('show');
}

function employeeSalary(id) {
    //alert(id);
    GetSalary(id);   // salary.js
    $("#salaryEmpid").val(id);
    $("#salaryModel").modal('show');
}