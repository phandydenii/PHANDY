$(document).ready(function () {
    $('#GuestModal').on('show.bs.modal', function () {

    });
});

$('#HistoryModal').modal('show');
tableRoom = $('#tableHistory').dataTable({
    ajax: {
        url: "/api/invoice_v_by_guestid/" + id,
        dataSrc: ""
    },
    columns:
        [
            {
                data: "startdate"
            },
            {
                data: "enddate"
            },
            {
                data: "wtotal"
            },
            {
                data: "etotal"
            },
            {
                data: "roomprice"
            },
            {
                data: "servicecharge"
            },
            {
                data: "grandtotal"
            },
            {
                data: "id",
                render: function (data) {
                    return "<button OnClick='OnEditRoom (" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-edit'></span> Edit</button>"
                        + "<button OnClick='OnDeleteRoom (" + data + ")' class='btn btn-danger btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-trash'></span> Delete</button>"
                    ;
                }
            }
        ],
    destroy: true,
    "info": false

});