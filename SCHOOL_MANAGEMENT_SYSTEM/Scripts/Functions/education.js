$(document).ready(function () {

    $(document).ajaxStart(function () {
        $('#loadingGif').addClass('show');
    }).ajaxStop(function () {
        $('#loadingGif').removeClass('show');
    });

    $('#educationModel').on('show.bs.modal', function () {

        //GetParrent(id);
    })
})

var tableEducation = [];
toastr.optionsOverride = 'positionclass = "toast-bottom-right"';
toastr.options.positionClass = 'toast-bottom-right';

function GetEducation(id) {
    //alert("Hello");
    tableEducation = $('#educationTable').DataTable({
        ajax: {
            url: "/api/EducationByEmployee/" + id,
            dataSrc: ""
        },
        columns: [

            //{
            //    data: "educationId"
            //},
            //{
            //    data: "educationEmpid"
            //},
            {
                data: "educationLevel"
            },
            {
                data: "skill"
            },
            {
                data: "fromYear"
            },
            {
                data: "toYear"
            },
            {
                data: "educationId",
                render: function (data) {
                    return "<button onclick='EducationEdit(" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px;'>Edit</button>" + "<button onclick='EducationDelete(" + data + ")' class='btn btn-danger btn-xs'>Delete</button>";
                }
            }
        ],
        destroy: true,
        "order": [0, "desc"],
        "info": false
    });

    DisableControl();
}

function EducationAction() {
    var action = '';
    action = document.getElementById('btnEducation').innerText;
    if (action == "Add New") {
        document.getElementById('btnEducation').innerText = "Save";
        EnableControl();
        $('#educationLevel').focus();

    } else if (action == "Save") {
        if ($('#educationLevel').val().trim() == "") {
            $('#educationLevel').css('border-color', 'red');
            $('#educationLevel').focus();
            toastr.info('Please enter education level!', "Server Response")
        } else {
            $('#educationLevel').css('border-color', '#cccccc');

            var dataSave = {
                educationLevel: $('#educationLevel').val(),
                skill: $('#skill').val(),
                fromYear: $('#fromYear').val(),
                toYear: $('#toYear').val(),
                educationEmpid: $('#educationEmpid').val(),
                //createdate: $('#createDate').val(),
                //createby: $('#createBy').val()
            };

            //console.log(dataSave);

            $.ajax({
                url: "/api/Educations",
                data: JSON.stringify(dataSave),
                type: "POST",
                contentType: "application/json;charset=utf-8",
                datatype: "json",
                success: function (result) {
                    toastr.success("Education has been created successfully.", "Server Response");
                    tableEducation.ajax.reload();
                    DisableControl();
                    ClearData();

                    //GetParrent(id);
                },
                error: function (errormessage) {
                    toastr.error("This Education is already exists in Database", "Service Response");
                }
            })
        }
    } else if (action == "Update") {
        if ($('#educationLevel').val().trim() == "") {
            $('#educationLevel').css('border-color', 'red');
            $('#educationLevel').focus();
            toastr.info('Please enter Level', "Server Response")
        } else {
            $('#educationLevel').css('border-color', '#cccccc');

            var data = {
                educationId: $('#educationId').val(),
                educationLevel: $('#educationLevel').val(),
                skill: $('#skill').val(),
                fromYear: $('#fromYear').val(),
                toYear: $('#toYear').val(),
                educationEmpid: $('#educationEmpid').val(),
                

            };

            //console.log(data);

            $.ajax({
                url: "/api/Educations/" + data.educationId,
                data: JSON.stringify(data),
                type: "PUT",
                contentType: "application/json;charset=utf-8",
                datatype: "json",
                success: function (result) {
                    toastr.success("Education has been update successfully.", "Server Response");
                    tableEducation.ajax.reload();
                    DisableControl();
                    ClearData();

                },
                error: function (errormessage) {
                    toastr.error("This Education Save Not Complete!", "Service Response");
                }
            })
        }
    }

}

function EducationEdit(educationId) {
    //alert(id);
    $.ajax({
        url: "/api/Educations/" + educationId,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            $('#educationId').val(result.educationId);
            $('#educationLevel').val(result.educationLevel);
            $('#skill').val(result.skill);
            $('#fromYear').val(result.fromYear);
            $('#toYear').val(result.toYear);
            $('#educationEmpid').val(result.educationEmpid);
           
            document.getElementById('btnEducation').innerText = "Update";
            //alert(result.Id);
            EnableControl();
            $('#educationLevel').focus();
        },
        error: function (errormessage) {
            toastr.error("This Education have no in Database", "Service Response");
        }
    });

}

function EducationDelete(id) {
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
                    url: "/api/Educations/" + id,
                    type: "DELETE",
                    contentType: "application/json;charset=utf-8",
                    datatype: "json",
                    success: function (result) {
                        tableEducation.ajax.reload();
                        toastr.success("Education has been Deleted successfully!", "Service Response");

                    },
                    error: function (errormessage) {
                        toastr.error("This Education Cannot Delete from Database!", "Service Response");

                    }
                });
            }
        }
    });

}

function DisableControl() {
    document.getElementById('educationLevel').disabled = true;
    document.getElementById('skill').disabled = true;
    document.getElementById('fromYear').disabled = true;
    document.getElementById('toYear').disabled = true;
    document.getElementById('btnEducation').innerText = "Add New";
}

function EnableControl() {
    document.getElementById('educationLevel').disabled = false;
    document.getElementById('skill').disabled = false;
    document.getElementById('fromYear').disabled = false;
    document.getElementById('toYear').disabled = false;
}

function ClearData() {
    $('#educationLevel').val('');
    $('#skill').val('');
    $('#fromYear').val('');
    $('#toYear').val('');
}