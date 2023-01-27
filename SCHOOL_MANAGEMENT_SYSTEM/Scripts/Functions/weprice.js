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
            ],
    });
}

function AddWaterEletric() {
    $('#waterprice').removeAttr('readonly');
    $('#electricprice').removeAttr('readonly');
    document.getElementById('btnWEAction').innerText =="Save";
    $('#waterprice').focus()
}

function CloseWE() {
    window.location.reload(true);
}