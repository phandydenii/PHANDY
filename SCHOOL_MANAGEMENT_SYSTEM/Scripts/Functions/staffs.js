$(document).ready(function () {

    GetStaff();
})

var tableStaff = [];
function GetStaff() {
    tableStaff = $('#tableStaff').DataTable({
        ajax: {
            url: "/api/staffs",
            dataSrc: ""
        },
        columns: [
            {
                data: "id"
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
                data: "phone"
            },
            {
                data: "dob"
            },
            {
                data: "address"
            },
            {
                data: "email"
            },
            {
                data: "identityno"
            },
            
            {
                data: "id",
                render: function (data) {
                    return "<button onclick='PositionEdit(" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px;'>Edit</button>"
                         + "<button onclick='PositionDelete(" + data + ")' class='btn btn-danger btn-xs'>Delete</button>";
                }
            }
        ],
        destroy: true,
        "order": [0, "desc"],
        "info": false
    });
}