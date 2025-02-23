﻿
$(document).ready(function () {
    $('#BuildingModal').on('show.bs.modal', function () {
        $(document).ajaxStart(function () {
            $('#loadingGif').addClass('show');
        }).ajaxStop(function () {
            $('#loadingGif').removeClass('show');
        });
        GetBuilding();
        document.getElementById('buildingname').readOnly = true;
        document.getElementById('buildingnamekh').readOnly = true;
    });
});

var tableBuilding = [];
function GetBuilding() {
    tableBuilding = $('#tableBuilding').dataTable({
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
                        return "<button OnClick='OnEditBuilding (" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px'><span class='glyphicon glyphicon-edit'></span></button>" +
                               "<button OnClick='OnDeleteBuilding (" + data + ")' class='btn btn-danger btn-xs'><span class='glyphicon glyphicon-trash'></span></button>";
                    }
                }
            ],
        destroy: true,
        "order": [[0, "desc"]],
        "info": false

    });
}

function OnEditBuilding(id) {
    document.getElementById('btnSaveBuilding').innerText = "Update";
    document.getElementById('buildingname').readOnly = false;
    document.getElementById('buildingnamekh').readOnly = false;
    $.ajax({
        url: "/api/buildings/"+id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $('#buildingid').val(result.id);
            $('#buildingname').val(result.buildingname);
            $('#buildingnamekh').val(result.buildingnamekh);
        },
        error: function (errormesage) {
            toastr.error("Insert record faild...!" + errormesage, "Server Respond");
        }
    });
}

function OnDeleteBuilding(id) {
    bootbox.confirm({
        title: "",
        message: "<h4>Do you want to delete record '"+id+"' ?</h4>",
        button: {
            cancel: {
                label: "No",
                ClassName: "btn btn-default btn-xs",
            },
            confirm: {
                label: "Yes",
                ClassName: "btn btn-danger btn-xs"
            }
        },
        callback: function (result) {
            //alert(id);
            if (result) {
                $.ajax({
                    url: "/api/buildings/" + id,
                    type: "DELETE",
                    contentType: "application/json;charset=utf-8",
                    datatype: "json",
                    success: function (result) {
                        tableBuilding.DataTable().ajax.reload();
                        toastr.success("Delete record successfully!", "Service Response");
                    },
                    error: function (errormessage) {
                        toastr.error("Delete record faild!", "Service Response");
                    }
                });
            }
        }
    });

}

function validateForm() {
    var isValid = true;
    if ($('#buildingname').val().trim() == "") {
        $('#buildingname').css('border-color', 'red');
        $('#buildingname').focus();
        isValid = false;
    } else {
        $('#buildingname').css('border-color', '#cccccc');
        if ($('#buildingnamekh').val().trim() == "") {
            $('#buildingnamekh').css('border-color', 'red');
            $('#buildingnamekh').focus();
            isValid = false;
        }else{
            $('#buildingnamekh').css('border-color', '#cccccc');
        }
    }
    return isValid;
}