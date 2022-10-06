$(document).ready(function () {

    $(document).ajaxStart(function () {
        $('#loadingGif').addClass('show');
    }).ajaxStop(function () {
        $('#loadingGif').removeClass('show');
    });

    $('#studyperiodModel').on('show.bs.modal', function () {

        GetPeriod();
    })
})

var tableBranch = [];
toastr.optionsOverride = 'positionclass = "toast-bottom-right"';
toastr.options.positionClass = 'toast-bottom-right';

function GetPeriod() {
    tableBranch = $('#periodTable').DataTable({
        ajax: {
            url: "/api/Studyperiods",
            dataSrc: ""
        },
        columns: [
            //{
            //    data: "studyperiodid"
            //},
            {
                data: "period"
            },
            {
                data: "adminfee"
            },
            {
                data: "periodstatus"
            },
            {
                data: "studyperiodid",
                render: function (data) {
                    return "<button onclick='periodEdit(" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px;'>Edit</button>" + "<button onclick='periodDelete(" + data + ")' class='btn btn-danger btn-xs'>Delete</button>";
                }
            }
        ],
        destroy: true,
        "order": [0, "desc"],
        "info": false
    });
}

function PeriodAction() {
    var action = '';
    action = document.getElementById('btnPeriod').innerText;
    if (action == "Add New") {
        document.getElementById('btnPeriod').innerText = "Save";
        document.getElementById('period').disabled = false;
        $('#period').focus();

    } else if (action == "Save") {
        if ($('#period').val().trim() == "") {
            $('#period').css('border-color', 'red');
            $('#period').focus();
            toastr.info('Please enter Period!', "Server Response")
        } else {
            $('#period').css('border-color', '#cccccc');

            var dataSave = {
                period: $('#period').val(),
                adminfee: $('#adminfee').val(),
                periodstatus: $('#periodstatus').val()
            };

            $.ajax({
                url: "/api/Studyperiods",
                data: JSON.stringify(dataSave),
                type: "POST",
                contentType: "application/json;charset=utf-8",
                datatype: "json",
                success: function (result) {
                    toastr.success("Period has been created successfully.", "Server Response");
                    tableBranch.ajax.reload();
                    document.getElementById('btnPeriod').innerText = "Add New";
                    $('#period').val('');
                    document.getElementById('period').disabled = true;
                },
                error: function (errormessage) {
                    toastr.error("This Period is already exists in Database", "Service Response");
                }
            })
        }
    } else if (action == "Update") {
        if ($('#period').val().trim() == "") {
            $('#period').css('border-color', 'red');
            $('#period').focus();
            toastr.info('Please enter Period !', "Server Response")
        } else {
            $('#period').css('border-color', '#cccccc');

            var data = {
                studyperiodid: $('#studyperiodid').val(),
                period: $('#period').val(),
                adminfee: $('#adminfee').val(),
                periodstatus: $('#periodstatus').val()

            };

            //console.log(data);

            $.ajax({
                url: "/api/Studyperiods/" + data.studyperiodid,
                data: JSON.stringify(data),
                type: "PUT",
                contentType: "application/json;charset=utf-8",
                datatype: "json",
                success: function (result) {
                    toastr.success("Period has been update successfully.", "Server Response");
                    tableBranch.ajax.reload();
                    document.getElementById('btnPeriod').innerText = "Add New";
                    $('#period').val('');
                    $('#adminfee').val('');
                    $('#periodstatus').val('');
                    document.getElementById('period').disabled = true;
                    document.getElementById('adminfee').disabled = true;
                    document.getElementById('periodstatus').disabled = true;
                },
                error: function (errormessage) {
                    toastr.error("This Period is already exists in Database", "Service Response");
                }
            })
        }
    }

}

function periodEdit(id) {
    $.ajax({
        url: "/api/Studyperiods/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            $('#studyperiodid').val(result.studyperiodid);
            $('#period').val(result.period);
            $('#adminfee').val(result.adminfee);
            $('#periodstatus').val(result.periodstatus);

            document.getElementById('btnPeriod').innerText = "Update";
            document.getElementById('period').disabled = false;
            document.getElementById('adminfee').disabled = false;
            document.getElementById('periodstatus').disabled = false;
            $('#period').focus();
        },
        error: function (errormessage) {
            toastr.error("This Period is already exists in Database", "Service Response");
        }
    });

}

function periodDelete(id) {
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
                    url: "/api/Studyperiods/" + id,
                    type: "DELETE",
                    contentType: "application/json;charset=utf-8",
                    datatype: "json",
                    success: function (result) {
                        tableBranch.ajax.reload();
                        toastr.success("Period has been Deleted successfully!", "Service Response");
                    },
                    error: function (errormessage) {
                        toastr.error("This Period is already exists in Database", "Service Response");
                    }
                });
            }
        }
    });
}