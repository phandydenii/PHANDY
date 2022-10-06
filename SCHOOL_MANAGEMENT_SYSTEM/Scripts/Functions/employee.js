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
            url: (departmentId == "all") ? "/api/Employee?departmentid=all" : "/api/Employee?departmentid=" + departmentId,
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
                     data: "namekh"
                 },
                  {
                      data: "sex"
                  },
               {
                   data: "phone"
               },
               //{
               //    data: "dob",
               //    render: function (data) {
               //        return moment(new Date(data)).format('DD-MMM-YYYY');
               //    }
               //},

                {
                    data: "address"
                },
                //{
                //    data: "email"
                //},
                //{
                //    data: "identityno"
                //},
                 //{
                 //    data: "shippertype"
                 //},
                 //{
                 //    data: "vehiracle"
                 //},
                 //{
                 //    data: "plateno"
                 //},
                //{
                //    data: "photo"
                //},
                {
                    data: "phone_card"
                },

                {
                    data: "petroluem"
                },
                {
                    data: "deliveryin"
                },

                {
                    data: "deliveryout"
                },
            {
                data: "id",
                render: function (data) {
                    return "<button onclick='employeeSalary(" + data + ")' class='btn-success btn-xs pull-right' style='margin-right:0px;'>Salary</button>" + "<button onclick='OpenBonusModel(" + data + ")' class='btn btn-success btn-xs' style='margin-right:5px;'​>Bonus</button>" + " <button onclick='EmployeeEdit(" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px;'​>Edit</button>" + "<button onclick='EmployeeDelete(" + data + ")' class='btn btn-danger btn-xs' >Delete</button>";
                }
            },
        ],
        destroy: true,
        "order": [0, "desc"],
        "info": false
    });
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

function EmployeeAction() {
    var action = '';
    action = document.getElementById('btnEmp').innerText;
    if (action == "Add New") {
        $('#empname').focus();
        $('#dob').val(moment().format('YYYY-MM-DD'));
        document.getElementById('btnEmp').innerText = "Save";
        document.getElementById('empname').disabled = false;
        document.getElementById('empnamekh').disabled = false;
        document.getElementById('sex').disabled = false;
        document.getElementById('phone').disabled = false;
        document.getElementById('dob').disabled = false;
        document.getElementById('address').disabled = false;
        document.getElementById('email').disabled = false;
        document.getElementById('identityno').disabled = false;
        document.getElementById('shippertype').disabled = false;
        document.getElementById('vehiracle').disabled = false;
        document.getElementById('plateno').disabled = false;
        document.getElementById('phone_card').disabled = false;
        document.getElementById('petroluem').disabled = false;
        document.getElementById('deliveryin').disabled = false;
        document.getElementById('deliveryout').disabled = false;
        $('#photo').attr('src', '../Images/no_image.png');
        $("#empname").focus();
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

        data.append("name", $("#empname").val());
        data.append("namekh", $("#empnamekh").val());
        data.append("sex", $("#sex").val());
        data.append("phone", $("#phone").val());
        data.append("address", $("#address").val());
        data.append("showroomid", $("#showroomid").val());
        data.append("departmentid", $("#departmentid").val());
        data.append("positionid", $("#positionid").val());
        data.append("dob", $("#dob").val());
        data.append("email", $("#email").val());
        data.append("identityno", $("#identityno").val());
        data.append("shippertype", $("#shippertype").val());
        data.append("vehiracle", $("#vehiracle").val());
        data.append("plateno", $("#plateno").val());
        data.append("phone_card", $("#phone_card").val());
        data.append("petroluem", $("#petroluem").val());
        data.append("deliveryin", $("#deliveryin").val());
        data.append("deliveryout", $("#deliveryout").val());


        //console.log(files[0]);

        $.ajax({
            type: "POST",
            url: "/api/employee",
            contentType: false,
            processData: false,
            data: data,
            success: function (result) {

                //console.log(result);

                toastr.success("Employeee has been created successfully.", "Server Response");
                tableEmployee.ajax.reload();

                $('#id').val(result.id);
                $('#employeeModel').modal('hide');

                document.getElementById('btnEmp').innerText = "Add New";
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
            data.append("photo", files[0]);
        }
        data.append("id", $('#id'));
        data.append("name", $("#empname").val());
        data.append("namekh", $("#empnamekh").val());
        data.append("sex", $("#sex").val());
        data.append("phone", $("#phone").val());
        data.append("dob", $("#dob").val());
        data.append("address", $("#address").val());
        data.append("email", $("#email").val());
        data.append("positionid", $("#positionid").val());
        data.append("departmentid", $("#departmentid").val());
        data.append("showroomid", $("#showroomid").val());
        data.append("identityno", $("#identityno").val());
        data.append("shippertype", $("#shippertype").val());
        data.append("vehiracle", $("#vehiracle").val());
        data.append("plateno", $("#plateno").val());
        data.append("phone_card", $("#phone_card").val());
        data.append("petroluem", $("#petroluem").val());
        data.append("deliveryin", $("#deliveryin").val());
        data.append("deliveryout", $("#deliveryout").val());
        data.append("file_old", $("#file_old").val());

        console.log(data);

        $.ajax({
            type: "PUT",
            url: "/api/Employee/" + $('#id').val(),
            contentType: false,
            processData: false,
            data: data,
            success: function (result) {

                //console.log(result);

                toastr.success("Employeee has been updated successfully.", "Server Response");
                tableEmployee.ajax.reload();

                //$('#employeeId').val(result.id);
                $('#employeeModel').modal('hide');

                document.getElementById('btnEmp').innerText = "Add New";
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

function EmployeeEdit(id) {
    ClearData();
    EnableControl();
    document.getElementById('btnEmp').innerText = "Update";

    $.ajax({
        url: "/api/Employee/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            $('#id').val(result.id);
            $('#empname').val(result.name);
            $('#empnamekh').val(result.namekh);
            $("#showroomid").val(result.showroomid);
            $("#departmentid").val(result.departmentid);
            $('#positionid').val(result.positionid).change();
            $('#sex').val(result.sex).change();
            $('#phone').val(result.phone);
            var dr = moment(result.dob).format("YYYY-MM-DD");
            $("#dob").val(dr);
            $('#address').val(result.address);
            $('#email').val(result.email);
            $('#identityno').val(result.identityno);
            $('#shippertype').val(result.shippertype).change();
            $("#vehiracle").val(result.vehiracle).change();
            $('#plateno').val(result.plateno);
            $('#phone_card').val(result.phone_card);
            $('#petroluem').val(result.petroluem);
            $('#deliveryin').val(result.deliveryin);
            $('#deliveryout').val(result.deliveryout);
            $('#file_old').val(result.photo);
            //console.log(result);
            //alert(result.photo);
            if (result.photo == "") {
                $('#photo').attr('src', '../Images/no_image.png');
            } else {
                //alert(result.img);
                $('#photo').attr('src', '../Images/' + result.photo);
            }

            //Enable Control
            document.getElementById('empname').disabled = false;
            document.getElementById('empnamekh').disabled = false;
            document.getElementById('sex').disabled = false;
            document.getElementById('phone').disabled = false;
            document.getElementById('dob').disabled = false;
            document.getElementById('address').disabled = false;
            document.getElementById('email').disabled = false;
            document.getElementById('identityno').disabled = false;
            document.getElementById('shippertype').disabled = false;
            document.getElementById('vehiracle').disabled = false;
            document.getElementById('plateno').disabled = false;
            document.getElementById('phone_card').disabled = false;
            document.getElementById('petroluem').disabled = false;
            document.getElementById('deliveryin').disabled = false;
            document.getElementById('deliveryout').disabled = false;

            $('#employeeModel').modal('show');
        },
        error: function (errormessage) {
            toastr.error("No Record Select!", "Service Response");
        }
    });

}

function EmployeeDelete(id) {
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
                    url: "/api/Employee/" + id,
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

function Validate() {
    var isValid = true;
    if ($('#empname').val().trim() == "") {
        $('#empname').css('border-color', 'red');
        $('#empname').focus();
        isValid = false;
    } else if ($('#empnamekh').val().trim() == "") {
        $('#empnamekh').css('border-color', 'red');
        $('#empnamekh').focus();
        isValid = false;
    } else if ($('#dob').val().trim() == "") {
        $('#dob').css('border-color', 'red');
        $('#dob').focus();
        isValid = false;
    } else {
        $('#empname').css('border-color', '#cccccc');
        $('#empnamekh').css('border-color', '#cccccc');
        $('#empname').focus();
    }
    return isValid;
}

function ClickAddnewEmployee() {
    document.getElementById('empname').disabled = true;
    document.getElementById('empnamekh').disabled = true;
    document.getElementById('sex').disabled = true;
    document.getElementById('phone').disabled = true;
    document.getElementById('dob').disabled = true;
    document.getElementById('address').disabled = true;
    document.getElementById('email').disabled = true;
    document.getElementById('identityno').disabled = true;
    document.getElementById('shippertype').disabled = true;
    document.getElementById('vehiracle').disabled = true;
    document.getElementById('plateno').disabled = true;
    document.getElementById('phone_card').disabled = true;
    document.getElementById('petroluem').disabled = true;
    document.getElementById('deliveryin').disabled = true;
    document.getElementById('deliveryout').disabled = true;
    $('#empname').val('');
    $('#empnamekh').val('');
    $('#sex').val('');
    $('#phone').val('');
    //$('#dob').val('');
    $('#pob').val('');
    $('#email').val('');
    $('#address').val('');
    $('#identityno').val('');
    $('#shippertype').val('');
    $('#vehiracle').val('');
    $('#plateno').val('0');
    $('#phone_card').val('0');
    $('#petroluem').val('0');
    $('#deliveryin').val('0.00');
    $('#deliveryout').val('0.00');
    $('#empname').focus();
    $('#photo').attr('src', '../Images/no_image.png');
    document.getElementById('btnEmp').innerText = "Add New";
}

function OpenBonusModel(id) {
    GetBonus(id);   // salary.js
    $("#employeeid").val(id);
    $("#bonusModel").modal('show');
}

function employeeSalary(id) {
    //alert(id);
    GetSalary(id);   // salary.js
    //alert(id);
    $("#empid").val(id);
    $("#salaryModel").modal('show');
    
}