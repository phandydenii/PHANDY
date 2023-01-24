
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
                    data: "name"
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
                    data: "payforroom",
                },
                {
                    data: "id",
                    render: function (data, type, row) {
                        return "<button OnClick='CheckInEdit (" + data + ")' class='btn btn-warning btn-xs' style='margin-top:0px''><span class='glyphicon glyphicon-edit'></span> Edit</button>"
                             + "<button OnClick='CheckOut (" + data + ")' class='btn btn-primary btn-xs' style='margin-left:5px'><span class='glyphicon glyphicon-log-out'></span> Check Out</button>"
                        ;

                        //if (row.active == 0) {
                        //    return "<div class='btn-group'><a href='#' class='btn btn-primary btn-xs'><span class='glyphicon glyphicon-cog'></span> Action</a><a href='#' class='btn btn-primary btn-xs dropdown-toggle' data-toggle='dropdown' aria-expanded='false'><span class='caret'></span></a>"
                        //            + "<ul class='dropdown-menu'>"
                        //            + "<li>"
                        //                + "<button OnClick='CheckInEdit (" + data + ")' class='btn btn-warning btn-xs' style='margin-top:0px''><span class='glyphicon glyphicon-edit'></span> Edit</button>"
                        //                + "<button OnClick='CheckOut (" + data + ")' class='btn btn-primary btn-xs' style='margin-top:5px'><span class='glyphicon glyphicon-log-out'></span> Check Out</button>"
                        //                //+ "<button OnClick='PayDamages (" + row.guestid + ")' class='btn btn-info btn-xs' style='margin-top:5px'><span class='glyphicon glyphicon-pencil'></span> Pay Damages</button>"
                        //            + "</li>"
                        //            + "</ul>"
                        //        + "</div>"
                        //    ;
                        //}
                        //else {
                        //    return "<div class='btn-group'><a href='#' class='btn btn-primary btn-xs'><span class='glyphicon glyphicon-cog'></span> Action</a><a href='#' class='btn btn-primary btn-xs dropdown-toggle' data-toggle='dropdown' aria-expanded='false'><span class='caret'></span></a>"
                        //            + "<ul class='dropdown-menu'>"
                        //            + "<li>"
                        //                + "<button OnClick='CheckOut (" + data + ")' class='btn btn-primary btn-xs' style='margin-top:5px'><span class='glyphicon glyphicon-log-out'></span> Check Out</button>"
                        //                //+ "<button OnClick='PayDamages (" + row.guestid + ")' class='btn btn-info btn-xs' style='margin-top:5px'><span class='glyphicon glyphicon-pencil'></span> Pay Damages</button>"
                        //            + "</li>"
                        //            + "</ul>"
                        //        + "</div>"
                        //    ;
                        //}
                    }
                }
            ],
        
        destroy: true,
    });
}
 
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
    GetPayDemages($('#guestid').val(), fromdate, today);

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
                    if (row.paid == 0) {
                        return "<button OnClick='EditProp(" + data + ");' class='btn btn-warning btn-xs' style='margin-top:0px'><span class='glyphicon glyphicon-edit'></span> Edit</button>"
                         + "<button OnClick='DeletePrp(" + data + "," + row.propertyname + ");' class='btn btn-danger btn-xs' style='margin-top:0px;margin-left:5px'><span class='glyphicon glyphicon-trash'></span> Delete</button>"
                    ;
                    } else {
                        return "<span class='text-primary'><span class='glyphicon glyphicon-ok'></span></span>";
                    }
                    
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
    $.ajax({
        url: "/api/checkin_v/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            //alert(result.name);
            var checkindate = moment(result.checkindate).format('DD-MMM-YYYY');
            var startdate = moment(result.checkindate).format('DD-MMM-YYYY');
            var enddate = moment(result.checkindate).format('DD-MMM-YYYY');
            $('#roomid').val(result.roomid);
            $('#checkindate').val(checkindate);
            $('#startdate').val(startdate);
            $('#enddate').val(enddate);

            $('#guestid').val(result.guestid);
            $('#name').val(result.name);
            $('#namekh').val(result.namekh);
            $('#man').val(result.man);
            $('#women').val(result.women);
            $('#child').val(result.child);
            $('#wrecord').val(result.wstartrecode);
            $('#precord').val(result.estartrecord);
            $('#prepaid').val(result.prepaid);
            $('#payforroom').val(result.payforroom);
        },
        error: function (errormessage) {
            toastr.error("No Record Select!", "Service Response");
        }
    });
}

function SaveCheckIn() {  
    var data = {
        id: $('#checkinid').val(),
        checkindate: $('#checkindate').val(),
        roomid: $('#roomid').val(),
        guestid: $('#guestid').val(),
        child: $('#child').val(),
        man: $('#man').val(),
        women: $('#women').val(),
        payforroom: $('#payforroom').val(),
        startdate: $('#startdate').val(),
        enddate: $('#enddate').val(),
        active: false,      
    };
    $.ajax({
        url: "/api/checkins/" + data.id,
        data: JSON.stringify(data),
        type: "PUT",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            //UpdateWater();
            //UpdatePower();
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