function OnClickRoom(b) {
    $("#RoomDetailModal").modal('show');
    $("#roomid").val(b);

    //Get Item
    $.ajax({
        url: "/api/roomdetails_v/" + b,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            $.each(result, function (key, value) {
                //$("#itemname").append(result[index].itemnamekh);
                //$('#exampleid').append("<tr>\<td>" + value.itemnamekh + "</td>\</tr>");
                $('#itlist').append("<li class='list-group-item'>" + "<span class='badge'>1</span>" + value.itemnamekh + " " + value.itemname + "</li>");
            });
        },
        error: function (errormessage) {
            toastr.error("No Record Select!", "Service Response");
        }
    });

    //Get Room By ID
    $.ajax({
        url: "/api/checkindetail_v/" + b,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {

            $("#guestnname").text(result.guestname);
            $("#guestnamekh").text(result.guestnamekh);
            $("#gender").text(result.sex);
            $("#phone").text(result.phone);
            $("#men").text(result.man);
            $("#women").text(result.women);
            $("#child").text(result.child);
            $("#checkindate").text(result.checkindate);
            $("#startdate").text(result.startdate);
            $("#enddate").text(result.enddate);
             
            
            
        },
        error: function (errormessage) {
            toastr.error("No Record Select!", "Service Response");
        }
    });
    

    $.ajax({
        url: "/api/rooms/" + b,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            $("#lblRoom").text('Room ' + result.room_no +" "+result.status);

            $("#rmno").text(result.room_no);
            $("#roomtype").text(result.roomtypename);
            $("#flno").text(result.floorno);
            $("#sc").text(result.servicecharge + '$');
            $("#roomprice").text(result.price + '$');
            $("#rmky").text(result.roomkey);
            $("#roomstatus").text(result.status);

            //$.each(result, function (key, value) {
            //    $('#itlist').append("<li class='list-group-item'>" + "<span class='badge'>1</span>" + value.itemnamekh + " " + value.itemname + "</li>");
            //});


            
            
        },
        error: function (errormessage) {
            toastr.error("No Record Select!", "Service Response");
        }
    });
}

$('#btnCloseRoomDetail').click(function () {
    window.location.reload(true);
});
