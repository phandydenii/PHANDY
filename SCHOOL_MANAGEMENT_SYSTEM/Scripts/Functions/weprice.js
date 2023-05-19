$(document).ready(function () {
    $(document).ajaxStart(function () {
        $('#LoadingGif').addClass('show');
    }).ajaxStop(function () {
        $('#LoadingGif').removeClass('show');
    });
    $('#WEPriceModel').on('show.bs.modal', function () {
        GetWEPrice();
        $('#waterprice').attr('readonly', 'readonly');
        $('#electricprice').attr('readonly', 'readonly');
    });
    
})
var tableWEPrice = [];
function GetWEPrice() {
    tableWEPrice=$('#tableWEPrice').dataTable({
        ajax: {
            url: "/api/weprice",
            dataSrc: ""
        },
        columns:
            [
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
                    data: "waterprice"
                },
                {
                    data: "electricprice"
                },
                {
                    data: "id",
                    render: function (data) {
                        return "<button onclick='DeleteWE(" + data + ")' class='btn btn-danger btn-xs'><span class='glyphicon glyphicon-trash'></span></button>";
                    }
                }
            ],
        destroy: true,
        "order": [0, "desc"],
        "info": false
    });
}

function AddNew() {
    $('#waterprice').removeAttr('readonly');
    $('#electricprice').removeAttr('readonly');
    $('#waterprice').focus()
    document.getElementById('btnSaveWE').style.display="block";
    document.getElementById('btnAddWE').style.display = "none";
}
$('#btnSaveWE').click(function () {
    var button = $(this);
    button.attr('disabled', 'disabled');
    setTimeout(function () {
        button.removeAttr('disabled');
    }, 3000);
    var data = {
        waterprice: $('#waterprice').val(),
        electricprice: $('#electricprice').val(),
        status: true,
        IsDeleted: false,
    };

    $.ajax({
        url: "/api/weprice",
        data: JSON.stringify(data),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var button = $(this);
            button.attr('disabled', 'disabled');
            setTimeout(function () {
                button.removeAttr('disabled');
            }, 3000);

            toastr.success("Insert data successfully.", "Server Response");
            $('#tableWEPrice').DataTable().ajax.reload();

            $('#waterprice').val('0'),
            $('#electricprice').val('0'),
            $('#waterprice').attr('readonly','readonly');
            $('#electricprice').attr('readonly','readonly');

            document.getElementById('btnSaveWE').style.display = "none";
            document.getElementById('btnAddWE').style.display = "block";
        },
        error: function (errormessage) {
            toastr.error("Insert data faild.", "Server Response");
        }
    });

});

function DeleteWE(id) {
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
                    url: "/api/weprice/" + id,
                    contentType: false,
                    processData: false,
                    // data: status,
                    success: function (result) {
                        toastr.success("Delete successfully.", "Server Response");
                        $('#tableWEPrice').DataTable().ajax.reload();
                    },
                    error: function (error) {
                        toastr.error("Delete fail!", "Server Response");
                    }
                });
            }
        }
    });
    
}

function CloseWE() {
    window.location.reload(true);
}