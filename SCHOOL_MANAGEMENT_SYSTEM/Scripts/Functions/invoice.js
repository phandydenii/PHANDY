$(document).ready(function () {

    $(document).ajaxStart(function () {
        $('#loadingGif').addClass('show');
    }).ajaxStop(function () {
        $('#loadingGif').removeClass('show');
    });
    GetInvoice($('#date').val(), "not_paid");
})

var tableInvoice = [];
var tableInvoiceDetail = [];
$('#status').on('change', function () {
    var date = $('#date').val();
    GetInvoice(date, this.value);
    alert(this.value+" "+date);
});

$('#date').on('change', function () {
    var status = $('#status').val();
    GetInvoice(this.value, status);

});

function GetInvoice(date,status) {
    tableInvoice = $('#InvoiceTable').DataTable({
        ajax: {
            url: "/api/invoice_v/1/" + date + "/" + status,
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
                data: "etotal",
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
                data: "id",
                render: function (data, type, row) {
                    var d = new Date();
                    var date = d.getMonth() + 1 + "/" + d.getDate() + "/" + d.getFullYear();
                    var invdate = row.invoicedate;
                    var td = Math.floor(data-invdate);
                    if (row.paid==1) {
                        return "<span class='label label-primary'><span class='glyphicon glyphicon-ok'></span> Paid </span>";
                    } else {
                        return "<span class='label label-danger'><span class='glyphicon glyphicon-close'></span>Not Paid " + td + "</span>";
                    }
                }
            },
            {
                data: "id",
                render: function (data, type, row) {
                    if (row.paid == 0) {
                        return "<button OnClick='OnPayment(" + data + ");' class='btn btn-warning btn-xs' style='margin-top:0px'><span class='glyphicon glyphicon-usd'></span> Pay Now</button>"
                             + "<button OnClick='EditInvoice (" + data + ")' class='btn btn-warning btn-xs' style='margin-top:0px'><span class='glyphicon glyphicon-edit'></span> Edit</button>"
                        ;
                    } else {
                        return "<div class='btn-group'><a href='#' class='btn btn-primary btn-xs'><span class='glyphicon glyphicon-cog'></span> Action</a><a href='#' class='btn btn-primary btn-xs dropdown-toggle' data-toggle='dropdown' aria-expanded='false'><span class='caret'></span></a>"
                                   + "<ul class='dropdown-menu'>"
                                     + "<li>"
                                        + "<button OnClick='EditInvoice (" + data + ")' class='btn btn-warning btn-xs' style='margin-top:0px'><span class='glyphicon glyphicon-edit'></span> Edit</button>"
                                        + "<button OnClick='InvoiceDetail11 (" + data + ")' class='btn btn-info btn-xs' style='margin-top:5px'><span class='glyphicon glyphicon-list-alt'></span> Detail</button>"
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
        
        $('#id').val(id);
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
        $('#totalpower').val(totalelectric.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'));

        $('#servicecharge').val(result.servicecharge);
        $('#totalpay').val(result.grandtotal);
        $('#totalpaykh').val(result.totalriel);
        $('#note').val(result.note);

        $('#checkinid').val(result.checkinid);

        GetPayDemage(result.guestid, stdate, enddate);
        //alert(result.wprerecord);
    },
    error: function (errormessage) {
        toastr.error("Load Record Error", "Service Response");
    }
    });

    $.get("/api/WEPrice/1/2", function (data) {
        $("#wpid").val(data.id);
        $("#wprice").text(data.waterprice);
        $("#pprice").text(data.powerprice);
    });


    $.get("/api/ExchangeRates/1/2", function (data) {
        $("#exrate").val(data.rate);
        $("#lblidroom").text(data.rate);
    });
}

function GetPayDemage(id, fromdate, todate) {
    $.ajax({
        url: "/api/paydemages/" + id + "/" + fromdate + "/" + todate,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {

            var valsum = 0;

            $.each(result, function (key, value) {
                valsum += parseFloat(value.price);
                $('#itemname').append("<label>" + value.item.itemname + "</label>" + "=" + "<label>" + value.price + "$, </label>");

            });
            if (valsum != '0') {
                $('#paydemage').text("សម្ភារៈខូចខាត ​ ");
                $('#total').text("Total =");
                $('#itemprice').text(valsum);
            }

        },
        error: function (errormessage) {
            toastr.error("No Record Select!", "Service Response");
        }
    });
}

function EditInvoice(id) {
    $("#PaymentModal").modal('show');
    document.getElementById('btnSavePayment').innerText = "Update";
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
            $('#weid').val(result.weid);
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
            $('#totalpower').val(totalelectric.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'));

            $('#paydollar').val(result.paydollar);
            $('#payriel').val(result.payriel);
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

    $.get("/api/WEPrice/1/2", function (data) {
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


function wendrecordChange() {
    var waternewrecord = document.getElementById('wendrecord').value;
    var wateroldrecord = document.getElementById('wstartrecord').value;
    $.ajax({
        url: "/api/ExchangeRates/1/2",
        method: "GET",
        success: function (data) {
            var rate = data.rate;
            var totalwaterusage = ((parseFloat(waternewrecord) - parseFloat(wateroldrecord)) * parseFloat($('#wprice').text())) / rate;
            if (totalwaterusage <= 0) {
                alert('Faild!');
                $('#wendrecord').focus();
            } else {
                $('#totalelectric').val(totalwaterusage.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'));
                TotalPay();
            }
        },
        error: function (err) {
            toastr.error("no data record");
        }
    });
}
function eendrecordChange() {
    var powernewrecord = document.getElementById('eendrecord').value;
    var powerroldrecord = document.getElementById('estartrecord').value;

    $.ajax({
        url: "/api/ExchangeRates/1/2",
        method: "GET",
        success: function (data) {
            var rate = data.rate;
            var totalwaterusage = ((parseInt(powernewrecord) - parseInt(powerroldrecord)) * parseFloat($('#pprice').text())) / rate;
            if (totalwaterusage <= 0) {
                alert('Faild!');
                $('#wendrecord').focus();
            } else {
                $('#totalelectric').val(totalwaterusage.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'));
                TotalPay();
            }
        },
        error: function (err) {
            toastr.error("no data record");
        }
    });

}
function TotalPay() {
    var totalwaterusage = document.getElementById('totalwater').value;
    var totalpowerusage = document.getElementById('totalelectric').value;
    var roomprice = document.getElementById('roomprice').value;
    var servicecharge = document.getElementById('servicecharge').value;
    var grandtotal = parseFloat(totalwaterusage) + parseFloat(totalpowerusage) + parseFloat(roomprice) + parseFloat(servicecharge);
    $('#totalpay').val(grandtotal.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'));
    $('#totalpaykh').val((grandtotal * 4130).toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'));


}


function OnPayAction() {
    var action = document.getElementById('btnSavePayment').innerText;
    if (action == "Save") {
        var data = new FormData();
        data.append("grandtotal", $("#totalpay").val());
        data.append("totalriel", $("#totalpaykh").val());
        data.append("totaldollar", $("#totalpay").val());
        data.append("totalother", $("#servicecharge").val());
        data.append("note", $("#note").val());
        data.append("payriel", $("#payriel").val());
        data.append("paydollar", $("#paydollar").val());
        data.append("paid", true);

        $.ajax({
            type: "PUT",
            url: "/api/invoices/" + $("#id").val(),
            contentType: false,
            processData: false,
            data: data,
            success: function (result) {
                //UpdateWaterPayment();
                //UpdatePowerPayment();
                toastr.success("Print Invoice successfully!.", "Server Response");
                window.location.reload(true);

                
            },
            error: function (errormesage) {
                toastr.error("Print invoice faild...!", "Server Respond");

            }
        });
    } else {

    }
    
};


function UpdateWaterPayment() {
    $.ajax({
        type: "PUT",
        url: "/api/updatewaters/" + $("#id").val() + "/" + $('#eendrecord').val(),
        contentType: false,
        processData: false,
        success: function (result) {
            //toastr.success("Update water payment has been Update successfully.", "Server Response");
        },
        error: function (error) {
            toastr.error("Update water payment status fail!", "Server Response");
        }
    });
}

function UpdatePowerPayment() {
    $.ajax({
        type: "PUT",
        url: "/api/updatepowers/" + $("#id").val() + "/" + $('#eendrecord').val(),
        contentType: false,
        processData: false,
        success: function (result) {
            //toastr.success("Room Status has been Update successfully.", "Server Response");
        },
        error: function (error) {
            toastr.error("Update room status fail!", "Server Response");
        }
    });
}
function OnClosePayment() {
    window.location.reload(true);
}


function TestAction() {
    $.ajax({
        url: "/api/invoice_v",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            window.location = "invoice-report/1";
        },
        error: function (errormessage) {
            toastr.error("Load Record Error", "Service Response");
        }
    });

    
}