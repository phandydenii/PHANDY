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
            InsertWEUsage(result);
            $('#guestid').val(result);
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
        payforroom: $('#payforrooms').val(),
        paydollar: $('#paydollar').val(),
        payriel: $('#payriel').val(),
    };
    $.ajax({
        url: "/api/checkins",
        data: JSON.stringify(data),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            UpdateRoomStatus();
        },
        error: function (errormesage) {
            toastr.error("This Name Guest is exist in Database", "Server Respond");
            return false;
        }
    });
}




function InsertWEUsage(id) {
    
    var data = new FormData();
    data.append("guestid", id);
    data.append("startdate", $('#checkindate').val());
    data.append("enddate", $('#checkindate').val());
    data.append("wstartrecord", $('#wstartrecords').val());
    data.append("wendrecord", $('#wstartrecords').val());
    data.append("estartrecord", $('#estartrecords').val());
    data.append("eendrecord", $('#estartrecords').val());
    $.ajax({
        type: "POST",
        url: "/api/waterelectricusages",
        contentType: false,
        processData: false,
        data: data,
        success: function (result) {
            toastr.success("Check In successfully.", "Server Response");
            window.location.reload(true);
        },
        error: function (errormesage) {
            toastr.error("Electric usage insert faild!", "Server Respond");
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

