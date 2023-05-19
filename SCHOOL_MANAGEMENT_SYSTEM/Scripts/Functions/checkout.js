
$(document).ready(function () {
    GetCheckOut();
})

var tbCheckOut = [];
function GetCheckOut() {
    tbCheckOut = $('#tableCheckOut').dataTable({
        ajax: {
            url: "/api/checkouts",
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
                    data: "guest.name"
                },
                {
                    data: "guest.namekh"
                },
                {
                    data: "total"
                },
                {
                    data: "paybefor"
                },
                {
                    data: "returnamount",

                },
                {
                    data: "totalpayment",

                },
                {
                    data: "id",
                    render: function (data, type, row) {
                        const EndDate = new Date();
                        const StartDate = new Date(row.date);
                        const oneDay = 1000 * 60 * 60 * 24;
                        const start = Date.UTC(EndDate.getFullYear(), EndDate.getMonth(), EndDate.getDate());
                        const end = Date.UTC(StartDate.getFullYear(), StartDate.getMonth(), StartDate.getDate());
                        var result = (start - end) / oneDay;

                        return "<button OnClick='EditCheckOut (" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-edit'></span> Edit</button>"
                            + "<button OnClick='ReturnCheckOut (" + data + ")' class='btn btn-danger btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-repeat'></span> Return</button>"
                        ; 
                    }
                }
            ],
        destroy: true,
    });
}

function EditCheckOut(id) {
    $('#CheckOutModal').modal("show")
    $.ajax({
        url: "/api/checkout-v/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            var res = result[0];
            var date = moment(res.date).format("YYYY-MM-DD");
            $("#checkoutid").val(id);
            $("#checkoutdate").val(date);
            $("#checkoutid").val(res.id);
            $("#rmid").val(res.roomid);
            $("#gid").val(res.gid);
            $("#weid").val(res.weid);
            var stdate = moment(res.startdate).format("YYYY-MM-DD");
            var endate = moment(res.enddate).format("YYYY-MM-DD");
            $("#startdatechout").val(stdate);
            $("#enddatechout").val(endate);

            $("#wstart").val(res.wstartrecord);
            $("#wendrecords").val(res.wendrecord);
            $("#estart").val(res.estartrecord);
            $("#eendrecords").val(res.eendrecord);

            $("#rmprice").val(res.price);
            $("#svprice").val(res.servicecharge);
            $("#paybefor").val(res.paybefor);
            $("#totalroomprice").val(res.totalroomprice);

            $("#wtotal").val(res.totalwater.toFixed(2));
            $("#etotal").val(res.totaleletric.toFixed(2));
            $("#returnamount").val(res.returnamount);
            $("#grandtotal").val(res.grandtotal);
            $("#paydollar").val(res.paydollar);
            $("#payriel").val(res.payriel);
            $("#totalpayment").val(res.totalpayment);
            $("#description").val(res.description);
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


function ReturnCheckOut(id) {
    $.ajax({
        url: "/api/checkout-v/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            var res = result[0];
            var date = moment(res.date).format("YYYY-MM-DD");
            $("#checkoutid").val(id);
            $("#checkoutdate").val(date);
            $("#checkoutid").val(res.id);
            $("#rmid").val(res.roomid);
            $("#gid").val(res.gid);
            $("#weid").val(res.weid);
            var stdate = moment(res.startdate).format("YYYY-MM-DD");
            var endate = moment(res.enddate).format("YYYY-MM-DD");
            $("#startdatechout").val(stdate);
            $("#enddatechout").val(endate);

            $("#wstart").val(res.wstartrecord);
            $("#wendrecords").val(res.wendrecord);
            $("#estart").val(res.estartrecord);
            $("#eendrecords").val(res.eendrecord);

            $("#rmprice").val(res.price);
            $("#svprice").val(res.servicecharge);
            $("#paybefor").val(res.paybefor);
            $("#totalroomprice").val(res.totalroomprice);

            $("#wtotal").val(res.totalwater.toFixed(2));
            $("#etotal").val(res.totaleletric.toFixed(2));
            $("#returnamount").val(res.returnamount);
            $("#grandtotal").val(res.grandtotal);
            $("#paydollar").val(res.paydollar);
            $("#payriel").val(res.payriel);
            $("#totalpayment").val(res.totalpayment);
            $("#description").val(res.description);
        },
        error: function (errormessage) {
            toastr.error("No Record Select!", "Service Response");
        }
    });
    bootbox.confirm({
        message: "Are you sure want to return this record?",
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
            if (result) {
                UpdateStatusRoom($("#rmid").val());
                UpdateGuestStatus($("#gid").val());
                DeleteCheckOut(id);
                DeleteWE($("#weid").val());
                toastr.success("Return successfully.", "Server Response");
                window.location.reload(true);
            }
        }
    });
}

function UpdateStatusRoom(id) {
    $.ajax({
        type: "PUT",
        url: "/api/updateroomstatus/" + id + "/CHECK-IN",
        contentType: false,
        processData: false,
        // data: status,
        success: function (result) {
        },
        error: function (error) {
            toastr.error("Update room status fail!", "Server Response");
        }
    });
}
function UpdateGuestStatus(id) {
    $.ajax({
        type: "PUT",
        url: "/api/updategueststatus/" + id + "/CheckIn",
        contentType: false,
        processData: false,
        // data: status,
        success: function (result) {
        },
        error: function (error) {
            toastr.error("Update room status fail!", "Server Response");
        }
    });
}


function DeleteCheckOut(id) {
    $.ajax({
        type: "DELETE",
        url: "/api/checkouts/" + id,
        contentType: false,
        processData: false,
        // data: status,
        success: function (result) {
        },
        error: function (error) {
            toastr.error("Update room status fail!", "Server Response");
        }
    });
}

function DeleteWE(id) {
    $.ajax({
        type: "DELETE",
        url: "/api/WaterElectricUsages/" + id,
        contentType: false,
        processData: false,
        // data: status,
        success: function (result) {
        },
        error: function (error) {
            toastr.error("Update room status fail!", "Server Response");
        }
    });
}


