
$(document).ready(function () {

    $(document).ajaxStart(function () {
        $('#LoadingGif').addClass('show');
    }).ajaxStop(function () {
        $('#LoadingGif').removeClass('show');
    });
    $('#exchangeModel').on('show.bs.modal', function () {
        GetRate();
        $('#rate').attr('readonly', 'readonly');
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
                    return "<button onclick='RateDelete(" + data + ")' class='btn btn-danger btn-xs'><span class='glyphicon glyphicon-trash'></span></button>";
                }
            }
        ],
        destroy: true,
        "order": [0, "desc"],
        "info": false

    });
}
function AddRateAction() {
    $('#rate').focus()
    $('#rate').removeAttr('readonly');
    document.getElementById('btnRateAdd').style.display = "none";
    document.getElementById('btnRateSave').style.display = "block";
}
function SaveRateAction() {
    if ($('#rate').val().trim() == "") {
        $('#rate').css('border-color', 'red');
        $('#rate').focus();
        toastr.info("Please enter rate.", "Server Response");
    }
    else {
        var data = {
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
                toastr.success("insert data successfully.", "Server Response");
                $('#tableRate').DataTable().ajax.reload();
                $('#rate').val('0');
                $('#rate').attr('readonly', 'readonly');
                document.getElementById('btnRateAdd').style.display = "block";
                document.getElementById('btnRateSave').style.display = "none";
            },
            error: function (errormessage) {
                toastr.error("Insert data faild.", "Server Response");
            }
        });
    }
    
}

function RateDelete(id) {
    bootbox.confirm({
        message: "Are you sure you want to delete this?",
        buttons: {
            confirm: {
                label: 'Yes',
                className: 'btn-success'
            },
            cancel: {
                label: 'No',
                className: 'btn-danger'
            }
        },
        callback: function (result) {
            if (result) {
                $.ajax({
                    type: "PUT",
                    url: "/api/ExchangeRates/" + id,
                    contentType: false,
                    processData: false,
                    // data: status,
                    success: function (result) {
                        toastr.success("Delete successfully.", "Server Response");
                        $('#tableRate').DataTable().ajax.reload();
                    },
                    error: function (error) {
                        toastr.error("Delete fail!", "Server Response");
                    }
                });
            }
        }
    });
}

function CloseModal() {
    window.location.reload(true);
}