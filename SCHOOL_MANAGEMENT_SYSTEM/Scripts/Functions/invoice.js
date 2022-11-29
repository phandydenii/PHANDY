$(document).ready(function () {

    $(document).ajaxStart(function () {
        $('#loadingGif').addClass('show');
    }).ajaxStop(function () {
        $('#loadingGif').removeClass('show');
    });
    GetInvoice("not_paid");
})

var tableInvoice = [];
$('#status').on('change', function () {
    GetInvoice(this.value);
});
function GetInvoice(status) {
    tableInvoice = $('#InvoiceTable').DataTable({
        ajax: {
            url: "/api/invoice_v/"+status,
            dataSrc: ""
        },
        columns: [
            {
                data: "id",
                render: function (data) {
                    return "RL" + ("000000" + data).slice(-6);
                }
            },
            {
                data: "invoicedate",
                render: function (data) {
                    return moment(new Date(data)).format('DD-MMM-YYYY');
                }
            },
            {
                data: "guestname",
            },
            {
                data: "roomno",
            },
            {
                data: "roomprice",
                render: function (data) {
                    return parseFloat(data).toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,');
                }
            },
            {
                data: "wtotal",
                render: function (data) {
                    if (data <= 0) {
                        return 0;
                    } else {
                        return parseFloat(data).toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,');
                    }
                }
            },
            {
                data: "ptotal",
                render: function (data) {
                    if (data <= 0) {
                        return 0;
                    } else {
                        return parseFloat(data).toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,');
                    }
                }
            },
            {
                data: "grandtotal",
                render: function (data) {
                    return parseFloat(data).toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,');
                }
            },
            {
                data: "totalriel",
                render: function (data) {
                    return parseFloat(data).toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,');
                }
            },
            {
                data: "totaldollar",
                render: function (data) {
                    return parseFloat(data).toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,');
                }
            },
            {
                data: "owe",
                render: function (data) {
                    if (data == 0) {
                        return parseFloat(data).toFixed(2);
                    } else {
                        return "<span class='label label-danger'>" + data + "</span>";
                    }
                }
            },
            
            {
                data: "id",
                render: function (data, type, row) {
                    if (row.paid==1) {
                        return "<span class='label label-primary'><span class='glyphicon glyphicon-ok'></span> Paid</span>";
                    } else {
                        return "<span class='label label-danger'><span class='glyphicon glyphicon-close'></span>Not Paid</span>";
                    }
                }
            },
            {
                data: "id",
                render: function (data, type, row) {
                    if (row.paid == 0) {
                        return "<button OnClick='OnPayment(" + data + ");' class='btn btn-warning btn-xs' style='margin-top:0px'><span class='glyphicon glyphicon-usd'></span> Pay Now</button>";
                    } else {
                        return "<div class='btn-group'><a href='#' class='btn btn-primary btn-xs'><span class='glyphicon glyphicon-cog'></span> Action</a><a href='#' class='btn btn-primary btn-xs dropdown-toggle' data-toggle='dropdown' aria-expanded='false'><span class='caret'></span></a>"
                                   + "<ul class='dropdown-menu'>"
                                     + "<li>"
                                        + "<button OnClick='EditInvoice (" + data + ")' class='btn btn-warning btn-xs' style='margin-top:0px'><span class='glyphicon glyphicon-edit'></span> Edit</button>"
                                        + "<button OnClick='InvoiceDetail (" + data + ")' class='btn btn-info btn-xs' style='margin-top:5px'><span class='glyphicon glyphicon-list-alt'></span> Detail</button>"
                                        + "<button OnClick='DeleteInvoice (" + data + ")' class='btn btn-danger btn-xs' style='margin-top:5px'><span class='glyphicon glyphicon-trash'></span> Delete</button>"
                                     + "</li>"
                                   + "</ul>"
                              + "</div>"
                        ;
                    }
                }
            }
        ],
        
        destroy: true,
    });
}

function OnPayment(id) {

    $("#PaymentModal").modal('show');
    $.ajax({
    url: "/api/invoice_v/"+id,
    type: "GET",
    contentType: "application/json;charset=utf-8",
    datatype: "json",
    success: function (result) {
        $('#id').val(result.id);
        $('#invoiceno').val(result.invoiceno);
        $("#guestname").val(result.guestname);
        $("#room").val(result.roomno);
        $('#roomprice').val(result.roomprice);
        $('#oldwaterrecord').val(result.wprerecord);
        $('#newwaterrecord').val(result.wcurrentrecord);
        var totalwater = result.wtotal;
        $('#totalwater').val(totalwater.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'));

        $('#oldpowerrecord').val(result.pprerecord);
        $('#newpowerrecord').val(result.pcurrentrecord);
        var totalpower = result.ptotal;
        $('#totalpower').val(totalpower.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'));

        $('#servicecharge').val(result.servicecharge);
        $('#totalpay').val(result.grandtotal);
        $('#totalpaykh').val(result.totalriel);
        $('#note').val(result.note);

        $('#checkinid').val(result.checkinid);
    },
    error: function (errormessage) {
        toastr.error("Load Record Error", "Service Response");
    }
    });

    $.get("/api/WaterPoserPrice/1/2", function (data) {
        $("#wpid").val(data.id);
        $("#wprice").text(data.waterprice);
        $("#pprice").text(data.powerprice);
    });


    $.get("/api/ExchangeRates/1/2", function (data) {
        $("#exrate").val(data.rate);
        $("#lblidroom").text(data.rate);
    });
}

function EditInvoice(id) {
    $("#PaymentModal").modal('show');
    document.getElementById('btnPayment').innerHTML == "Update";
    $.ajax({
        url: "/api/invoice_v/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            $('#id').val(result.id);
            $('#invoiceno').val(result.invoiceno);
            $("#guestname").val(result.guestname);
            $("#room").val(result.roomno);
            $('#roomprice').val(result.roomprice);
            $('#oldwaterrecord').val(result.wprerecord);
            $('#newwaterrecord').val(result.wcurrentrecord);
            var totalwater = result.wtotal;
            $('#totalwater').val(totalwater.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'));

            $('#oldpowerrecord').val(result.pprerecord);
            $('#newpowerrecord').val(result.pcurrentrecord);
            var totalpower = result.ptotal;
            $('#totalpower').val(totalpower.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'));

            $('#servicecharge').val(result.servicecharge);
            $('#totalpay').val(result.grandtotal);
            $('#totalpaykh').val(result.totalriel);
            $('#note').val(result.note);

            $('#checkinid').val(result.checkinid);
        },
        error: function (errormessage) {
            toastr.error("Load Record Error", "Service Response");
        }
    });

    $.get("/api/WaterPoserPrice/1/2", function (data) {
        $("#wpid").val(data.id);
        $("#wprice").text(data.waterprice);
        $("#pprice").text(data.powerprice);
    });


    $.get("/api/ExchangeRates/1/2", function (data) {
        $("#exrate").val(data.rate);
        $("#lblidroom").text(data.rate);
    });
}

function DeleteInvoice(id) {
    // alert('hi');
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
            //alert(id);
            if (result) {
                $.ajax({
                    type: "PUT",
                    url: "/api/invoices/delete/"+id,
                    contentType: false,
                    processData: false,
                    success: function (result) {
                        $('#InvoiceTable').DataTable().ajax.reload();
                        toastr.success("Invoice Deleted successfully!", "Service Response");
                    },
                    error: function (error) {
                        toastr.error("Invoice Can't be Deleted", "Service Response");
                    }
                });

              
            }
        }
    });

}