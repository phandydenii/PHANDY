
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
            url: "/api/checkin_v",
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
                    data: "floorno"
                },
                {
                    data: "building",
                   
                },
                {
                    data: "pay",

                },
                {
                    data: "id",
                    render: function (data) {
                        //return "<button OnClick='CheckInEdit (" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-edit'></span> Edit</button>"
                        //     + "<button OnClick='CheckOut (" + data + ")' class='btn btn-primary btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-log-out'></span> Check Out</button>"
                        //     + "<button OnClick='PayDamages (" + data + ")' class='btn btn-primary btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-log-out'></span> Pay Damages</button>"
                        //;

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
$('#fromdate').on('change', function () {
    var fromdate = this.value;
    var today = $('#today').val();
    GetPayDemages($('#checkinid').val(), fromdate, today);

});

function PayDamages(id) {

    $("#PayDamagesModal").modal("show");
    $('#checkinid').val(id);
    var fromdate = $('#fromdate').val();
    var today = $('#today').val();
    GetPayDemages(id, fromdate ,today);
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
                data: "item.price",
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
            $('#propertyname').val(result.propertyname);
            $('#price').val(result.price);
            $('#note').val(result.note);
        },
        error: function (errormessage) {
            toastr.error("No Record Select!", "Service Response");
        }
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
            $('#guestname').val(result.name);
            $('#guestnamekh').val(result.namekh);
            var checkindate = moment(result.checkindate).format("YYYY-MM-DD");
            $('#checkindate').val(checkindate);
            $('#roomno').val(result.room_no);
            $('#servicecharge').val(result.servicecharge);
            $('#roomprice').val(result.price);
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