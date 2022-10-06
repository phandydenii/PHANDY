$(document).ready(function () {

    $(document).ajaxStart(function () {
        $('#loadingGif').addClass('show');
    }).ajaxStop(function () {
        $('#loadingGif').removeClass('show');
    });

    $('#courseModel').on('show.bs.modal', function () {

        GetCourse();
        
    })
})

var tableBranch = [];
toastr.optionsOverride = 'positionclass = "toast-bottom-right"';
toastr.options.positionClass = 'toast-bottom-right';

function GetCourse() {
    tableBranch = $('#courseTable').DataTable({
        ajax: {
            url: "/api/Courses",
            dataSrc: ""
        },
        columns: [
            //{
            //    data: "id"
            //},
            {
                data: "coursecode"
            },
            {
                data: "coursename"
            },
            {
                data: "coursenamekh"
            },
            {
                data: "tutionfee"
            },
            {
                data: "id",
                render: function (data) {
                    return "<button onclick='courseEdit(" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px;'>Edit</button>" + "<button onclick='courseDelete(" + data + ")' class='btn btn-danger btn-xs'>Delete</button>";
                }
            }
        ],
        destroy: true,
        "order": [0, "desc"],
        "info": false
    });
    DisableControl();
}

function CourseAction() {
    var action = '';
    action = document.getElementById('btnCourse').innerText;
    if (action == "Add New") {
        document.getElementById('btnCourse').innerText = "Save";
        ClearData();
        EnableControl();
        $('#coursecode').focus();

    } else if (action == "Save") {
        if ($('#coursename').val().trim() == "") {
            $('#coursename').css('border-color', 'red');
            $('#coursename').focus();
            toastr.info('Please enter coursename!', "Server Response")
        } else {
            $('#coursename').css('border-color', '#cccccc');

            var dataSave = {
                coursecode: $('#coursecode').val(),
                coursename: $('#coursename').val(),
                coursenamekh: $('#coursenamekh').val(),
                tutionfee: $('#tutionfee').val()
            };

            $.ajax({
                url: "/api/Courses",
                data: JSON.stringify(dataSave),
                type: "POST",
                contentType: "application/json;charset=utf-8",
                datatype: "json",
                success: function (result) {
                    toastr.success("Course has been created successfully.", "Server Response");
                    tableBranch.ajax.reload();
                    document.getElementById('btnCourse').innerText = "Add New";
                    ClearData();
                    DisableControl();
                },
                error: function (errormessage) {
                    toastr.error("This Period is already exists in Database", "Service Response");
                }
            })
        }
    } else if (action == "Update") {
        if ($('#coursename').val().trim() == "") {
            $('#coursename').css('border-color', 'red');
            $('#coursename').focus();
            toastr.info('Please enter coursename !', "Server Response")
        } else {
            $('#coursename').css('border-color', '#cccccc');

            var data = {
                id: $('#id').val(),
                coursecode: $('#coursecode').val(),
                coursename: $('#coursename').val(),
                coursenamekh: $('#coursenamekh').val(),
                tutionfee: $('#tutionfee').val()

            };

            //console.log(data);

            $.ajax({
                url: "/api/Courses/" + data.id,
                data: JSON.stringify(data),
                type: "PUT",
                contentType: "application/json;charset=utf-8",
                datatype: "json",
                success: function (result) {
                    toastr.success("Course has been update successfully.", "Server Response");
                    tableBranch.ajax.reload();
                    ClearData();
                    DisableControl();
                },
                error: function (errormessage) {
                    toastr.error("This Course is already exists in Database", "Service Response");
                }
            })
        }
    }

}

function courseEdit(id) {
    $.ajax({
        url: "/api/Courses/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            $('#id').val(result.id);
            $('#coursecode').val(result.coursecode);
            $('#coursename').val(result.coursename);
            $('#coursenamekh').val(result.coursenamekh);
            $('#tutionfee').val(result.tutionfee);


            document.getElementById('btnCourse').innerText = "Update";
            EnableControl();
            $('#coursecode').focus();
        },
        error: function (errormessage) {
            toastr.error("This Course no have in Database", "Service Response");
        }
    });

}

function courseDelete(id) {
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
                    url: "/api/Courses/" + id,
                    type: "DELETE",
                    contentType: "application/json;charset=utf-8",
                    datatype: "json",
                    success: function (result) {
                        tableBranch.ajax.reload();
                        toastr.success("Course has been Deleted successfully!", "Service Response");
                    },
                    error: function (errormessage) {
                        toastr.error("This Course cannot delete from Database", "Service Response");
                    }
                });
            }
        }
    });
}

function DisableControl() {
    document.getElementById('coursecode').disabled = true;
    document.getElementById('coursename').disabled = true;
    document.getElementById('coursenamekh').disabled = true;
    document.getElementById('tutionfee').disabled = true;
    document.getElementById('btnCourse').innerText = "Add New";
}

function EnableControl() {
    document.getElementById('coursecode').disabled = false;
    document.getElementById('coursename').disabled = false;
    document.getElementById('coursenamekh').disabled = false;
    document.getElementById('tutionfee').disabled = false;
}

function ClearData() {
    $('#coursecode').val('');
    $('#coursename').val('');
    $('#coursenamekh').val('');
    $('#tutionfee').val('');
}

function ClickManageCourse() {
    DisableControl();
    ClearData();
    //$('#studentphoto').attr('src', '../Images/no_image.png');

}
