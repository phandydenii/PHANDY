$(document).ready(function () {

    $(document).ajaxStart(function () {
        $('#loadingGif').addClass('show');
    }).ajaxStop(function () {
        $('#loadingGif').removeClass('show');
    });

    $('#studylanguageModel').on('show.bs.modal', function () {

        GetGrade();
    })
})

var tableBranch = [];
toastr.optionsOverride = 'positionclass = "toast-bottom-right"';
toastr.options.positionClass = 'toast-bottom-right';

function GetGrade() {
    
    tableBranch = $('#languageTable').DataTable({
        ajax: {
            url: "/api/Studylanguages",
            dataSrc: ""
        },
        columns: [
            //{
            //    data: "studylanguageid"
            //},
            {
                data: "language"
            },
            {
                data: "languagestatus"
            },
            {
                data: "studylanguageid",
                render: function (data) {
                    return "<button onclick='languageEdit(" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px;'>Edit</button>" + "<button onclick='languageDelete(" + data + ")' class='btn btn-danger btn-xs'>Delete</button>";
                }
            }
        ],
        destroy: true,
        "order": [0, "desc"],
        "info": false
    });
}

function languageAction() {
    var action = '';
    action = document.getElementById('btnLanguage').innerText;
    if (action == "Add New") {
        document.getElementById('btnLanguage').innerText = "Save";
        document.getElementById('language').disabled = false;
        $('#language').focus();

    } else if (action == "Save") {
        if ($('#language').val().trim() == "") {
            $('#language').css('border-color', 'red');
            $('#language').focus();
            toastr.info('Please enter Language!', "Server Response")
        } else {
            $('#language').css('border-color', '#cccccc');

            var dataSave = {
                language: $('#language').val(),
                languagestatus: $('#languagestatus').val()
            };

            $.ajax({
                url: "/api/Studylanguages",
                data: JSON.stringify(dataSave),
                type: "POST",
                contentType: "application/json;charset=utf-8",
                datatype: "json",
                success: function (result) {
                    toastr.success("Language has been created successfully.", "Server Response");
                    tableBranch.ajax.reload();
                    document.getElementById('btnLanguage').innerText = "Add New";
                    $('#language').val('');
                    document.getElementById('language').disabled = true;
                },
                error: function (errormessage) {
                    toastr.error("This Language is already exists in Database", "Service Response");
                }
            })
        }
    } else if (action == "Update") {
        if ($('#language').val().trim() == "") {
            $('#language').css('border-color', 'red');
            $('#language').focus();
            toastr.info('Please enter Language !', "Server Response")
        } else {
            $('#language').css('border-color', '#cccccc');

            var data = {
                studylanguageid: $('#studylanguageid').val(),
                language: $('#language').val(),
                languagestatus: $('#languagestatus').val()

            };

            //console.log(data);

            $.ajax({
                url: "/api/Studylanguages/" + data.studylanguageid,
                data: JSON.stringify(data),
                type: "PUT",
                contentType: "application/json;charset=utf-8",
                datatype: "json",
                success: function (result) {
                    toastr.success("Language has been update successfully.", "Server Response");
                    tableBranch.ajax.reload();
                    document.getElementById('btnLanguage').innerText = "Add New";
                    $('#language').val('');
                    $('#languagestatus').val('');
                    document.getElementById('language').disabled = true;
                    document.getElementById('languagestatus').disabled = true;
                },
                error: function (errormessage) {
                    toastr.error("This Language is already exists in Database", "Service Response");
                }
            })
        }
    }

}

function languageEdit(id) {
    $.ajax({
        url: "/api/Studylanguages/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            $('#studylanguageid').val(result.studylanguageid);
            $('#language').val(result.language);
            $('#languagestatus').val(result.languagestatus);

            document.getElementById('btnLanguage').innerText = "Update";
            document.getElementById('language').disabled = false;
            document.getElementById('languagestatus').disabled = false;
            $('#language').focus();
        },
        error: function (errormessage) {
            toastr.error("This Language is already exists in Database", "Service Response");
        }
    });
    
}

function languageDelete(id) {
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
                    url: "/api/Studylanguages/" + id,
                    type: "DELETE",
                    contentType: "application/json;charset=utf-8",
                    datatype: "json",
                    success: function (result) {
                        tableBranch.ajax.reload();
                        toastr.success("Language has been Deleted successfully!", "Service Response");
                    },
                    error: function (errormessage) {
                        toastr.error("This Language already use in Database", "Service Response");
                    }
                });
            }
        }
        
    });
}


function DisableControl() {
    document.getElementById('language').disabled = false;
    document.getElementById('languagestatus').disabled = false;
    document.getElementById('btnStudent').innerText = "Add New";

}

function EnableControl() {
    document.getElementById('language').disabled = false;
    document.getElementById('languagestatus').disabled = false;
}