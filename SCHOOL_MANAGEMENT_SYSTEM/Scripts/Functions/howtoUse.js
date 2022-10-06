
$(document).ready(function () {

    
    $(document).ajaxStart(function () {
        $('#loadingGif').addClass('show');
    }).ajaxStop(function () {
        $('#loadingGif').removeClass('show');
    });
    
    GetHowtoUse();
    $('#HowtoUseModel').on('show.bs.modal', function () {
    }); 
})


var tableHowtoUse = [];
function GetHowtoUse() {
    //alert(departmentId);
    tableHowtoUse = $('#tableHowtoUse').DataTable({
        ajax: {
            url: "/api/HowtoUses",
            dataSrc: ""
        },
        columns: [
            {
                data: "id"
            },
            {
                data: "note",
            },
            {
                data: "employee.name",
            },
            {
                data: "attachfile",
            },
            {
                data: "id",
                render: function (data) {
                    return "<button onclick='HowtoUseEdit(" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px;'​>Edit</button>" + "<button onclick='HowtoUseDelete(" + data + ")' class='btn btn-danger btn-xs' >Delete</button>";
                }
            },
        ],
        destroy: true,
        "order": [0, "desc"],
        "info": false
    });
}


//
function Upload() {
   
}
 



function HowtoUseAction() {
    $('#Upload').click(function () {
        alert('hi');
        
    });
    var action = '';
    action = document.getElementById('btnSaveHowtoUse').innerText;
    //alert(action);

    if (action == "Add New") {
        document.getElementById('btnSaveHowtoUse').innerText = 'Save';
        $('#note').val('');
        $('#myfile').val('');
        document.getElementById('note').disabled = false;
        document.getElementById('employeeid').disabled = false;

    }
    else if (action == "Save") {

        //var res = Validateemployee();
        //if (res == false) {
        //    return false;
        //}
        var data = new FormData();

        var files = $('#files').get(0).files;

        if (files.length > 0) {
            data.append("attachfile", files[0]);
        }
        
        data.append("employeeid", $('#employeeid').val());
        data.append("note", $('#note').val());

        $.ajax({
            url: "/api/HowtoUses",
            data: data,
            type: "POST",
            contentType: false,
            processData: false,
            dataType: "json",
            success: function (result) {
                toastr.success("New Use has been Created", "Server Respond");
                $('#tableHowtoUse').DataTable().ajax.reload();
                document.getElementById('btnSaveHowtoUse').innerText = 'Add New';
                document.getElementById('employeeid').disabled = true;
                document.getElementById('note').disabled = true;
                // $('#employeeName').val('');
                $("#HowtoUseModal").modal('hide');
                $('#note').val('');

            },
            error: function (errormesage) {
                $('#date').focus();
                toastr.error("This Name is exist in Database", "Server Respond")
            }

        });

    } else if (action == "Update") {

        var data = new FormData();

        var files = $('#files').get(0).files;

        if (files.length > 0) {
            data.append("attachfile", files[0]);
        }

        data.append("id", $('#id').val());
        data.append("employeeid", $('#employeeid').val());
        data.append("note", $('#note').val());

        $.ajax({
            url: "/api/HowtoUses/" + $("#id").val(),
            data: data,
            type: "PUT",
            contentType: false,
            processData: false,
            dataType: "json",
            success: function (result) {
                toastr.success("Howtouse has been Updated", "Server Respond");
                $('#tableHowtoUse').DataTable().ajax.reload();
                document.getElementById('btnSaveHowtoUse').innerText = 'Add New';
                document.getElementById('employeeid').disabled = true;
                document.getElementById('note').disabled = true;
                // $('#employeeName').val('');
                $("#HowtoUseModel").modal('hide');
                $('#note').val('');
            },
            error: function (errormesage) {
                toastr.error("employee hasn't Updated in Database", "Server Respond")
            }
        });
    }
}

function HowtoUseEdit(id) {
    document.getElementById('employeeid').disabled = false;
    document.getElementById('note').disabled = false;
    document.getElementById('btnSaveHowtoUse').innerText = "Update";

    $.ajax({
        url: "/api/HowtoUses/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            $('#id').val(result.id);
            $('#employeeid').val(result.employeeid);
            $('#note').val(result.note);
            //$('#file_old').val(result.image);

            if (result.attachfile == "") {
                $("#files").attr('src', '../UploadFile');
            } else {
                $("#files").attr('src', '../UploadFile/' + result.attachfile);
            }
            $('#HowtoUseModel').modal('show');
        },
        error: function (errormessage) {
            toastr.error("No Record Select!", "Service Response");
        }
    });

}

function HowtoUseDelete(id) {
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
            if (result) {
                $.ajax({
                    url: "/api/HowtoUses/" + id,
                    type: "DELETE",
                    contentType: "application/json;charset=utf-8",
                    datatype: "json",
                    success: function (result) {
                        tableHowtoUse.ajax.reload();
                        toastr.success("HowtoUse has been Deleted successfully!", "Service Response");
                    },
                    error: function (errormessage) {
                        toastr.error("This HowtoUse is already exists in Database", "Service Response");
                    }
                });
            }
        }
    });
}



function ClickAddnewHowtoUse() {
    document.getElementById('note').disabled = true;
    document.getElementById('employeeid').disabled = true;
    $('#employeeid').val('');
    $('#myfile').attr('src', '../Images/no_image.png');
    document.getElementById('btnSaveHowtoUse').innerText = "Add New";
}