
$(document).ready(function () {
    $('#PrintNewInvoiceModal').on('show.bs.modal', function () {
        $('#odlrecordwater').focus();
    });
    $('#HistoryModal').on('show.bs.modal', function () {
        $('#rowhide').hide();
    });
    GetCheckInDetail();
})

var tbCheckInDetail = [];
function GetCheckInDetail() {
    tbCheckInDetail = $('#tableCheckInDetail').dataTable({
        ajax: {
            url: "/api/checkin_v",
            dataSrc: ""   
        },
        columns:
            [
                {
                    data: "id"
                },
                {
                    data: "namekh"
                },
                {
                    data: "checkindate",
                    render: function (data) {
                        return moment(new Date(data)).format('DD-MMM-YYYY');
                    }
                },
                
                {
                    data: "room_no"
                },
                {
                    data: "roomtypename"
                },
                {
                    data: "payforroom",
                },
                {
                    data: "prepaid",
                },
                {
                    data: "man"
                },
                {
                    data: "women"
                },
                {
                    data: "child",
                },
                {
                    data: "id",
                    render: function (data, type, row) {
                        return   "<button OnClick='CheckInEdit (" + data + ")' class='btn btn-warning btn-xs' style='margin-top:0px'><span class='glyphicon glyphicon-edit'></span> Edit</button>"
                            + "<button onclick='CheckOut(" + row.id + " ," + row.guestid + " , " + row.roomid + ")' class='btn btn-success btn-xs' style='border-width: 0px;margin-left:5px'><span class='glyphicon glyphicon-log-out'></span> Check Out</button>"
                        ;    
                    }
                }
            ],
        
        destroy: true,
    });
}

function CheckInEdit(id) {
    $("#CheckInModal").modal("show");
    $("#checkinid").val(id);
    $.ajax({
        url: "/api/checkin_v/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            //alert(result[0].wstartrecord);
            var checkindate = moment(result[0].checkindate).format("YYYY-MM-DD");
            var startdate = moment(result[0].checkindate).format("YYYY-MM-DD");
            var enddate = moment(result[0].checkindate).format("YYYY-MM-DD");
            $('#roomid').val(result[0].roomid);
            $('#checkindate').val(checkindate);
            $('#startdate').val(checkindate);
            $('#enddate').val(checkindate);
            if (result[0].active == 1) {
                $('#wrecord').attr('readonly', 'readonly');
                $('#precord').attr('readonly', 'readonly');
            }
            $('#guestid').val(result[0].guestid);
            $('#name').val(result[0].name);
            $('#namekh').val(result[0].namekh);
            $('#man').val(result[0].man);
            $('#women').val(result[0].women);
            $('#child').val(result[0].child);

            $('#weid').val(result[0].weid);
            $('#wrecord').val(result[0].wstartrecord);
            $('#precord').val(result[0].estartrecord);
            $('#prepaid').val(result[0].prepaid);
            $('#payforroom').val(result[0].payforroom);
            $('#total').val(result[0].payforroom + result[0].prepaid);
            $('#paydollar').val(result[0].paydollar);
            $('#payriel').val(result[0].payriel);

            $.get("/api/ExchangeRates/1/2", function (data) {
                $("#exrate").val(data.rate);
            });
        },
        error: function (errormessage) {
            toastr.error("No Record Select!", "Service Response");
        }
    });
}

function CheckOut(id,gid, rid) {
    $("#CheckOutModal").modal("show");
    $.ajax({
        url: "/api/checkin_v/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            $("#chinid").val(result[0].id);
            $("#gid").val(result[0].guestid);
            $("#rmid").val(result[0].roomid);
            var stdate = moment(result[0].enddate).format("YYYY-MM-DD");

            const EndDate = new Date();
            const StartDate = new Date(result[0].enddate);
            const oneDay = 1000 * 60 * 60 * 24;
            const start = Date.UTC(EndDate.getFullYear(), EndDate.getMonth(), EndDate.getDate());
            const end = Date.UTC(StartDate.getFullYear(), StartDate.getMonth(), StartDate.getDate());
            var countday = (start-end) / oneDay;


            var totalday = countday;
            if (countday >= 30) {
                totalday = 30;
            }
            $("#startdatechout").val(stdate);
            $("#wstart").val(result[0].wendrecord);
            $("#estart").val(result[0].eendrecord);
            $("#rmprice").val(result[0].price);
            $("#svprice").val(result[0].servicecharge);
            $("#paybefor").val(result[0].price + result[0].prepaid);
            $("#totalroomprice").val((result[0].price / 30 * parseInt(totalday)).toFixed(2));
        },
        error: function (errormessage) {
            toastr.error("No Record Select!", "Service Response");
        }
    });
    $.get("/api/WEPrice/1/2", function (data) {
        $("#wpid").val(data.id);
        $("#wprice").val(data.waterprice);
        $("#pprice").val(data.electricprice);
    });
    $.get("/api/ExchangeRates/1/2", function (data) {
        $("#exrate").val(data.rate);
        $("#lblExchangeRate").text("1 $ = " + data.rate + " រៀល");
    });
}


