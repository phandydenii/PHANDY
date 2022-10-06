$(document).ready(function () {

    $(document).ajaxStart(function () {
        $('#loadingGif').addClass('show');
    }).ajaxStop(function () {
        $('#loadingGif').removeClass('show');
    });

    $('#parrentstudentModel').on('show.bs.modal', function () {

        //GetParrent(id);
    })
})

var tableParrent = [];
toastr.optionsOverride = 'positionclass = "toast-bottom-right"';
toastr.options.positionClass = 'toast-bottom-right';

function GetParrentstudent(id) {
    //alert("Hello");
    tableParrent = $('#parrentstudentTable').DataTable({
        ajax: {
            url: "/api/ParrentByStudent/" + id,
            dataSrc: ""
        },
        columns: [

            //{
            //    data: "parrentId"
            //},
            //{
            //    data: "parrentStuId"
            //},
            {
                data: "fatherName"
            },
            {
                data: "fatherJob"
            },
            {
                data: "motherName"
            },
            {
                data: "motherJob"
            },
            {
                data: "parrentPhone"
            },
            {
                data: "parrentAddress"
            },
            {
                data: "contactPerson"
            },
            {
                data: "contactPhone"
            },
            {
                data: "parrentId",
                render: function (data) {
                    return "<button onclick='ParrentstudentEdit(" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px;'>Edit</button>" + "<button onclick='ParrentstudentDelete(" + data + ")' class='btn btn-danger btn-xs'>Delete</button>";
                }
            }
        ],
        destroy: true,
        "order": [0, "desc"],
        "info": false
    });

    //alert(tableParrent);

    DisableControl();
}

function ParrentstudentAction() {
    var action = '';
    action = document.getElementById('btnParrentstudent').innerText;
    if (action == "Add New") {
        document.getElementById('btnParrentstudent').innerText = "Save";
        EnableControl();
        $('#fatherName').focus();

    } else if (action == "Save") {
        if ($('#fatherName').val().trim() == "") {
            $('#fatherName').css('border-color', 'red');
            $('#fatherName').focus();
            toastr.info('Please enter Father Name.', "Server Response")
        } else {
            $('#fatherName').css('border-color', '#cccccc');

            var dataSave = {
                fathername: $('#fatherName').val(),
                fatherjob: $('#fatherJob').val(),
                mothername: $('#motherName').val(),
                motherjob: $('#motherJob').val(),
                parrentphone: $('#parrentPhone').val(),
                parrentaddress: $('#parrentAddress').val(),
                contactperson: $('#contactPerson').val(),
                contactphone: $('#contactPhone').val(),
                parrentStuId: $('#parrentStuId').val(),
                //createdate: $('#createDate').val(),
                //createby: $('#createBy').val()
            };



            $.ajax({
                url: "/api/Parrentstudents",
                data: JSON.stringify(dataSave),
                type: "POST",
                contentType: "application/json;charset=utf-8",
                datatype: "json",
                success: function (result) {
                    toastr.success("Parrent has been created successfully.", "Server Response");
                    tableParrent.ajax.reload();
                    DisableControl();
                    ClearData();

                    //GetParrent(id);
                },
                error: function (errormessage) {
                    toastr.error("This Parrent is already exists in Database", "Service Response");
                }
            })
        }
    } else if (action == "Update") {
        if ($('#fatherName').val().trim() == "") {
            $('#fatherName').css('border-color', 'red');
            $('#fatherName').focus();
            toastr.info('Please enter Parrent Name.', "Server Response")
        } else {
            $('#fatherName').css('border-color', '#cccccc');

            var data = {
                parrentid: $('#parrentId').val(),
                fathername: $('#fatherName').val(),
                fatherjob: $('#fatherJob').val(),
                mothername: $('#motherName').val(),
                motherjob: $('#motherJob').val(),
                parrentphone: $('#parrentPhone').val(),
                parrentaddress: $('#parrentAddress').val(),
                contactperson: $('#contactPerson').val(),
                contactphone: $('#contactPhone').val(),
                parrentstuid: $('#parrentStuId').val(),

            };

            //console.log(data.parrentstuid);

            $.ajax({
                url: "/api/Parrentstudents/" + data.parrentid,
                data: JSON.stringify(data),
                type: "PUT",
                contentType: "application/json;charset=utf-8",
                datatype: "json",
                success: function (result) {
                    toastr.success("Parrent has been update successfully.", "Server Response");
                    tableParrent.ajax.reload();
                    DisableControl();
                    ClearData();

                },
                error: function (errormessage) {
                    toastr.error("This Parrent is already exists in Database", "Service Response");
                }
            })
        }
    }

}

function ParrentstudentEdit(parrentId) {
    //alert(parrentId);
    $.ajax({
        url: "/api/Parrentstudents/" + parrentId,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            $('#parrentId').val(result.parrentId);
            $('#parrentStuId').val(result.parrentStuId);
            $('#fatherName').val(result.fatherName);
            $('#fatherJob').val(result.fatherJob);
            $('#motherName').val(result.motherName);
            $('#motherJob').val(result.motherJob);
            $('#parrentPhone').val(result.parrentPhone);
            $('#parrentAddress').val(result.parrentAddress);
            $('#contactPerson').val(result.contactPerson);
            $('#contactPhone').val(result.contactPhone);
            document.getElementById('btnParrentstudent').innerText = "Update";
            //alert(result.parrentStuId);
            EnableControl();
            $('#fatherName').focus();
        },
        error: function (errormessage) {
            toastr.error("This no parrent in Database", "Service Response");
        }
    });

}

function ParrentstudentDelete(id) {
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
                    url: "/api/Parrentstudents/" + id,
                    type: "DELETE",
                    contentType: "application/json;charset=utf-8",
                    datatype: "json",
                    success: function (result) {
                        tableParrent.ajax.reload();
                        toastr.success("Parrent has been Deleted successfully!", "Service Response");

                    },
                    error: function (errormessage) {
                        toastr.error("This Parrent Cannot Delete from Database!", "Service Response");

                    }
                });
            }
        }
    });

}

function DisableControl() {
    document.getElementById('fatherName').disabled = true;
    document.getElementById('fatherJob').disabled = true;
    document.getElementById('motherName').disabled = true;
    document.getElementById('motherJob').disabled = true;
    document.getElementById('parrentPhone').disabled = true;
    document.getElementById('parrentAddress').disabled = true;
    document.getElementById('contactPerson').disabled = true;
    document.getElementById('contactPhone').disabled = true;
    document.getElementById('parrentStuId').disabled = true;
    document.getElementById('btnParrentstudent').innerText = "Add New";
}

function EnableControl() {
    document.getElementById('fatherName').disabled = false;
    document.getElementById('fatherJob').disabled = false;
    document.getElementById('motherName').disabled = false;
    document.getElementById('motherJob').disabled = false;
    document.getElementById('parrentPhone').disabled = false;
    document.getElementById('parrentAddress').disabled = false;
    document.getElementById('contactPerson').disabled = false;
    document.getElementById('contactPhone').disabled = false;
    document.getElementById('parrentStuId').disabled = false;
}

function ClearData() {
    $('#fatherName').val('');
    $('#fatherJob').val('');
    $('#motherName').val('');
    $('#motherJob').val('');
    $('#parrentPhone').val('');
    $('#parrentAddress').val('');
    $('#contactPerson').val('');
    $('#contactPhone').val('');
}