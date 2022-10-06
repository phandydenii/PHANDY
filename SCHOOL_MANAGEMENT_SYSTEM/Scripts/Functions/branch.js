$(document).ready(function () {

    $(document).ajaxStart(function(){
        $('#loadingGif').addClass('show');
    }).ajaxStop(function(){
        $('#loadingGif').removeClass('show');
    });

    $('#branchModel').on('show.bs.modal', function () {
        
        GetBranch();
    })
})

var tableBranch = [];
toastr.optionsOverride = 'positionclass = "toast-bottom-right"';
toastr.options.positionClass = 'toast-bottom-right';

function GetBranch() {
    tableBranch = $('#branchTable').DataTable({
        ajax: {
            url: "/api/Branchs",
            dataSrc: ""
        },
        columns: [
            //{
            //    data:"id"
            //},
            {
                data:"name"
            },
            {
                data: "id",
                render: function (data) {
                    return "<button onclick='branchEdit(" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px;'>Edit</button>" + "<button onclick='branchDelete(" + data + ")' class='btn btn-danger btn-xs'>Delete</button>";
                }
            }
        ],
        destroy: true,
        "order": [0, "desc"],
        "info":false
    });
}

function BranchAction() {
    var action = '';
    action = document.getElementById('btnBonus').innerText;
    if (action == "Add New")
    {
        document.getElementById('btnBonus').innerText = "Save";
        document.getElementById('branchName').disabled = false;
        $('#branchName').focus();

    } else if (action == "Save") {
        if ($('#branchName').val().trim() == "") {
            $('#branchName').css('border-color', 'red');
            $('#branchName').focus();
            toastr.info('Please enter Branch Name.', "Server Response")
        } else {
            $('#branchName').css('border-color', '#cccccc');

            var dataSave = {
                Name: $('#branchName').val()
            };

            $.ajax({
                url: "/api/Branchs",
                data: JSON.stringify(dataSave),
                type: "POST",
                contentType: "application/json;charset=utf-8",
                datatype: "json",
                success: function (result) {
                    toastr.success("Branch has been created successfully.", "Server Response");
                    tableBranch.ajax.reload();
                    document.getElementById('btnBonus').innerText = "Add New";
                    $('#branchName').val('');
                    document.getElementById('branchName').disabled = true;
                },
                error: function (errormessage) {
                    toastr.error("This Branch is already exists in Database", "Service Response");
                }
            })
        }
    }else if(action=="Update"){
        if ($('#branchName').val().trim() == "") {
            $('#branchName').css('border-color', 'red');
            $('#branchName').focus();
            toastr.info('Please enter Branch Name.', "Server Response")
        } else {
            $('#branchName').css('border-color', '#cccccc');

            var data = {
                Id: $('#branchId').val(),
                Name: $('#branchName').val()
            };

            //console.log(data);

            $.ajax({
                url: "/api/Branchs/" + data.Id,
                data: JSON.stringify(data),
                type: "PUT",
                contentType: "application/json;charset=utf-8",
                datatype: "json",
                success: function (result) {
                    toastr.success("Branch has been update successfully.", "Server Response");
                    tableBranch.ajax.reload();
                    document.getElementById('btnBonus').innerText = "Add New";
                    $('#branchName').val('');
                    document.getElementById('branchName').disabled = true;
                },
                error: function (errormessage) {
                    toastr.error("This Branch is already exists in Database", "Service Response");
                }
            })
        }
    }

}

function branchEdit(id) {
    $.ajax({
        url: "/api/Branchs/"+id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            $('#branchId').val(result.id);
            $('#branchName').val(result.name);
            document.getElementById('btnBonus').innerText = "Update";
            document.getElementById('branchName').disabled = false;
            $('#branchName').focus();
        },
        error: function (errormessage) {
            toastr.error("This Branch is already exists in Database", "Service Response");
        }
    });

}

function branchDelete(id) {
    bootbox.confirm({
        title: "",
        message: "Are you sure want to delete this?",
        button:{
            cancel: {
                label: "Cancel",
                ClassName:"btn-default",
            },
            confirm: {
                label: "Delete",
                ClassName:"btn-danger"
            }
        },
        callback: function (result) {
            if (result) {
                $.ajax({
                    url: "/api/Branchs/" + id,
                    type: "DELETE",
                    contentType: "application/json;charset=utf-8",
                    datatype: "json",
                    success: function (result) {
                        tableBranch.ajax.reload();
                        toastr.success("Branch has been Deleted successfully!", "Service Response");
                    },
                    error: function (errormessage) {
                        toastr.error("This Branch is already use in Database", "Service Response");
                    }
                });
            }
        }
    });
}