$(document).ready(function () {
    $('#HistoryModal').on('show.bs.modal', function () {
        $('#rowhide').hide();
        GetGuestHistory();
    });
    
});

var tableHistory = [];
function GetGuestHistory() {
    tableHistory = $('#tableHistory').dataTable({
        ajax: {
            url: "/api/invoice_v_by_guestid/" + $('#guestid').val(),
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
                        return data.toFixed(2);
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

function OnEditRoom() {
    $('#rowhide').show();
}
function OnDeleteRoom() {
    $('#rowhide').hide();
}