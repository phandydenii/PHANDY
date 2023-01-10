$(document).ready(function () {
    $(document).ajaxStart(function () {
        $('#LoadingGif').addClass('show');
    }).ajaxStop(function () {
        $('#LoadingGif').removeClass('show');
    });
    $('#WEPriceModel').on('show.bs.modal', function () {
        GetWEPrice();
        alert('hi');
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
                    data: "date"
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