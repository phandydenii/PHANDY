$(document).ready(function () {
    $(document).ajaxStart(function () {
        $('#loadingGif').addClass('show');
    }).ajaxStop(function () {
        $('#loadingGif').removeClass('show');
    });
})


///========OnPaymentAction Button====
function OnPaymentAction() {
    var action = document.getElementById('SavePaymentbtn').innerText;
        var data = new FormData();
        data.append("grandtotal", $("#totalpay").val());
        data.append("totalriel", $("#totalpaykh").val());
        data.append("totaldollar", $("#totalpay").val());
        data.append("totalother", $("#servicecharge").val());
        data.append("note", $("#note").val());
        data.append("payriel", $("#payriel").val());
        data.append("paydollar", $("#paydollar").val());
        if (SavePaymentbtn == "Save") {
            data.append("paid", true);
        } else {
            data.append("paid", false);
        }

        $.ajax({
            type: "PUT",
            url: "/api/invoices/" + $("#id").val(),
            contentType: false,
            processData: false,
            data: data,
            success: function (result) {
                toastr.success("Print Invoice successfully!.", "Server Response");
                window.location = "invoice-report/" + $("#id").val();
            },
            error: function (errormesage) {
                toastr.error("Print invoice faild...!", "Server Respond");

            }
        });
};

function UpdateWaterElectric() {
    var data = new FormData();
    data.append("startdate", $('#startdate').val());
    data.append("enddate", $('#enddate').val());
    data.append("wendrecord", $('#wendrecord').val());
    data.append("eendrecord", $('#eendrecord').val());
    $.ajax({
        type: "PUT",
        url: "/api/updateweendrecord/" + $("#weid").val(),
        contentType: false,
        processData: false,
        data: data,
        success: function (result) {
            //
        },
        error: function (errormesage) {
            toastr.error("Electric usage insert faild!", "Server Respond");
            return false;
        }
    });
}


//For Update and Payment Invoice
function wendrecordChange() {
    var waternewrecord = document.getElementById('wendrecord').value;
    var wateroldrecord = document.getElementById('wstartrecord').value;
    $.ajax({
        url: "/api/ExchangeRates/1/2",
        method: "GET",
        success: function (data) {
            var rate = data.rate;
            var totalwaterusage = ((parseFloat(waternewrecord) - parseFloat(wateroldrecord)) * parseFloat($('#wprice').text())) / parseFloat(rate);
            if (totalwaterusage <= 0) {
                alert('Faild!');
                $('#wendrecord').focus();
            } else {
                $('#totalwater').val(totalwaterusage.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'));
                TotalPay();
            }
        },
        error: function (err) {
            toastr.error("no data record");
        }
    });
}
function electricChange() {
    var eend = document.getElementById('eendrecord').value;
    var estart = document.getElementById('estartrecord').value;
    $.ajax({
        url: "/api/ExchangeRates/1/2",
        method: "GET",
        success: function (data) {
            var rate = data.rate;
            var totalelectricusage = ((parseFloat(eend) - parseFloat(estart)) * parseFloat($('#pprice').text())) / parseFloat(rate);
            if (totalelectricusage <= 0) {
                alert('Faild!');
                $('#eendrecord').focus();
            } else {
                $('#totalelectric').val(totalelectricusage.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'));
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
function OnClosePayment() {
    window.location.reload(true);
}
