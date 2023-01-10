
$(document).ready(function () {
    $('#PrintNewInvoiceModal').on('show.bs.modal', function () {
        $('#odlrecordwater').focus();
    });
    GetCheckInDetail();
})

var tbCheckInDetail = [];
function GetCheckInDetail() {
    tbCheckInDetail = $('#tableCheckInDetail').dataTable({
        ajax: {
            url: "/api/checkins",
            dataSrc: ""   
        },
        columns:
            [
                {
                    data: "id"
                },
                {
                    data: "guest.name"
                },
                {
                    data: "checkindate",
                    render: function (data) {
                        return moment(new Date(data)).format('DD-MMM-YYYY');
                    }
                },
                
                {
                    data: "room.room_no"
                },
                {
                    data: "prepaid",
                },
                //{
                //    data: "guest.id",
                //    render: function (data, type, row) {
                //        var chcekindate = new Date(row.checkindate).getDay();
                //        var now =new Date().getDay();
                //        var m = now - chcekindate;
                //        if(m>=0){
                //        return "<button OnClick='PrintInvoice (" + data + ")' class='btn btn-warning btn-xs' style='margin-top:0px''><span class='glyphicon glyphicon-edit'></span> Print "+m+"</button>"
                //        } else {
                //            return "";
                //        }
                //    }
                //},
                {
                    data: "id",
                    render: function (data, type, row) {
                        return "<div class='btn-group'><a href='#' class='btn btn-primary btn-xs'><span class='glyphicon glyphicon-cog'></span> Action</a><a href='#' class='btn btn-primary btn-xs dropdown-toggle' data-toggle='dropdown' aria-expanded='false'><span class='caret'></span></a>"
                                    + "<ul class='dropdown-menu'>"
                                    + "<li>"
                                        + "<button OnClick='CheckInEdit (" + data + ")' class='btn btn-warning btn-xs' style='margin-top:0px''><span class='glyphicon glyphicon-edit'></span> Edit</button>"
                                        + "<button OnClick='CheckOut (" + data + ")' class='btn btn-primary btn-xs' style='margin-top:5px'><span class='glyphicon glyphicon-log-out'></span> Check Out</button>"
                                        + "<button OnClick='PayDamages (" + data + ")' class='btn btn-info btn-xs' style='margin-top:5px'><span class='glyphicon glyphicon-pencil'></span> Pay Damages</button>"
                                    + "</li>"
                                    + "</ul>"
                                + "</div>"
                        ;                  
                    }
                }
            ],
        
        destroy: true,
    });
}

//function PrintInvoice(id) {
//    $("#PrintNewInvoiceModal").modal("show");
//    $.ajax({
//        url: "/api/invoice-v/newinvoie/1",
//        type: "GET",
//        contentType: "application/json;charset=utf-8",
//        datatype: "json",
//        success: function (result) {
//            $("#checkinid").val(result[0]["checkinid"]);
//            alert(result[0]["checkinid"]);
//            $('#name').val(result[0]["name"]);
//            $('#roomid').val(result[0]["roomid"]);
//            $("#room_no").val(result[0]["room_no"]);
//            $("#roompricee").val(result[0]["price"]);
//            $("#svprice").val(result[0]["servicecharge"]);
//            $("#recordpowerold").val(result[0]["estartrecord"]);
//            $("#recordwaterold").val(result[0]["wstartrecord"]);
//            $("#weid").val(result[0]["weid"]);
//            var checkindid = result[0]["checkinid"];
//            var enddate = moment(result[0]["enddate"]).format("YYYY-MM-DD");
//            $("#begindate").val(enddate);
//            $("#guestid").val(result[0]["guestid"]);
//            $.get("/api/invoicemaxid", function (data) {
//                $('#invno').val(data);
//            });
//        },
//        error: function (errormessage) {
//            toastr.error("No Record Select!", "Service Response");
//        }
//    });

//    $.get("/api/ExchangeRates/1/2", function (data) {
//        $('#exrate').val(data.rate);
//    });

//    $.get("/api/WEPrice/1/1", function (data) {
//        $('#wprice').val(data.waterprice);
//        $('#pprice').val(data.electricprice);
//    });
//}

$('#itemid').on('change', function () {
    $("#itemprice").val("");
    if ($("#itemid").val() != "0") {
        $.get("/api/items/" + $("#itemid").val(), function (data) {
            $("#itemprice").val(data.price);
        });
    }
});

$('#fromdate').on('change', function () {
    var fromdate = this.value;
    var today = $('#today').val();
    GetPayDemages($('#checkinid').val(), fromdate, today);

});

function PayDamages(guestid) {

    $("#PayDamagesModal").modal("show");
    $('#guestid').val(guestid);
    var fromdate = $('#fromdate').val();
    var today = $('#today').val();
    GetPayDemages(guestid, fromdate, today);
}

function GetPayDemages(id,fromdate,todate) {
    $('#tablePayDamages').DataTable({
        ajax: {
            url: "/api/paydemages/" + id + "/" + fromdate + "/" + todate,
            dataSrc: ""
        },
        columns: [
            //{
            //    data: "id",
            //},
            {
                data: "item.itemname",
            },
            {
                data: "price",
            },
            {
                data: "id",
                render: function (data, type, row) {
                    return "<button OnClick='EditProp(" + data + ");' class='btn btn-warning btn-xs' style='margin-top:0px'><span class='glyphicon glyphicon-edit'></span> Edit</button>"
                         + "<button OnClick='DeletePrp(" + data + "," + row.propertyname + ");' class='btn btn-danger btn-xs' style='margin-top:0px;margin-left:5px'><span class='glyphicon glyphicon-trash'></span> Delete</button>"
                    ;
                }
            }
        ],

        destroy: true,
    });
}


function DeletePrp(id, propertyname) {
    bootbox.confirm({
        title: "",
        message: "<h3>Are you sure want to delete  " + propertyname + " ?</h3>",
        buttons: {
            confirm: {
                label: 'Yes',
                className: 'btn-success btn-sm'
            },
            cancel: {
                label: 'No',
                className: 'btn-danger btn-sm'
            }
        },
        callback: function (result) {
            if (result) {
                $.ajax({
                    url: "/api/paydemages/" + id,
                    type: "DELETE",
                    contentType: "application/json;charset=utf-8",
                    datatype: "json",
                    success: function (result) {
                        toastr.success("Record delete successfully!", "Service Response");
                        $('#tablePayDamages').DataTable().ajax.reload();
                    },
                    error: function (errormessage) {
                        toastr.error("Record delte faild!", "Service Response");
                    }
                });
            }
        }
    });
}

function CheckInEdit(id) {
    $("#CheckInModal").modal("show");
    $("#checkinid").val(id);
    document.getElementById('btnSaveCheckIn').innerText == "Update";
    $.ajax({
        url: "/api/checkins/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            alert(result.id);
            $('#guestname').val(result.guest.name);
            $('#guestnamekh').val(result.guest.namekh);
            var checkindate = moment(result.checkindate).format("YYYY-MM-DD");
            $('#checkindate').val(checkindate);
            $('#roomno').val(result.room.room_no);
            $('#servicecharge').val(result.room.servicecharge);
            $('#roomprice').val(result.room.price);
            var startdate = moment(result.startdate).format("YYYY-MM-DD");
            var enddate = moment(result.enddate).format("YYYY-MM-DD")
            $('#startdate').val(startdate);
            $('#enddate').val(enddate);
            $('#man').val(result.man);
            $('#women').val(result.women);
            $('#child').val(result.child);
            $('#wrecord').val(result.woldrecord);
            $('#precord').val(result.poldrecord);
        },
        error: function (errormessage) {
            toastr.error("No Record Select!", "Service Response");
        }
    });
}

function SaveCheckIn() {
    var data = new FormData();
    data.append("startdate", $("#startdate").val());
    data.append("enddate", $("#enddate").val());
    data.append("child", $("#child").val());
    data.append("man", $("#man").val());
    data.append("women", $("#women").val());
       
    $.ajax({
        type: "PUT",
        url: "/api/checkins/"+$('#id').val(),
        contentType: false,
        processData: false,
        data: data,
        success: function (result) {
            UpdateWater();
            UpdatePower();
            toastr.success("Update check in successfully!", "Server Respond");
        },
        error: function (error) {
            //console.log(error);
            toastr.error("Please check all selected field!.", "Server Response");
        }
    });
}

function UpdateWater() {
    var data = {
        predate: $('#checkindate').val(),
        checkinid: $('#checkinid').val(),
        prerecord: $('#wrecord').val(),
    };
    $.ajax({
        url: "/api/updatewaters/"+data.checkinid+"/"+data.predate,
        data: JSON.stringify(data),
        type: "PUT",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
           
        },
        error: function (errormesage) {
            toastr.error("Create invoice faild...!"+errormesage, "Server Respond");
        }
    });
}

function UpdatePower() {
    var data = {
        predate: $('#checkindate').val(),
        checkinid: $('#checkinid').val(),
        prerecord: $('#precord').val(),
    };
    $.ajax({
        url: "/api/updatepowers/" + data.checkinid + "/" + data.predate,
        data: JSON.stringify(data),
        type: "PUT",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {

        },
        error: function (errormesage) {
            toastr.error("Create invoice faild...!" + errormesage, "Server Respond");
        }
    });
}

function SavePayDemage() {
    if ($('#price').val() == "") {
        $('#price').focus();
        return false;
    }
    if ($('#itemid').val() != 0) {
        var action = document.getElementById('btnSavePayDemage').innerText;
        if (action == "Save") {
            var data = new FormData();
            data.append("guestid", $('#guestid').val());
            data.append("itemid", $('#itemid').val());
            data.append("price", $('#price').val());
            data.append("note", $('#note').val());
            $.ajax({
                type: "POST",
                url: "/api/paydemages",
                contentType: false,
                processData: false,
                data: data,
                success: function (result) {
                    $('#tablePayDamages').DataTable().ajax.reload();
                },
                error: function (errormesage) {
                    toastr.error("Water usage insert faild!", "Server Respond");
                    return false;
                }

            });
        } else if (action == "Update") {
            var data = new FormData();
            data.append("guestid", $('#guestid').val());
            data.append("itemid", $('#itemid').val());
            data.append("price", $('#price').val());
            data.append("note", $('#note').val());
            $.ajax({
                type: "PUT",
                url: "/api/paydemages/" + $('#pdid').val(),
                contentType: false,
                processData: false,
                data: data,
                success: function (result) {
                    $('#tablePayDamages').DataTable().ajax.reload();
                },
                error: function (errormesage) {
                    toastr.error("Water usage insert faild!", "Server Respond");
                    return false;
                }

            });
        }
    } else {
        alert('Please choose item');
        $('#itemid').focus();
    }

}

function EditProp(id) {
    $("#PayDamagesModal").modal("show");
    document.getElementById('btnSavePayDemage').innerText = "Update";
    $.ajax({
        url: "/api/paydemages/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            $('#pdid').val(result.id);
            $('#itemid').val(result.itemid);
            $('#price').val(result.price);
            $('#note').val(result.note);
        },
        error: function (errormessage) {
            toastr.error("No Record Select!", "Service Response");
        }
    });
}

function CloseForm() {
    window.location.reload(true);
}