
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
                        return "<button OnClick='OnEditItem (" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-edit'></span></button>" +
                            "<button OnClick='ItemDelete (" + data + ")' class='btn btn-danger btn-xs'><span class='glyphicon glyphicon-trash'></span></button>";
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
       
    }
    else if (action == "Save") {
        
        

    } else if (action == "Update") {
        
    }
}

function OnEditItem(id) {
    $("#ItemModal").modal('show');
    EnableControlItem();
    document.getElementById('btnAddItem').style.display = "none";
    document.getElementById('btnSaveItem').style.display = "none";
    document.getElementById('btnUpdateItem').style.display = "block";
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
}

function EnableControlItem() {
    document.getElementById('itemname').disabled = false;
    document.getElementById('itemnamekh').disabled = false;
    document.getElementById('prices').disabled = false;
    document.getElementById('remark').disabled = false;
}

function ClearControlItem() {
    $('#itemname').val('');
    $('#itemnamekh').val('');
    $('#prices').val('');
    $('#remark').val('');
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
            }
        }
    }
    return isValid;
}

