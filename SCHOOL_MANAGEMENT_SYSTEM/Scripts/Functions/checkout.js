
$(document).ready(function () {
    GetCheckOut();
})

var tbCheckOut = [];
function GetCheckOut() {
    tbCheckOut = $('#tableCheckOut').dataTable({
        ajax: {
            url: "/api/checkouts",
            dataSrc: ""
        },
        columns:
            [
                {
                    data: "id"
                },
                {
                    data: "date",
                    render: function (data) {
                        return moment(new Date(data)).format('DD-MMM-YYYY');
                    }
                },
                {
                    data: "total"
                },
                {
                    data: "paybefor"
                },
                {
                    data: "returnamount",

                },
                {
                    data: "totalpayment",

                },
                {
                    data: "id",
                    render: function (data) {
                        return "<button OnClick='EditCheckOut (" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-edit'></span> Edit</button>"
                             + "<button OnClick='DeleteCheckOut (" + data + ")' class='btn btn-danger btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-trush'></span> Delete</button>"
                        ; 
                    }
                }
            ],
        destroy: true,
    });
}
