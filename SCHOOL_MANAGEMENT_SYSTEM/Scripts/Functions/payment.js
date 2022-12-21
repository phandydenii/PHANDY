$(document).ready(function () {

    $(document).ajaxStart(function () {
        $('#loadingGif').addClass('show');
    }).ajaxStop(function () {
        $('#loadingGif').removeClass('show');
    });
    InvoiceDetail();
    alert($('#id').val());
})


var tableInvoiceDetail = [];
function InvoiceDetail() {
    
    tableInvoiceDetail = $('#tableInvoiceDetail').DataTable({
        ajax: {
            url: "/api/invoicedetails/" + $('#id').val(),
            dataSrc: ""
        },
        columns: [
            {
                data: "id",
            },
            {
                data: "invoiceid",
            },
            {
                data: "itemname",
            },
            {
                data: "price",
            },
            {
                data: "waterusageid",
            },
            {
                data: "electricusageid",
            },
            {
                data: "id",
                render: function (data) {
                    return "<button OnClick='EditInvDetail(" + data + ");' class='btn btn-warning btn-xs' style='margin-top:0px'><span class='glyphicon glyphicon-usd'></span> Edit</button>";
                    + "<button OnClick='DeleteInvDetail(" + data + ");' class='btn btn-warning btn-xs' style='margin-top:0px'><span class='glyphicon glyphicon-usd'></span> Delete</button>"
                    ;
                }
            }
        ],

        destroy: true,
    });
}



    