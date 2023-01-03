$(document).ready(function () {
    $('#CheckOutModal').on('show.bs.modal', function () {
        TotalPayment();
    });
})

function CheckOutAction() {
    InsertPowerCheckOut();
    InsertWaterCheckOut();
}


function InsertPowerCheckOut() {
    var data = new FormData();
    data.append("checkinid", $('#chinid').val());
    data.append("predate", $('#startdate').val());
    data.append("prerecord", $('#eoldrecord').val());
    data.append("currentdate", $('#enddate').val());
    data.append("currentrecord", $('#enewrecord').val());

    $.ajax({
        type: "POST",
        url: "/api/electrics",
        contentType: false,
        processData: false,
        data: data,
        success: function (result) {
            //
        },
        error: function (errormesage) {
            toastr.error("Water usage insert faild!", "Server Respond");
            return false;
        }

    });
}
function InsertWaterCheckOut() {
    var data = new FormData();
    data.append("checkinid", $('#chinid').val());
    data.append("predate", $('#startdate').val());
    data.append("prerecord", $('#woldrecord').val());
    data.append("currentdate", $('#enddate').val());
    data.append("currentrecord", $('#wnewrecord').val());

    $.ajax({
        type: "POST",
        url: "/api/waterusage",
        contentType: false,
        processData: false,
        data: data,
        success: function (result) {
            InsertCheckOut();
        },
        error: function (errormesage) {
            toastr.error("Water usage insert faild!", "Server Respond");
            return false;
        }
    });
}

function InsertCheckOut() {
    var data = {
        roomid: $('#rmid').val(),
        guestid: $('#gid').val(),
        total: $('#totalamount').val(),
        paybefor: $('#totalpaybefor').val(),
        returnamount: $('#returnamt').val(),
        totalpayment: $('#totalpay').val(),
        description: $('#description').val(),
    };
    $.ajax({
        url: "/api/checkouts",
        data: JSON.stringify(data),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            //$('#checkoutid').val(result)
            CheckOutDetail();
            UpdateStatusGuest();
            UpdateRoomStatusCheckOut(result);
            toastr.success("Check In successfully.", "Server Response");
            window.location.reload(true);
        },
        error: function (errormesage) {
            toastr.error("This Name Guest is exist in Database", "Server Respond");
            return false;
        }

    });
}

function CheckOutDetail(id) {

    var data = new FormData();
    data.append("checkoutid", id);
    data.append("checkinid", $('#chinid').val());
    data.append("fromdate", $('#startdate').val());
    data.append("todate", $('#enddate').val());

    $.ajax({
        url: "/api/checkoutdetails",
        data: JSON.stringify(data),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            //toastr.success("Check out successfully.", "Server Response");
        },
        error: function (errormesage) {
            toastr.error("This Name Guest is exist in Database", "Server Respond");
            return false;
        }

    });
}
function UpdateRoomStatusCheckOut() {
    $.ajax({
        type: "PUT",
        url: "/api/updateroomstatus/" + $("#rmid").val() + "/FREE",
        contentType: false,
        processData: false,
        // data: status,
        success: function (result) {
            //
        },
        error: function (error) {
            toastr.error("Unblock room fail!", "Server Response");
        }
    });
}
function UpdateStatusGuest() {
    $.ajax({
        type: "PUT",
        url: "/api/updategueststatus/" + $("#guestid").val() + "/CHECK-OUT",
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


function RecordWaterChange() {
    var wnewrecord = document.getElementById('wnewrecord').value;
    var woldrecord = document.getElementById('woldrecord').value;
    var wtotal = ((parseFloat(wnewrecord) - parseFloat(woldrecord)) * parseFloat($('#wprice').val())) / parseFloat($('#exrate').val());
    if (wtotal <= 0) {
        alert('Faild!');
        $('#wnewrecord').focus();
    } else {
        $('#wtotal').val(wtotal.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'));
        TotalPayment();
    }
}
function RecordElectricChange() {
    var enewrecord = document.getElementById('enewrecord').value;
    var eoldrecord = document.getElementById('eoldrecord').value;
    var etotal = ((parseFloat(enewrecord) - parseFloat(eoldrecord)) * parseFloat($('#pprice').val())) / parseFloat($('#exrate').val());
    if (etotal <= 0) {
        alert('Faild!');
        $('#enewrecord').focus();
    } else {
        $('#etotal').val(etotal.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'));
        TotalPayment();
    }
}

function TotalPayment() {
    var watertotal = document.getElementById('wtotal').value;
    var eletrictotal = document.getElementById('etotal').value;
    var roomprice = document.getElementById('rmprice').value;
    var servicecharge = document.getElementById('svprice').value;
    var totalpaybefor = document.getElementById('totalpaybefor').value;
    var table = document.getElementById("tblPayDemage"),
    sumVal = 0;
    for (var i = 1; i < table.rows.length; i++) {
        sumVal = sumVal + parseFloat(table.rows[i].cells[1].innerHTML);
    }
    var grandtotal = parseFloat(watertotal) + parseFloat(eletrictotal) + parseFloat(roomprice) + parseFloat(servicecharge) + parseFloat(sumVal);
    $('#totalamount').val(grandtotal.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'));
    $('#grandtotalkh').val((grandtotal * parseFloat($('#exrate').val())).toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'));
    var totalreturn = grandtotal - parseFloat(totalpaybefor);
    if (totalreturn >= 0) {
        $('#totalpay').val(totalreturn.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'));
    } else {
        $('#returnamt').val((totalreturn * -1).toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'));
    }
    alert(sumVal);
}