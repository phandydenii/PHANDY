$(document).ready(function () {
    $('#HistoryModal').on('show.bs.modal', function () {
        GetHistoryInvoice($('#guestid').val());
    });
    
});

function GetHistoryInvoice(id) {
    alert(id);
    $('#tableHistory').dataTable({
        ajax: {
            url: "/api/invoice_v_by_guestid/" + id,
            dataSrc: ""
        },
        columns:
            [
                {
                    data: "startdate",
                    render: function (data) {
                        return moment(new Date(data)).format('DD-MMM-YYYY');
                    }
                },
                {
                    data: "enddate",
                    render: function (data) {
                        return moment(new Date(data)).format('DD-MMM-YYYY');
                    }
                },
                {
                    data: "waterusage",
                    render: function (data) {
                        return data.toFixed(2);
                    }
                },
                {
                    data: "electricusage",
                    render: function (data) {
                        return data.toFixed(2);
                    }
                },
                {
                    data: "wtotal",
                    render: function (data) {
                        return data.toFixed(2);
                    }
                },
                {
                    data: "etotal",
                    render: function (data) {
                        return data.toFixed(2);
                    }
                },
                {
                    data: "grandtotal",
                    render: function (data) {
                        return data.toFixed(2);
                    }
                },
                {
                    data: "id",
                    render: function (data) {
                        return "<button OnClick='OnEditInvHistory (" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-edit'></span></button>"
                            + "<button OnClick='OnDeleteInvoiceHistory (" + data + ")' class='btn btn-danger btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-trash'></span></button>"
                            ;

                    }
                }
            ],
        destroy: true,
        "order": [[0, "asc"]],

    });
}
function Close() {
    window.location.reload(true);
}


function OnEditInvHistory() {
    $("#HistoryModal").modal('hide');
    $("#PaymentModal").modal('show');
}
