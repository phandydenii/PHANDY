
$(document).ready(function () {
    $('#BuildingModal').on('show.bs.modal', function () {
        $(document).ajaxStart(function () {
            $('#loadingGif').addClass('show');
        }).ajaxStop(function () {
            $('#loadingGif').removeClass('show');
        });
        GetBuilding();
    });
});

var tableBuilding = [];
function GetBuilding() {
    tableFloor = $('#tableBuilding').dataTable({
        ajax: {
            url: "/api/buildings",
            dataSrc: ""
        },
        columns:
            [
                {
                    data: "id"
                },
                {
                    data: "buildingname"
                },
                {
                    data: "buildingnamekh"
                },
                {
                    data: "id",
                    render: function (data) {
                        return "<button OnClick='OnEditBuilding (" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-edit'></span> Edit</button>" +
                               "<button OnClick='OnDeleteBuilding (" + data + ")' class='btn btn-danger btn-xs'><span class='glyphicon glyphicon-trash'></span> Delete</button>";
                    }
                }
            ],
        destroy: true,
        "order": [[0, "desc"]],
        "info": false

    });
}