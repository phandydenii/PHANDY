
$(document).ready(function () {

    $(document).ajaxStart(function () {
        $('#LoadingGif').addClass('show');
    }).ajaxStop(function () {
        $('#LoadingGif').removeClass('show');
    });
    $('#exchangeModel').on('show.bs.modal', function () {
        GetRate();
    });
})
var tableGrade = [];

function GetRate() {
    tableGrade = $('#tableRate').DataTable({
        ajax: {
            url: "/api/ExchangeRates",
            dataSrc: ""
        },
        columns: [
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
                data: "rate"
            },
            {
                data: "id",
                render: function (data) {
                    return "<button onclick='RateEdit(" + data + ")' class='btn btn-warning btn-xs' style='margin-right: 5px;'>Edit</button>" + "<button onclick='RateDelete(" + data + ")' class='btn btn-danger btn-xs'>Delete</button>";
                }
            }
        ],
        destroy: true,
        "order": [0, "desc"],
        "info": false

    });
    document.getElementById('rate').disabled = false;
    document.getElementById('btnRate').innerText = "Add New";
}

function RateAction() {
    var action = '';
    action = document.getElementById('btnRate').innerText;

    if (action == "Add New") {
        document.getElementById('btnRate').innerText = "Save";
        document.getElementById('rate').disabled = false;
        $('#rate').focus();
    }
    else if (action == "Save") {
        if ($('#rate').val().trim() == "") {
            $('#rate').css('border-color', 'red');
            $('#rate').focus();
            toastr.info("Please enter rate.", "Server Response");
        }
        else {
            $('#rate').css('border-color', '#cccccc');

            var data = {

                date: "2019-09-19",
                rate: $('#rate').val(),
                status: true
            };

            $.ajax({
                url: "/api/ExchangeRates",
                data: JSON.stringify(data),
                type: "POST",
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    toastr.success("New Exchage Rate has been created successfully.", "Server Response");
                    tableGrade.ajax.reload();
                    document.getElementById('btnRate').innerText = "Add New";
                    $('#rate').val('');
                    document.getElementById('rate').disabled = true;
                },
                error: function (errormessage) {
                    toastr.error("This Rate is already exists in database.", "Server Response");
                }
            });
        }
    }
    else if (action == "Update") {
        if ($('#rate').val().trim() == "") {
            $('#rate').css('border-color', 'red');
            $('#rate').focus();
            toastr.info("Please enter rate", "Server Response");
        }
        else {
            $('#rate').css('border-color', '#cccccc');

            var data = {
                id: $('#rateid').val(),
                rate: $('#rate').val(),
                status: true
            };

            $.ajax({
                url: "/api/ExchangeRates/" + data.id,
                data: JSON.stringify(data),
                type: "PUT",
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (result) {
                    toastr.success("Exchage Rate has been updated successfully.", "Server Response");
                    tableGrade.ajax.reload();
                    document.getElementById('btnGrade').innerText = "Add New";
                    $('#tableGrade').val('');
                    document.getElementById('GradeName').disabled = true;
                },
                error: function (errormessage) {
                    toastr.error("This grade is already exists in database.", "Server Response");
                }
            });
        }
    }
}

function RateEdit(id) {
    $.ajax({
        url: "/api/ExchangeRates/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {

            document.getElementById('btnRate').innerText = "Update";
            document.getElementById('rate').disabled = false;
            $('#rate').val(result.rate);
            $('#rateid').val(result.rateid);
            //console.log(result);
        },
        error: function (errormessage) {
            toastr.error("Unexpected happened.", "Server Response");
        }
    });
}

function RateDelete(id) {
    bootbox.confirm({
        title: "Confirmation",
        message: "Are you sure you want to delete this?",
        button: {
            cancel: {
                label: "Cancel",
                className: "btn-default"
            },
            confirm: {
                label: "Delete",
                className: "btn-danger"
            }
        },
        callback: function (result) {
            if (result) {
                $.ajax({
                    url: "/api/ExchangeRates/" + id,
                    type: "DELETE",
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (result) {
                        tableGrade.ajax.reload();
                        toastr.success("Rate has been deleted successfully.", "Server Response");
                        document.getElementById('btnRate').innerText = "Add New";
                        $("#rate").val('');
                        document.getElementById('rate').disabled = true;
                    },
                    error: function (errormessage) {
                        toastr.error("Unexpected happened.", "Server Response");
                    }
                });
            }
        }
    });
}