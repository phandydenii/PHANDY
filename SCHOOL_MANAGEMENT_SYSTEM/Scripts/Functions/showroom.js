$(document).ready(function () {

    $(document).ajaxStart(function () {
        $('#loadingGif').addClass('show');
    }).ajaxStop(function () {
        $('#loadingGif').removeClass('show');
    });

    $('#showroomModel').on('show.bs.modal', function () {

        GetShowroom();
        DisableControl();

        document.getElementById('btnShowroom').innerText = "Add New";

    })
})

var tableShowroom = [];
toastr.optionsOverride = 'positionclass = "toast-bottom-right"';
toastr.options.positionClass = 'toast-bottom-right';

function GetShowroom() {
    tableShowroom = $('#showroomTable').DataTable({
        ajax: {
            url: "/api/Showroom",
            dataSrc: ""
        },
        columns: [
            //{
            //    data:"id"
            //},
            {
                data: "name"
            },
            {
                data: "address"
            },
            {
                data: "owner"
            },
            {
                data: "phone"
            },
            {
                data: "id",
                render: function (data) {
                    return "<button onclick='showroomEdit(" + data + ")' class='btn btn-warning btn-xs' style='margin-right:5px;'>Edit</button>" + "<button onclick='showroomDelete(" + data + ")' class='btn btn-danger btn-xs'>Delete</button>";
                }
            }
        ],
        destroy: true,
        "order": [0, "desc"],
        "info": false
    });
}

function ShowroomAction() {
    var action = '';
    action = document.getElementById('btnShowroom').innerText;
    if (action == "Add New") {
        EnableControl();
        ClearData();
        document.getElementById('btnShowroom').innerText = "Save";
        $('#name').focus();

    } else if (action == "Save") {
        if ($('#name').val().trim() == "") {
            $('#name').css('border-color', 'red');
            $('#name').focus();
            toastr.info('Please enter Showroom Name.', "Server Response")
        } else {
            $('#name').css('border-color', '#cccccc');

            var dataSave = {
                name: $('#name').val(),
                address: $('#address').val(),
                owner: $('#owner').val(),
                phone: $('#phonenumber').val(),
            };

            $.ajax({
                url: "/api/Showroom",
                data: JSON.stringify(dataSave),
                type: "POST",
                contentType: "application/json;charset=utf-8",
                datatype: "json",
                success: function (result) {
                    toastr.success("Showroom has been created successfully.", "Server Response");
                    tableShowroom.ajax.reload();
                    document.getElementById('btnShowroom').innerText = "Add New";
                    $('#name').val('');
                    $('#address').val(''),
                    $('#owner').val(''),
                    $('#phone').val(''),
                    document.getElementById('name').disabled = true;
                },
                error: function (errormessage) {
                    toastr.error("This Department is already exists in Database", "Service Response");
                }
            })
        }
    } else if (action == "Update") {
        if ($('#name').val().trim() == "") {
            $('#name').css('border-color', 'red');
            $('#name').focus();
            toastr.info('Please enter Showroom Name.', "Server Response")
        } else {
            $('#name').css('border-color', '#cccccc');

            var data = {
                id: $('#id').val(),
                name: $('#name').val(),
                address: $('#address').val(),
                owner: $('#owner').val(),
                phone: $('#phonenumber').val(),
            };

            console.log(data);

            $.ajax({
                url: "/api/Showroom/" + data.id,
                data: JSON.stringify(data),
                type: "PUT",
                contentType: "application/json;charset=utf-8",
                datatype: "json",
                success: function (result) {
                    toastr.success("Showroom has been update successfully.", "Server Response");
                    tableShowroom.ajax.reload();
                    document.getElementById('btnShowroom').innerText = "Add New";
                    $('#name').val('');
                    document.getElementById('name').disabled = true;
                },
                error: function (errormessage) {
                    toastr.error("This Showroom is already exists in Database", "Service Response");
                }
            })
        }
    }

}

function showroomEdit(id) {
    $.ajax({
        url: "/api/Showroom/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            $('#id').val(result.id);
            $('#name').val(result.name);
            $('#address').val(result.address);
            $('#phonenumber').val(result.phone);
            $('#owner').val(result.owner);


            document.getElementById('btnShowroom').innerText = "Update";
            document.getElementById('name').disabled = false;
            document.getElementById('address').disabled = false;
            document.getElementById('phonenumber').disabled = false;
            document.getElementById('owner').disabled = false;
            $('#name').focus();
        },
        error: function (errormessage) {
            toastr.error("This Showroom is already exists in Database", "Service Response");
        }
    });

}

function showroomDelete(id) {
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
                    url: "/api/Showroom/" + id,
                    type: "DELETE",
                    contentType: "application/json;charset=utf-8",
                    datatype: "json",
                    success: function (result) {
                        tableShowroom.ajax.reload();
                        toastr.success("Showroom has been Deleted successfully!", "Service Response");
                        DisableControl();
                        document.getElementById('btnShowroom').innerText = "Add New";
                    },
                    error: function (errormessage) {
                        toastr.error("This Showroom cannot delete it already use in Database", "Service Response");
                    }
                });
            }
        }
    });
}

function EnableControl() {
    document.getElementById('name').disabled = false;
    document.getElementById('address').disabled = false;
    document.getElementById('owner').disabled = false;
    document.getElementById('phonenumber').disabled = false;
}

function DisableControl() {
    document.getElementById('name').disabled = true;
    document.getElementById('address').disabled = true;
    document.getElementById('owner').disabled = true;
    document.getElementById('phonenumber').disabled = true;
}

function ClearData() {
    $('#name').val('');
    $('#address').val('');
    $('#phonenumber').val('');
    $('#owner').val('');
}