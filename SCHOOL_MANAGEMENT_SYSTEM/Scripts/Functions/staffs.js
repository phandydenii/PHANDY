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
                data: "dob",
                render: function (data) {
                    return moment(new Date(data)).format('DD-MMM-YYYY');
                }
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
                data: "position.positionnamekh"
            },
            {
                data: "id",
                render: function (data) {
                    return "<button onclick='EditStaff(" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px;'>Edit</button>"
                         + "<button onclick='DeleteStaff(" + data + ")' class='btn btn-danger btn-xs'>Delete</button>";
                }
            }
        ],
        destroy: true,
        "order": [0, "desc"],
        "info": false
    });
}

function CloseModalStaff() {
    window.location.reload(true);
}


function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#photo').attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}

function SaveStaffAction() {
    var action = document.getElementById('btnSaveStaff').innerText;
    if (action == "Save") {
        var data = new FormData();
        data.append("positionid", $("#position").val());
        data.append("name", $("#staffname").val());
        data.append("namekh", $("#staffnamekh").val());
        data.append("sex", $("#sex").val());
        data.append("phone", $("#phone").val());
        data.append("dob", $("#dob").val());
        data.append("address", $("#address").val());
        data.append("email", $("#email").val());
        data.append("identityno", $("#identityno").val());
        var files = $("#file").get(0).files;
        if (files.length > 0) {
            data.append("photo", files[0]);
        }
        $.ajax({
            type: "POST",
            url: "/api/staffs",
            contentType: false,
            processData: false,
            data: data,
            success: function (result) {
                toastr.success("Insert record successfully.", "Server Response");
                tableStaff.ajax.reload();
                window.location.reload(true);
            },
            error: function (error) {
                console.log(error);
                toastr.error("Record Already Exists!.", "Server Response");
            }
        });
    } else if (action == "Update") {
        var data = new FormData();
        data.append("positionid", $("#position").val());
        data.append("name", $("#staffname").val());
        data.append("namekh", $("#staffnamekh").val());
        data.append("sex", $("#sex").val());
        data.append("phone", $("#phone").val());
        data.append("dob", $("#dob").val());
        data.append("address", $("#address").val());
        data.append("email", $("#email").val());
        data.append("identityno", $("#identityno").val());
        data.append("file_old", $("#file_old").val());
        var files = $("#file").get(0).files;
        if (files.length > 0) {
            data.append("photo", files[0]);
        }
        $.ajax({
            type: "PUT",
            url: "/api/staffs/"+$('#staffid').val(),
            contentType: false,
            processData: false,
            data: data,
            success: function (result) {
                toastr.success("Update record successfully.", "Server Response");
                tableStaff.ajax.reload();
                window.location.reload(true);
            },
            error: function (error) {
                console.log(error);
                toastr.error("Record Already Exists!.", "Server Response");
            }
        });
    }
   
}

function EditStaff(id) {
    $("#StaffModal").modal('show');
    action = document.getElementById('btnSaveStaff').innerText = "Update";
    $.ajax({
        url: "/api/staffs/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {

            $('#staffid').val(result.id);
            $('#staffname').val(result.name);
            $('#staffnamekh').val(result.namekh);
            $('#position').val(result.positionid).change();
            $('#sex').val(result.sex).change();
            $('#phone').val(result.phone);
            var dr = moment(result.dob).format("YYYY-MM-DD");
            $("#dob").val(dr);
            $('#address').val(result.address);
            $('#email').val(result.email);
            $('#identityno').val(result.identityno);
            $('#file_old').val(result.photo);
            if (result.photo == "") {
                $('#photo').attr('src', '../Images/no_image.png');
            } else {
                $('#photo').attr('src', '../Images/' + result.photo);
            }

        },
        error: function (errormessage) {
            toastr.error("No Record Select!", "Service Response");
        }
    });
}


function DeleteStaff(id) {
    bootbox.confirm({
        title: "",
        message: "<h3>Are you sure want to delete record "+id+" ?</h3>",
        buttons: {
            confirm: {
                label: 'Yes',
                className: 'btn-success btn-sm'
            },
            cancel: {
                label: 'No',
                className: 'btn-danger btn-sm'
            }
        },
        callback: function (result) {
            if (result) {
                $.ajax({
                    url: "/api/staffs/" + id,
                    type: "DELETE",
                    contentType: "application/json;charset=utf-8",
                    datatype: "json",
                    success: function (result) {
                        toastr.success("Record delete successfully!", "Service Response");
                        window.location.reload(true);
                    },
                    error: function (errormessage) {
                        toastr.error("Record delte faild!", "Service Response");
                    }
                });
            }
        }
    });
}