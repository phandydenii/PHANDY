$(document).ready(function () {

    $(document).ajaxStart(function(){
        $('#loadingGif').addClass('show');
    }).ajaxStop(function(){
        $('#loadingGif').removeClass('show');
    });

    $('#departmentModel').on('show.bs.modal', function () {
        
        GetDepartment();
        document.getElementById('departmentName').disabled = true;
        document.getElementById('btnDepartment').innerText = "Add New";

    })
})

var tableDepartment = [];
toastr.optionsOverride = 'positionclass = "toast-bottom-right"';
toastr.options.positionClass = 'toast-bottom-right';

function GetDepartment() {
    tableDepartment = $('#departmentTable').DataTable({
        ajax: {
            url: "/api/Departments",
            dataSrc: ""
        },
        columns: [
            //{
            //    data:"id"
            //},
            {
                data:"name"
            },
            {
                data: "id",
                render: function (data) {
                    return "<button onclick='departmentEdit(" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px;'>Edit</button>" + "<button onclick='departmentDelete(" + data + ")' class='btn btn-danger btn-xs'>Delete</button>";
                }
            }
        ],
        destroy: true,
        "order": [0, "desc"],
        "info":false
    });
}

function DepartmentAction() {
    var action = '';
    action = document.getElementById('btnDepartment').innerText;
    if (action == "Add New")
    {
        document.getElementById('btnDepartment').innerText = "Save";
        document.getElementById('departmentName').disabled = false;
        $('#departmentName').focus();

    } else if (action == "Save") {
        if ($('#departmentName').val().trim() == "") {
            $('#departmentName').css('border-color', 'red');
            $('#departmentName').focus();
            toastr.info('Please enter Department Name.', "Server Response")
        } else {
            $('#departmentName').css('border-color', '#cccccc');

            var dataSave = {
                Name: $('#departmentName').val()
            };

            $.ajax({
                url: "/api/Departments",
                data: JSON.stringify(dataSave),
                type: "POST",
                contentType: "application/json;charset=utf-8",
                datatype: "json",
                success: function (result) {
                    toastr.success("Department has been created successfully.", "Server Response");
                    tableDepartment.ajax.reload();
                    document.getElementById('btnDepartment').innerText = "Add New";
                    $('#departmentName').val('');
                    document.getElementById('departmentName').disabled = true;
                },
                error: function (errormessage) {
                    toastr.error("This Department is already exists in Database", "Service Response");
                }
            })
        }
    }else if(action=="Update"){
        if ($('#departmentName').val().trim() == "") {
            $('#departmentName').css('border-color', 'red');
            $('#departmentName').focus();
            toastr.info('Please enter Department Name.', "Server Response")
        } else {
            $('#departmentName').css('border-color', '#cccccc');

            var data = {
                Id: $('#departmentId').val(),
                Name: $('#departmentName').val()
            };

            //console.log(data);

            $.ajax({
                url: "/api/Departments/" + data.Id,
                data: JSON.stringify(data),
                type: "PUT",
                contentType: "application/json;charset=utf-8",
                datatype: "json",
                success: function (result) {
                    toastr.success("Department has been update successfully.", "Server Response");
                    tableDepartment.ajax.reload();
                    document.getElementById('btnDepartment').innerText = "Add New";
                    $('#departmentName').val('');
                    document.getElementById('departmentName').disabled = true;
                },
                error: function (errormessage) {
                    toastr.error("This Department is already exists in Database", "Service Response");
                }
            })
        }
    }

}

function departmentEdit(id) {
    $.ajax({
        url: "/api/Departments/"+id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            $('#departmentId').val(result.id);
            $('#departmentName').val(result.name);
            document.getElementById('btnDepartment').innerText = "Update";
            document.getElementById('departmentName').disabled = false;
            $('#departmentName').focus();
        },
        error: function (errormessage) {
            toastr.error("This Department is already exists in Database", "Service Response");
        }
    });

}

function departmentDelete(id) {
    bootbox.confirm({
        title: "",
        message: "Are you sure want to delete this?",
        button:{
            cancel: {
                label: "Cancel",
                ClassName:"btn-default",
            },
            confirm: {
                label: "Delete",
                ClassName:"btn-danger"
            }
        },
        callback: function (result) {
            if (result) {
                $.ajax({
                    url: "/api/Departments/" + id,
                    type: "DELETE",
                    contentType: "application/json;charset=utf-8",
                    datatype: "json",
                    success: function (result) {
                        tableDepartment.ajax.reload();
                        toastr.success("Department has been Deleted successfully!", "Service Response");
                        document.getElementById('departmentName').disabled = true;
                        document.getElementById('btnDepartment').innerText = "Add New";
                    },
                    error: function (errormessage) {
                        toastr.error("This Department cannot delete it already use in Database", "Service Response");
                    }
                });
            }
        }
    });
}