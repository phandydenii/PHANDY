$(document).ready(function () {
    $('#HistoryModal').on('show.bs.modal', function () {
        $('#rowhide').hide();
    });
    GetGuest();

});

var tableGuest = [];
function GetGuest() {
    tableGuest = $('#tableGuest').DataTable({
        ajax: {
            url: "/api/Guests",
            dataSrc: ""
        },
        columns: [
            {
                data: "id",
            },
            {
                data: "name"
            },
            {
                data: "namekh"
            },
            {
                data: "sex"
            },
            {
                data: "dob",
                render: function (data) {
                    return moment(new Date(data)).format('DD-MMM-YYYY');
                }
            },
            {
                data: "address"
            },
            {
                data: "phone"
            },
            {
                data: "email"
            },
            {
                data: "status",
                render: function (data) {
                    if (data == "Book") {
                        return "<span class='label label-warning'><span class='glyphicon glyphicon-ok'></span> Book</span>";
                    } else if (data == "CheckIn") {
                        return "<span class='label label-danger'><span class='glyphicon glyphicon-log-in'></span> Check In</span>";
                    } else if (data == "Expire") {
                        return "<span class='label label-default'><span class='glyphicon glyphicon-ban-circle'></span> Expire</span>";
                    } else if (data == "Cancel") {
                        return "<span class='label label-default'><span class='glyphicon glyphicon-minus-sign'></span> Cancel</span>";
                    } else {
                        return "<span class='label label-success'><span class='glyphicon glyphicon-log-out'></span> Check Out</span>";
                    }
                    
                },
            },
            {
                data: "id",
                render: function (data, type, row) {
                    if (row.status == "CheckIn" || row.status == "Book") {
                        return "<button onclick='GuestEdit(" + data + ")' class='btn btn-warning btn-xs' style='border-width: 0px; width: 65px; margin-right: 5px;margin-top:5px'><span class='glyphicon glyphicon-edit'></span> Edit</button>"
                            + "<button onclick='AddRelative(" + data + ")' class='btn btn-primary btn-xs' style='border-width: 0px; width: 65px; margin-right: 5px;margin-top:5px'><span class='glyphicon glyphicon-option-vertical'></span> More</button>"
                            ;
                    } else {
                        return "";
                    }
                },
            }
        ],
        destroy: true,
        "info": false
    });
}


var tableHistory = [];
function GuestHistory(id) {
    $('#HistoryModal').modal('show');
    $('#guestid').val(id);
    tableHistory = $('#tableHistory').dataTable({
        ajax: {
            url: "/api/invoice_v_by_guestid/"+id,
            dataSrc: ""
        },
        columns:
            [
                {
                    data: "startdate",
                    render: function (data) {
                    return moment(new Date(data)).format('DD-MMM-YYYY');
                }
                },
                {
                    data: "enddate",
                    render: function (data) {
                    return moment(new Date(data)).format('DD-MMM-YYYY');
                    }
                },
                {
                    data: "waterusage",
                    render: function (data) {
                        return data.toFixed(2);
                    }
                },
                {
                    data: "electricusage",
                    render: function (data) {
                        return data.toFixed(2);
                    }
                },
                {
                    data: "wtotal",
                    render: function (data) {
                        return data.toFixed(2);
                    }
                },
                {
                    data: "etotal",
                    render: function (data) {
                        return data.toFixed(2);
                    }
                },
                {
                    data: "grandtotal",
                    render: function (data) {
                        return data.toFixed(2) + "<button OnClick='OnEditRoom (" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-edit'></span></button>"
                            + "<button OnClick='OnDeleteRoom (" + data + ")' class='btn btn-danger btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-trash'></span></button>";
                    }
                },
                {
                    data: "id",
                    render: function (data) {
                            return "<button OnClick='OnEditRoom (" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-edit'></span></button>"
                            + "<button OnClick='OnDeleteRoom (" + data + ")' class='btn btn-danger btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-trash'></span></button>"
                        ;
                        
                    }
                }
            ],
        destroy: true,
        "order": [[0, "asc"]],

    });
}

function AddRelative() {
    $("#HistoryModal").modal('show');
}

function GuestEdit(id) {
    $("#GuestModal").modal('show');
    $.ajax({
        url: "/api/Guests/" + id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#guestid').val(result.id);
            $('#name').val(result.name);
            $('#namekh').val(result.namekh);
            $('#sex').val(result.sex);
            var dr = moment(result.dob).format("YYYY-MM-DD");
            $("#dob").val(dr);
            $('#address').val(result.address);
            $('#national').val(result.nationality);
            $('#phone').val(result.phone);
            $('#email').val(result.email);
            $('#ssn').val(result.ssn);
            $('#passport').val(result.passport);
            $('#status').val(result.status);
        },
        error: function (errormessage) {
            toastr.error("Something unexpected happen.", "Server Response");
        }
    });
}

