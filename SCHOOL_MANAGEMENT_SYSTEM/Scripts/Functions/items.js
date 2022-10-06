$(document).ready(function () {

    GetItem();
    $('#ItemModal').on('show.bs.modal', function () {
        $('#itemname').focus();
        ClearControlItem();
        DisableControlItem();
    });
});
var tableItem = [];
function GetItem() {
    tableItem = $('#tableItem').dataTable({
        ajax: {
            url: "/api/Items",
            dataSrc: ""
        },
        columns:
            [
                {
                    data: "id"
                },
                {
                    data: "itemname"
                },
                {
                    data: "itemnamekh"
                },
                {
                    data: "price"
                },
                {
                    data: "remark"
                },
                {
                    data: "status"
                },
                {
                    data: "id",
                    render: function (data) {
                        return "<button OnClick='OnEditItem (" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-edit'></span> Edit</button>" +
                            "<button OnClick='ItemDelete (" + data + ")' class='btn btn-danger btn-xs'><span class='glyphicon glyphicon-trash'></span> Delete</button>";
                    }
                }
            ],
        destroy: true,
        "order": [[0, "desc"]],
        "info": false

    });
}


function OnEditItem(id) {
    alert('hi'+id);
    //document.getElementById('itemname').disabled = false;
    $("#ItemModal").modal('show');
    //action = document.getElementById('btnSaveItem').innerText = "Update";
    //$.ajax({
    //    url: "/api/Items/" + id,
    //    type: "GET",
    //    contentType: "application/json;charset=utf-8",
    //    datatype: "json",
    //    success: function (result) {
    //        $('#Itemid').val(result.id);
    //        $("#itemname").val(result.itemname);
    //        $("#itemnamekh").val(result.itemnamekh);
    //        $("#prices").val(result.price);
    //        $("#remark").val(result.remark);
    //        $("#statuss").val(result.status);

    //        $("#ItemModal").modal('show');
    //        alert(result.id);

    //    },
    //    error: function (errormessage) {
    //        toastr.error("No Record Select!", "Service Response");
    //    }
    //});

}