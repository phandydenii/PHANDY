function OnSaveCheckInNow() {
    InsertGuest();
}

function InsertGuest() {
    var data = new FormData();
    data.append("name", $("#name").val());
    data.append("namekh", $("#namekh").val());
    data.append("sex", $("#gsex").val());
    data.append("dob", $("#dob").val());
    data.append("address", $("#addr").val());
    data.append("nationality", $("#national").val());
    data.append("phone", $("#phone").val());
    data.append("email", $("#gemail").val());
    data.append("ssn", $("#gssn").val());
    data.append("passport", $("#gpassport").val());
    data.append("status", 'CHECK-IN');
    $.ajax({
        type: "POST",
        url: "/api/guests",
        contentType: false,
        processData: false,
        data: data,
        success: function (result) {
            CheckInNow(result);
            $('#guestid').val(result);
            //toastr.success("Guest in successfully!", "Server Respond");
        },
        error: function (error) {
            //console.log(error);
            toastr.error("Please check all selected field!.", "Server Response");
        }
    });

}

function CheckInNow(gid) {
    var data = {
        roomid: $('#chnowroomid').val(),
        guestid: gid,
        child: $('#numchild').val(),
        man: $('#numman').val(),
        women: $('#numwomen').val(),
        pay: $('#pay').val(),
    };
    $.ajax({
        url: "/api/checkins",
        data: JSON.stringify(data),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            UpdateRoomStatus();
            //InsertPower(result);
            //InsertWater(result);
            //CreateInvoice();
            InsertWaterElectricUsage(result);

           // $('#checkinid').val(result);
            
        },
        error: function (errormesage) {
            toastr.error("Check In faild!", "Server Respond");
            return false;
        }

    });
}

function UpdateRoomStatus() {
    $.ajax({
        type: "PUT",
        url: "/api/updateroomstatus/" + $("#chnowroomid").val() + "/CHECK-IN",
        contentType: false,
        processData: false,
        // data: status,
        success: function (result) {
            //toastr.success("Room Status has been Update successfully.", "Server Response");
        },
        error: function (error) {
            toastr.error("Update room status fail!", "Server Response");
        }
    });
}


function InsertWaterElectricUsage(id) {
    var data = new FormData();
    data.append("checkinid", id);
    data.append("startdate", $('#datenow').val());
    data.append("enddate", $('#datenow').val());
    data.append("wstartrecord", $('#wrecord').val());
    data.append("wendrecord", $('#wrecord').val());
    data.append("estartrecord", $('#erecord').val());
    data.append("eendrecord", $('#erecord').val());
    $.ajax({
        type: "POST",
        url: "/api/waterelectricusages",
        contentType: false,
        processData: false,
        data: data,
        success: function (result) {
            CreateInvoice();
        },
        error: function (errormesage) {
            toastr.error("Electric usage insert faild!", "Server Respond");
            return false;
        }
    });
}


function InsertPower(id) {
    var data = new FormData();
    data.append("checkinid", id);
    data.append("predate", $('#datenow').val());
    data.append("prerecord", $('#erecord').val());
    data.append("currentdate", $('#datenow').val());
    data.append("currentrecord", $('#erecord').val());
    $.ajax({
        type: "POST",
        url: "/api/electrics",
        contentType: false,
        processData: false,
        data: data,
        success: function (result) {
            //$('#electricid').val(result);
        },
        error: function (errormesage) {
            toastr.error("Electric usage insert faild!", "Server Respond");
            return false;
        }
    });
}

function InsertWater(id) {
    var data = new FormData();
    data.append("checkinid", id);
    data.append("predate", $('#datenow').val());
    data.append("prerecord", $('#wrecord').val());
    data.append("currentdate", $('#datenow').val());
    data.append("currentrecord", $('#wrecord').val());
    $.ajax({
        type: "POST",
        url: "/api/waterusage",
        contentType: false,
        processData: false,
        data: data,
        success: function (result) {
            //$('#waterid').val(result);
            CreateInvoice();
        },
        error: function (errormesage) {
            toastr.error("Water usage insert faild!", "Server Respond");
            return false;
        }

    });
}

function CreateInvoice() {
    var data = {
        guestid: $('#guestid').val(),
        roomid: $('#chnowroomid').val(),
    };
    $.ajax({
        url: "/api/invoices",
        data: JSON.stringify(data),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            toastr.success("Check In successfully.", "Server Response");
            window.location.reload(true);
        },
        error: function (errormesage) {
            toastr.error("Insert invoice faild...!", "Server Respond");
        }
    });
}