
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

//Save  
function OnItemAction() {
    var action = '';
    action = document.getElementById('btnSaveItem').innerText;
    if (action == "Add New") {
        document.getElementById('btnSaveItem').innerText = 'Save';
        EnableControlItem();
        $('#itemname').focus();
    }
    else if (action == "Save") {
        
        var res = ValidationFormItem();
        if (res == false) {
            return false;
        }
        

        var data = {
            itemname: $('#itemname').val(),
            itemname: $('#itemnamekh').val(),
            price: $('#prices').val(),
            remark: $('#remark').val(),
            status: $('#statuss').val(),

        };

        $.ajax({
            url: "/api/Items",
            data: JSON.stringify(data),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                toastr.success("New Item has been Created", "Server Respond");
                $('#tableItem').DataTable().ajax.reload();
                document.getElementById('btnSaveItem').innerText = "Add New";
                DisableControlItem();
                ClearControlItem();
            },
            error: function (errormesage) {
                $('#ItemName').focus();
                toastr.error("This Name is exist in Database", "Server Respond")
            }

        });

    } else if (action == "Update") {
        var res = ValidationFormItem();
        if (res == false) {
            return false;
        }

        var data = {
            id: $('#Itemid').val(),
            itemname: $('#itemname').val(),
            itemnamekh: $('#itemnamekh').val(),
            price: $('#prices').val(),
            remark: $('#remark').val(),
            status: $('#statuss').val(),
        };


        $.ajax({
            url: "/api/Items/" + data.id,
            data: JSON.stringify(data),
            type: "PUT",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                //tableItem.DataTable().ajax.reload();
                DisableControlItem();
                ClearControlItem();
                toastr.success("Item has been Updated", "Server Respond");
                //$("#ItemModal").modal('hide');
                window.location.reload(true);
            },
            error: function (errormesage) {
                toastr.error("Item hasn't Updated in Database", "Server Respond")
            }
        });
    }
}

function OnEditItem(id) {
    $("#ItemModal").modal('show');
    EnableControlItem();
    action = document.getElementById('btnSaveItem').innerText = "Update";
    $.ajax({
        url: "/api/Items/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            $('#Itemid').val(result.id);
            $("#itemname").val(result.itemname);
            $("#itemnamekh").val(result.itemnamekh);
            $("#prices").val(result.price);
            $("#remark").val(result.remark);
            $("#statuss").val(result.status);

           
        },
        error: function (errormessage) {
            toastr.error("No Record Select!", "Service Response");
        }
    });

}

function ItemDelete(id) {
    // alert('hi');
    bootbox.confirm({
        title: "",
        message: "Are you sure want to delete this?",
        button: {
            cancel: {
                label: "Cancel",
                ClassName: "btn-default",
            },
            confirm: {
                label: "Delete",
                ClassName: "btn-danger"
            }
        },
        callback: function (result) {
            //alert(id);
            if (result) {
                $.ajax({
                    url: "/api/Items/" + id,
                    type: "DELETE",
                    contentType: "application/json;charset=utf-8",
                    datatype: "json",
                    success: function (result) {
                        $('#tableItem').DataTable().ajax.reload();
                        toastr.success("Item Deleted successfully!", "Service Response");
                    },
                    error: function (errormessage) {
                        toastr.error("Item Can't be Deleted", "Service Response");
                    }
                });
            }
        }
    });
}


function DisableControlItem() {
    document.getElementById('itemname').disabled = true;
    document.getElementById('itemnamekh').disabled = true;
    document.getElementById('prices').disabled = true;
    document.getElementById('remark').disabled = true;
    document.getElementById('statuss').disabled = true;

}

function EnableControlItem() {
    document.getElementById('itemname').disabled = false;
    document.getElementById('itemnamekh').disabled = false;
    document.getElementById('prices').disabled = false;
    document.getElementById('remark').disabled = false;
    document.getElementById('statuss').disabled = false;
}

function ClearControlItem() {
    $('#itemname').val('');
    $('#itemnamekh').val('');
    $('#prices').val('');
    $('#remark').val('');
    $('#statuss').val('');
}

function AddnewItemAction() {
    document.getElementById('btnSaveItem').innerText = "Add New";
    DisableControlItem();
    ClearControlItem();
}

function ValidationFormItem() {
    var isValid = true;
    if ($('#itemname').val().trim() === "") {
        $('#itemname').css('border-color', 'red');
        $('#itemname').focus();
        isValid = false;
    }
    else {
        $('#itemname').css('border-color', '#cccccc');
        if ($('#itemnamekh').val().trim() === "") {
            $('#itemnamekh').css('border-color', 'red');
            $('#itemnamekh').focus();
            isValid = false;
        }
        else {
            $('#itemnamekh').css('border-color', '#cccccc');
            if ($('#prices').val().trim() === "") {
                $('#prices').css('border-color', 'red');
                $('#prices').focus();
                isValid = false;
            }
            else {
                $('#prices').css('border-color', '#cccccc');
                if ($('#statuss').val().trim() === "") {
                    $('#statuss').css('border-color', 'red');
                    $('#statuss').focus();
                    isValid = false;

                }
                else {
                    $('#Status').css('border-color', '#cccccc');

                }
            }
        }
    }
    return isValid;
}

