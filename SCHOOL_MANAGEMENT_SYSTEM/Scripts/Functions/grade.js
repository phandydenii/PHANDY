$(document).ready(function () {

    $(document).ajaxStart(function () {
        $('#loadingGif').addClass('show');
    }).ajaxStop(function () {
        $('#loadingGif').removeClass('show');
    });

    $('#gradeModel').on('show.bs.modal', function () {

        GetGrade();
    })
})

var tableBranch = [];
toastr.optionsOverride = 'positionclass = "toast-bottom-right"';
toastr.options.positionClass = 'toast-bottom-right';

function GetGrade() {
    tableBranch = $('#gradeTable').DataTable({
        ajax: {
            url: "/api/Grades",
            dataSrc: ""
        },
        columns: [
            //{
            //    data: "gradeid"
            //},
            {
                data: "gradename"
            },
            //{
            //    data: "shiftstatus"
            //},
            {
                data: "gradeid",
                render: function (data) {
                    return "<button onclick='gradeEdit(" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px;'>Edit</button>" + "<button onclick='gradeDelete(" + data + ")' class='btn btn-danger btn-xs'>Delete</button>";
                }
            }
        ],
        destroy: true,
        "order": [0, "desc"],
        "info": false
    });
}

function GradeAction() {
    var action = '';
    action = document.getElementById('btnGrade').innerText;
    if (action == "Add New") {
        document.getElementById('btnGrade').innerText = "Save";
        document.getElementById('gradename').disabled = false;
        $('#gradename').focus();

    } else if (action == "Save") {
        if ($('#gradename').val().trim() == "") {
            $('#gradename').css('border-color', 'red');
            $('#gradename').focus();
            toastr.info('Please enter Grade!', "Server Response")
        } else {
            $('#gradename').css('border-color', '#cccccc');

            var dataSave = {
                gradename: $('#gradename').val(),
                gradestatus: $('#gradestatus').val()
            };

            $.ajax({
                url: "/api/Grades",
                data: JSON.stringify(dataSave),
                type: "POST",
                contentType: "application/json;charset=utf-8",
                datatype: "json",
                success: function (result) {
                    toastr.success("Grade has been created successfully.", "Server Response");
                    tableBranch.ajax.reload();
                    document.getElementById('btnGrade').innerText = "Add New";
                    $('#gradename').val('');
                    document.getElementById('gradename').disabled = true;
                },
                error: function (errormessage) {
                    toastr.error("This Grade is already exists in Database", "Service Response");
                }
            })
        }
    } else if (action == "Update") {
        if ($('#gradename').val().trim() == "") {
            $('#gradename').css('border-color', 'red');
            $('#gradename').focus();
            toastr.info('Please enter Grade !', "Server Response")
        } else {
            $('#gradename').css('border-color', '#cccccc');

            var data = {
                gradeid: $('#gradeid').val(),
                gradename: $('#gradename').val(),
                gradestatus: $('#gradestatus').val()

            };

            //console.log(data);

            $.ajax({
                url: "/api/Grades/" + data.gradeid,
                data: JSON.stringify(data),
                type: "PUT",
                contentType: "application/json;charset=utf-8",
                datatype: "json",
                success: function (result) {
                    toastr.success("Grade has been update successfully.", "Server Response");
                    tableBranch.ajax.reload();
                    document.getElementById('btnGrade').innerText = "Add New";
                    $('#gradename').val('');
                    $('#gradestauts').val('');
                    document.getElementById('gradename').disabled = true;
                    document.getElementById('gradestatus').disabled = true;
                },
                error: function (errormessage) {
                    toastr.error("This Grade is already exists in Database", "Service Response");
                }
            })
        }
    }

}

function gradeEdit(id) {
    $.ajax({
        url: "/api/Grades/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            $('#gradeid').val(result.gradeid);
            $('#gradename').val(result.gradename);
            $('#gradestatus').val(result.gradestatus);

            document.getElementById('btnGrade').innerText = "Update";
            document.getElementById('gradename').disabled = false;
            document.getElementById('gradestatus').disabled = false;
            $('#gradename').focus();
        },
        error: function (errormessage) {
            toastr.error("This Shift is already exists in Database", "Service Response");
        }
    });

}

function gradeDelete(id) {
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
                    url: "/api/Grades/" + id,
                    type: "DELETE",
                    contentType: "application/json;charset=utf-8",
                    datatype: "json",
                    success: function (result) {
                        tableBranch.ajax.reload();
                        toastr.success("Grade has been Deleted successfully!", "Service Response");
                    },
                    error: function (errormessage) {
                        toastr.error("This Grade is already exists in Database", "Service Response");
                    }
                });
            }
        }
    });
}