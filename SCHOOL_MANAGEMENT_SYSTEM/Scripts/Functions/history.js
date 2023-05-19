$(document).ready(function () {
    $('#HistoryModal').on('show.bs.modal', function () {
        GetHistoryInvoice($('#guestid').val());
    });
    $('.paginate_button.active').css("background-color", "#f00");
});
function CloseHistory() {
    window.location.reload(true);
}
function GetHistoryInvoice(id) {
    $('#tableHistory').dataTable({
        ajax: {
            url: "/api/invoice_v_by_guestid/"+id,
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



function OnEditInvHistory(id) {
    $("#HistoryModal").modal('hide');
    $("#PaymentModal").modal('show');
    $("#SavePaymentbtn").hide();
    
    $.ajax({
        url: "/api/invoice_v/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            $('#id').val(id);
            $('#weid').val(result.weid);
            var invoiceno = "RL" + ("000000" + result.id).slice(-6)
            $('#invoiceno').val(invoiceno);
            $('#guestid').val(result.guestid);
            $("#guestname").val(result.guestname);
            $("#room").val(result.roomno);
            $('#roomprice').val(result.roomprice);
            var stdate = moment(result.startdate).format("YYYY-MM-DD");
            var enddate = moment(result.enddate).format("YYYY-MM-DD");
            $("#startdate").val(stdate);
            $("#enddate").val(enddate);
            $('#wstartrecord').val(result.wstartrecord);
            $('#wendrecord').val(result.wendrecord);
            var totalwater = result.wtotal;
            $('#totalwater').val(totalwater.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'));
            $('#estartrecord').val(result.estartrecord);
            $('#eendrecord').val(result.eendrecord);
            var totalelectric = result.etotal;
            $('#totalelectric').val(totalelectric.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'));
            $('#servicecharge').val(result.servicecharge);
            $('#totalpay').val(result.grandtotal.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'));
            $('#totalpaykh').val(result.totalriel.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'));
            $('#note').val(result.note);
            $('#checkinid').val(result.checkinid);
            $('#paydollar').val(result.paydollar);
            $('#payriel').val(result.payriel);
        },
        error: function (errormessage) {
            toastr.error("Load Record Error", "Service Response");
        }
    });

    $.get("/api/WEPrice/1/2", function (data) {
        $("#wpid").val(data.id);
        $("#wprice").text(data.waterprice);
        $("#pprice").text(data.electricprice);
    });


    $.get("/api/ExchangeRates/1/2", function (data) {
        $("#exrate").val(data.rate);
        $("#lblidroom").text(data.rate);
    });
}
